using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using BinkyRailways.Core.Logging;
using BinkyRailways.Core.Model;
using BinkyRailways.Core.Util;
using NLog;

namespace BinkyRailways.Core.State.Impl
{
    /// <summary>
    /// State of a single command station.
    /// </summary>
    public abstract class CommandStationState  : EntityState<ICommandStation>, ICommandStationState
    {
        /// <summary>
        /// Fired when the value of <see cref="Idle"/> has changed.
        /// </summary>
        public event EventHandler IdleChanged;

        protected readonly Logger Log;
        private readonly AsynchronousWorker myWorker;
        private readonly string[] addressSpaces;
        private readonly List<IJunctionState> junctions = new List<IJunctionState>();
        private readonly List<ILocState> locs = new List<ILocState>();
        private readonly List<IInputState> inputs = new List<IInputState>();
        private readonly List<ISignalState> signals = new List<ISignalState>();
        private readonly StateProperty<bool> power;
        private bool initializationNeeded;
        private readonly List<IInitializeAtPowerOn> initializationObjects = new List<IInitializeAtPowerOn>();
        private bool idle;

        /// <summary>
        /// Default ctor
        /// </summary>
        protected CommandStationState(ICommandStation entity, RailwayState railwayState, string[] addressSpaces)
            : base(entity, railwayState)
        {
            Log = LogManager.GetLogger(LogNames.CommandStationPrefix + entity.Description);
            myWorker = new AsynchronousWorker("csworker-" + entity.Description);
            this.addressSpaces = addressSpaces ?? Empty<string>.Array;
            power = new StateProperty<bool>(this, false, null, 
                x => PostWorkerAction(() => OnRequestedPowerChanged(x)), 
                OnActualPowerChanged, false);
            myWorker.Wait += OnWorkerWait;
        }

        /// <summary>
        /// Called when the worker will wait.
        /// </summary>
        protected virtual void OnWorkerWait(object sender, AsynchronousWorker.WaitEventArgs e)
        {
        }

        /// <summary>
        /// Gets the entity
        /// </summary>
        ICommandStation ICommandStationState.Model { get { return Entity; } }

        /// <summary>
        /// Junctions driven by this command station
        /// </summary>
        [DisplayName(@"Junctions")]
        public IEnumerable<IJunctionState> Junctions { get { return junctions; } }

        /// <summary>
        /// Locomotives driven by this command station
        /// </summary>
        [DisplayName(@"Locs")]
        public IEnumerable<ILocState> Locs { get { return locs; } }

        /// <summary>
        /// Input signals driven by this command station (overlaps with <see cref="Sensors"/>).
        /// </summary>
        [DisplayName(@"Inputs")]
        public IEnumerable<IInputState> Inputs { get { return inputs; } }

        /// <summary>
        /// Sensors driven by this command station
        /// </summary>
        [DisplayName(@"Sensors")]
        public IEnumerable<ISensorState> Sensors { get { return inputs.OfType<ISensorState>(); } }

        /// <summary>
        /// Signals driven by this command station
        /// </summary>
        [DisplayName(@"Signals")]
        public IEnumerable<ISignalState> Signals { get { return signals; } }

        /// <summary>
        /// Enable/disable power on the railway
        /// </summary>
        [DisplayName(@"Power")]
        public IStateProperty<bool> Power { get { return power; } }

        /// <summary>
        /// Has the command station not send or received anything for a while.
        /// </summary>
        [DisplayName(@"Idle")]
        public bool Idle
        {
            get { return idle; }
            private set
            {
                if (idle != value)
                {
                    idle = value;

                    // Initialize objects
                    if (value && initializationNeeded && Power.Actual)
                    {
                        // Perform initialization
                        initializationNeeded = false;
                        RailwayState.Dispatcher.PostAction(InitializeObjects);
                    }

                    // Notify others
                    IdleChanged.Fire(this);
                }
            }
        }

        /// <summary>
        /// Request to enable/disable the power on the track.
        /// </summary>
        protected abstract void OnRequestedPowerChanged(bool value);

        /// <summary>
        /// Actual power value on the track has changed.
        /// </summary>
        protected virtual void OnActualPowerChanged(bool value)
        {
            // Make sure that we initialize when idle.
            initializationNeeded = true;
        }

        /// <summary>
        /// Send the speed and direction of the given loc towards the railway.
        /// </summary>
        public void SendLocSpeedAndDirection(ILocState loc)
        {
            PostWorkerAction(() => OnSendLocSpeedAndDirection(loc));
        }

        /// <summary>
        /// Send the direction of the given switch towards the railway.
        /// </summary>
        public void SendSwitchDirection(ISwitchState @switch)
        {
            PostWorkerAction(() => OnSendSwitchDirectionAndStartFeedback(@switch));
        }

        /// <summary>
        /// Send the direction of the given turntable towards the railway.
        /// </summary>
        public void SendTurnTablePosition(ITurnTableState turnTable)
        {
            PostWorkerAction(() => OnSendTurnTablePosition(turnTable));
        }

        /// <summary>
        /// Send the on/off value of a binary output.
        /// </summary>
        public void SendBinaryOutput(IBinaryOutputState output)
        {
            PostWorkerAction(() => OnSendBinaryOutput(output));
        }

        /// <summary>
        /// Send the pattern of a 4-stage clock output.
        /// </summary>
        public void SendClock4StageOutput(IClock4StageOutputState output)
        {
            PostWorkerAction(() => OnSendClock4StageOutput(output));
        }

        /// <summary>
        /// Send the on/off value of a binary output.
        /// </summary>
        public void SendBinaryOutput(Address address, bool value)
        {
            PostWorkerAction(() => OnSendBinaryOutput(address, value));
        }

        /// <summary>
        /// Send the speed and direction of the given loc towards the railway.
        /// This method is called on my worker thread.
        /// </summary>
        protected virtual void OnSendLocSpeedAndDirection(ILocState loc)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Send the direction of the given switch towards the railway.
        /// This method is called on my worker thread.
        /// When the direction is set, arrange the feedback if needed.
        /// </summary>
        private void OnSendSwitchDirectionAndStartFeedback(ISwitchState @switch)
        {
            OnSendSwitchDirection(@switch);
            if (!@switch.HasFeedback || @switch.RailwayState.VirtualMode.Enabled)
            {
                // Wait for feedback
                var direction = @switch.Direction.Requested;
                PostWorkerDelayedAction(TimeSpan.FromMilliseconds(@switch.SwitchDuration),
                    () => OnSwitchFeedbackTimeout(@switch, direction));
            }
        }

        /// <summary>
        /// Send the position of the given turntable towards the railway.
        /// This method is called on my worker thread.
        /// </summary>
        protected virtual void OnSendTurnTablePosition(ITurnTableState turnTable)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Send the on/off value of a binary output.
        /// </summary>
        protected virtual void OnSendBinaryOutput(IBinaryOutputState output)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Send the pattern of a 4-stage clock output.
        /// </summary>
        protected virtual void OnSendClock4StageOutput(IClock4StageOutputState output)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Send the on/off value of a binary output.
        /// </summary>
        protected virtual void OnSendBinaryOutput(Address address, bool value)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The feedback timeout of the given switch it reached.
        /// Set the actual direction.
        /// </summary>
        private void OnSwitchFeedbackTimeout(ISwitchState @switch, SwitchDirection requestedDirection)
        {
            if (@switch.Direction.Requested == requestedDirection)
            {
                @switch.Direction.Actual = requestedDirection;
                Log.Debug("Switch feedback time for {0}", @switch);
            }
        }

        /// <summary>
        /// Send the direction of the given switch towards the railway.
        /// This method is called on my worker thread.
        /// </summary>
        protected virtual void OnSendSwitchDirection(ISwitchState @switch)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Cleanup
        /// </summary>
        public override void Dispose()
        {
            base.Dispose();
            junctions.Clear();
            locs.Clear();
            inputs.Clear();
            signals.Clear();
            power.Dispose();
            myWorker.Dispose();
        }

        /// <summary>
        /// Prepare this state for use in a live railway. 
        /// Make sure all relevant connections to other state objects are resolved.
        /// </summary>
        /// <returns>True if the entity is now ready for use in a live railway, false otherwise.</returns>
        protected override bool TryPrepareForUse(IStateUserInterface ui, IStatePersistence statePersistence)
        {
            return true;
        }

        /// <summary>
        /// Refresh the state of the <see cref="Idle"/> property.
        /// </summary>
        protected void RefreshIdle()
        {
            Idle = IsIdle();
        }

        /// <summary>
        /// Determine if this command station is idle.
        /// </summary>
        protected virtual bool IsIdle()
        {
            return myWorker.Idle;
        }

        /// <summary>
        /// Post the given action of the worker action queue or this command station.
        /// </summary>
        protected void PostWorkerAction(Action action)
        {
            if (action == null)
                throw new ArgumentNullException("action");
            Idle = false;
            myWorker.PostAction(action);
        }

        /// <summary>
        /// Post the given action of the worker action queue or this command station.
        /// </summary>
        protected void PostWorkerDelayedAction(TimeSpan timeout, Action action)
        {
            if (action == null)
                throw new ArgumentNullException("action");
            myWorker.PostDelayedAction(timeout, action);
        }

        /// <summary>
        /// Post the given action of the dispatchers action queue.
        /// </summary>
        protected void PostDispatchAction(Action action)
        {
            if (action == null)
                throw new ArgumentNullException("action");
            RailwayState.Dispatcher.PostAction(action);
        }

        /// <summary>
        /// Can this command station be used to serve the given network?
        /// </summary>
        /// <param name="entity">The entity being search for.</param>
        /// <param name="network">The network in question</param>
        /// <param name="exactMatch">Set to true when there is an exact match in address type and address space, false otherwise.</param>
        public bool Supports(IAddressEntity entity, Network network, out bool exactMatch)
        {
            exactMatch = false;
            if (!Entity.GetSupportedAddressTypes(entity).Contains(network.Type))
                return false;
            // There is a match in type, look for an exact match
            var addressSpace = network.AddressSpace;
            if (string.IsNullOrEmpty(addressSpace))
            {
                // No address space specified, so no exact match
                return true;
            }
            exactMatch = addressSpaces.Any(x => string.Equals(addressSpace, x, StringComparison.OrdinalIgnoreCase));
            if ((addressSpaces.Length > 0) && !exactMatch)
            {
                // We have specific address spaces we support, the network has a different specific
                // address space, so we do not support it
                return false;
            }
            return true;
        }

        /// <summary>
        /// Add a junction to the list of junctions controlled by this command station.
        /// </summary>
        internal void AddJunction(IJunctionState junction)
        {
            junctions.Add(junction);
            AddInitializationObject(junction as IInitializeAtPowerOn);
        }

        /// <summary>
        /// Add a loc to the list of locs controlled by this command station.
        /// </summary>
        internal void AddLoc(ILocState loc)
        {
            locs.Add(loc);
            AddInitializationObject(loc as IInitializeAtPowerOn);
        }

        /// <summary>
        /// Add an input to the list of inputs controlled by this command station.
        /// </summary>
        internal void AddInput(IInputState input)
        {
            inputs.Add(input);
            AddInitializationObject(input as IInitializeAtPowerOn);
        }

        /// <summary>
        /// Add a signal to the list of signal controlled by this command station.
        /// </summary>
        internal void AddSignal(ISignalState signal)
        {
            signals.Add(signal);
            AddInitializationObject(signal as IInitializeAtPowerOn);
        }

        /// <summary>
        /// Add the given object to the initialization list.
        /// </summary>
        private void AddInitializationObject(IInitializeAtPowerOn iObject)
        {
            if (iObject != null)
            {
                initializationObjects.Add(iObject);
            }
        }

        /// <summary>
        /// Initialize all objects.
        /// This method must be called on the dispatcher thread.
        /// </summary>
        private void InitializeObjects()
        {
            foreach (var iterator in initializationObjects.OrderBy(x => x.Priority))
            {
                var iObject = iterator;
                try
                {
                    Log.Trace(() => string.Format(Strings.InitializingX, iObject.Description));
                    iObject.Initialize();
                }
                catch (Exception ex)
                {
                    Log.LogException(LogLevel.Error, string.Format(Strings.ErrInitializationOfXFailedBecauseY, iObject.Description, ex.Message), ex);
                }
            }
        }

        /// <summary>
        /// Gets advanced info for the given loc
        /// </summary>
        internal abstract string GetLocInfo(ILocState loc);
    }
}

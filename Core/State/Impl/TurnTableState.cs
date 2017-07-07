using System;
using System.Collections.Generic;
using System.ComponentModel;
using BinkyRailways.Core.Model;

namespace BinkyRailways.Core.State.Impl
{
    /// <summary>
    /// State of a single turntable.
    /// </summary>
    public sealed class TurnTableState  : ActiveJunctionState<ITurnTable>, ITurnTableState, IInitializeAtPowerOn, IInitializationJunctionState
    {
        private readonly Address positionAddress1;
        private readonly Address positionAddress2;
        private readonly Address positionAddress3;
        private readonly Address positionAddress4;
        private readonly Address positionAddress5;
        private readonly Address positionAddress6;
        private readonly bool invertPositions;
        private readonly Address writeAddress;
        private readonly bool invertWrite;
        private readonly Address busyAddress;
        private readonly bool invertBusy;
        private readonly BusyInput busyInput;
        private readonly int firstPosition;
        private readonly int lastPosition;
        private readonly int initialPosition;
        private readonly StateProperty<int> position;
        private bool initializePositionNeeded;

        /// <summary>
        /// Default ctor
        /// </summary>
        public TurnTableState(ITurnTable entity, RailwayState railwayState)
            : base(entity, railwayState)
        {
            positionAddress1 = entity.PositionAddress1;
            positionAddress2 = (positionAddress1 != null) ? entity.PositionAddress2 : null;
            positionAddress3 = (positionAddress2 != null) ? entity.PositionAddress3 : null;
            positionAddress4 = (positionAddress3 != null) ? entity.PositionAddress4 : null;
            positionAddress5 = (positionAddress4 != null) ? entity.PositionAddress5 : null;
            positionAddress6 = (positionAddress5 != null) ? entity.PositionAddress6 : null;
            invertPositions = entity.InvertPositions;
            writeAddress = entity.WriteAddress;
            invertWrite = entity.InvertWrite;
            busyAddress = entity.BusyAddress;
            invertBusy = entity.InvertBusy;
            firstPosition = entity.FirstPosition;
            lastPosition = entity.LastPosition;
            initialPosition = entity.InitialPosition;
            position = new StateProperty<int>(this, -1, null, OnRequestedPositionChanged, null);
            busyInput = new BusyInput(this);
        }

        /// <summary>
        /// Addresses of first position bits.
        /// Lowest bit comes first.
        /// This is an output signal.
        /// </summary>
        [DisplayName(@"Position addresses")]
        public IEnumerable<Address> PositionAddresses
        {
            get 
            { 
                yield return positionAddress1;
                if (positionAddress2 == null)
                    yield break;
                yield return positionAddress2;
                if (positionAddress3 == null)
                    yield break;
                yield return positionAddress3;
                if (positionAddress4 == null)
                    yield break;
                yield return positionAddress4;
                if (positionAddress5 == null)
                    yield break;
                yield return positionAddress5;
                if (positionAddress6 == null)
                    yield break;
                yield return positionAddress6;
            }
        }

        /// <summary>
        /// If set, the straight/off commands used for position addresses are inverted.
        /// </summary>
        [DisplayName(@"Invert positions")]
        public bool InvertPositions { get { return invertPositions; } }

        /// <summary>
        /// Address of the line used to indicate a "write address".
        /// This is an output signal.
        /// </summary>
        [DisplayName(@"Write address")]
        public Address WriteAddress { get { return writeAddress; } }

        /// <summary>
        /// If set, the straight/off command used for "write address" line is inverted.
        /// </summary>
        [DisplayName(@"Invert write address")]
        public bool InvertWrite { get { return invertWrite; } }

        /// <summary>
        /// Gets the busy input state.
        /// </summary>
        [DisplayName(@"Busy address")]
        public IInputState Busy { get { return busyInput; } }

        /// <summary>
        /// First position number. Typically 1.
        /// </summary>
        [DisplayName(@"First position")]
        public int FirstPosition { get { return firstPosition; } }

        /// <summary>
        /// Last position number. Typically 63.
        /// </summary>
        [DisplayName(@"Last position")]
        public int LastPosition { get { return lastPosition; } }

        /// <summary>
        /// Position number used to initialize the turntable with?
        /// </summary>
        [DisplayName(@"Initial position")]
        public int InitialPosition { get { return initialPosition; } }

        /// <summary>
        /// Position of the turntable.
        /// </summary>
        [DisplayName(@"Position")]
        public IStateProperty<int> Position { get { return position; } }

        /// <summary>
        /// Forward position request to command station
        /// </summary>
        private void OnRequestedPositionChanged(int value)
        {
            var cs = CommandStation;
            if (cs != null)
            {
                cs.SendTurnTablePosition(this);
            }
        }

        /// <summary>
        /// Prepare this state for use in a live railway. 
        /// Make sure all relevant connections to other state objects are resolved.
        /// </summary>
        /// <returns>True if the entity is now ready for use in a live railway, false otherwise.</returns>
        protected override bool TryPrepareForUse(IStateUserInterface ui, IStatePersistence statePersistence)
        {
            if (!base.TryPrepareForUse(ui, statePersistence))
                return false;
            CommandStation.AddInput(busyInput);
            return true;
        }

        /// <summary>
        /// Consider the next request a First request after power down.
        /// This ensures that the switch is always initialized properly.
        /// </summary>
        protected override void OnPowerDown()
        {
            base.OnPowerDown();
            position.FirstRequest = true;
        }

        /// <summary>
        /// Called when a Power up is received.
        /// </summary>
        protected override void OnPowerUp()
        {
            base.OnPowerUp();
            initializePositionNeeded = true;
        }

        /// <summary>
        /// Used for ordering initialization called.
        /// </summary>
        InitializationPriority IInitializeAtPowerOn.Priority
        {
            get { return InitializationPriority.Junction; }
        }

        /// <summary>
        /// Perform initialization actions.
        /// This method is always called on the dispatcher thread.
        /// </summary>
        void IInitializeAtPowerOn.Initialize()
        {
            if (initializePositionNeeded)
            {
                initializePositionNeeded = false;
                if (LockedBy == null)
                {
                    position.Requested = initialPosition;
                }
            }
        }

        /// <summary>
        /// Set initial position
        /// </summary>
        void IInitializationJunctionState.Initialize()
        {
            position.Requested = initialPosition;
        }

        /// <summary>
        /// Busy signal has changed.
        /// </summary>
        private void BusyChanged(bool value)
        {
            if (value && !position.IsConsistent)
            {
                // turntable is now moving
            }
            else if (!value && !position.IsConsistent)
            {
                // turntable has stopped
                position.Actual = position.Requested;
            }
        }

        /// <summary>
        /// Input state attached to the Busy signal.
        /// </summary>
        private class BusyInput : IInputState
        {
            public event EventHandler RequestedStateChanged;
            public event EventHandler ActualStateChanged;

            private readonly ActualStateProperty<bool> active;
            private readonly TurnTableState turnTable;

            /// <summary>
            /// Default ctor
            /// </summary>
            internal BusyInput(TurnTableState turnTable)
            {
                this.turnTable = turnTable;
                active = new ActualStateProperty<bool>(turnTable, false, ValidateActive, turnTable.BusyChanged);
            }

            /// <summary>
            /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
            /// </summary>
            /// <filterpriority>2</filterpriority>
            void IDisposable.Dispose()
            {
                // Do nothing
            }

            /// <summary>
            /// Unique ID of the underlying entity (if any)
            /// </summary>
            IEntity IEntityState.Entity
            {
                get { return null; }
            }

            /// <summary>
            /// Unique ID of the underlying entity (if any)
            /// </summary>
            string IEntityState.EntityId
            {
                get { return string.Empty; }
            }

            /// <summary>
            /// Gets the description of the entity.
            /// </summary>
            string IEntityState.Description
            {
                get { return "Busy signal"; }
            }

            /// <summary>
            /// Gets the railway state this object is a part of.
            /// </summary>
            IRailwayState IEntityState.RailwayState
            {
                get { return turnTable.RailwayState; }
            }

            /// <summary>
            /// Is this entity fully resolved such that is can be used in the live railway?
            /// </summary>
            bool IEntityState.IsReadyForUse
            {
                get { return turnTable.IsReadyForUse; }
            }

            /// <summary>
            /// Address of the entity
            /// </summary>
            Address IInputState.Address
            {
                get { return turnTable.busyAddress; }
            }

            /// <summary>
            /// Is this sensor in the 'active' state?
            /// </summary>
            IActualStateProperty<bool> IInputState.Active
            {
                get { return active; }
            }

            /// <summary>
            /// Input the given value when <see cref="ITurnTable.InvertBusy"/> is set.
            /// </summary>
            private bool ValidateActive(bool value)
            {
                if (turnTable.invertBusy)
                    return !value;
                return value;
            }
        }
    }
}

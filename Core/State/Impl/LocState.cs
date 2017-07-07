using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using BinkyRailways.Core.Model;
using BinkyRailways.Core.State.Automatic;
using BinkyRailways.Core.Util;
using LocFunction = BinkyRailways.Core.Model.LocFunction;
using Newtonsoft.Json;

namespace BinkyRailways.Core.State.Impl
{
    /// <summary>
    /// State of a single locomotive.
    /// </summary>
    [JsonObject]
    public sealed class LocState : EntityState<ILoc>, ILocState, IInitializeAtPowerOn
    {
        /// <summary>
        /// All settings of this loc will be reset, because the loc is taken of the track.
        /// </summary>
        public event EventHandler BeforeReset;

        /// <summary>
        /// All settings of this loc have been reset, because the loc is taken of the track.
        /// </summary>
        public event EventHandler AfterReset;

        private ICommandStationState commandStation;
        private readonly StateProperty<bool> controlledAutomatically;
        private readonly ActualStateProperty<AutoLocState> automaticState;
        private readonly ActualStateProperty<IRouteStateForLoc> currentRoute;
        private readonly ActualStateProperty<bool> waitAfterCurrentRoute;
        private readonly ActualStateProperty<IRouteState> nextRoute;
        private readonly ActualStateProperty<IBlockState> currentBlock;
        private readonly ActualStateProperty<BlockSide> currentBlockEnterSide;
        private readonly ActualStateProperty<DateTime> startNextRouteTime;
        private readonly ActualStateProperty<DateTime> durationExceedsCurrentRouteTime;
        private readonly SpeedProperty speed;
        private readonly StateProperty<int> speedInSteps;
        private readonly StateProperty<LocDirection> direction;
        private readonly ActualStateProperty<bool> reversing;
        private readonly StateProperty<bool> f0;
        private readonly Dictionary<LocFunction, IStateProperty<bool>> functionStates;
        private readonly List<ILockableState> lockedEntities = new List<ILockableState>();
        private IStatePersistence statePersistence;
        private readonly Address address;
        private readonly RecentlyVisitedBlocks recentlyVisitedBlocks = new RecentlyVisitedBlocks();
        private IRouteSelector routeSelector;
        private IRouteEventBehaviorState lastEventBehavior;
        private readonly ActualStateProperty<IRouteOption[]> lastRouteOptions;

        /// <summary>
        /// Default ctor
        /// </summary>
        public LocState(ILoc entity, RailwayState railwayState)
            : base(entity, railwayState)
        {
            address = entity.Address;
            controlledAutomatically = new StateProperty<bool>(this, false, ValidateControlledAutomatically, null, OnControlledAutomaticallyChanged);
            automaticState = new ActualStateProperty<AutoLocState>(this, AutoLocState.AssignRoute, null, OnAutomaticStateChanged);
            currentRoute = new ActualStateProperty<IRouteStateForLoc>(this, null, null, null);
            waitAfterCurrentRoute = new ActualStateProperty<bool>(this, false, null, null);
            nextRoute = new ActualStateProperty<IRouteState>(this, null, null, null);
            currentBlock = new ActualStateProperty<IBlockState>(this, null, ValidateCurrentBlock, x => {
                recentlyVisitedBlocks.Insert(x);
                SaveCurrentBlock();
            });
            currentBlockEnterSide = new ActualStateProperty<BlockSide>(this, BlockSide.Back, null, x => SaveCurrentBlock());
            startNextRouteTime = new ActualStateProperty<DateTime>(this, DateTime.Now, null, null);
            durationExceedsCurrentRouteTime = new ActualStateProperty<DateTime>(this, DateTime.MaxValue, null, null);
            speedInSteps = new StateProperty<int>(this, 0, LimitSpeedSteps, OnRequestedSpeedStepsChanged, null);
            speed = new SpeedProperty(this);
            direction = new StateProperty<LocDirection>(this, LocDirection.Forward, null, OnRequestedDirectionChanged, x => SaveCurrentBlock());
            reversing = new ActualStateProperty<bool>(this, false, ValidateReversing, null);
            f0 = new StateProperty<bool>(this, true, null, OnRequestedFunctionChanged, null);
            functionStates = new Dictionary<LocFunction, IStateProperty<bool>>();
            foreach (var lf in entity.Functions)
            {
                var f = new StateProperty<bool>(this, false, null, OnRequestedFunctionChanged, null);
                functionStates[lf.Function] = f;
            }
            lastRouteOptions = new ActualStateProperty<IRouteOption[]>(this, null, null, null);
        }

        /// <summary>
        /// Address of the entity
        /// </summary>
        [DisplayName(@"Address")]
        public Address Address { get { return address; } }

        /// <summary>
        /// Percentage of speed steps for the slowest speed of this loc.
        /// Value between 1 and 100.
        /// </summary>
        [DisplayName(@"Slow speed")]
        public int SlowSpeed { get { return Entity.SlowSpeed; } }

        /// <summary>
        /// Percentage of speed steps for the medium speed of this loc.
        /// Value between 1 and 100.
        /// </summary>
        [DisplayName(@"Medium speed")]
        public int MediumSpeed { get { return Entity.MediumSpeed; } }

        /// <summary>
        /// Percentage of speed steps for the maximum speed of this loc.
        /// Value between 1 and 100.
        /// </summary>
        [DisplayName(@"Max. speed")]
        public int MaximumSpeed { get { return Entity.MaximumSpeed; } }

        /// <summary>
        /// Gets the number of speed steps supported by this loc.
        /// </summary>
        [DisplayName(@"#Speed steps")]
        public int SpeedSteps { get { return Entity.SpeedSteps; } }

        /// <summary>
        /// Gets/sets the image of the given loc.
        /// </summary>
        /// <value>Null if there is no image.</value>
        /// <remarks>Image must be png, bmp, gif, jpg, wmf or emf</remarks>
        [DisplayName(@"Image")]
        [JsonIgnore]
        public Stream Image { get { return Entity.Image; } }

        /// <summary>
        /// Gets the name of the given function.
        /// </summary>
        /// <returns>True if a function name was found, false if a default name is used.</returns>
        public string GetFunctionName(LocFunction function, out bool isCustom)
        {
            var setting = Entity.Functions.FirstOrDefault(x => x.Function == function);
            if (setting != null)
            {
                // Custom name
                isCustom = true;
                return setting.Description;
            }
            isCustom = false;
            return (function == LocFunction.Light) ? "Light" : string.Format("F{0}", (int) function);
        }

        /// <summary>
        /// Gets the name of the person that owns this loc.
        /// </summary>
        [DisplayName(@"Owner")]
        public string Owner { get { return Entity.Owner; } }

        /// <summary>
        /// Is it allowed for this loc to change direction?
        /// </summary>
        [DisplayName(@"Change direction")]
        public ChangeDirection ChangeDirection { get { return Entity.ChangeDirection; } }

        /// <summary>
        /// Is this loc controlled by the automatic loc controller?
        /// </summary>
        [DisplayName(@"Controlled automatically")]
        public IStateProperty<bool> ControlledAutomatically { get { return controlledAutomatically; } }

        /// <summary>
        /// Is it allowed to set the ControlledAutoatically property to true?
        /// </summary>
        public bool CanSetAutomaticControl
        {
            get
            {
                // A current block must be set
                return (CurrentBlock.Actual != null);
            }
        }

        /// <summary>
        /// The current state of this loc in the automatic loc controller.
        /// </summary>
        [DisplayName(@"Automatic state")]
        public IActualStateProperty<AutoLocState> AutomaticState { get { return automaticState; } }

        /// <summary>
        /// Gets the route that this loc is currently taking.
        /// </summary>
        [DisplayName(@"Current route")]
        [JsonIgnore]
        public IActualStateProperty<IRouteStateForLoc> CurrentRoute { get { return currentRoute; } }

        /// <summary>
        /// Should the loc wait when the current route has finished?
        /// </summary>
        [DisplayName(@"Wait after current route")]
        public IActualStateProperty<bool> WaitAfterCurrentRoute { get { return waitAfterCurrentRoute; } }

        /// <summary>
        /// Time when this loc will exceed the maximum duration of the current route.
        /// </summary>
        [DisplayName(@"Duration exceeds current route time")]
        public IActualStateProperty<DateTime> DurationExceedsCurrentRouteTime { get { return durationExceedsCurrentRouteTime; } }

        /// <summary>
        /// Is the maximum duration of the current route this loc is taken exceeded?
        /// </summary>
        [DisplayName(@"Is max duration of current route exceeded?")]
        public bool IsCurrentRouteDurationExceeded
        {
            get
            {
                if (!ControlledAutomatically.Actual)
                    return false;
                if (CurrentRoute.Actual == null)
                    return false;
                return (DateTime.Now > DurationExceedsCurrentRouteTime.Actual);
            }
        }

        /// <summary>
        /// Gets the route that this loc will take when the current route has finished.
        /// This property is only set by the automatic loc controller.
        /// </summary>
        [DisplayName(@"Next route")]
        public IActualStateProperty<IRouteState> NextRoute { get { return nextRoute; } }

        /// <summary>
        /// Gets the block that the loc is currently in.
        /// </summary>
        [DisplayName(@"Current block")]
        [JsonIgnore]
        public IActualStateProperty<IBlockState> CurrentBlock { get { return currentBlock; } }

        /// <summary>
        /// Gets the side at which the current block was entered.
        /// </summary>
        [DisplayName(@"Current block enter side")]
        public IActualStateProperty<BlockSide> CurrentBlockEnterSide { get { return currentBlockEnterSide; } }

        /// <summary>
        /// Time when this loc will start it's next route.
        /// </summary>
        [DisplayName(@"Start next route time")]
        public IActualStateProperty<DateTime> StartNextRouteTime { get { return startNextRouteTime; } }

        /// <summary>
        /// Route options as considered last by the automatic train controller.
        /// </summary>
        [Browsable(false)]
        public IActualStateProperty<IRouteOption[]> LastRouteOptions { get { return lastRouteOptions; } }

        /// <summary>
        /// Route options as considered last by the automatic train controller.
        /// </summary>
        [DisplayName(@"Last route options")]
        public string[] LastRouteOptionsText
        {
            get
            {
                var arr = LastRouteOptions.Actual;
                if (arr == null) return null;
                return arr.Select(x => x.ToString()).ToArray();
            }
        }

        /// <summary>
        /// Gets/sets a selector used to select the next route from a list of possible routes.
        /// If no route selector is set, a default will be created.
        /// </summary>
        [JsonIgnore]
        public IRouteSelector RouteSelector
        {
            get
            {
                var result = routeSelector;
                if (result == null)
                {
                    result  = new DefaultRouteSelector();
                    routeSelector = result;
                }
                return result;
            }
            set
            {
                if (routeSelector != value)
                {
                    routeSelector = value;
                    OnActualStateChanged();
                }
            }
        }

        /// <summary>
        /// Current speed of this loc as percentage of the speed steps of the loc.
        /// Value between 0 and 100.
        /// Setting this value will result in a request to its command station to alter the speed.
        /// </summary>
        [DisplayName(@"Speed")]
        public IStateProperty<int> Speed
        {
            get { return speed; }
        }

        /// <summary>
        /// Gets a human readable representation of the speed of the loc.
        /// </summary>
        public string GetSpeedText()
        {
            var result = Speed.Actual.ToString();
            var direction = Direction.Actual;
            var postfix = (direction == LocDirection.Forward) ? Strings.LocForwardPostfix : Strings.LocReversePostfix;
            return result + postfix;
        }

        /// <summary>
        /// Gets a human readable representation of the state of the loc.
        /// </summary>
        public string GetStateText()
        {
            var prefix = string.Empty;
            var targetRouteSelector = RouteSelector as TargetBlockRouteSelector;
            if (targetRouteSelector != null)
            {
                prefix = "@" + targetRouteSelector.TargetBlock.Description + " ";
            }
            var block = CurrentBlock.Actual;

            if (ControlledAutomatically.Actual)
            {
                // Automatic
                var route = CurrentRoute.Actual;
                var state = AutomaticState.Actual;
                switch (state)
                {
                    case AutoLocState.AssignRoute:
                        return prefix + Strings.LocStateAssignRoute;
                    case AutoLocState.ReversingWaitingForDirectionChange:
                        return prefix + Strings.LocStateReversingWaitingForDirectionChange;
                    case AutoLocState.WaitingForAssignedRouteReady:
                        return prefix + Strings.LocStatePreparingRoute;
                    case AutoLocState.Running:
                        return prefix + route.Route.Description;
                    case AutoLocState.EnterSensorActivated:
                        return prefix + route.Route.Description + " .";
                    case AutoLocState.EnteringDestination:
                        return prefix + route.Route.Description + " ..";
                    case AutoLocState.ReachedSensorActivated:
                        return prefix + route.Route.Description + " ...";
                    case AutoLocState.ReachedDestination:
                        return prefix + route.Route.Description + " ....";
                    case AutoLocState.WaitingForDestinationTimeout:
                        var delay = Math.Max(0, StartNextRouteTime.Actual.Subtract(DateTime.Now).TotalSeconds);
                        return prefix + string.Format(Strings.LocStateWaitingXSeconds, (int)Math.Round(delay));
                    case AutoLocState.WaitingForDestinationGroupMinimum:
                        return prefix + Strings.LocStateWaitingForGroupMinimum;
                    default:
                        throw new ArgumentException("Unknown loc state: " + state);
                }
            }

            // Manual
            return prefix + ((block != null) ? string.Format(Strings.LocStateManualInX, block) : Strings.LocStateManual);
        }

        /// <summary>
        /// Gets the actual speed of the loc in speed steps
        /// Value between 0 and the maximum number of speed steps supported by this loc.
        /// Setting this value will result in a request to its command station to alter the speed.
        /// </summary>
        [DisplayName(@"Speed in steps")]
        public IStateProperty<int> SpeedInSteps
        {
            get { return speedInSteps; }
        }

        /// <summary>
        /// Directional lighting of the loc.
        /// Setting this value will result in a request to its command station to alter the value.
        /// </summary>
        [DisplayName(@"F0")]
        public IStateProperty<bool> F0 { get { return f0; } }


        /// <summary>
        /// Return the state of a function.
        /// </summary>
        /// <returns>True if such a state exists, false otherwise</returns>
        public bool TryGetFunctionState(LocFunction function, out IStateProperty<bool> state)
        {
            return functionStates.TryGetValue(function, out state);
        }

        /// <summary>
        /// Return all functions that have state.
        /// </summary>
        public IEnumerable<LocFunction> Functions
        {
            get { return functionStates.Keys; }
        }

        /// <summary>
        /// Requested speedsteps value has changed.
        /// Forward to command station.
        /// </summary>
        private void OnRequestedSpeedStepsChanged(int value)
        {
            if (commandStation != null)
            {
                commandStation.SendLocSpeedAndDirection(this);
            }
        }

        /// <summary>
        /// Current direction of this loc.
        /// Setting this value will result in a request to its command station to alter the direction.
        /// </summary>
        [DisplayName(@"Direction")]
        public IStateProperty<LocDirection> Direction
        {
            get { return direction; }
        }

        /// <summary>
        /// Is this loc reversing out of a dead end?
        /// This can only be true for locs that are not allowed to change direction.
        /// </summary>
        [DisplayName(@"Reversing")]
        public IActualStateProperty<bool> Reversing
        {
            get { return reversing; }
        }

        /// <summary>
        /// Try to assign the given loc to the given block.
        /// Assigning is only possible when the loc is not controlled automatically and
        /// the block can be assigned by the given loc.
        /// If the loc is already assigned to another block, this assignment is removed
        /// and the block on that block is unlocked.
        /// </summary>
        /// <param name="block">The new block to assign to. If null, the loc will only be unassigned from the current block.</param>
        /// <param name="currentBlockEnterSide">The site to which the block is entered (invert of facing)</param>
        /// <returns>True on success, false otherwise</returns>
        public bool AssignTo(IBlockState block, BlockSide currentBlockEnterSide)
        {
            return RailwayState.Dispatcher.PostActionAndWait(() => OnAssignTo(block, currentBlockEnterSide));
        }

        /// <summary>
        /// Forcefully reset of settings of this loc.
        /// This should be used when a loc is taken of the track.
        /// </summary>
        public void Reset()
        {
            RailwayState.Dispatcher.PostAction(() => OnReset(25));
        }

        /// <summary>
        /// Forcefully reset of settings of this loc.
        /// This should be used when a loc is taken of the track.
        /// </summary>
        private void OnReset(int maxRetries)
        {
            // Stop
            Speed.Requested = 0;
            ControlledAutomatically.Requested = false;

            // Disconnect everything
            BeforeReset.Fire(this);

            // Disconnect from block
            if (!AssignTo(null, BlockSide.Back))
            {
                if (maxRetries > 0)
                {
                    RailwayState.Dispatcher.PostAction(() => OnReset(maxRetries - 1));
                } 
            }

            // Disconnect everything
            AfterReset.Fire(this);
        }

        /// <summary>
        /// Gets command station specific (advanced) info for this loc.
        /// </summary>
        public string CommandStationInfo
        {
            get { return (commandStation != null) ? ((CommandStationState) commandStation).GetLocInfo(this) : string.Empty; } 
        }

        /// <summary>
        /// Save the current state to the state persistence.
        /// </summary>
        void ILocState.PersistState()
        {
            SaveCurrentBlock();
        }

        /// <summary>
        /// Gets zero or more blocks that were recently visited by this loc.
        /// The first block was last visited.
        /// </summary>
        IEnumerable<IBlockState> ILocState.RecentlyVisitedBlocks
        {
            get { return recentlyVisitedBlocks; }
        }

        /// <summary>
        /// Behavior of the last event triggered by this loc.
        /// </summary>
        IRouteEventBehaviorState ILocState.LastEventBehavior
        {
            get { return lastEventBehavior; }
            set { lastEventBehavior = value; }
        }

        /// <summary>
        /// Is the speed behavior of the last event set to default?
        /// </summary>
        bool ILocState.IsLastEventBehaviorSpeedDefault
        {
            get
            {
                var behavior = lastEventBehavior;
                return (behavior == null) || (behavior.SpeedBehavior == LocSpeedBehavior.Default);
            }
        }

        /// <summary>
        /// Try to assign the given loc to the given block.
        /// This helper is run on the state dispatcher thread.
        /// </summary>
        /// <returns>True on success, false otherwise</returns>
        private bool OnAssignTo(IBlockState block, BlockSide currentBlockEnterSide)
        {
            if (ControlledAutomatically.Actual || ControlledAutomatically.Requested)
                return false;

            // No change?
            var current = CurrentBlock.Actual;
            if (current == block)
                return true;

            // Try to lock new block
            if (block != null)
            {
                ILocState tmp;
                if (!block.CanLock(this, out tmp))
                    return false;
            }

            // Unassign from current block
            if (current != null)
            {
                current.AssertLockedBy(this);
                current.Unlock(null);
            }
            CurrentBlock.Actual = null;

            // Now assign to new block (if any)
            if (block != null)
            {
                block.Lock(this);
                CurrentBlockEnterSide.Actual = currentBlockEnterSide;
                CurrentBlock.Actual = block;
            }
            else
            {
                // Unassign -> unlock all
                UnlockAll();
            }

            return true;
        }

        /// <summary>
        /// Requested direction value has changed.
        /// Forward to command station.
        /// </summary>
        private void OnRequestedDirectionChanged(LocDirection value)
        {
            if (commandStation != null) 
            {
                commandStation.SendLocSpeedAndDirection(this);
            }
        }

        /// <summary>
        /// Requested function (f0...) value has changed.
        /// Forward to command station.
        /// </summary>
        private void OnRequestedFunctionChanged(bool value)
        {
            if (commandStation != null)
            {
                if (!f0.IsConsistent || functionStates.Values.Any(x => !x.IsConsistent)) {
                    commandStation.SendLocSpeedAndDirection(this);
                }
            }
        }

        /// <summary>
        /// The <see cref="ControlledAutomatically"/> value has changed.
        /// </summary>
        private void OnControlledAutomaticallyChanged(bool newValue)
        {
            if (!newValue)
            {
                // Unlock next route (if needed)
                var route = NextRoute.Actual;
                if (route != null)
                {
                    NextRoute.Actual = null;
                    var block = CurrentBlock.Actual;
                    route.Unlock(block);
                }
            }
        }

        /// <summary>
        /// Update settings according to the new automatic loc state.
        /// </summary>
        private void OnAutomaticStateChanged(AutoLocState newState)
        {
            var route = CurrentRoute.Actual;
            switch (newState)
            {
                case AutoLocState.Running:
                    // Setup max duration exceeds time
                    if (route == null)
                    {
                        DurationExceedsCurrentRouteTime.Actual = DateTime.MaxValue;
                    }
                    else
                    {
                        DurationExceedsCurrentRouteTime.Actual = DateTime.Now + TimeSpan.FromSeconds(route.Route.MaxDuration);
                    }
                    break;
                case AutoLocState.AssignRoute:
                case AutoLocState.ReachedDestination:
                    // Reset max duration exceeds time
                    DurationExceedsCurrentRouteTime.Actual = DateTime.MaxValue;
                    break;

            }
            // Trigger actions
            if (route != null)
            {
                switch (newState)
                {
                    case AutoLocState.EnteringDestination:
                        route.Route.FireEnteringDestinationTrigger(this);
                        break;
                    case AutoLocState.ReachedDestination:
                        route.Route.FireDestinationReachedTrigger(this);
                        break;
                }
            }
        }

        /// <summary>
        /// Limit the given value to a valid speed steps value.
        /// </summary>
        private int LimitSpeedSteps(int value)
        {
            return Math.Max(0, Math.Min(value, Entity.SpeedSteps - 1));
        }

        /// <summary>
        /// Prepare this state for use in a live railway. 
        /// Make sure all relevant connections to other state objects are resolved.
        /// </summary>
        /// <returns>True if the entity is now ready for use in a live railway, false otherwise.</returns>
        protected override bool TryPrepareForUse(IStateUserInterface ui, IStatePersistence statePersistence)
        {
            var cs = RailwayState.SelectCommandStation(Entity);
            if (cs == null)
                return false;
            this.statePersistence = statePersistence;
            cs.AddLoc(this);
            commandStation = cs;
            return true;
        }

        /// <summary>
        /// Save the current block state
        /// </summary>
        private void SaveCurrentBlock()
        {
            if (statePersistence != null)
            {
                var currentBlockState = CurrentBlock.Actual;
                statePersistence.SetLocState(RailwayState, this, currentBlockState, CurrentBlockEnterSide.Actual, Direction.Actual);
            }
        }

        /// <summary>
        /// Add a state object to the list of entities locked by this locomotive.
        /// </summary>
        internal void AddLockedEntity(ILockableState entity)
        {
            lockedEntities.Add(entity);
        }

        /// <summary>
        /// Remove a state object from the list of entities locked by this locomotive.
        /// </summary>
        internal void RemoveLockedEntity(ILockableState entity)
        {
            lockedEntities.Remove(entity);
        }

        /// <summary>
        /// Validate that the <see cref="ControlledAutomatically"/> property can be set to the given value.
        /// </summary>
        private bool ValidateControlledAutomatically(bool value)
        {
            if (value)
            {
                // A current block must be set
                if (CurrentBlock.Actual == null)
                {
                    throw new ArgumentException(Strings.Cannot_set_loc_in_automatic_controlled_mode_without_an_assigned_block);
                }
            }
            return value;
        }

        /// <summary>
        /// Validate that the <see cref="CurrentBlock"/> property can be set to the given value/
        /// </summary>
        private IBlockState ValidateCurrentBlock(IBlockState block)
        {
            if (block == null)
            {
                // Validate a reset
                if (ControlledAutomatically.Actual)
                {
                    throw new ArgumentException(Strings.LocCannotResetCurrentBlockInAutomaticMode);
                }
                // Otherwise ok.
                return block;
            }
            else
            {
                // Validate a 'set'.
                // The block must be locked by this loc
                if (!block.IsLockedBy(this))
                {
                    throw new ArgumentException(Strings.LocBlockIsNotLockedByThisLoc);
                }
                return block;
            }
        }

        /// <summary>
        /// Validate that the <see cref="Reversing"/> property can be set to the given value/
        /// </summary>
        private bool ValidateReversing(bool value)
        {
            if (value)
            {
                if (Entity.ChangeDirection == ChangeDirection.Allow)
                {
                    throw new ArgumentException(Strings.LocReversingNotAllowed);
                }
            }
            return value;
        }

        /// <summary>
        /// Unlock all entities locked by me
        /// </summary>
        public void UnlockAll()
        {
            while (lockedEntities.Count > 0)
            {
                lockedEntities[0].Unlock(null);
            }
        }

        /// <summary>
        /// Cleanup
        /// </summary>
        public override void Dispose()
        {
            base.Dispose();
            speedInSteps.Dispose();
            direction.Dispose();
            UnlockAll();
        }

        /// <summary>
        /// Implementation of Speed property
        /// </summary>
        [JsonObject]
        public sealed class SpeedProperty : IStateProperty<int>
        {
            /// <summary>
            /// Fired when the requested value has changed.
            /// </summary>
            public event EventHandler RequestedChanged
            {
                add { owner.speedInSteps.RequestedChanged += value; }
                remove { owner.speedInSteps.RequestedChanged -= value; }
            }

            /// <summary>
            /// Fired when the actual value has changed.
            /// </summary>
            public event EventHandler ActualChanged
            {
                add { owner.speedInSteps.ActualChanged += value; }
                remove { owner.speedInSteps.ActualChanged -= value; }
            }

            private readonly LocState owner;

            /// <summary>
            /// Default ctor
            /// </summary>
            public SpeedProperty(LocState owner)
            {
                this.owner = owner;
            }

            /// <summary>
            /// Gets / sets the requested value
            /// </summary>
            public int Requested
            {
                get { return ConvertFromSpeedSteps(owner.SpeedInSteps.Requested); }
                set { owner.SpeedInSteps.Requested = ConvertToSpeedSteps(value); }
            }

            /// <summary>
            /// Gets / sets the actual value
            /// </summary>
            [ReadOnly(true)]
            public int Actual
            {
                get { return ConvertFromSpeedSteps(owner.SpeedInSteps.Actual); }
                set { owner.SpeedInSteps.Actual = ConvertToSpeedSteps(value); }
            }

            /// <summary>
            /// Is the request value equal to the actual value?
            /// </summary>
            public bool IsConsistent { get { return owner.speedInSteps.IsConsistent; } }

            /// <summary>
            /// Convert a speed percentage to speed steps
            /// </summary>
            private int ConvertToSpeedSteps(int percentage)
            {
                var p = Math.Max(0, Math.Min(100, percentage)) / 100.0;
                return (int)Math.Round(p * (owner.Entity.SpeedSteps - 1));
            }

            /// <summary>
            /// Convert a speed steps value to a percentage value
            /// </summary>
            private int ConvertFromSpeedSteps(int steps)
            {
                double maxSteps = owner.Entity.SpeedSteps - 1;
                var p = Math.Max(0, Math.Min(maxSteps, steps)) / maxSteps;
                return (int)Math.Round(p * 100);
            }

            /// <summary>
            /// Convert to string
            /// </summary>
            public override string ToString()
            {
                return string.Format("{0}/{1}", Requested, Actual);
            }
        }

        /// <summary>
        /// Used for ordering initialization called.
        /// </summary>
        InitializationPriority IInitializeAtPowerOn.Priority
        {
            get { return InitializationPriority.Loc; }
        }

        /// <summary>
        /// Perform initialization actions.
        /// This method is always called on the dispatcher thread.
        /// </summary>
        void IInitializeAtPowerOn.Initialize()
        {
            if (!ControlledAutomatically.Actual)
            {
                var isAssigned = (CurrentBlock.Actual != null);
                SpeedInSteps.Requested = 0;
                var initDirection = isAssigned ? Direction.Actual : LocDirection.Forward;
                Direction.Requested = initDirection;
                OnRequestedSpeedStepsChanged(0);
                OnRequestedDirectionChanged(initDirection);
            }
            else
            {
                // Loc is running automatically, make sure current/next route are still prepared.
                var route = CurrentRoute.Actual;
                if (route != null)
                {
                    route.Route.Prepare();
                }
                var next = NextRoute.Actual;
                if (next != null)
                {
                    next.Prepare();
                }
            }
        }
    }
}

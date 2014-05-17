using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using BinkyRailways.Core.Model;

namespace BinkyRailways.Core.State.Impl
{
    /// <summary>
    /// State of a single block.
    /// </summary>
    public sealed class BlockState : LockableState<IBlock>, IBlockState
    {
        private readonly StateProperty<bool> closed;
        private readonly ILocPredicateState waitPermissions;
        private IStatePersistence statePersistence;
        private readonly List<IJunctionState> junctions = new List<IJunctionState>();
        private bool deadEnd;
        private IBlockGroupState blockGroup;

        /// <summary>
        /// Default ctor
        /// </summary>
        public BlockState(IBlock block, RailwayState railwayState)
            : base(block, railwayState)
        {
            closed = new StateProperty<bool>(this, false, null, x => UpdateClosed(), x => SaveClosed());
            waitPermissions = new LocPredicateState(block.WaitPermissions, railwayState);
        }

        /// <summary>
        /// Probability (in percentage) that a loc that is allowed to wait in this block
        /// will actually wait.
        /// When set to 0, no locs will wait (unless there is no route available).
        /// When set to 100, all locs (that are allowed) will wait.
        /// </summary>
        [DisplayName(@"Wait probability")]
        public int WaitProbability { get { return Entity.WaitProbability; } }

        /// <summary>
        /// Minimum amount of time to wait (if <see cref="WaitProbability"/> is set) in seconds.
        /// </summary>
        [DisplayName(@"Min. wait time")]
        public int MinimumWaitTime { get { return Entity.MinimumWaitTime; } }

        /// <summary>
        /// Maximum amount of time to wait (if <see cref="WaitProbability"/> is set) in seconds.
        /// </summary>
        [DisplayName(@"Max. wait time")]
        public int MaximumWaitTime { get { return Entity.MaximumWaitTime; } }

        /// <summary>
        /// Gets the predicate used to decide which locs are allowed to wait in this block.
        /// </summary>
        [DisplayName(@"Wait permissions")]
        public ILocPredicateState WaitPermissions { get { return waitPermissions; } }

        /// <summary>
        /// By default the front of the block is on the right of the block.
        /// When this property is set, that is reversed to the left of the block.
        /// Setting this property will only alter the display behavior of the block.
        /// </summary>
        [DisplayName(@"Reverse sides")]
        public bool ReverseSides { get { return Entity.ReverseSides; } }

        /// <summary>
        /// Is it allowed for locs to change direction in this block?
        /// </summary>
        [DisplayName(@"Change direction")]
        public ChangeDirection ChangeDirection { get { return Entity.ChangeDirection; } }

        /// <summary>
        /// Must reversing locs change direction (back to normal) in this block?
        /// </summary>
        [DisplayName(@"Change direction reversing locs")]
        public bool ChangeDirectionReversingLocs { get { return Entity.ChangeDirectionReversingLocs; } }

        /// <summary>
        /// Gets all sensors that are either an "entering" or a "reached" sensor for a route
        /// that leads to this block.
        /// </summary>
        [DisplayName(@"Sensors")]
        public IEnumerable<ISensorState> Sensors
        {
            get 
            {
                var routes = RailwayState.RouteStates.Where(x => x.To == this).ToList();
                return RailwayState.SensorStates.Where(x => routes.Any(r => r.Contains(x)));
            }
        }

        /// <summary>
        /// Is this block closed for traffic?
        /// </summary>
        [DisplayName(@"Closed")]
        public IStateProperty<bool> Closed { get { return closed; } }

        /// <summary>
        /// Can a loc only leave this block at the same side it got in?
        /// </summary>
        [DisplayName(@"Is dead end")]
        public bool IsDeadEnd { get { return deadEnd; } }

        /// <summary>
        /// Is this block considered a station?
        /// </summary>
        [DisplayName(@"Is station")]
        public bool IsStation
        {
            get { return Entity.IsStation; }
        }

        /// <summary>
        /// Gets the state of the group this block belongs to.
        /// Can be null.
        /// </summary>
        [DisplayName(@"Group")]
        public IBlockGroupState BlockGroup { get { return blockGroup; } }

        /// <summary>
        /// Is there a loc waiting in this block?
        /// </summary>
        [DisplayName(@"Has waiting loc")]
        public bool HasWaitingLoc
        {
            get
            {
                var loc = LockedBy;
                if ((loc == null) || (State != Core.State.BlockState.Occupied)) return false;
                var automaticState = loc.AutomaticState.Actual;
                return (((automaticState == AutoLocState.AssignRoute) && (loc.Speed.Requested == 0)) ||
                        (automaticState == AutoLocState.WaitingForDestinationTimeout) || 
                        (automaticState == AutoLocState.WaitingForDestinationGroupMinimum));
            }
        }

        /// <summary>
        /// Gets the current state of this block
        /// </summary>
        [DisplayName(@"State")]
        public State.BlockState State
        {
            get
            {
                var loc = LockedBy;
                if (loc != null)
                {
                    // Locked
                    if (loc.CurrentBlock.Actual == this)
                    {
                        // Block is in use
                        return Core.State.BlockState.Occupied;
                    }
                    var currentRoute = loc.CurrentRoute.Actual;
                    if ((currentRoute != null) && (currentRoute.Route.To == this))
                    {
                        if (loc.AutomaticState.Actual == AutoLocState.EnteringDestination)
                        {
                            // Loc is entering this block
                            return Core.State.BlockState.Entering;
                        }
                        // Loc is on route to this block
                        return Core.State.BlockState.Destination;
                    }
                    return Core.State.BlockState.Locked;
                }

                // Closed?
                if (closed.Actual)
                {
                    return Core.State.BlockState.Closed;
                }

                // Not locked
                if (Sensors.Any(x => x.Active.Actual))
                {
                    return Core.State.BlockState.OccupiedUnexpected;
                }
                return Core.State.BlockState.Free;
            }
        }

        /// <summary>
        /// Called when this object is unlocked.
        /// </summary>
        protected override void AfterUnlock()
        {
            base.AfterUnlock();
            UpdateClosed();
        }

        /// <summary>
        /// Requested value of Closed property changed.
        /// </summary>
        private void UpdateClosed()
        {
            if ((!closed.IsConsistent) && (!this.IsLocked()))
            {
                closed.Actual = closed.Requested;
            }
        }

        /// <summary>
        /// Save the value of Closed property in state persistence.
        /// </summary>
        private void SaveClosed()
        {
            if (statePersistence != null)
            {
                statePersistence.SetBlockState(RailwayState, this, closed.Actual);
            }
        }

        /// <summary>
        /// Prepare this state for use in a live railway. 
        /// Make sure all relevant connections to other state objects are resolved.
        /// </summary>
        /// <returns>True if the entity is now ready for use in a live railway, false otherwise.</returns>
        protected override bool TryPrepareForUse(IStateUserInterface ui, IStatePersistence statePersistence)
        {
            // Load state
            this.statePersistence = statePersistence;
            bool closed;
            if (statePersistence.TryGetBlockState(RailwayState, this, out closed))
            {
                Closed.Requested = closed;
            }
            junctions.Clear();
            var myJunctionEntities = Entity.Module.Junctions.Where(x => x.Block == Entity);          
            junctions.AddRange(myJunctionEntities.Select(x => RailwayState.JunctionStates[x]));

            var groupEntity = Entity.BlockGroup;
            blockGroup = (groupEntity != null) ? RailwayState.BlockGroupStates[groupEntity] : null;

            return true;
        }

        /// <summary>
        /// Wrap up the preparation fase.
        /// Actions here can only be performed when all routes have been prepared.
        /// </summary>
        internal void FinalizePrepare()
        {
            if (IsReadyForUse)
            {
                // Determine dead end
                var hasRoutesToBack = RailwayState.RouteStates.Any(x => (x.ToBlockSide == BlockSide.Back) && (x.To == this));
                var hasRoutesToFront = RailwayState.RouteStates.Any(x => (x.ToBlockSide == BlockSide.Front) && (x.To == this));
                var hasRoutesFromBack = RailwayState.RouteStates.Any(x => (x.FromBlockSide == BlockSide.Back) && (x.From == this));
                var hasRoutesFromFront = RailwayState.RouteStates.Any(x => (x.FromBlockSide == BlockSide.Front) && (x.From == this));
                deadEnd =
                    (hasRoutesToBack && !hasRoutesFromFront) ||
                    (hasRoutesToFront && !hasRoutesFromBack);
            }
        }

        /// <summary>
        /// Gets all entities that must be locked in order to lock me.
        /// </summary>
        protected override IEnumerable<ILockableStateImpl> UnderlyingLockableEntities
        {
            get { return junctions.Cast<ILockableStateImpl>(); }
        }
    }
}

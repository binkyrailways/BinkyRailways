using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using BinkyRailways.Core.ComponentModel;
using BinkyRailways.Core.Model;
using BinkyRailways.Core.Util;

namespace BinkyRailways.Core.State.Impl
{
    /// <summary>
    /// State of a single route.
    /// </summary>
    public sealed partial class RouteState : LockableState<IRoute>, IRouteState
    {
        private readonly IRoute[] routes;
        private IBlockState from;
        private IBlockState to;
        private readonly List<JunctionWithState> crossingJunctions = new List<JunctionWithState>();
        private readonly List<RouteEventState> events = new List<RouteEventState>();
        private readonly LocPredicateState permissions;
        private bool hasNonStraightSwitches;
        private CriticalSectionRoutes criticalSectionRoutes;
        private bool closed;
        private readonly ActionTriggerState enteringDestinationTrigger;
        private readonly ActionTriggerState destinationReachedTrigger;

        /// <summary>
        /// Default ctor
        /// </summary>
        public RouteState(IRoute route, RailwayState railwayState)
            : this(new[] { route }, railwayState)
        {
        }

        /// <summary>
        /// Default ctor
        /// </summary>
        public RouteState(IRoute[] routes, RailwayState railwayState)
            : base(routes[0], railwayState)
        {
            this.routes = routes;
            permissions = new LocRoutePermissionsState(routes, railwayState);
            closed = routes.Any(x => x.Closed);
            events = routes.Last().Events.Select(x => new RouteEventState(x, railwayState)).ToList();
            enteringDestinationTrigger = new ActionTriggerState(routes[routes.Length - 1].EnteringDestinationTrigger, railwayState);
            destinationReachedTrigger = new ActionTriggerState(routes[routes.Length - 1].DestinationReachedTrigger, railwayState);
        }

        /// <summary>
        /// Gets the description of the entity.
        /// </summary>
        [DisplayName(@"Description")]
        public override string Description { get { return From + " -> " + To; } }

        /// <summary>
        /// Speed of locs when going this route.
        /// This value is a percentage of the maximum / medium speed of the loc.
        /// </summary>
        /// <value>0..100</value>
        [DisplayName(@"Speed")]
        public int Speed { get { return routes.Min(x => x.Speed); } }

        /// <summary>
        /// Probability (in percentage) that a loc will take this route.
        /// When multiple routes are available to choose from the route with the highest probability will have the highest
        /// chance or being chosen.
        /// </summary>
        /// <value>0..100</value>
        [DisplayName(@"Choose probability")]
        public int ChooseProbability { get { return routes.Min(x => x.ChooseProbability); } }

        /// <summary>
        /// Gets the source block.
        /// </summary>
        [DisplayName(@"From")]
        public IBlockState From { get { return from; } }

        /// <summary>
        /// Side of the <see cref="From"/> block at which this route will leave that block.
        /// </summary>
        [DisplayName(@"From side")]
        public BlockSide FromBlockSide { get { return routes[0].FromBlockSide; } }

        /// <summary>
        /// Gets the destination block.
        /// </summary>
        [DisplayName(@"To")]
        public IBlockState To { get { return to; } }

        /// <summary>
        /// Side of the <see cref="To"/> block at which this route will leave that block.
        /// </summary>
        [DisplayName(@"To side")]
        public BlockSide ToBlockSide { get { return routes[routes.Length - 1].ToBlockSide; } }

        /// <summary>
        /// Does this route require any switches to be in the non-straight state?
        /// </summary>
        [DisplayName(@"Has non-straight switches")]
        public bool HasNonStraightSwitches { get { return hasNonStraightSwitches; } }

        /// <summary>
        /// Is the given sensor listed as one of the "entering destination" sensors of this route?
        /// </summary>
        public bool IsEnteringDestinationSensor(ISensorState sensor, ILocState loc)
        {
            return events.Any(x => (x.Sensor == sensor) && x.Behaviors.Any(b => b.AppliesTo(loc) && (b.StateBehavior == RouteStateBehavior.Enter)));
        }

        /// <summary>
        /// Is the given sensor listed as one of the "entering destination" sensors of this route?
        /// </summary>
        public bool IsReachedDestinationSensor(ISensorState sensor, ILocState loc)
        {
            return events.Any(x => (x.Sensor == sensor) && x.Behaviors.Any(b => b.AppliesTo(loc) && (b.StateBehavior == RouteStateBehavior.Reached)));
        }

        /// <summary>
        /// Fire the actions attached to the entering destination trigger.
        /// </summary>
        public void FireEnteringDestinationTrigger(ILocState loc)
        {
            if (!enteringDestinationTrigger.IsEmpty)
            {
                RailwayState.Dispatcher.PostAction(() => enteringDestinationTrigger.Execute(new ActionContext(loc)));
            }
        }

        /// <summary>
        /// Fire the actions attached to the destination reached trigger.
        /// </summary>
        public void FireDestinationReachedTrigger(ILocState loc)
        {
            if (!destinationReachedTrigger.IsEmpty)
            {
                RailwayState.Dispatcher.PostAction(() => destinationReachedTrigger.Execute(new ActionContext(loc)));
            }
        }

        /// <summary>
        /// Gets all events configured for this route.
        /// </summary>
        public IEnumerable<IRouteEventState> Events
        {
            get { return events; }
        }

        /// <summary>
        /// Does this route contains the given block (either as from, to or crossing)
        /// </summary>
        public bool Contains(IBlockState blockState)
        {
            return (from == blockState) || (to == blockState);
        }

        /// <summary>
        /// Does this route contains the given sensor (either as entering or reached)
        /// </summary>
        public bool Contains(ISensorState sensorState)
        {
            return events.Any(x => x.Sensor == sensorState);
        }

        /// <summary>
        /// Does this route contains the given junction
        /// </summary>
        public bool Contains(IJunctionState junctionState)
        {
            return crossingJunctions.Any(x => x.Contains(junctionState));
        }

        /// <summary>
        /// Gets all sensors that are listed as entering/reached sensor of this route.
        /// </summary>
        [DisplayName(@"Sensors")]
        public IEnumerable<ISensorState> Sensors
        {
            get { return events.Select(x => x.Sensor); }
        }

        /// <summary>
        /// All routes that must be free before this route can be taken.
        /// </summary>
        [TypeConverter(typeof(ToStringTypeConverter))]
        [DisplayName(@"Critical section")]
        public ICriticalSectionRoutes CriticalSection { get { return criticalSectionRoutes; } }

        /// <summary>
        /// Gets the predicate used to decide which locs are allowed to use this route.
        /// </summary>
        [DisplayName(@"Permissions")]
        public ILocPredicateState Permissions { get { return permissions; } }

        /// <summary>
        /// Is this route open for traffic or not?
        /// Setting to true, allows for maintance etc. on this route.
        /// </summary>
        [DisplayName(@"Closed")]
        public bool Closed { get { return closed; } }

        /// <summary>
        /// Maximum time in seconds that this route should take.
        /// If a loc takes this route and exceeds this duration, a warning is given.
        /// </summary>
        [DisplayName(@"Max. duration")]
        public int MaxDuration { get { return routes.Min(x => x.MaxDuration); } }

        /// <summary>
        /// Prepare all junctions in this route, such that it can be taken.
        /// </summary>
        public void Prepare()
        {
            foreach (var item in crossingJunctions)
            {
                item.Prepare();
            }
        }

        /// <summary>
        /// Are all junctions set in the state required by this route?
        /// </summary>
        [DisplayName(@"Is prepared")]
        public bool IsPrepared
        {
            get { return crossingJunctions.All(x => x.IsPrepared); }
        }

        /// <summary>
        /// Create a specific route state for the given loc.
        /// </summary>
        public IRouteStateForLoc CreateStateForLoc(ILocState loc)
        {
            return new RouteStateForLoc(loc, this);
        }

        /// <summary>
        /// Can the given underlying entity be locked by the intended owner?
        /// </summary>
        protected override bool CanLockUnderlyingEntity(ILockableState entity, ILocState owner, out ILocState lockedBy)
        {
            if (base.CanLockUnderlyingEntity(entity, owner, out lockedBy))
            {
                return true;
            }
            return ((entity == From) && entity.IsLockedBy(owner));
        }

        /// <summary>
        /// Prepare this state for use in a live railway. 
        /// Make sure all relevant connections to other state objects are resolved.
        /// </summary>
        /// <returns>True if the entity is now ready for use in a live railway, false otherwise.</returns>
        protected override bool TryPrepareForUse(IStateUserInterface ui, IStatePersistence statePersistence)
        {
            var fromBlock = routes[0].From as IBlock;
            if (fromBlock == null)
                return false;
            from = RailwayState.BlockStates[fromBlock];
            var lastRoute = routes[routes.Length - 1];
            var toBlock = lastRoute.To as IBlock;
            if (toBlock == null)
                return false;
            to = RailwayState.BlockStates[toBlock];
            destinationReachedTrigger.PrepareForUse(ui, statePersistence);

            foreach (var route in routes)
            {
                foreach (var item in route.CrossingJunctions)
                {
                    var junctionState = RailwayState.JunctionStates[item.Junction];
                    var state = item.Accept(Default<JunctionWithStateBuilder>.Instance, junctionState);
                    if (state == null)
                        return false;
                    crossingJunctions.Add(state);
                    hasNonStraightSwitches |= state.IsNonStraight;
                }
            }

            events.ForEach(x => x.PrepareForUse(ui, statePersistence));
            permissions.PrepareForUse(ui, statePersistence);


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
                // Calculate criticical sections
                criticalSectionRoutes = new CriticalSectionBuilder(this, RailwayState).Build();                
            }
        }

        /// <summary>
        /// Gets all entities that must be locked in order to lock me.
        /// </summary>
        protected override IEnumerable<ILockableState> UnderlyingLockableEntities
        {
            get
            {
                yield return From;
                yield return To;
                foreach (var y in crossingJunctions.SelectMany(x => x.UnderlyingLockableEntities))
                {
                    yield return y;
                }
            }
        }

        /// <summary>
        /// Is this equal to other?
        /// </summary>
        public override bool Equals(object other)
        {
            return Equals(other as RouteState);
        }

        /// <summary>
        /// Is this equal to other?
        /// </summary>
        public bool Equals(RouteState other)
        {
            return (other != null) &&
                   (other.from == from) &&
                   (other.to == to) &&
                   (other.FromBlockSide == FromBlockSide) &&
                   (other.ToBlockSide == ToBlockSide);
        }

        /// <summary>
        /// Called when an entity in the railway model has changed.
        /// </summary>
        protected override void OnModelChanged()
        {
            base.OnModelChanged();
            closed = routes.Any(x => x.Closed);
        }

        public class EqualityComparer : IEqualityComparer<RouteState>
        {
            /// <summary>
            /// Determines whether the specified objects are equal.
            /// </summary>
            /// <returns>
            /// true if the specified objects are equal; otherwise, false.
            /// </returns>
            /// <param name="x">The first object of type <paramref name="T"/> to compare.</param><param name="y">The second object of type <paramref name="T"/> to compare.</param>
            public bool Equals(RouteState x, RouteState y)
            {
                return x.Equals(y);
            }

            /// <summary>
            /// Returns a hash code for the specified object.
            /// </summary>
            public int GetHashCode(RouteState obj)
            {
                return obj.Entity.GetHashCode();
            }
        }
    }
}

using BinkyRailways.Core.Model;

namespace BinkyRailways.Core.State.Impl
{
    /// <summary>
    /// State of a single route event behavior.
    /// </summary>
    public sealed class RouteEventBehaviorState : EntityState<IRouteEventBehavior>, IRouteEventBehaviorState
    {
        private readonly LocPredicateState appliesTo;

        /// <summary>
        /// Default ctor
        /// </summary>
        public RouteEventBehaviorState(IRouteEventBehavior behavior, RailwayState railwayState)
            : base(behavior, railwayState)
        {
            appliesTo = new LocPredicateState(behavior.AppliesTo, railwayState);
        }

        /// <summary>
        /// Does this behavior apply to the given loc?
        /// </summary>
        public bool AppliesTo(ILocState loc)
        {
            return appliesTo.Evaluate(loc);
        }

        /// <summary>
        /// How is the state of the route changed.
        /// </summary>
        public RouteStateBehavior StateBehavior
        {
            get { return Entity.StateBehavior; }
        }

        /// <summary>
        /// How is the speed of the occupying loc changed.
        /// </summary>
        public LocSpeedBehavior SpeedBehavior
        {
            get { return Entity.SpeedBehavior; }
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
    }
}

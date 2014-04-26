using System.Collections.Generic;
using System.Linq;
using BinkyRailways.Core.Model;

namespace BinkyRailways.Core.State.Impl
{
    /// <summary>
    /// State of a single route event.
    /// </summary>
    public sealed class RouteEventState : EntityState<IRouteEvent>, IRouteEventState
    {
        private ISensorState sensor;
        private readonly List<RouteEventBehaviorState> behaviors;

        /// <summary>
        /// Default ctor
        /// </summary>
        public RouteEventState(IRouteEvent @event, RailwayState railwayState)
            : base(@event, railwayState)
        {
            behaviors = @event.Behaviors.Select(x => new RouteEventBehaviorState(x, railwayState)).ToList();
        }

        /// <summary>
        /// Gets the source block.
        /// </summary>
        public ISensorState Sensor
        {
            get { return sensor; }
        }

        /// <summary>
        /// Gets all sensors that are listed as entering/reached sensor of this route.
        /// </summary>
        public IEnumerable<IRouteEventBehaviorState> Behaviors
        {
            get { return behaviors; }
        }

        /// <summary>
        /// Prepare this state for use in a live railway. 
        /// Make sure all relevant connections to other state objects are resolved.
        /// </summary>
        /// <returns>True if the entity is now ready for use in a live railway, false otherwise.</returns>
        protected override bool TryPrepareForUse(IStateUserInterface ui, IStatePersistence statePersistence)
        {
            sensor = RailwayState.SensorStates[Entity.Sensor];
            return (sensor != null);
        }
    }
}

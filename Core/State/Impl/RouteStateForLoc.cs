using System.Collections.Generic;
using System.Linq;

namespace BinkyRailways.Core.State.Impl
{
    /// <summary>
    /// State of a single route for a specific loc.
    /// </summary>
    internal sealed class RouteStateForLoc : IRouteStateForLoc
    {
        private readonly ILocState loc;
        private readonly IRouteState route;
        private readonly Dictionary<ISensorState, IRouteEventBehaviorState> behaviors = new Dictionary<ISensorState, IRouteEventBehaviorState>();

        /// <summary>
        /// Default ctor
        /// </summary>
        internal RouteStateForLoc(ILocState loc, IRouteState route)
        {
            this.loc = loc;
            this.route = route;

            foreach (var @event in route.Events)
            {
                var behavior = @event.Behaviors.FirstOrDefault(x => x.AppliesTo(loc));
                if (behavior != null) 
                {
                    behaviors[@event.Sensor] = behavior;
                }
            }
        }

        /// <summary>
        /// Gets the loc for which this route state is
        /// </summary>
        public ILocState Loc { get { return loc; } }

        /// <summary>
        /// Gets the underlying route state
        /// </summary>
        public IRouteState Route { get { return route; } }

        /// <summary>
        /// Try to get the behavior for the given sensor.
        /// </summary>
        public bool TryGetBehavior(ISensorState sensor, out IRouteEventBehaviorState behavior)
        {
            if (behaviors.TryGetValue(sensor, out behavior))
            {
                behaviors.Remove(sensor); // Avoid listening to the sensor twice
                return true;
            }
            return false;
        }

        /// <summary>
        /// Does this route contain an event for the given sensor for my loc?
        /// </summary>
        public bool Contains(ISensorState sensor)
        {
            return behaviors.ContainsKey(sensor);
        }
    }
}

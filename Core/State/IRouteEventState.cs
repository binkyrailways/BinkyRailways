using System.Collections.Generic;
using BinkyRailways.Core.Model;

namespace BinkyRailways.Core.State
{
    /// <summary>
    /// State of a single route event.
    /// </summary>
    public interface IRouteEventState : IEntityState<IRouteEvent>
    {
        /// <summary>
        /// Gets the source block.
        /// </summary>
        ISensorState Sensor { get; }

        /// <summary>
        /// Gets all sensors that are listed as entering/reached sensor of this route.
        /// </summary>
        IEnumerable<IRouteEventBehaviorState> Behaviors { get; }
    }
}

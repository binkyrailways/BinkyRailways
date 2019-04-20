using System.Collections.Generic;

namespace BinkyRailways.Core.State
{
    /// <summary>
    /// Set of routes that cannot be must be free when a route is taken.
    /// </summary>
    public interface ICriticalSectionRoutes : IEnumerable<IRouteState>
    {
        /// <summary>
        /// Are all routes not being used by any other loc than the given loc?
        /// </summary>
        bool AllFree(ILocState loc);

        /// <summary>
        /// Is the critical section empty?
        /// </summary>
        bool IsEmpty { get; }
    }
}

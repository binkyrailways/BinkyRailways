using System.Collections.Generic;

namespace BinkyRailways.Core.Model
{
    /// <summary>
    /// Set of route events.
    /// </summary>
    public interface IRouteEventSet : IEnumerable<IRouteEvent>
    {
        /// <summary>
        /// Gets the number of elements
        /// </summary>
        int Count { get; }

        /// <summary>
        /// Add the given item to this set
        /// </summary>
        IRouteEvent Add(ISensor sensor);

        /// <summary>
        /// Remove the given item from this set.
        /// </summary>
        /// <returns>True if it was removed, false otherwise</returns>
        bool Remove(IRouteEvent item);

        /// <summary>
        /// Remove all
        /// </summary>
        void Clear();
    }
}

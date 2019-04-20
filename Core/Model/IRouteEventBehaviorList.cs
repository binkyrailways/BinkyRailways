using System.Collections.Generic;

namespace BinkyRailways.Core.Model
{
    /// <summary>
    /// List of route event behaviors.
    /// </summary>
    public interface IRouteEventBehaviorList : IEnumerable<IRouteEventBehavior>
    {
        /// <summary>
        /// Gets the number of elements
        /// </summary>
        int Count { get; }

        /// <summary>
        /// Add the given item to this set
        /// </summary>
        IRouteEventBehavior Add();

        /// <summary>
        /// Add the given item to this set
        /// </summary>
        IRouteEventBehavior Add(ILocPredicate appliesTo);

        /// <summary>
        /// Remove the given item from this set.
        /// </summary>
        /// <returns>True if it was removed, false otherwise</returns>
        bool Remove(IRouteEventBehavior item);

        /// <summary>
        /// Remove all
        /// </summary>
        void Clear();
    }
}

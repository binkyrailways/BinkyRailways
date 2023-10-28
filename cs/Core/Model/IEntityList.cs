using System.Collections.Generic;

namespace BinkyRailways.Core.Model
{
    /// <summary>
    /// Ordered list of entities
    /// </summary>
    public interface IEntityList<T> : IEnumerable<T>
        where T : IEntity
    {
        /// <summary>
        /// Gets the number of elements
        /// </summary>
        int Count { get; }

        /// <summary>
        /// Gets an element at the given index.
        /// </summary>
        T this[int index] { get; }

        /// <summary>
        /// Remove the given item from this list.
        /// </summary>
        /// <returns>True if it was removed, false otherwise</returns>
        bool Remove(T item);

        /// <summary>
        /// Move the given item to the given new index.
        /// </summary>
        void MoveTo(T item, int index);

        /// <summary>
        /// Remove all items
        /// </summary>
        void Clear();
    }
}

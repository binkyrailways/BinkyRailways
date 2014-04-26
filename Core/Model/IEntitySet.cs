using System.Collections.Generic;

namespace BinkyRailways.Core.Model
{
    /// <summary>
    /// Set of entities.
    /// Each element may only occur once (if it occurs)
    /// </summary>
    public interface IEntitySet<T> : IEnumerable<T>
        where T : IEntity
    {
        /// <summary>
        /// Gets the number of elements
        /// </summary>
        int Count { get; }

        /// <summary>
        /// Remove the given item from this set.
        /// </summary>
        /// <returns>True if it was removed, false otherwise</returns>
        bool Remove(T item);

        /// <summary>
        /// Does this set contain the given item?
        /// </summary>
        bool Contains(T item);

        /// <summary>
        /// Does this set contain an item with the given id?
        /// </summary>
        bool ContainsId(string id);
    }

    /// <summary>
    /// Set of entities with an AddNew method.
    /// Each element may only occur once (if it occurs)
    /// </summary>
    public interface IEntitySet2<T> : IEntitySet<T>
        where T : IEntity
    {
        /// <summary>
        /// Add a new item to this set
        /// </summary>
        T AddNew();
    }

    /// <summary>
    /// Set of entities with an Add(item) method.
    /// Each element may only occur once (if it occurs)
    /// </summary>
    public interface IEntitySet3<T> : IEntitySet<T>
        where T : IEntity
    {
        /// <summary>
        /// Add the given item to this set
        /// </summary>
        void Add(T item);

        /// <summary>
        /// Copy all entries into the given destination.
        /// </summary>
        void CopyTo(IEntitySet3<T> destination);
    }
}

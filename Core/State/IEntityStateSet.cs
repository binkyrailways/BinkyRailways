using System.Collections.Generic;
using System.ComponentModel;
using BinkyRailways.Core.Model;

namespace BinkyRailways.Core.State
{
    /// <summary>
    /// Set of state objects for a specific type of entities.
    /// Each element may only occur once (if it occurs)
    /// </summary>
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public interface IEntityStateSet<TState, TEntity> : IEnumerable<TState>
        where TState : IEntityState<TEntity>
        where TEntity : IEntity
    {
        /// <summary>
        /// Gets the number of elements
        /// </summary>
        int Count { get; }

        /// <summary>
        /// Gets the state for the given entity
        /// </summary>
        TState this[TEntity entity] { get; }
    }
}

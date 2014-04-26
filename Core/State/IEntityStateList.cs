using System.Collections.Generic;
using System.ComponentModel;
using BinkyRailways.Core.Model;

namespace BinkyRailways.Core.State
{
    /// <summary>
    /// List of state objects for a specific type of entities.
    /// </summary>
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public interface IEntityStateList<TState, TEntity> : IEnumerable<TState>
        where TState : IEntityState<TEntity>
        where TEntity : IEntity
    {
        /// <summary>
        /// Gets the number of elements
        /// </summary>
        int Count { get; }
    }
}

using System;
using System.ComponentModel;
using BinkyRailways.Core.Model;

namespace BinkyRailways.Core.State
{
    /// <summary>
    /// State of a single entity.
    /// </summary>
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public interface IEntityState : IDisposable
    {
        /// <summary>
        /// A requested value of a property of this state object has changed.
        /// </summary>
        event EventHandler RequestedStateChanged;

        /// <summary>
        /// An actual value of a property of this state object has changed.
        /// </summary>
        event EventHandler ActualStateChanged;

        /// <summary>
        /// Underlying entity (if any)
        /// </summary>
        IEntity Entity { get; }

        /// <summary>
        /// Unique ID of the underlying entity (if any)
        /// </summary>
        string EntityId { get; }

        /// <summary>
        /// Gets the description of the entity.
        /// </summary>
        string Description { get; }

        /// <summary>
        /// Gets the railway state this object is a part of.
        /// </summary>
        [Browsable(false)]
        IRailwayState RailwayState { get; }

        /// <summary>
        /// Is this entity fully resolved such that is can be used in the live railway?
        /// </summary>
        bool IsReadyForUse { get; }
    }

    /// <summary>
    /// State of a single block.
    /// </summary>
    public interface IEntityState<T> : IEntityState 
        where T : IEntity
    {
    }
}

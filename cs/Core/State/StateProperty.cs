using System;
using System.ComponentModel;

namespace BinkyRailways.Core.State
{
    /// <summary>
    /// Value of a property in a state object.
    /// The value contains a requested value and an actual value.
    /// </summary>
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public interface IStateProperty<T> : IActualStateProperty<T>
    {
        /// <summary>
        /// Fired when the requested value has changed.
        /// </summary>
        event EventHandler RequestedChanged;

        /// <summary>
        /// Gets / sets the requested value
        /// </summary>
        T Requested { get; set; }

        /// <summary>
        /// Is the request value equal to the actual value?
        /// </summary>
        bool IsConsistent { get; }
    }
}

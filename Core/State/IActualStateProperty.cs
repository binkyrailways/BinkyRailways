using System;
using System.ComponentModel;

namespace BinkyRailways.Core.State
{
    /// <summary>
    /// Value of a property in a state object.
    /// The value contains an actual value.
    /// </summary>
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public interface IActualStateProperty<T>
    {
        /// <summary>
        /// Fired when the actual value has changed.
        /// </summary>
        event EventHandler ActualChanged;

        /// <summary>
        /// Gets / sets the actual value
        /// </summary>
        [ReadOnly(true)]
        T Actual { get; set; }
    }
}

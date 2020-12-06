using System.Collections.Generic;

namespace BinkyRailways.Core.State
{
    /// <summary>
    /// An order sequence of routes.
    /// </summary>
    public interface IRouteSequence : IEnumerable<IRouteState>
    {
        /// <summary>
        /// Gets the number of routes in this sequence.
        /// </summary>
        int Length { get; }

        /// <summary>
        /// Gets the first route in this sequence.
        /// Returns null if the sequence it empty.
        /// </summary>
        IRouteState First { get; }

        /// <summary>
        /// Gets the last route in this sequence.
        /// Returns null if the sequence it empty.
        /// </summary>
        IRouteState Last { get; }
    }
}

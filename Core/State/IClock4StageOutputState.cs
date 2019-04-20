using System.Collections.Generic;
using BinkyRailways.Core.Model;

namespace BinkyRailways.Core.State
{
    /// <summary>
    /// State of a single 4-stage clock output.
    /// </summary>
    public interface IClock4StageOutputState : IOutputState, IAddressEntityState
    {
        /// <summary>
        /// Addresses of first clock bits.
        /// Lowest bit comes first.
        /// This is an output signal.
        /// </summary>
        IEnumerable<Address> Addresses { get; }

        /// <summary>
        /// Gets the current clock period
        /// </summary>
        IStateProperty<Clock4Stage> Period { get; }

        /// <summary>
        /// Gets the current pattern
        /// </summary>
        IStateProperty<int> Pattern { get; }
    }
}

using System.Collections.Generic;
using BinkyRailways.Core.Model;

namespace BinkyRailways.Core.State
{
    /// <summary>
    /// State of a single turntable.
    /// </summary>
    public interface ITurnTableState : IJunctionState
    {
        /// <summary>
        /// Addresses of first position bits.
        /// Lowest bit comes first.
        /// This is an output signal.
        /// </summary>
        IEnumerable<Address> PositionAddresses { get; }

        /// <summary>
        /// If set, the straight/off commands used for position addresses are inverted.
        /// </summary>
        bool InvertPositions { get; }

        /// <summary>
        /// Address of the line used to indicate a "write address".
        /// This is an output signal.
        /// </summary>
        Address WriteAddress { get; }

        /// <summary>
        /// If set, the straight/off command used for "write address" line is inverted.
        /// </summary>
        bool InvertWrite { get; }

        /// <summary>
        /// Gets the busy input state.
        /// </summary>
        IInputState Busy { get; }

        /// <summary>
        /// First position number. Typically 1.
        /// </summary>
        int FirstPosition { get; }

        /// <summary>
        /// Last position number. Typically 63.
        /// </summary>
        int LastPosition { get; }

        /// <summary>
        /// Position number used to initialize the turntable with?
        /// </summary>
        int InitialPosition { get; }

        /// <summary>
        /// Position of the turntable.
        /// </summary>
        IStateProperty<int> Position { get; }
    }
}

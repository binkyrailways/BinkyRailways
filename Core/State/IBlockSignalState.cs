using System.Collections.Generic;
using BinkyRailways.Core.Model;

namespace BinkyRailways.Core.State
{
    /// <summary>
    /// State of a single block signal.
    /// </summary>
    public interface IBlockSignalState : ISignalState
    {
        /// <summary>
        /// Addresses used by the signal (0..4)
        /// Lowest bit comes first.
        /// This is an output signal.
        /// </summary>
        IEnumerable<Address> Addresses { get; }

        /// <summary>
        /// The block this signal covers
        /// </summary>
        IBlockState Block { get; }

        /// <summary>
        /// Type of signal
        /// </summary>
        BlockSignalType Type { get; }

        /// <summary>
        /// Which side of the block if the signal facing.
        /// </summary>
        BlockSide Position { get; }

        /// <summary>
        /// Color of the signal.
        /// </summary>
        IStateProperty<BlockSignalColor> Color { get; }

        /// <summary>
        /// Gets the next color that is supported by my entity.
        /// </summary>
        BlockSignalColor GetNextColor(BlockSignalColor current);
    }
}

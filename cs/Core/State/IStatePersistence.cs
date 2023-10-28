using BinkyRailways.Core.Model;

namespace BinkyRailways.Core.State
{
    /// <summary>
    /// Interface used to persist some pieces of state.
    /// </summary>
    public interface IStatePersistence
    {
        /// <summary>
        /// Save the open/closed state of the given block.
        /// </summary>
        void SetBlockState(IRailwayState railwayState, IBlockState block, bool closed);

        /// <summary>
        /// Load the open/closed state of the given block.
        /// </summary>
        /// <returns>True if the state was loaded properly.</returns>
        bool TryGetBlockState(IRailwayState railwayState, IBlockState block, out bool closed);

        /// <summary>
        /// Save the current block state of the given loc.
        /// </summary>
        void SetLocState(IRailwayState railwayState, ILocState loc, IBlockState currentBlock, BlockSide currentBlockEnterSide, LocDirection currentDirection);

        /// <summary>
        /// Load the current block state of the given loc.
        /// </summary>
        /// <returns>True if the state was loaded properly.</returns>
        bool TryGetLocState(IRailwayState railwayState, ILocState loc, out IBlockState currentBlock, out BlockSide currentBlockEnterSide, out LocDirection currentDirection);
    }
}

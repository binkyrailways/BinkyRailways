using System.Collections.Generic;
using BinkyRailways.Core.Model;

namespace BinkyRailways.Core.State
{
    /// <summary>
    /// State of a single block group.
    /// </summary>
    public interface IBlockGroupState : IEntityState<IBlockGroup>
    {
        /// <summary>
        /// Gets all blocks in this group.
        /// </summary>
        IEnumerable<IBlockState> Blocks { get; }
    }
}

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

        /// <summary>
        /// The minimum number of locs that must be present in this group.
        /// Locs cannot leave if that results in a lower number of locs in this group.
        /// </summary>
        int MinimumLocsInGroup { get; }

        /// <summary>
        /// Is the condition met to require the minimum number of locs in this group?
        /// </summary>
        bool MinimumLocsInGroupEnabled { get; }

        /// <summary>
        /// Are there enough locs in this group so that one lock can leave?
        /// </summary>
        bool FirstLocCanLeave { get; }
    }
}

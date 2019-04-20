namespace BinkyRailways.Core.Model
{
    /// <summary>
    /// Group of blocks that share a similar function.
    /// </summary>
    public interface IBlockGroup : IModuleEntity
    {
        /// <summary>
        /// The minimum number of locs that must be present in this group.
        /// Locs cannot leave if that results in a lower number of locs in this group.
        /// </summary>
        int MinimumLocsInGroup { get; set; }

        /// <summary>
        /// The minimum number of locs that must be on the track before the <see cref="MinimumLocsInGroup"/> becomes active.
        /// </summary>
        int MinimumLocsOnTrackForMinimumLocsInGroupStart { get; set; }
    }
}

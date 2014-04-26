namespace BinkyRailways.Core.State
{
    internal enum InitializationPriority
    {
        /// <summary>
        /// Always initialize locs first
        /// </summary>
        Loc,

        /// <summary>
        /// Initialize junctions later
        /// </summary>
        Junction,

        /// <summary>
        /// Initialize signals even later
        /// </summary>
        Signal
    }
}

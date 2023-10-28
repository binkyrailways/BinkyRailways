namespace BinkyRailways.Core.Model
{
    public enum StationMode
    {
        /// <summary>
        /// Automatically decide if a block is a station
        /// </summary>
        Auto,

        /// <summary>
        /// A block is always a station
        /// </summary>
        Always,

        /// <summary>
        /// A block is never a station
        /// </summary>
        Never
    }
}

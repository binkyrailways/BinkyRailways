namespace BinkyRailways.Core.State
{
    public enum BlockSignalColor
    {
        /// <summary>
        /// Stop
        /// </summary>
        Red,

        /// <summary>
        /// Passing permitted, no limitations
        /// </summary>
        Green,

        /// <summary>
        /// Reduce velocity, next signal can be red.
        /// </summary>
        Yellow,

        /// <summary>
        /// Reduce velocity, thrown switches in route.
        /// </summary>
        White
    }
}

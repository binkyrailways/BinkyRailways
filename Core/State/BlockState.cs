namespace BinkyRailways.Core.State
{
    public enum BlockState
    {
        /// <summary>
        /// Block is available, but not occupied, not claimed
        /// </summary>
        Free,

        /// <summary>
        /// Block is occupied by a loc, which is expected
        /// </summary>
        Occupied,

        /// <summary>
        /// A sensor of this block is active, which is unexpected
        /// </summary>
        OccupiedUnexpected,

        /// <summary>
        /// Block is locked by a coming loc.
        /// That loc is now taking a route that leads to this block.
        /// </summary>
        Destination,

        /// <summary>
        /// Block is locked by a coming loc.
        /// That loc is now entering this block.
        /// </summary>
        Entering,

        /// <summary>
        /// Block is locked by a coming loc.
        /// That loc's next route will lead to this block.
        /// </summary>
        Locked,

        /// <summary>
        /// Block has been taken out of use
        /// </summary>
        Closed
    }
}

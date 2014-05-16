namespace BinkyRailways.Core.Model
{
    /// <summary>
    /// Place in the railway which is occupied by a single train and where a train can stop.
    /// </summary>
    public interface IBlock : IEndPoint 
    {
        /// <summary>
        /// Probability (in percentage) that a loc that is allowed to wait in this block
        /// will actually wait.
        /// When set to 0, no locs will wait (unless there is no route available).
        /// When set to 100, all locs (that are allowed) will wait.
        /// </summary>
        int WaitProbability { get; set; }

        /// <summary>
        /// Minimum amount of time to wait (if <see cref="WaitProbability"/> is set) in seconds.
        /// </summary>
        int MinimumWaitTime { get; set; }

        /// <summary>
        /// Maximum amount of time to wait (if <see cref="WaitProbability"/> is set) in seconds.
        /// </summary>
        int MaximumWaitTime { get; set; }

        /// <summary>
        /// Gets the predicate used to decide which locs are allowed to wait in this block.
        /// </summary>
        ILocStandardPredicate WaitPermissions { get; }

        /// <summary>
        /// By default the front of the block is on the right of the block.
        /// When this property is set, that is reversed to the left of the block.
        /// Setting this property will only alter the display behavior of the block.
        /// </summary>
        bool ReverseSides { get; set; }

        /// <summary>
        /// Is it allowed for locs to change direction in this block?
        /// </summary>
        ChangeDirection ChangeDirection { get; set; }

        /// <summary>
        /// Must reversing locs change direction (back to normal) in this block?
        /// </summary>
        bool ChangeDirectionReversingLocs { get; set; }

        /// <summary>
        /// Determines how the system decides if this block is part of a station
        /// </summary>
        StationMode StationMode { get; set; }

        /// <summary>
        /// Is this block considered a station?
        /// </summary>
        bool IsStation { get; }

        /// <summary>
        /// The block group that this block belongs to (if any).
        /// </summary>
        IBlockGroup BlockGroup { get; set; }
    }
}

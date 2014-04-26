using System.Collections.Generic;
using BinkyRailways.Core.Model;

namespace BinkyRailways.Core.State
{
    /// <summary>
    /// State of a single block.
    /// </summary>
    public interface IBlockState : IEntityState<IBlock>, ILockableState
    {
        /// <summary>
        /// Probability (in percentage) that a loc that is allowed to wait in this block
        /// will actually wait.
        /// When set to 0, no locs will wait (unless there is no route available).
        /// When set to 100, all locs (that are allowed) will wait.
        /// </summary>
        int WaitProbability { get; }

        /// <summary>
        /// Minimum amount of time to wait (if <see cref="WaitProbability"/> is set) in seconds.
        /// </summary>
        int MinimumWaitTime { get; }

        /// <summary>
        /// Maximum amount of time to wait (if <see cref="WaitProbability"/> is set) in seconds.
        /// </summary>
        int MaximumWaitTime { get; }

        /// <summary>
        /// Gets the predicate used to decide which locs are allowed to wait in this block.
        /// </summary>
        ILocPredicateState WaitPermissions { get; }

        /// <summary>
        /// By default the front of the block is on the right of the block.
        /// When this property is set, that is reversed to the left of the block.
        /// Setting this property will only alter the display behavior of the block.
        /// </summary>
        bool ReverseSides { get; }

        /// <summary>
        /// Is it allowed for locs to change direction in this block?
        /// </summary>
        ChangeDirection ChangeDirection { get; }

        /// <summary>
        /// Must reversing locs change direction (back to normal) in this block?
        /// </summary>
        bool ChangeDirectionReversingLocs { get; }

        /// <summary>
        /// Gets all sensors that are either an "entering" or a "reached" sensor for a route
        /// that leads to this block.
        /// </summary>
        IEnumerable<ISensorState> Sensors { get; }

        /// <summary>
        /// Gets the current state of this block
        /// </summary>
        BlockState State { get; }

        /// <summary>
        /// Is this block closed for traffic?
        /// </summary>
        IStateProperty<bool> Closed { get; }

        /// <summary>
        /// Can a loc only leave this block at the same side it got in?
        /// </summary>
        bool IsDeadEnd { get; }
    }
}

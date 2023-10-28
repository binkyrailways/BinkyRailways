using System;
using System.Collections.Generic;
using System.IO;
using BinkyRailways.Core.Model;

namespace BinkyRailways.Core.State
{
    /// <summary>
    /// State of a single locomotive.
    /// </summary>
    public interface ILocState : IEntityState<ILoc>
    {
        /// <summary>
        /// All settings of this loc will be reset, because the loc is taken of the track.
        /// </summary>
        event EventHandler BeforeReset;

        /// <summary>
        /// All settings of this loc have been reset, because the loc is taken of the track.
        /// </summary>
        event EventHandler AfterReset;

        /// <summary>
        /// Address of the entity
        /// </summary>
        Address Address { get; }

        /// <summary>
        /// Percentage of speed steps for the slowest speed of this loc.
        /// Value between 1 and 100.
        /// </summary>
        int SlowSpeed { get; }

        /// <summary>
        /// Percentage of speed steps for the medium speed of this loc.
        /// Value between 1 and 100.
        /// </summary>
        int MediumSpeed { get; }

        /// <summary>
        /// Percentage of speed steps for the maximum speed of this loc.
        /// Value between 1 and 100.
        /// </summary>
        int MaximumSpeed { get; }

        /// <summary>
        /// Is this loc controlled by the automatic loc controller?
        /// </summary>
        IStateProperty<bool> ControlledAutomatically { get; }

        /// <summary>
        /// Gets the number of speed steps supported by this loc.
        /// </summary>
        int SpeedSteps { get; }

        /// <summary>
        /// Gets/sets the image of the given loc.
        /// </summary>
        /// <value>Null if there is no image.</value>
        /// <remarks>Image must be png, bmp, gif, jpg, wmf or emf</remarks>
        Stream Image { get; }

        /// <summary>
        /// Gets the name of the given function.
        /// </summary>
        string GetFunctionName(LocFunction function, out bool isCustom);

        /// <summary>
        /// Is it allowed for this loc to change direction?
        /// </summary>
        ChangeDirection ChangeDirection { get; }

        /// <summary>
        /// Gets the name of the person that owns this loc.
        /// </summary>
        string Owner { get; }

        /// <summary>
        /// Is it allowed to set the ControlledAutoatically property to true?
        /// </summary>
        bool CanSetAutomaticControl { get; }

        /// <summary>
        /// The current state of this loc in the automatic loc controller.
        /// </summary>
        IActualStateProperty<AutoLocState> AutomaticState { get; }

        /// <summary>
        /// Gets the route that this loc is currently taking.
        /// Do not assign this property directly, instead use the assign methods.
        /// </summary>
        IActualStateProperty<IRouteStateForLoc> CurrentRoute { get; }

        /// <summary>
        /// Should the loc wait when the current route has finished?
        /// </summary>
        IActualStateProperty<bool> WaitAfterCurrentRoute { get; }

        /// <summary>
        /// Time when this loc will exceed the maximum duration of the current route.
        /// </summary>
        IActualStateProperty<DateTime> DurationExceedsCurrentRouteTime { get; }

        /// <summary>
        /// Is the maximum duration of the current route this loc is taken exceeded?
        /// </summary>
        bool IsCurrentRouteDurationExceeded { get; }

        /// <summary>
        /// Gets the route that this loc will take when the current route has finished.
        /// This property is only set by the automatic loc controller.
        /// </summary>
        IActualStateProperty<IRouteState> NextRoute { get; }

        /// <summary>
        /// Gets the block that the loc is currently in.
        /// </summary>
        IActualStateProperty<IBlockState> CurrentBlock { get; }

        /// <summary>
        /// Gets the side at which the current block was entered.
        /// </summary>
        IActualStateProperty<BlockSide> CurrentBlockEnterSide { get; }

        /// <summary>
        /// Time when this loc will start it's next route.
        /// </summary>
        IActualStateProperty<DateTime> StartNextRouteTime { get; }

        /// <summary>
        /// Route options as considered last by the automatic train controller.
        /// </summary>
        IActualStateProperty<IRouteOption[]> LastRouteOptions { get; }

        /// <summary>
        /// Possible deadlock detected set by the automatic loc controller.
        /// </summary>
        IActualStateProperty<bool> PossibleDeadlock { get; }

        /// <summary>
        /// Gets/sets a selector used to select the next route from a list of possible routes.
        /// If no route selector is set, a default will be created.
        /// </summary>
        IRouteSelector RouteSelector { get; set; }

        /// <summary>
        /// Current speed of this loc as percentage of the speed steps of the loc.
        /// Value between 0 and 100.
        /// Setting this value will result in a request to its command station to alter the speed.
        /// </summary>
        IStateProperty<int> Speed { get; }

        /// <summary>
        /// Gets a human readable representation of the speed of the loc.
        /// </summary>
        string GetSpeedText();

        /// <summary>
        /// Gets a human readable representation of the state of the loc.
        /// </summary>
        string GetStateText();

        /// <summary>
        /// Gets the actual speed of the loc in speed steps
        /// Value between 0 and the maximum number of speed steps supported by this loc.
        /// Setting this value will result in a request to its command station to alter the speed.
        /// </summary>
        IStateProperty<int> SpeedInSteps { get; }

        /// <summary>
        /// Current direction of this loc.
        /// Setting this value will result in a request to its command station to alter the direction.
        /// </summary>
        IStateProperty<LocDirection> Direction { get; }

        /// <summary>
        /// Is this loc reversing out of a dead end?
        /// This can only be true for locs that are not allowed to change direction.
        /// </summary>
        IActualStateProperty<bool> Reversing { get; }

        /// <summary>
        /// Directional lighting of the loc.
        /// Setting this value will result in a request to its command station to alter the value.
        /// </summary>
        IStateProperty<bool> F0 { get; }

        /// <summary>
        /// Return the state of a function.
        /// </summary>
        /// <returns>True if such a state exists, false otherwise</returns>
        bool TryGetFunctionState(LocFunction function, out IStateProperty<bool> state);

        /// <summary>
        /// Return all functions that have state.
        /// </summary>
        IEnumerable<LocFunction> Functions { get; }

        /// <summary>
        /// Try to assign the given loc to the given block.
        /// Assigning is only possible when the loc is not controlled automatically and
        /// the block can be assigned by the given loc.
        /// If the loc is already assigned to another block, this assignment is removed
        /// and the block on that block is unlocked.
        /// </summary>
        /// <param name="block">The new block to assign to. If null, the loc will only be unassigned from the current block.</param>
        /// <param name="currentBlockEnterSide">The site to which the block is entered (invert of facing)</param>
        /// <returns>True on success, false otherwise</returns>
        bool AssignTo(IBlockState block, BlockSide currentBlockEnterSide);

        /// <summary>
        /// Gets command station specific (advanced) info for this loc.
        /// </summary>
        string CommandStationInfo { get; }

        /// <summary>
        /// Forcefully reset of settings of this loc.
        /// This should be used when a loc is taken of the track.
        /// </summary>
        void Reset();

        /// <summary>
        /// Save the current state to the state persistence.
        /// </summary>
        void PersistState();

        /// <summary>
        /// Gets zero or more blocks that were recently visited by this loc.
        /// The first block was last visited.
        /// </summary>
        IEnumerable<IBlockState> RecentlyVisitedBlocks { get; }

        /// <summary>
        /// Behavior of the last event triggered by this loc.
        /// </summary>
        IRouteEventBehaviorState LastEventBehavior { get; set; }

        /// <summary>
        /// Is the speed behavior of the last event set to default?
        /// </summary>
        bool IsLastEventBehaviorSpeedDefault { get; }
    }
}

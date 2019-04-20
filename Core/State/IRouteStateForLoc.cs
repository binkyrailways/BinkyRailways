namespace BinkyRailways.Core.State
{
    /// <summary>
    /// State of a single route for a specific loc.
    /// </summary>
    public interface IRouteStateForLoc 
    {
        /// <summary>
        /// Gets the loc for which this route state is
        /// </summary>
        ILocState Loc { get; }

        /// <summary>
        /// Gets the underlying route state
        /// </summary>
        IRouteState Route { get; }

        /// <summary>
        /// Try to get the behavior for the given sensor.
        /// </summary>
        bool TryGetBehavior(ISensorState sensor, out IRouteEventBehaviorState behavior);

        /// <summary>
        /// Does this route contain an event for the given sensor for my loc?
        /// </summary>
        bool Contains(ISensorState sensor);
    }
}

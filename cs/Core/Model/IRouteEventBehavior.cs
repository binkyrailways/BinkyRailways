namespace BinkyRailways.Core.Model
{
    /// <summary>
    /// Possible behavior of a route event.
    /// </summary>
    public interface IRouteEventBehavior : IModuleEntity
    {
        /// <summary>
        /// Predicate used to select the locs to which this event applies.
        /// </summary>
        ILocPredicate AppliesTo { get; }

        /// <summary>
        /// How is the state of the route changed.
        /// </summary>
        RouteStateBehavior StateBehavior { get; set; }

        /// <summary>
        /// How is the speed of the occupying loc changed.
        /// </summary>
        LocSpeedBehavior SpeedBehavior { get; set; }
    }
}

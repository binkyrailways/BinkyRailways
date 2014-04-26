namespace BinkyRailways.Core.Model
{
    /// <summary>
    /// Sensor event in a route.
    /// </summary>
    public interface IRouteEvent : IModuleEntity
    {
        /// <summary>
        /// Sensor that triggers this event
        /// </summary>
        ISensor Sensor { get; }

        /// <summary>
        /// Gets the list of behaviors to choose from.
        /// The first matching behavior is used.
        /// </summary>
        IRouteEventBehaviorList Behaviors { get; }
    }
}

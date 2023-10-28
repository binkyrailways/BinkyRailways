namespace BinkyRailways.Core.Model
{
    /// <summary>
    /// Set of sensors used as enter/reached set in a route.
    /// </summary>
    public interface IRouteSensorSet : IEntitySet3<ISensor>
    {
        /// <summary>
        /// Gets the containing route.
        /// </summary>
        IRoute Route { get; }

        /// <summary>
        /// Set type
        /// </summary>
        RouteSensorSetType Type { get; }
    }
}

namespace BinkyRailways.Core.Model.Impl
{
    /// <summary>
    /// Unbreakable part of an entire railway.
    /// </summary>
    partial class Route
    {
        /// <summary>
        /// Set of of sensors in the reached destination set.
        /// </summary>
        public sealed class ReachedDestinationSensorSet : SensorRefSet
        {
            private readonly Route route;

            /// <summary>
            /// Default ctor
            /// </summary>
            internal ReachedDestinationSensorSet(Route route)
                : base(route, RouteSensorSetType.Reached, route)
            {
                this.route = route;
            }

            /// <summary>
            /// The given item has been added to this set.
            /// </summary>
            protected override void OnAdded(Sensor item)
            {
                route.EnteringDestinationSensors.Remove(item);
            }
        }
    }
}

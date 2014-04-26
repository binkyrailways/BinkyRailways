namespace BinkyRailways.Core.Model.Impl
{
    /// <summary>
    /// Unbreakable part of an entire railway.
    /// </summary>
    partial class Route
    {
        /// <summary>
        /// Set of of sensors in the entering destination set.
        /// </summary>
        public sealed class EnteringDestinationSensorSet : SensorRefSet
        {
            private readonly Route route;

            /// <summary>
            /// Default ctor
            /// </summary>
            internal EnteringDestinationSensorSet(Route route)
                : base(route, RouteSensorSetType.Entering, route)
            {
                this.route = route;
            }

            /// <summary>
            /// The given item has been added to this set.
            /// </summary>
            protected override void OnAdded(Sensor item)
            {
                route.ReachedDestinationSensors.Remove(item);
            }
        }
    }
}

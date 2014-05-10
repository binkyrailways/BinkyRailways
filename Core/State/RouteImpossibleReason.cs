namespace BinkyRailways.Core.State
{
    /// <summary>
    /// Reasons why a route is not possible
    /// </summary>
    public enum RouteImpossibleReason
    {
        /// <summary>
        /// Route is possible
        /// </summary>
        None,

        /// <summary>
        /// Route is locked by another loc
        /// </summary>
        Locked,

        /// <summary>
        /// Route is closed
        /// </summary>
        Closed,

        /// <summary>
        /// Destination block is closed
        /// </summary>
        DestinationClosed,

        /// <summary>
        /// A sensor in the route is active
        /// </summary>
        SensorActive,

        /// <summary>
        /// There is opposing traffic in future routes
        /// </summary>
        OpposingTraffic,

        /// <summary>
        /// A change in direction of the loc is needed for this block
        /// </summary>
        DirectionChangeNeeded,

        /// <summary>
        /// Loc has no permission for this route
        /// </summary>
        NoPermission,

        /// <summary>
        /// Critical section is occupied
        /// </summary>
        CriticalSectionOccupied,

        /// <summary>
        /// Future deadlock
        /// </summary>
        DeadLock
    }
}

namespace BinkyRailways.Core.State
{
    /// <summary>
    /// Possible route of a loc, which a possible flag and description why it is not possible (if applicable).
    /// </summary>
    public interface IRouteOption
    {
        /// <summary>
        /// Gets the considered route
        /// </summary>
        IRouteState Route { get; }

        /// <summary>
        /// Is this a possibility
        /// </summary>
        bool IsPossible { get; }

        /// <summary>
        /// Reason why this route is not possible.
        /// </summary>
        RouteImpossibleReason Reason { get; }

        /// <summary>
        /// Gets a human readable description of the reason.
        /// </summary>
        string ReasonDescription { get; }
    }
}

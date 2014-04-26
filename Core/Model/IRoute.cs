namespace BinkyRailways.Core.Model
{
    /// <summary>
    /// Route from one block to another.
    /// </summary>
    public interface IRoute : IModuleEntity, IActionTriggerSource
    {
        /// <summary>
        /// Starting point of the route
        /// </summary>
        IEndPoint From { get; set; }

        /// <summary>
        /// Side of the <see cref="From"/> block at which this route will leave that block.
        /// </summary>
        BlockSide FromBlockSide { get; set; }

        /// <summary>
        /// End point of the route
        /// </summary>
        IEndPoint To { get; set; }

        /// <summary>
        /// Side of the <see cref="To"/> block at which this route will enter that block.
        /// </summary>
        BlockSide ToBlockSide { get; set; }

        /// <summary>
        /// Set of junctions with their states that are crossed when taking this route.
        /// </summary>
        IJunctionWithStateSet CrossingJunctions { get; }

        /// <summary>
        /// Set of events that change the state of the route and it's running loc.
        /// </summary>
        IRouteEventSet Events { get; }

        /// <summary>
        /// Speed of locs when going this route.
        /// This value is a percentage of the maximum / medium speed of the loc.
        /// </summary>
        /// <value>0..100</value>
        int Speed { get; set; }

        /// <summary>
        /// Probability (in percentage) that a loc will take this route.
        /// When multiple routes are available to choose from the route with the highest probability will have the highest
        /// chance or being chosen.
        /// </summary>
        /// <value>0..100</value>
        int ChooseProbability { get; set; }

        /// <summary>
        /// Gets the predicate used to decide which locs are allowed to use this route.
        /// </summary>
        ILocStandardPredicate Permissions { get; }

        /// <summary>
        /// Is this route open for traffic or not?
        /// Setting to true, allows for maintance etc. on this route.
        /// </summary>
        bool Closed { get; set; }

        /// <summary>
        /// Maximum time in seconds that this route should take.
        /// If a loc takes this route and exceeds this duration, a warning is given.
        /// </summary>
        int MaxDuration { get; set; }

        /// <summary>
        /// Trigger fired when a loc has starts entering the destination of this route.
        /// </summary>
        IActionTrigger EnteringDestinationTrigger { get; }

        /// <summary>
        /// Trigger fired when a loc has reached the destination of this route.
        /// </summary>
        IActionTrigger DestinationReachedTrigger { get; }
    }
}

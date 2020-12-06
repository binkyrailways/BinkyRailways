using System.Collections.Generic;
using BinkyRailways.Core.Model;

namespace BinkyRailways.Core.State
{
    /// <summary>
    /// State of a single route.
    /// </summary>
    public interface IRouteState : IEntityState<IRoute>, ILockableState
    {
        /// <summary>
        /// Speed of locs when going this route.
        /// This value is a percentage of the maximum / medium speed of the loc.
        /// </summary>
        /// <value>0..100</value>
        int Speed { get; }

        /// <summary>
        /// Probability (in percentage) that a loc will take this route.
        /// When multiple routes are available to choose from the route with the highest probability will have the highest
        /// chance or being chosen.
        /// </summary>
        /// <value>0..100</value>
        int ChooseProbability { get; }

        /// <summary>
        /// Gets the source block.
        /// </summary>
        IBlockState From { get; }

        /// <summary>
        /// Side of the <see cref="From"/> block at which this route will leave that block.
        /// </summary>
        BlockSide FromBlockSide { get; }

        /// <summary>
        /// Gets the destination block.
        /// </summary>
        IBlockState To { get; }

        /// <summary>
        /// Side of the <see cref="To"/> block at which this route will enter that block.
        /// </summary>
        BlockSide ToBlockSide { get; }

        /// <summary>
        /// Does this route require any switches to be in the non-straight state?
        /// </summary>
        bool HasNonStraightSwitches { get; }

        /// <summary>
        /// Is the given sensor listed as one of the "entering destination" sensors of this route?
        /// </summary>
        bool IsEnteringDestinationSensor(ISensorState sensor, ILocState loc);

        /// <summary>
        /// Is the given sensor listed as one of the "entering destination" sensors of this route?
        /// </summary>
        bool IsReachedDestinationSensor(ISensorState sensor, ILocState loc);

        /// <summary>
        /// Does this route contains the given block (either as from, to or crossing)
        /// </summary>
        bool Contains(IBlockState blockState);

        /// <summary>
        /// Does this route contains the given sensor (either as entering or reached)
        /// </summary>
        bool Contains(ISensorState sensorState);

        /// <summary>
        /// Does this route contains the given junction
        /// </summary>
        bool Contains(IJunctionState junctionState);

        /// <summary>
        /// Gets all sensors that are listed as entering/reached sensor of this route.
        /// </summary>
        IEnumerable<ISensorState> Sensors { get; }

        /// <summary>
        /// All routes that must be free before this route can be taken.
        /// </summary>
        ICriticalSectionRoutes CriticalSection { get; }

        /// <summary>
        /// Gets all events configured for this route.
        /// </summary>
        IEnumerable<IRouteEventState> Events { get; }

        /// <summary>
        /// Gets the predicate used to decide which locs are allowed to use this route.
        /// </summary>
        ILocPredicateState Permissions { get; }

        /// <summary>
        /// Is this route open for traffic or not?
        /// Setting to true, allows for maintance etc. on this route.
        /// </summary>
        bool Closed { get; }

        /// <summary>
        /// Maximum time in seconds that this route should take.
        /// If a loc takes this route and exceeds this duration, a warning is given.
        /// </summary>
        int MaxDuration { get; }

        /// <summary>
        /// Prepare all junctions in this route, such that it can be taken.
        /// </summary>
        void Prepare();

        /// <summary>
        /// Are all junctions set in the state required by this route?
        /// </summary>
        bool IsPrepared { get; }

        /// <summary>
        /// Create a specific route state for the given loc.
        /// </summary>
        IRouteStateForLoc CreateStateForLoc(ILocState loc);

        /// <summary>
        /// Fire the actions attached to the entering destination trigger.
        /// </summary>
        void FireEnteringDestinationTrigger(ILocState loc);

        /// <summary>
        /// Fire the actions attached to the destination reached trigger.
        /// </summary>
        void FireDestinationReachedTrigger(ILocState loc);
    }
}

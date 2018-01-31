using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using BinkyRailways.Core.Model;
using BinkyRailways.Core.State.Impl;

namespace BinkyRailways.Core.State.Automatic
{
    /// <summary>
    /// Route availability tester for the current state of the railway.
    /// </summary>
    internal sealed class LiveRouteAvailabilityTester : RouteAvailabilityTester
    {
        /// <summary>
        /// Default ctor
        /// </summary>
        internal LiveRouteAvailabilityTester(IRailwayState railwayState)
            : base(railwayState)
        {
        }

        /// <summary>
        /// Can the given route be taken by the given loc?
        /// </summary>
        /// <param name="route">The route being investigated</param>
        /// <param name="loc">The loc a route should be choosen for</param>
        /// <param name="locDirection">The direction the loc is facing in the From block of the given <see cref="route"/>.</param>
        /// <param name="avoidDirectionChanges">If true, the route is considered not available if a direction change is needed.</param>
        /// <returns>True if the route can be locked and no sensor in the route is active (outside current route).</returns>
        public override IRouteOption IsAvailableFor(IRouteState route, ILocState loc, Model.BlockSide locDirection, bool avoidDirectionChanges, bool lastResort)
        {
            // Perform standard testing first
            var result = base.IsAvailableFor(route, loc, locDirection, avoidDirectionChanges, lastResort);
            if (!result.IsPossible)
            {
                return result;
            }

            // If last resort, we're ok when critical section is empty.
            if (lastResort)
            {
                if (route.CriticalSection.IsEmpty || route.CriticalSection.AllFree(loc))
                {
                    return result;
                }
            }

            // Now test future possibilities of the entire railway to detect possible deadlocks.
            var genSet = new FutureAlternativeSet(this, route, loc);
            if (!genSet.Test())
            {
                // Not Ok
                return new RouteOption(route, RouteImpossibleReason.DeadLock);
            }
            return result;
        }

        /// <summary>
        /// Set of alternatives.
        /// At least one generation must still be alive after X alternatives.
        /// </summary>
        private class FutureAlternativeSet
        {
            private readonly List<FutureAlternative> alternatives = new List<FutureAlternative>();
            private readonly int minGenerations;
            private readonly List<ILocState> autoLocs;
            private readonly ILocState testLoc;

            /// <summary>
            /// Default ctor
            /// </summary>
            public FutureAlternativeSet(LiveRouteAvailabilityTester live, IRouteState route, ILocState loc)
            {
                this.testLoc = loc;
                minGenerations = live.railwayState.BlockStates.Count;
                var tester = new FutureRouteAvailabilityTester(live.railwayState);
                tester.TakeRoute(route, loc);
                alternatives.Add(new FutureAlternative(tester, 0));
                autoLocs = live.railwayState.LocStates.Where(x => x.ControlledAutomatically.Actual && (x.CurrentBlock.Actual != null)).ToList();
            }

            /// <summary>
            /// Add the given generation to the end of the list.
            /// </summary>
            public void Add(FutureAlternative g)
            {
                alternatives.Add(g);
            }

            /// <summary>
            /// Run the future generation test.
            /// </summary>
            /// <returns>True if there is a dead-lock free future or the test loc has reached a station.</returns>
            public bool Test()
            {
                while (alternatives.Count > 0)
                {
                    var g = alternatives[0];
                    if (g.Generation >= minGenerations)
                    {
                        // We've found a dead-lock free alternative.
                        return true;
                    }
                    var removed = false;
                    while ((g.Generation < minGenerations) && (!removed))
                    {
                        bool stationReached;
                        var anyLocMoved = g.Increment(this, autoLocs, testLoc, out stationReached);
                        if (stationReached)
                        {
                            // We're done
                            return true;
                        }
                        if (!anyLocMoved)
                        {
                            // This generation has a deadlock.
                            alternatives.RemoveAt(0);
                            removed = true;
                        }
                    }
                }
                // Are there alternatives left?
                if (alternatives.Count == 0)
                {
                    // Oops, deadlock
                    return false;
                }
                // There are deadlock free alternatives
                return true;
            }

            public override string ToString()
            {
                return string.Format("{0} alternatives", alternatives.Count);
            }
        }

        /// <summary>
        /// Represent a possible future state.
        /// </summary>
        private class FutureAlternative
        {
            private readonly FutureRouteAvailabilityTester state;
            private int generation;

            /// <summary>
            /// Default ctor
            /// </summary>
            public FutureAlternative(FutureRouteAvailabilityTester state, int generation)
            {
                if (state == null)
                    throw new ArgumentNullException("state");
                this.state = state;
                this.generation = generation;
            }

            public int Generation
            {
                get { return generation; }
            }

            /// <summary>
            /// Increment the generation and move all locs.
            /// </summary>
            /// <returns>True if at least one loc has moved, false otherwise.</returns>
            public bool Increment(FutureAlternativeSet genSet, IEnumerable<ILocState> locs, ILocState testLoc, out bool stationReached)
            {
                generation++;
                stationReached = false;

                // Try to move the test loc first
                var moved = MoveLoc(genSet, testLoc);
                // Are we in a block that is considered a station for the loc?
                var block = state.GetCurrentBlock(testLoc);
                if ((block != null) && (block.IsStationFor(testLoc)))
                {
                    // Yes, the loc may stop here
                    stationReached = true;
                    return true;
                }
                if (moved)
                {
                    // We've moved so still no deadlock
                    return true;
                }
                // Try one of the other locs
                foreach (var loc in locs.Where(x => x != testLoc))
                {
                    moved = MoveLoc(genSet, loc);
                    if (moved)
                    {
                        // Something moved
                        return true;
                    }
                }
                return false;
            }

            /// <summary>
            /// Try to move the given loc to a new block.
            /// </summary>
            /// <returns>True if the loc has moved, false if there are no routes available.</returns>
            private bool MoveLoc(FutureAlternativeSet genSet, ILocState loc)
            {
                // Get possible routes
                var currentBlock = state.GetCurrentBlock(loc);
                var currentBlockEnterSide = state.GetCurrentBlockEnterSide(loc);
                var locDirection = currentBlockEnterSide.Invert();
                var avoidDirectionChanges = (loc.ChangeDirection == ChangeDirection.Avoid);
                var routeFromFromBlock = state.RailwayState.GetAllPossibleNonClosedRoutesFromBlock(currentBlock);
                var routeOptions = routeFromFromBlock.Select(x => state.IsAvailableFor(x, loc, locDirection, avoidDirectionChanges, false)).ToList();
                var possibleRoutes = routeOptions.Where(x => x.IsPossible).Select(x => x.Route).ToList();

                // If no routes, then return
                if (!possibleRoutes.Any()) return false;
                if (possibleRoutes.Count > 1)
                {
                    // Create alternate generations
                    foreach (var route in possibleRoutes.Skip(1))
                    {
                        var alternateState = new FutureRouteAvailabilityTester(state);
                        alternateState.TakeRoute(route, loc);
                        genSet.Add(new FutureAlternative(alternateState, generation));
                    }
                }
                // Take the first route
                var firstRoute = possibleRoutes[0];
                state.TakeRoute(firstRoute, loc);
                return true;
            }

            public override string ToString()
            {
                return string.Format("#{0} -> {1}", generation, state.ToString());
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using BinkyRailways.Core.Model;

namespace BinkyRailways.Core.State.Automatic
{
    internal static class Extensions
    {
        /// <summary>
        /// Is is needed for a loc facing in the given direction (in the From block of the given route)
        /// to chance direction when taking this route?
        /// </summary>
        /// <param name="route">The route being investigated</param>
        /// <param name="locDirection">The direction the loc is facing in the From block of the given <see cref="route"/>.</param>
        internal static bool IsDirectionChangeNeeded(this IRouteState route, BlockSide locDirection)
        {
            var fromSide = route.FromBlockSide;
            return (fromSide != locDirection);
        }

        /// <summary>
        /// Can the given route be taken by the given loc?
        /// </summary>
        /// <param name="route">The route being investigated</param>
        /// <param name="railwayState">State of the entire railway</param>
        /// <param name="loc">The loc a route should be choosen for</param>
        /// <param name="locDirection">The direction the loc is facing in the From block of the given <see cref="route"/>.</param>
        /// <param name="avoidDirectionChanges">If true, the route is considered not available if a direction change is needed.</param>
        /// <returns>True if the route can be locked and no sensor in the route is active (outside current route).</returns>
        internal static bool IsAvailableFor(this IRouteState route, IRailwayState railwayState,  ILocState loc, BlockSide locDirection, bool avoidDirectionChanges)
        {
            if (!route.CanLock(loc))
            {
                // Cannot lock
                return false;
            }

            // Route closed?
            if (route.Closed)
            {
                // Route closed
                return false;
            }

            // Target blocked closed?
            if (route.To.Closed.Actual || route.To.Closed.Requested)
            {
                // Destination closed.
                return false;
            }

            // Check opposite traffic
            if (railwayState.HasTrafficInOppositeDirection(route, loc))
            {
                // Traffic in opposite direction found
                return false;
            }

            // Check critical section of route
            if (!route.CriticalSection.AllFree(loc))
            {
                // Some route in critical section not free
                return false;
            }

            // Check direction
            if (IsDirectionChangeNeeded(route, locDirection))
            {
                if (avoidDirectionChanges)
                {
                    // Do not take this route because a direction change would be needed.
                    return false;
                }
                if (loc.ChangeDirection != ChangeDirection.Allow)
                {
                    // Loc does not allow direction changes
                    if (!route.From.IsDeadEnd)
                    {
                        return false;
                    }
                    // Loc will reverse out of a dead end
                }
                if (route.From.ChangeDirection != ChangeDirection.Allow)
                {
                    // From block does not allowed direction changes
                    return false;
                }
            }

            // Check permissions
            if (!route.Permissions.Evaluate(loc))
            {
                // Loc not allowed by permissions
                return false;
            }

            // Check sensor states
            var activeSensors = route.Sensors.Where(x => x.Active.Actual);
            var currentRoute = loc.CurrentRoute.Actual;
            if (currentRoute == null)
            {
                // There must be no active sensor
                return !activeSensors.Any();
            }

            // The loc has a current route.
            // There must not be any active sensor that is not listed in the current route.
            return !activeSensors.Any(x => !currentRoute.Route.Contains(x));
        }

        /// <summary>
        /// Gets a selection of all possible routes from the given block.
        /// </summary>
        internal static IEnumerable<IRouteState> GetAllPossibleRoutesFromBlock(this IRailwayState railwayState, IBlockState fromBlock)
        {
            return railwayState.RouteStates.Where(x => (x.From == fromBlock));
        }

        /// <summary>
        /// Gets a selection of all possible non-closed routes from the given block.
        /// </summary>
        internal static IEnumerable<IRouteState> GetAllPossibleNonClosedRoutesFromBlock(this IRailwayState railwayState, IBlockState fromBlock)
        {
            return railwayState.GetAllPossibleRoutesFromBlock(fromBlock).Where(x => !x.Closed);
        }

        /// <summary>
        /// Are there more than 1 non-closed routes from the given block.
        /// </summary>
        internal static bool HasMultipleNonClosedRoutesFromBlock(this IRailwayState railwayState, IBlockState fromBlock)
        {
            return (railwayState.GetAllPossibleNonClosedRoutesFromBlock(fromBlock).Count() > 1);
        }

        /// <summary>
        /// Is there traffic in the opposite direction of the given route?
        /// Ignore all locs equal to the given loc.
        /// </summary>
        internal static bool HasTrafficInOppositeDirection(this IRailwayState railwayState, IRouteState route, ILocState currentLoc)
        {
            var toBlock = route.To;
            var loc = toBlock.LockedBy;
            if ((loc != null) && (loc != currentLoc))
            {
                // Check current route
                var locRoute = loc.CurrentRoute.Actual;
                if ((locRoute != null) && (locRoute.Route.To == toBlock))
                {
                    // We found opposite traffic
                    if (!loc.CanChangeDirectionIn(toBlock))
                    {
                        // The loc cannot change direction in to block, so there is absolutely opposite traffic.
                        return true;
                    }
                }
                // Check next route
                var nextRoute = loc.NextRoute.Actual;
                if ((nextRoute != null) && (nextRoute.To == toBlock))
                {
                    // We found opposite traffic
                    if (!loc.CanChangeDirectionIn(toBlock))
                    {
                        // The loc cannot change direction in to block, so there is absolutely opposite traffic.
                        return true;
                    }
                }
            }

            // So far not traffic found
            // Check next routes, unless there are multiple next routes
            var nextRoutes = railwayState.GetAllPossibleNonClosedRoutesFromBlock(toBlock).ToList();
            if (nextRoutes.Count > 1)
            {
                // After the given route there are multiple alternatives.
                return false;
            } 
            if (nextRoutes.Count == 0)
            {
                // No routes after this route
                return false;
            }
            // Only 1 alternative, check that
            return railwayState.HasTrafficInOppositeDirection(nextRoutes[0], currentLoc);
        }

        /// <summary>
        /// Can the given loc change direction in the given block?
        /// </summary>
        internal static bool CanChangeDirectionIn(this ILocState loc, IBlockState block)
        {
            return (loc.ChangeDirection == ChangeDirection.Allow) && (block.ChangeDirection == ChangeDirection.Allow);
        }

        /// <summary>
        /// Take a gamble.
        /// The result is true or false, where the given chance is for "true".
        /// </summary>
        internal static bool Gamble(this Random rnd, int chance)
        {
            if ((chance < 0) || (chance > 100))
                throw new ArgumentOutOfRangeException("chance", chance, "Must be between 0..100");
            if (chance == 0)
                return false;
            if (chance == 100)
                return true;
            var value = rnd.Next(100);
            return (value <= chance);
        }

        /// <summary>
        /// Take a random choice between one of the options.
        /// Each option has a probability.
        /// </summary>
        internal static T Gamble<T>(this Random rnd, params Tuple<T, int>[] options)
        {
            var boundaries = new int[options.Length];
            var max = 0;
            for (var i = 0; i < options.Length; i++)
            {
                max += options[i].Item2 + 1;
                boundaries[i] = max;
            }

            var value = rnd.Next(max + 1);
            for (var i = 0; i < options.Length; i++)
            {
                if (value <= boundaries[i])
                    return options[i].Item1;
            }
            // We should never get here
            return options[options.Length - 1].Item1;
        }
    }
}

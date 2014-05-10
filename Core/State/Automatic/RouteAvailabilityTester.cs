﻿using System.Linq;
using BinkyRailways.Core.Model;

namespace BinkyRailways.Core.State.Automatic
{
    /// <summary>
    /// Tester for the availablity of a route for a loc.
    /// </summary>
    internal abstract class RouteAvailabilityTester
    {
        protected readonly IRailwayState railwayState;

        /// <summary>
        /// Default ctor
        /// </summary>
        protected RouteAvailabilityTester(IRailwayState railwayState)
        {
            this.railwayState = railwayState;
        }

        /// <summary>
        /// Can the given route be taken by the given loc?
        /// </summary>
        /// <param name="route">The route being investigated</param>
        /// <param name="loc">The loc a route should be choosen for</param>
        /// <param name="locDirection">The direction the loc is facing in the From block of the given <see cref="route"/>.</param>
        /// <param name="avoidDirectionChanges">If true, the route is considered not available if a direction change is needed.</param>
        /// <returns>True if the route can be locked and no sensor in the route is active (outside current route).</returns>
        public virtual bool IsAvailableFor(IRouteState route, ILocState loc, BlockSide locDirection, bool avoidDirectionChanges)
        {
            if (!CanLock(route, loc))
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
            if (HasTrafficInOppositeDirection(route, loc))
            {
                // Traffic in opposite direction found
                return false;
            }

            // Check critical section of route
            if (!IsCriticalSectionFree(route, loc))
            {
                // Some route in critical section not free
                return false;
            }

            // Check direction
            if (route.IsDirectionChangeNeeded(locDirection))
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
            if (IsAnySensorActive(route, loc))
            {
                // Route is not available
                return false;
            }

            // Route is available
            return true;
        }

        /// <summary>
        /// Can the given route be locked for the given loc?
        /// </summary>
        protected virtual bool CanLock(IRouteState route, ILocState loc)
        {
            return route.CanLock(loc);
        }

        /// <summary>
        /// Is there are traffic in the opposite direction of the given route (not including the given loc).
        /// </summary>
        protected bool HasTrafficInOppositeDirection(IRouteState route, ILocState currentLoc)
        {
            var toBlock = route.To;
            if (HasTrafficInOppositeDirection(toBlock, route.ToBlockSide, currentLoc))
            {
                return true;
            }

            // Check next routes
            var locDirection = route.ToBlockSide.Invert();
            var nextRoutes = railwayState.GetAllPossibleNonClosedRoutesFromBlock(toBlock).ToList();
            //var nextRoutes = railwayState.GetAllPossibleNonClosedRoutesFromBlock(toBlock, locDirection).ToList();

            if (nextRoutes.Count == 0)
            {
                // No next routes at all, we do no longer care about opposing traffic
                return false;
            }
            if (nextRoutes.Count > 1)
            {
                // Multiple next routes, we stop now
                return false;
            }
            // Only one route possible, check that for opposing traffic
            return HasTrafficInOppositeDirection(nextRoutes[0], currentLoc);
        }

        /// <summary>
        /// Is there are traffic in the opposite direction of the given to-block of a route?
        /// </summary>
        protected virtual bool HasTrafficInOppositeDirection(IBlockState toBlock, BlockSide toBlockSide, ILocState currentLoc)
        {
            var loc = toBlock.LockedBy;
            if ((loc != null) && (loc != currentLoc))
            {
                // Check current route
                var locRoute = loc.CurrentRoute.Actual;
                if ((locRoute != null) && (locRoute.Route.To == toBlock))
                {
                    var locEnterSide = loc.CurrentBlockEnterSide.Actual;
                    if (locEnterSide != toBlockSide)
                    {
                        // We found opposite traffic
                        if (!loc.CanChangeDirectionIn(toBlock))
                        {
                            // The loc cannot change direction in to block, so there is absolutely opposite traffic.
                            return true;
                        }
                    }
                }
                // Check next route
                var nextRoute = loc.NextRoute.Actual;
                if ((nextRoute != null) && (nextRoute.To == toBlock))
                {
                    var locEnterSide = loc.CurrentBlockEnterSide.Actual;
                    if (locEnterSide != toBlockSide)
                    {
                        // We found opposite traffic
                        if (!loc.CanChangeDirectionIn(toBlock))
                        {
                            // The loc cannot change direction in to block, so there is absolutely opposite traffic.
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Is the critical section for the given route free for the given loc?
        /// </summary>
        protected virtual bool IsCriticalSectionFree(IRouteState route, ILocState loc)
        {
            return route.CriticalSection.AllFree(loc);
        }

        /// <summary>
        /// Is any of the sensors of the given route active?
        /// Sensors that are also in the current route of the given loc are ignored.
        /// </summary>
        protected virtual bool IsAnySensorActive(IRouteState route, ILocState loc)
        {
            var activeSensors = route.Sensors.Where(x => x.Active.Actual);
            var currentRoute = loc.CurrentRoute.Actual;
            if (currentRoute == null)
            {
                // There must be no active sensor
                return activeSensors.Any();
            }

            // The loc has a current route.
            // There must not be any active sensor that is not listed in the current route.
            return activeSensors.Any(x => !currentRoute.Route.Contains(x));            
        }
    }
}

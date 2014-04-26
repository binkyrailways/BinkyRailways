using System;
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
        /// <param name="loc">The loc a route should be choosen for</param>
        /// <param name="locDirection">The direction the loc is facing in the From block of the given <see cref="route"/>.</param>
        /// <param name="avoidDirectionChanges">If true, the route is considered not available if a direction change is needed.</param>
        /// <returns>True if the route can be locked and no sensor in the route is active (outside current route).</returns>
        internal static bool IsAvailableFor(this IRouteState route, ILocState loc, BlockSide locDirection, bool avoidDirectionChanges)
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

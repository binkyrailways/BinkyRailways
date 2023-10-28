using System.Collections.Generic;
using System.Linq;
using BinkyRailways.Core.Model;
using BinkyRailways.Core.Model.Impl;

namespace BinkyRailways.Core.State.Impl
{
    /// <summary>
    /// State of an entire railway.
    /// </summary>
    partial class RailwayState 
    {
        /// <summary>
        /// Gets all resolved command stations
        /// </summary>
        private static IEnumerable<ICommandStation> ResolveCommandStations(IRailway railway, bool runInVirtualMode)
        {
            if (runInVirtualMode)
            {
                yield return new VirtualCommandStation { Description = Strings.VirtualCommandStationDescription };
            }
            else
            {
                foreach (var csRef in railway.CommandStations)
                {
                    ICommandStation cs;
                    if (csRef.TryResolve(out cs))
                        yield return cs;
                }
            }
        }

        /// <summary>
        /// Gets all resolved locomotives
        /// </summary>
        private static IEnumerable<ILoc> ResolveLocs(IRailway railway)
        {
            foreach (var locRef in railway.Locs)
            {
                ILoc loc;
                if (locRef.TryResolve(out loc))
                    yield return loc;
            }
        }

        /// <summary>
        /// Gets all resolved modules
        /// </summary>
        private static IEnumerable<IModule> ResolveModules(IRailway railway)
        {
            foreach (var moduleRef in railway.Modules)
            {
                IModule module;
                if (moduleRef.TryResolve(out module))
                    yield return module;
            }
        }

        /// <summary>
        /// Try to find all routes that span multiple modules
        /// </summary>
        private IEnumerable<IRouteState> ResolveInterModuleRoutes(IRailway railway, IEnumerable<IModule> modules)
        {
            var startRoutes = modules.SelectMany(x => x.Routes.Where(r => r.IsFromBlockToEdge())).ToList();
            var result = startRoutes.SelectMany(startRoute => ResolveRoutesStartingWith(railway, startRoute)).ToList();
            return result;
        }

        /// <summary>
        /// Try to find all routes starting with the given route.
        /// </summary>
        private IEnumerable<IRouteState> ResolveRoutesStartingWith(IRailway railway, IRoute route)
        {
            var partialRoutes = new List<PartialRoute>();
            partialRoutes.Add(new PartialRoute(route));
            var completed = new List<PartialRoute>();
            var result = new List<IRouteState>();

            while (partialRoutes.Any())
            {
                // Take the first partial route
                var partialRoute = partialRoutes[0];
                partialRoutes.RemoveAt(0);

                // Find all extended partial routes
                var nextPartialRoutes = FindNextStep(railway, partialRoute).ToList();
                foreach (var nextPartialRoute in nextPartialRoutes)
                {
                    if (nextPartialRoute.IsComplete)
                    {
                        // We've found a complete route
                        if (!completed.Contains(nextPartialRoute))
                        {
                            result.Add(nextPartialRoute.CreateRouteState(this));
                        }
                        else
                        {
                            completed.Add(nextPartialRoute);
                        }
                    }
                    else
                    {
                        // The next partial route needs "completion"
                        partialRoutes.Add(nextPartialRoute);
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// Look for all partial routes that are an extension of the initial partial route.
        /// </summary>
        private static IEnumerable<PartialRoute> FindNextStep(IRailway railway, PartialRoute initial)
        {
            var edge = FindConnectedEdge(railway, initial.LastRoute);
            if (edge == null)
                yield break;

            foreach (var route in edge.Module.Routes.Where(route => route.From == edge))
            {
                yield return new PartialRoute(initial, route);
            }
        }

        /// <summary>
        /// Lookup the edge (on the "other") module connected with the destination of the given route.
        /// </summary>
        /// <returns>Null if not found</returns>
        private static IEdge FindConnectedEdge(IRailway railway, IRoute route)
        {
            var edge = (IEdge)route.To;
            foreach (var conn in railway.ModuleConnections)
            {
                if (conn.EdgeA == edge)
                {
                    return conn.EdgeB;
                }
                if (conn.EdgeB == edge)
                {
                    return conn.EdgeA;
                }
            }
            return null;
        }
    }
}

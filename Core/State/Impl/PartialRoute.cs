using System;
using System.Collections.Generic;
using BinkyRailways.Core.Model;

namespace BinkyRailways.Core.State.Impl
{
    /// <summary>
    /// Helper class for finding inter-module routes.
    /// </summary>
    internal sealed class PartialRoute
    {
        private readonly List<IRoute> routes = new List<IRoute>();

        /// <summary>
        /// Default ctor
        /// </summary>
        public PartialRoute(IRoute startRoute)
        {
            if (!startRoute.IsFromBlockToEdge()) 
                throw new ArgumentException("StartRoute should be 'From block to Edge'");
            routes.Add(startRoute);
        }

        /// <summary>
        /// Clone ctor
        /// </summary>
        public PartialRoute(PartialRoute source, IRoute addition)
        {
            routes.AddRange(source.routes);
            routes.Add(addition);
        }

        /// <summary>
        /// Create a route state for my routes.
        /// </summary>
        public IRouteState CreateRouteState(RailwayState railwayState)
        {
            return new RouteState(routes.ToArray(), railwayState);
        }

        /// <summary>
        /// Gets the last added route
        /// </summary>
        public IRoute LastRoute
        {
            get { return routes[routes.Count - 1]; }
        }

        /// <summary>
        /// Is the destination of last route a block?
        /// </summary>
        public bool IsComplete
        {
            get { return LastRoute.IsToInternal(); }
        }

        /// <summary>
        /// Is this equal to other?
        /// </summary>
        public override bool Equals(object obj)
        {
            return Equals(obj as PartialRoute);
        }

        /// <summary>
        /// Is this equal to other?
        /// </summary>
        public bool Equals(PartialRoute other)
        {
            return (other != null) && (routes.Equals(other.routes));
        }

        /// <summary>
        /// Serves as a hash function for a particular type. 
        /// </summary>
        public override int GetHashCode()
        {
            return routes.GetHashCode();
        }

        public override string ToString()
        {
            return routes[0].From + " -> " + LastRoute.To;
        }
    }
}

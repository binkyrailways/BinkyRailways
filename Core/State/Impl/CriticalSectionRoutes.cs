using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using BinkyRailways.Core.ComponentModel;
using BinkyRailways.Core.Model;

namespace BinkyRailways.Core.State.Impl
{
    /// <summary>
    /// Set of routes that cannot be must be free when a route is taken.
    /// </summary>
    [TypeDescriptionProvider(typeof(CollectionTypeDescriptionProvider))]
    public sealed class CriticalSectionRoutes : ICriticalSectionRoutes
    {
        private readonly List<IRouteState> routes;

        /// <summary>
        /// Default ctor
        /// </summary>
        internal CriticalSectionRoutes(IEnumerable<IRouteState> routes)
        {
            this.routes = new List<IRouteState>(routes);
        }

        /// <summary>
        /// Are all routes not being used by any other loc than the given loc?
        /// </summary>
        [DisplayName(@"AllFree")]
        public bool AllFree(ILocState allowedLoc)
        {
            // Check routes
            if (routes.Any(x => x.IsLocked()))
                return false;
            // Check blocks in the routes
            foreach (var route in routes)
            {
                var toBlock = route.To;
                var loc = toBlock.LockedBy;
                if ((loc != null) && (loc != allowedLoc) && (loc.CurrentBlockEnterSide.Actual == route.ToBlockSide))
                {
                    // If the loc occupying the blokc cannot change direction, we may not enter this critical section.
                    if (loc.ChangeDirection == ChangeDirection.Avoid)
                        return false;
                    // If changing direction in the blockis not allowed, we may not enter this critical section.
                    if (toBlock.ChangeDirection == ChangeDirection.Avoid)
                        return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Collections.Generic.IEnumerator`1"/> that can be used to iterate through the collection.
        /// </returns>
        /// <filterpriority>1</filterpriority>
        public IEnumerator<IRouteState> GetEnumerator()
        {
            return routes.GetEnumerator();
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.IEnumerator"/> object that can be used to iterate through the collection.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public override string ToString()
        {
            return string.Join(", ", routes.Select(x => x.Description).ToArray());
        }
    }
}

using System.Collections.Generic;
using System.Linq;
using BinkyRailways.Core.Model;

namespace BinkyRailways.Core.State.Impl
{
    /// <summary>
    /// State for a loc predicate.
    /// </summary>
    public sealed class LocRoutePermissionsState : LocPredicateState
    {
        private readonly List<LocPredicateState> predicates;

        /// <summary>
        /// Default ctor
        /// </summary>
        internal LocRoutePermissionsState(IEnumerable<IRoute> routes, RailwayState railwayState)
            : base(null, railwayState)
        {
            predicates = routes.Where(r => !r.Permissions.IsEmpty).Select(r => new LocPredicateState(r.Permissions, railwayState)).ToList();
        }

        /// <summary>
        /// Evaluate this predicate for the given loc.
        /// </summary>
        public override bool Evaluate(ILocState loc)
        {
            return predicates.All(x => x.Evaluate(loc));
        }
    }
}

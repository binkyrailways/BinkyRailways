using BinkyRailways.Core.Model;
using BinkyRailways.Core.Util;

namespace BinkyRailways.Core.State.Impl
{
    /// <summary>
    /// State for a loc predicate.
    /// </summary>
    public class LocPredicateState : EntityState<ILocPredicate>, ILocPredicateState
    {
        /// <summary>
        /// Default ctor
        /// </summary>
        public LocPredicateState(ILocPredicate entity, RailwayState railwayState)
            : base(entity, railwayState)
        {
        }

        /// <summary>
        /// Evaluate this predicate for the given loc.
        /// </summary>
        public virtual bool Evaluate(ILocState loc)
        {
            return Entity.Accept(Default<LocPredicateEvaluator>.Instance, loc);
        }

        /// <summary>
        /// Prepare this state for use in a live railway. 
        /// Make sure all relevant connections to other state objects are resolved.
        /// </summary>
        /// <returns>True if the entity is now ready for use in a live railway, false otherwise.</returns>
        protected override bool TryPrepareForUse(IStateUserInterface ui, IStatePersistence statePersistence)
        {
            return true;
        }
    }
}

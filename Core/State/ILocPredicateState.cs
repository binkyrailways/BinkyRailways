using BinkyRailways.Core.Model;

namespace BinkyRailways.Core.State
{
    /// <summary>
    /// State for a loc predicate.
    /// </summary>
    public interface ILocPredicateState : IEntityState<ILocPredicate>
    {
        /// <summary>
        /// Evaluate this predicate for the given loc.
        /// </summary>
        bool Evaluate(ILocState loc);
    }
}

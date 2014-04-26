namespace BinkyRailways.Core.Model
{
    /// <summary>
    /// Predicate that evaluates based on zero or more predicates.
    /// </summary>
    public interface ILocPredicatesPredicate : ILocPredicate
    {
        /// <summary>
        /// Gets the set of nested predicates.
        /// </summary>
        ILocPredicateSet Predicates { get; }

        /// <summary>
        /// Is the <see cref="Predicates"/> set emtpy?
        /// </summary>
        bool IsEmpty { get; }
    }
}

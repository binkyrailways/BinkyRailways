namespace BinkyRailways.Core.Model
{
    /// <summary>
    /// Set of loc predicates
    /// </summary>
    public interface ILocPredicateSet : IEntitySet<ILocPredicate>
    {
        /// <summary>
        /// Add the given predicate to this set.
        /// </summary>
        void Add(ILocPredicate predicate);

        /// <summary>
        /// Remove all predicates
        /// </summary>
        void Clear();
    }
}

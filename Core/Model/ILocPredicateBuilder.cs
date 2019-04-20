namespace BinkyRailways.Core.Model
{
    /// <summary>
    /// Used to create predicate instances.
    /// </summary>
    public interface ILocPredicateBuilder 
    {
        /// <summary>
        /// Create an 'and' predicate
        /// </summary>
        ILocAndPredicate CreateAnd();

        /// <summary>
        /// Create an 'or' predicate
        /// </summary>
        ILocOrPredicate CreateOr();

        /// <summary>
        /// Create a 'loc equals' predicate
        /// </summary>
        ILocEqualsPredicate CreateEquals(ILoc loc);

        /// <summary>
        /// Create a 'loc group equals' predicate
        /// </summary>
        ILocGroupEqualsPredicate CreateGroupEquals(ILocGroup group);

        /// <summary>
        /// Create a 'loc is allowed to change direction' predicate
        /// </summary>
        ILocCanChangeDirectionPredicate CreateCanChangeDirection();

        /// <summary>
        /// Create a 'allowed between start and end time' predicate
        /// </summary>
        ILocTimePredicate CreateTime(Time periodStart, Time periodEnd);
    }
}

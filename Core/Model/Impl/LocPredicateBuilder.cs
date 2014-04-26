namespace BinkyRailways.Core.Model.Impl
{
    /// <summary>
    /// Set of loc predicates
    /// </summary>
    public sealed class LocPredicateBuilder : ILocPredicateBuilder
    {
        /// <summary>
        /// Create an 'and' predicate
        /// </summary>
        public ILocAndPredicate CreateAnd()
        {
            return new LocAndPredicate();
        }

        /// <summary>
        /// Create an 'or' predicate
        /// </summary>
        public ILocOrPredicate CreateOr()
        {
            return new LocOrPredicate();
        }

        /// <summary>
        /// Create a 'loc equals' predicate
        /// </summary>
        public ILocEqualsPredicate CreateEquals(ILoc loc)
        {
            ILocEqualsPredicate x = new LocEqualsPredicate();
            x.Loc = loc;
            return x;
        }

        /// <summary>
        /// Create a 'loc group equals' predicate
        /// </summary>
        public ILocGroupEqualsPredicate CreateGroupEquals(ILocGroup group)
        {
            ILocGroupEqualsPredicate x = new LocGroupEqualsPredicate();
            x.Group = group;
            return x;
        }

        /// <summary>
        /// Create a 'loc is allowed to change direction' predicate
        /// </summary>
        public ILocCanChangeDirectionPredicate CreateCanChangeDirection()
        {
            return new LocCanChangeDirectionPredicate();
        }

        /// <summary>
        /// Create a 'allowed between start and end time' predicate
        /// </summary>
        public ILocTimePredicate CreateTime(Time periodStart, Time periodEnd)
        {
            return new LocTimePredicate { PeriodStart = periodStart, PeriodEnd = periodEnd };
        }
    }
}

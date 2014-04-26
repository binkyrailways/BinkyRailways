using System.Linq;

namespace BinkyRailways.Core.Model.Impl
{
    /// <summary>
    /// Predicate that evaluates to true if all nested predicates evaluate to true.
    /// </summary>
    public sealed class LocAndPredicate : LocPredicatesPredicate, ILocAndPredicate
    {
        /// <summary>
        /// Accept a visit by the given visitor
        /// </summary>
        public override TReturn Accept<TReturn, TData>(EntityVisitor<TReturn, TData> visitor, TData data)
        {
            return visitor.Visit(this, data);
        }

        /// <summary>
        /// Create an empty instance.
        /// </summary>
        protected override LocPredicatesPredicate CreateInstance()
        {
            return new LocAndPredicate();
        }

        /// <summary>
        /// Human readable name of this type of entity.
        /// </summary>
        public override string TypeName
        {
            get { return Strings.LocAndPredicateTypeName; }
        }

        /// <summary>
        /// Convert to a string
        /// </summary>
        public override string ToString()
        {
            return "(" + string.Join(Strings.OperatorAnd, Predicates.Select(x => x.ToString()).ToArray()) + ")";
        }
    }
}

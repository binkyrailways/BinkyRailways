using System.Linq;

namespace BinkyRailways.Core.Model.Impl
{
    /// <summary>
    /// Predicate that evaluates to true if at least one of the nested predicates evaluate to true.
    /// </summary>
    public sealed class LocOrPredicate : LocPredicatesPredicate, ILocOrPredicate
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
            return new LocOrPredicate();
        }

        /// <summary>
        /// Human readable name of this type of entity.
        /// </summary>
        public override string TypeName
        {
            get { return Strings.LocOrPredicateTypeName; }
        }

        /// <summary>
        /// Convert to a string
        /// </summary>
        public override string ToString()
        {
            return "(" + string.Join(Strings.OperatorOr, Predicates.Select(x => x.ToString()).ToArray()) + ")";
        }

        /// <summary>
        /// Evaluate this predicate for the given loc.
        /// </summary>
        public override bool Evaluate(ILoc loc)
        {
            return Predicates.Any(x => x.Evaluate(loc));
        }
    }
}

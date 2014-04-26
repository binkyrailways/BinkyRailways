namespace BinkyRailways.Core.Model.Impl
{
    /// <summary>
    /// Predicate that evaluates to true the given loc is allowed to change direction.
    /// </summary>
    public sealed class LocCanChangeDirectionPredicate : LocPredicate, ILocCanChangeDirectionPredicate
    {
        /// <summary>
        /// Accept a visit by the given visitor
        /// </summary>
        public override TReturn Accept<TReturn, TData>(EntityVisitor<TReturn, TData> visitor, TData data)
        {
            return visitor.Visit(this, data);
        }

        /// <summary>
        /// Create a deep clone.
        /// </summary>
        protected internal override LocPredicate Clone()
        {
            return new LocCanChangeDirectionPredicate();
        }

        /// <summary>
        /// Human readable name of this type of entity.
        /// </summary>
        public override string TypeName
        {
            get { return Strings.LocChangeChangeDirectionPredicateTypeName; }
        }

        /// <summary>
        /// Convert to a string
        /// </summary>
        public override string ToString()
        {
            return Strings.LocCanChangeDirectionPredicateDescription;
        }
    }
}

namespace BinkyRailways.Core.Model.Impl
{
    /// <summary>
    /// Set of loc predicates
    /// </summary>
    public sealed class LocPredicateSet : EntitySet<LocPredicate, ILocPredicate>
    {
        /// <summary>
        /// Default ctor
        /// </summary>
        internal LocPredicateSet(ModuleEntity owner)
            : base(owner)
        {
        }

        /// <summary>
        /// Implementation of ILocPredicateSet 
        /// </summary>
        public new ILocPredicateSet Set
        {
            get { return (ILocPredicateSet) base.Set; }
        }

        /// <summary>
        /// Create an implementation of IEntitySet.
        /// </summary>
        protected override SetImpl CreateSetImpl()
        {
            return new SetImpl2(this);
        }

        private sealed class SetImpl2 : SetImpl, ILocPredicateSet
        {
            private readonly LocPredicateSet impl;

            /// <summary>
            /// Default ctor
            /// </summary>
            internal SetImpl2(LocPredicateSet impl) : base(impl)
            {
                this.impl = impl;
            }

            /// <summary>
            /// Remove all entries
            /// </summary>
            public void Clear()
            {
                impl.RemoveAll(x => true);
            }
        }
    }
}

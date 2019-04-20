namespace BinkyRailways.Core.Model.Impl
{
    /// <summary>
    /// Set of entities.
    /// Each element may only occur once (if it occurs)
    /// Each element is stored by it's content in XML.
    /// </summary>
    public abstract class EntitySet2<T, TIntf> : EntitySet<T, TIntf>, IEntitySet2<T>
        where T : class, TIntf, IEntityInternals, new()
        where TIntf : IEntity
    {
        /// <summary>
        /// Default ctor
        /// </summary>
        internal EntitySet2(Entity owner)
            : base(owner)
        {
        }

        /// <summary>
        /// Implementation of IEntitySet2
        /// </summary>
        public new IEntitySet2<TIntf> Set
        {
            get { return (IEntitySet2<TIntf>)base.Set; }
        }

        /// <summary>
        /// Add a new given item to this set
        /// </summary>
        public T AddNew()
        {
            var item = new T();
            Add(item);
            return item;
        }

        /// <summary>
        /// Create an implementation of IEntitySet.
        /// </summary>
        protected override SetImpl CreateSetImpl()
        {
            return new SetImpl2(this);
        }

        /// <summary>
        /// Implementation of IEntitySet
        /// </summary>
        protected class SetImpl2 : SetImpl, IEntitySet2<TIntf>
        {
            private readonly EntitySet2<T, TIntf> impl;

            /// <summary>
            /// Default ctor
            /// </summary>
            internal SetImpl2(EntitySet2<T, TIntf> impl)
                : base(impl)
            {
                this.impl = impl;
            }

            /// <summary>
            /// Add the given item to this set
            /// </summary>
            public TIntf AddNew()
            {
                return impl.AddNew();
            }
        }
    }
}

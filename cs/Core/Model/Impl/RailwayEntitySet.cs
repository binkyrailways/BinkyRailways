namespace BinkyRailways.Core.Model.Impl
{
    /// <summary>
    /// Set of module entities.
    /// </summary>
    public class RailwayEntitySet<T, TIntf, TEntity> : EntitySet2<T, TIntf>
        where T : RailwayEntity, TIntf, new()
        where TIntf : IPersistentEntityRef<TEntity>
        where TEntity : IEntity
    {
        private readonly Railway railway;

        /// <summary>
        /// Default ctor
        /// </summary>
        internal RailwayEntitySet(Railway railway)
            : base(railway)
        {
            this.railway = railway;
        }

        /// <summary>
        /// Implementation of IEntitySet2
        /// </summary>
        public new IPersistentEntityRefSet<TIntf, TEntity> Set
        {
            get { return (IPersistentEntityRefSet<TIntf, TEntity>) base.Set; }
        }

        /// <summary>
        /// Gets the containing railway.
        /// </summary>
        protected Railway Railway { get { return railway; } }

        /// <summary>
        /// The given item has been added to this set.
        /// </summary>
        protected override void OnAdded(T item)
        {
            item.Railway = railway;
        }

        /// <summary>
        /// The given item has been removed from this set.
        /// </summary>
        protected override void OnRemoved(T item)
        {
            item.Railway = null;
        }

        /// <summary>
        /// Create an implementation of IEntitySet.
        /// </summary>
        protected override SetImpl CreateSetImpl()
        {
            return new SetImpl3(this);
        }

        private sealed class SetImpl3 : SetImpl2, IPersistentEntityRefSet<TIntf, TEntity>
        {
            /// <summary>
            /// Default ctor
            /// </summary>
            internal SetImpl3(RailwayEntitySet<T, TIntf, TEntity> impl)
                : base(impl)
            {
            }

            /// <summary>
            /// Add a reference to the given entity
            /// </summary>
            public TIntf Add(TEntity entity)
            {
                var item = new T();
                item.Id = entity.Id;
                Add(item);
                return item;
            }
        }
    }
}

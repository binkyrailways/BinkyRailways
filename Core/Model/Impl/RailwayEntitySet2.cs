namespace BinkyRailways.Core.Model.Impl
{
    /// <summary>
    /// Set of railway entities.
    /// </summary>
    public class RailwayEntitySet2<T, TIntf> : EntitySet2<T, TIntf>
        where T : RailwayEntity, TIntf, new()
        where TIntf : IEntity
    {
        private readonly Railway railway;

        /// <summary>
        /// Default ctor
        /// </summary>
        internal RailwayEntitySet2(Railway railway)
            : base(railway)
        {
            this.railway = railway;
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
    }
}

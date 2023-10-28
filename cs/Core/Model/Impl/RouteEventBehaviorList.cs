namespace BinkyRailways.Core.Model.Impl
{
    /// <summary>
    /// Set of of junctions with their state.
    /// </summary>
    public sealed class RouteEventBehaviorList : EntityList<RouteEventBehavior, IRouteEventBehavior>
    {
        private readonly RouteEvent @event;

        /// <summary>
        /// Default ctor
        /// </summary>
        internal RouteEventBehaviorList(RouteEvent @event)
            : base(@event)
        {
            this.@event = @event;
        }

        /// <summary>
        /// Implementation of IJunctionWithStateSet 
        /// </summary>
        public new IRouteEventBehaviorList List
        {
            get { return (IRouteEventBehaviorList)base.List; }
        }

        /// <summary>
        /// The given item has been added to this set.
        /// </summary>
        protected override void OnAdded(RouteEventBehavior item)
        {
            item.RouteEvent = @event;
        }

        /// <summary>
        /// The given item has been removed from this set.
        /// </summary>
        protected override void OnRemoved(RouteEventBehavior item)
        {
            item.RouteEvent = null;
        }

        /// <summary>
        /// Create an implementation of IEntitySet.
        /// </summary>
        protected override ListImpl CreateListImpl()
        {
            return new ListImpl2(this);
        }

        /// <summary>
        /// Refresh the RouteEvent property of each item.
        /// </summary>
        internal void LinkItems()
        {
            foreach (var item in this)
            {
                item.RouteEvent = @event;
                item.LinkItem();
            }
        }

        private sealed class ListImpl2 : ListImpl, IRouteEventBehaviorList
        {
            private readonly RouteEventBehaviorList impl;

            /// <summary>
            /// Default ctor
            /// </summary>
            internal ListImpl2(RouteEventBehaviorList impl)
                : base(impl)
            {
                this.impl = impl;
            }

            /// <summary>
            /// Add the given item to this set
            /// </summary>
            IRouteEventBehavior IRouteEventBehaviorList.Add()
            {
                var item = new RouteEventBehavior();
                impl.Add(item);
                return item;
            }

            /// <summary>
            /// Add the given item to this set
            /// </summary>
            IRouteEventBehavior IRouteEventBehaviorList.Add(ILocPredicate appliesTo)
            {
                var item = new RouteEventBehavior { AppliesTo = (LocStandardPredicate)appliesTo };
                impl.Add(item);
                return item;
            }

            /// <summary>
            /// Remove the given item from this set.
            /// </summary>
            /// <returns>True if it was removed, false otherwise</returns>
            bool IRouteEventBehaviorList.Remove(IRouteEventBehavior item)
            {
                return impl.Remove((RouteEventBehavior) item);
            }

            /// <summary>
            /// Remove all
            /// </summary>
            void IRouteEventBehaviorList.Clear()
            {
                impl.Clear();
            }
        }
    }
}
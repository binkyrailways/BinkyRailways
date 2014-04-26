using System.Linq;

namespace BinkyRailways.Core.Model.Impl
{
    /// <summary>
    /// Unbreakable part of an entire railway.
    /// </summary>
    partial class Route
    {
        /// <summary>
        /// Set of of route events with their state.
        /// </summary>
        public sealed class EventSet : EntitySet<RouteEvent, IRouteEvent>
        {
            private readonly Route route;

            /// <summary>
            /// Default ctor
            /// </summary>
            internal EventSet(Route route)
                : base(route)
            {
                this.route = route;
            }

            /// <summary>
            /// Does this set contain an event for the given sensor?
            /// </summary>
            public bool Contains(ISensor sensor)
            {
                return this.Any(x => x.Sensor == sensor);
            }

            /// <summary>
            /// Implementation of IJunctionWithStateSet 
            /// </summary>
            public new IRouteEventSet Set
            {
                get { return (IRouteEventSet)base.Set; }
            }

            /// <summary>
            /// Create an implementation of IEntitySet.
            /// </summary>
            protected override SetImpl CreateSetImpl()
            {
                return new SetImpl2(this);
            }

            /// <summary>
            /// The given item has been added to this set.
            /// </summary>
            protected override void OnAdded(RouteEvent item)
            {
                item.Route = route;
            }

            /// <summary>
            /// The given item has been removed from this set.
            /// </summary>
            protected override void OnRemoved(RouteEvent item)
            {
                item.Route = null;
            }

            internal void LinkItems()
            {
                foreach (var @event in this)
                {
                    @event.Route = route;
                }
            }

            private sealed class SetImpl2 : SetImpl, IRouteEventSet
            {
                private readonly EventSet impl;

                /// <summary>
                /// Default ctor
                /// </summary>
                internal SetImpl2(EventSet impl)
                    : base(impl)
                {
                    this.impl = impl;
                }

                /// <summary>
                /// Add the given item to this set
                /// </summary>
                IRouteEvent IRouteEventSet.Add(ISensor sensor)
                {
                    var item = new RouteEvent { Sensor = (Sensor)sensor };
                    impl.Add(item);
                    return item;
                }

                /// <summary>
                /// Remove all
                /// </summary>
                void IRouteEventSet.Clear()
                {
                    impl.RemoveAll(x => true);
                }
            }
        }
    }
}

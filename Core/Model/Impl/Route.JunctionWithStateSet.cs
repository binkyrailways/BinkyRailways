using System.Linq;

namespace BinkyRailways.Core.Model.Impl
{
    /// <summary>
    /// Unbreakable part of an entire railway.
    /// </summary>
    partial class Route
    {
        /// <summary>
        /// Set of of junctions with their state.
        /// </summary>
        public sealed class JunctionWithStateSet : EntitySet<JunctionWithState, IJunctionWithState>
        {
            private readonly Route route;

            /// <summary>
            /// Default ctor
            /// </summary>
            internal JunctionWithStateSet(Route route)
                : base(route)
            {
                this.route = route;
            }

            /// <summary>
            /// Implementation of IJunctionWithStateSet 
            /// </summary>
            public new IJunctionWithStateSet Set
            {
                get { return (IJunctionWithStateSet) base.Set; }
            }

            /// <summary>
            /// Is the given junction contained in this set?
            /// </summary>
            public bool Contains(IJunction junction)
            {
                if (junction == null)
                    return false;
                var id = junction.Id;
                return this.Any(x => x.JunctionId == id);
            }

            /// <summary>
            /// The given item has been added to this set.
            /// </summary>
            protected override void OnAdded(JunctionWithState item)
            {
                item.Route = route;
            }

            /// <summary>
            /// The given item has been removed from this set.
            /// </summary>
            protected override void OnRemoved(JunctionWithState item)
            {
                item.Route = null;
            }

            /// <summary>
            /// Create an implementation of IEntitySet.
            /// </summary>
            protected override SetImpl CreateSetImpl()
            {
                return new SetImpl2(this);
            }

            private sealed class SetImpl2 : SetImpl, IJunctionWithStateSet
            {
                private readonly JunctionWithStateSet impl;

                /// <summary>
                /// Default ctor
                /// </summary>
                internal SetImpl2(JunctionWithStateSet impl)
                    : base(impl)
                {
                    this.impl = impl;
                }

                /// <summary>
                /// Add the given item to this set
                /// </summary>
                public void Add(IPassiveJunction item)
                {
                    Add(new PassiveJunctionWithState((PassiveJunction) item));
                }

                /// <summary>
                /// Add the given item to this set
                /// </summary>
                public void Add(ISwitch item, SwitchDirection direction)
                {
                    Add(new SwitchWithState((Switch)item, direction));
                }

                /// <summary>
                /// Add the given item to this set
                /// </summary>
                public void Add(ITurnTable item, int position)
                {
                    Add(new TurnTableWithState((TurnTable)item, position));
                }

                /// <summary>
                /// Remove the given item from this set.
                /// </summary>
                /// <returns>True if it was removed, false otherwise</returns>
                public bool Remove(IJunction item)
                {
                    var itemWithState = this.FirstOrDefault(x => x.Junction == item);
                    return (itemWithState != null) && Remove(itemWithState);
                }

                /// <summary>
                /// Remove all
                /// </summary>
                public void Clear()
                {
                    impl.RemoveAll(x => true);
                }

                /// <summary>
                /// Does this set contain the given junction with some state?
                /// </summary>
                public bool Contains(IJunction item)
                {
                    return this.Any(x => x.Junction == item);
                }

                /// <summary>
                /// Copy (clone) all my entries into the given destination.
                /// </summary>
                public void CopyTo(IJunctionWithStateSet destination)
                {
                    foreach (var x in impl)
                    {
                        ((SetImpl2) destination).Add(x.Clone());
                    }
                }
            }
        }
    }
}

namespace BinkyRailways.Core.Model.Impl
{
    /// <summary>
    /// Unbreakable part of an entire railway.
    /// </summary>
    partial class Module 
    {
        /// <summary>
        /// Set of junctions contained in this module.
        /// </summary>
        public class JunctionSet : ModuleEntitySet<Junction, IJunction>
        {
            /// <summary>
            /// Default ctor
            /// </summary>
            internal JunctionSet(Module module)
                : base(module, module)
            {
            }

            /// <summary>
            /// Implementation of IJunctionSet
            /// </summary>
            public new IJunctionSet Set
            {
                get { return (IJunctionSet)base.Set; }
            }

            /// <summary>
            /// The given item has been removed from this set.
            /// </summary>
            protected override void OnRemoved(Junction item)
            {
                foreach (var route in Module.Routes)
                {
                    route.CrossingJunctions.RemoveAll(x => x.JunctionId == item.Id);
                }
                base.OnRemoved(item);
            }

            /// <summary>
            /// Create an implementation of IEntitySet.
            /// </summary>
            protected override SetImpl CreateSetImpl()
            {
                return new JunctionSetImpl(this);
            }

            private sealed class JunctionSetImpl : SetImpl, IJunctionSet
            {
                /// <summary>
                /// Default ctor
                /// </summary>
                internal JunctionSetImpl (JunctionSet impl) : base(impl)
                {                   
                }

                /// <summary>
                /// Add a new passive junction
                /// </summary>
                public IPassiveJunction AddPassiveJunction()
                {
                    var item = new PassiveJunction();
                    Add(item);
                    return item;                   
                }

                /// <summary>
                /// Add a new standard switch
                /// </summary>
                public ISwitch AddSwitch()
                {
                    var item = new Switch();
                    Add(item);
                    return item;
                }

                /// <summary>
                /// Add a new turntable
                /// </summary>
                public ITurnTable AddTurnTable()
                {
                    var item = new TurnTable();
                    Add(item);
                    return item;
                }
            }
        }
    }
}

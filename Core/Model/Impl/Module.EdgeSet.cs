namespace BinkyRailways.Core.Model.Impl
{
    /// <summary>
    /// Unbreakable part of an entire railway.
    /// </summary>
    partial class Module 
    {
        /// <summary>
        /// Set of edges of this module.
        /// </summary>
        public class EdgeSet : ModuleEntitySet2<Edge, IEdge>
        {
            /// <summary>
            /// Default ctor
            /// </summary>
            internal EdgeSet(Module module) : base(module)
            {
            }

            /// <summary>
            /// The given item has been removed from this set.
            /// </summary>
            protected override void OnRemoved(Edge item)
            {
                Module.Routes.RemoveAll(x => x.References(item));
                base.OnRemoved(item);
            }
        }
    }
}

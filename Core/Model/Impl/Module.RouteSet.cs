namespace BinkyRailways.Core.Model.Impl
{
    /// <summary>
    /// Unbreakable part of an entire railway.
    /// </summary>
    partial class Module 
    {
        /// <summary>
        /// Set of blocks contained in this module.
        /// </summary>
        public class RouteSet : ModuleEntitySet2<Route, IRoute>
        {
            /// <summary>
            /// Default ctor
            /// </summary>
            internal RouteSet(Module module) : base(module)
            {
            }
        }
    }
}

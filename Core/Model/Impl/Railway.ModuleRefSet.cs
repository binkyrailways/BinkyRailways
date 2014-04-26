namespace BinkyRailways.Core.Model.Impl
{
    /// <summary>
    /// Entire railway.
    /// </summary>
    partial class Railway
    {
        /// <summary>
        /// Set of modules of this railway.
        /// </summary>
        public class ModuleRefSet : RailwayEntitySet<ModuleRef, IModuleRef, IModule>
        {
            /// <summary>
            /// Default ctor
            /// </summary>
            internal ModuleRefSet(Railway railway)
                : base(railway)
            {
            }
        }
    }
}

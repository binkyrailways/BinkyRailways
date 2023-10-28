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
        public class ModuleConnectionSet : RailwayEntitySet2<ModuleConnection, IModuleConnection>
        {
            /// <summary>
            /// Default ctor
            /// </summary>
            internal ModuleConnectionSet(Railway railway)
                : base(railway)
            {
            }
        }
    }
}

namespace BinkyRailways.Core.Model.Impl
{
    /// <summary>
    /// Entire railway.
    /// </summary>
    partial class Railway
    {
        /// <summary>
        /// Set of locomotives of this railway.
        /// </summary>
        public class LocRefSet : RailwayEntitySet<LocRef, ILocRef, ILoc>
        {
            /// <summary>
            /// Default ctor
            /// </summary>
            internal LocRefSet(Railway railway)
                : base(railway)
            {
            }
        }
    }
}

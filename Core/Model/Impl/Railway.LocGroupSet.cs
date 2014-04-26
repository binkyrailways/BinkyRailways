namespace BinkyRailways.Core.Model.Impl
{
    /// <summary>
    /// Entire railway.
    /// </summary>
    partial class Railway
    {
        /// <summary>
        /// Set of loc groups in this railway.
        /// </summary>
        public class LocGroupSet : RailwayEntitySet2<LocGroup, ILocGroup>
        {
            /// <summary>
            /// Default ctor
            /// </summary>
            internal LocGroupSet(Railway railway)
                : base(railway)
            {
            }
        }
    }
}

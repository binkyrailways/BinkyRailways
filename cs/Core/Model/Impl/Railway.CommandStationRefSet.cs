namespace BinkyRailways.Core.Model.Impl
{
    /// <summary>
    /// Entire railway.
    /// </summary>
    partial class Railway
    {
        /// <summary>
        /// Set of command stations of this railway.
        /// </summary>
        public class CommandStationRefSet : RailwayEntitySet<CommandStationRef, ICommandStationRef, ICommandStation>
        {
            /// <summary>
            /// Default ctor
            /// </summary>
            internal CommandStationRefSet(Railway railway)
                : base(railway)
            {
            }
        }
    }
}

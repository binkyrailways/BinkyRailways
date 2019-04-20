using System;

namespace BinkyRailways.WinApp.Controls.VirtualCanvas
{
    internal class VCItemPlacementEventArgs : EventArgs
    {
        private readonly VCItemPlacement placement;

        /// <summary>
        /// Default ctor
        /// </summary>
        /// <param name="placement"></param>
        internal VCItemPlacementEventArgs(VCItemPlacement placement)
        {
            this.placement = placement;
        }

        /// <summary>
        /// Gets the placement involved in the event.
        /// </summary>
        internal VCItemPlacement Placement
        {
            get { return placement; }
        }
    }
}

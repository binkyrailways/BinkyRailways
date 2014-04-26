using System.Drawing;

namespace BinkyRailways.WinApp.Controls.VirtualCanvas
{
    public interface IZoomableVCItemContainer : IVCItemContainer
    {
        /// <summary>
        /// Gets a scaling from this container to it's clients.
        /// </summary>
        float ZoomFactor { get; set; }

        /// <summary>
        /// Convert a point from my items space to the controls space 
        /// </summary>
        PointF Local2Global(PointF pt);
    }
}

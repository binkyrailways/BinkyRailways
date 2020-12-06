using System;
using System.Drawing;

namespace BinkyRailways.WinApp.Controls.VirtualCanvas
{
    /// <summary>
    /// Painting event
    /// </summary>
    public class ItemPaintEventArgs : EventArgs
    {
        private readonly Graphics graphics;
        private readonly Rectangle visibleRectangle;

        /// <summary>
        /// Default ctor
        /// </summary>
        public ItemPaintEventArgs(Graphics graphics, Rectangle visibleRectangle)
        {
            this.graphics = graphics;
            this.visibleRectangle = visibleRectangle;
        }

        /// <summary>
        /// Gets the graphics to draw on. 
        /// The origin is always at 0,0 and the zoomfactor is already adjusted for.
        /// </summary>
        public Graphics Graphics { get { return graphics; } }

        /// <summary>
        /// Gets the rectangle within this item that is actually visible.
        /// </summary>
        public Rectangle VisibleRectangle { get { return visibleRectangle; } }

        /// <summary>
        /// Gets the effective zoomfactor
        /// </summary>
        public float ZoomFactor
        {
            get
            {
                var points = new[] {new PointF(1f, 1f)};
                graphics.Transform.TransformVectors(points);
                return Math.Max(points[0].X, points[0].Y);
            }
        }
    }
}

using System;
using System.Drawing;

namespace BinkyRailways.WinApp.Controls.VirtualCanvas.Handlers
{
    public class RectangleEventArgs : EventArgs
    {
        private readonly Rectangle bounds;

        /// <summary>
        /// Default ctor
        /// </summary>
        /// <param name="bounds"></param>
        public RectangleEventArgs(Rectangle bounds)
        {
            this.bounds = bounds;
        }

        /// <summary>
        /// Gets the rectangle
        /// </summary>
        public Rectangle Bounds
        {
            get { return bounds; }
        }
    }
}

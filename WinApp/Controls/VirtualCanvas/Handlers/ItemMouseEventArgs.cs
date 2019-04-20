using System;
using System.Drawing;
using System.Windows.Forms;

namespace BinkyRailways.WinApp.Controls.VirtualCanvas.Handlers
{
    public class ItemMouseEventArgs : EventArgs
    {
        private readonly CursorController cursorController;

        /// <summary>
        /// Default ctor
        /// </summary>
        public ItemMouseEventArgs(ItemMouseEventArgs source, float x, float y)
        {
            Button = source.Button;
            Clicks = source.Clicks;
            Delta = source.Delta;
            X = x;
            Y = y;
            cursorController = source.cursorController;
        }

        /// <summary>
        /// Default ctor
        /// </summary>
        internal ItemMouseEventArgs(MouseEventArgs source, CursorController cursorController)
        {
            Button = source.Button;
            Clicks = source.Clicks;
            Delta = source.Delta;
            X = source.X;
            Y = source.Y;
            this.cursorController = cursorController;
        }

        public MouseButtons Button { get; private set; }
        public int Clicks { get; private set; }
        public int Delta { get; private set; }
        public float X { get; private set; }
        public float Y { get; private set; }
        public PointF Location { get { return new PointF(X, Y); } }

        /// <summary>
        /// Gets / sets the current mouse cursor
        /// </summary>
        public Cursor Cursor
        {
            get { return cursorController.Cursor; }
            set { cursorController.Cursor = value; }
        }
    }
}

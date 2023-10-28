using System.Windows.Forms;

namespace BinkyRailways.WinApp.Controls.VirtualCanvas.Handlers
{
    /// <summary>
    /// Keyboard event
    /// </summary>
    public class ItemKeyEventArgs : KeyEventArgs
    {
        private readonly CursorController cursorController;

        /// <summary>
        /// Default ctor
        /// </summary>
        /// <param name="e"></param>
        internal ItemKeyEventArgs(KeyEventArgs e, CursorController cursorController)
            : base(e.KeyData)
        {
            this.cursorController = cursorController;
        }

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

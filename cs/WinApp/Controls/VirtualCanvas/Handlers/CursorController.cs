using System;
using System.Windows.Forms;

namespace BinkyRailways.WinApp.Controls.VirtualCanvas.Handlers
{
    internal sealed class CursorController : IDisposable
    {
        private readonly Control control;
        private Cursor cursor = Cursors.Default;

        /// <summary>
        /// Default ctor
        /// </summary>
        /// <param name="control"></param>
        public CursorController(Control control)
        {
            this.control = control;
        }

        /// <summary>
        /// Gets / sets the cursor value.
        /// </summary>
        public Cursor Cursor
        {
            get { return cursor; }
            set { cursor = value; }
        }

        #region IDisposable Members

        void IDisposable.Dispose()
        {
            if (control.Cursor != cursor)
            {
                control.Cursor = cursor;
            }
        }

        #endregion
    }
}

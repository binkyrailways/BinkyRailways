using System.Drawing;
using System.Windows.Forms;

namespace BinkyRailways.WinApp.Controls.VirtualCanvas.Handlers
{
    public class ItemDragEventArgs : DragEventArgs
    {
        private readonly DragEventArgs controlEventArgs;

        /// <summary>
        /// Default ctor
        /// </summary>
        /// <param name="button"></param>
        /// <param name="clicks"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="delta"></param>
        public ItemDragEventArgs(ItemDragEventArgs source, int x, int y)
            :
            base(source.Data, source.KeyState, x, y, source.AllowedEffect, source.Effect)
        {
            this.controlEventArgs = source.controlEventArgs;
        }

        /// <summary>
        /// Default ctor
        /// </summary>
        /// <param name="button"></param>
        /// <param name="clicks"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="delta"></param>
        public ItemDragEventArgs(DragEventArgs source, Point clientPt)
            :
            base(source.Data, source.KeyState, clientPt.X, clientPt.Y, source.AllowedEffect, source.Effect)
        {
            this.controlEventArgs = source;
        }

        /// <summary>
        /// Gets or sets the target drop effect in a drag-and-drop operation. 
        /// </summary>
        public new DragDropEffects Effect
        {
            get { return controlEventArgs.Effect; }
            set { controlEventArgs.Effect = value; }
        }
    }
}

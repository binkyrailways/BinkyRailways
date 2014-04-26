using System.Windows.Forms;

namespace BinkyRailways.WinApp.Controls.VirtualCanvas.Handlers
{
    public abstract class KeyboardHandler
    {
        private readonly KeyboardHandler next;

        /// <summary>
        /// Default ctor
        /// </summary>
        protected KeyboardHandler(KeyboardHandler next)
        {
            this.next = next;
        }

        /// <summary>
        /// Catch desired keys
        /// </summary>
        public virtual bool IsInputKey(Keys keyData)
        {
            if (next != null) { return next.IsInputKey(keyData); }
            return false;
        }

        /// <summary>
        /// Key is down on this item
        /// </summary>
        /// <returns>True if the event was handled, false otherwise.</returns>
        public virtual bool OnKeyDown(VCItem sender, ItemKeyEventArgs e)
        {
            if (next != null) { return next.OnKeyDown(sender, e); }
            return false;
        }

        /// <summary>
        /// Key is up on this item
        /// </summary>
        /// <returns>True if the event was handled, false otherwise.</returns>
        public virtual bool OnKeyUp(VCItem sender, ItemKeyEventArgs e)
        {
            if (next != null) { return next.OnKeyUp(sender, e); }
            return false;
        }

        /// <summary>
        /// Mouse handlers can draw over an already drawn item.
        /// </summary>
        /// <param name="e"></param>
        public virtual void OnPostPaint(VCItem sender, ItemPaintEventArgs e)
        {
            if (next != null) { next.OnPostPaint(sender, e); }
        }
    }
}

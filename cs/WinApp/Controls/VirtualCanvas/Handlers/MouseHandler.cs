using System;

namespace BinkyRailways.WinApp.Controls.VirtualCanvas.Handlers
{
    public abstract class MouseHandler
    {
        private readonly MouseHandler next;

        /// <summary>
        /// Default ctor
        /// </summary>
        /// <param name="next"></param>
        protected MouseHandler(MouseHandler next)
        {
            this.next = next;
        }

        /// <summary>
        /// Mouse is clicked on this item
        /// </summary>
        /// <param name="e"></param>
        /// <returns>True if the event was handled, false otherwise.</returns>
        public virtual bool OnMouseClick(VCItem sender, ItemMouseEventArgs e)
        {
            if (next != null) { return next.OnMouseClick(sender, e); }
            return false;
        }

        /// <summary>
        /// Mouse is double clicked on this item
        /// </summary>
        /// <param name="e"></param>
        /// <returns>True if the event was handled, false otherwise.</returns>
        public virtual bool OnMouseDoubleClick(VCItem sender, ItemMouseEventArgs e)
        {
            if (next != null) { return next.OnMouseDoubleClick(sender, e); }
            return false;
        }

        /// <summary>
        /// Mouse button down on this item
        /// </summary>
        /// <param name="e"></param>
        /// <returns>True if the event was handled, false otherwise.</returns>
        public virtual bool OnMouseDown(VCItem sender, ItemMouseEventArgs e)
        {
            if (next != null) { return next.OnMouseDown(sender, e); }
            return false;
        }

        /// <summary>
        /// Mouse button up on this item
        /// </summary>
        /// <param name="e"></param>
        /// <returns>True if the event was handled, false otherwise.</returns>
        public virtual bool OnMouseUp(VCItem sender, ItemMouseEventArgs e)
        {
            if (next != null) { return next.OnMouseUp(sender, e); }
            return false;
        }

        /// <summary>
        /// Mouse move within this item
        /// </summary>
        /// <param name="e"></param>
        /// <returns>True if the event was handled, false otherwise.</returns>
        public virtual bool OnMouseMove(VCItem sender, ItemMouseEventArgs e)
        {
            if (next != null) { return next.OnMouseMove(sender, e); }
            return false;
        }

        /// <summary>
        /// Mouse has entered this item
        /// </summary>
        /// <param name="e"></param>
        public virtual void OnMouseEnter(VCItem sender, EventArgs e)
        {
            if (next != null) { next.OnMouseEnter(sender, e); }
        }

        /// <summary>
        /// Mouse has left this item
        /// </summary>
        /// <param name="e"></param>
        /// <returns>True if the event was handled, false otherwise.</returns>
        public virtual void OnMouseLeave(VCItem sender, EventArgs e)
        {
            if (next != null) { next.OnMouseLeave(sender, e); }
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

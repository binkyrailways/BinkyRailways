using System;

namespace BinkyRailways.WinApp.Controls.VirtualCanvas.Handlers
{
    public abstract class DragDropHandler
    {
        private readonly DragDropHandler next;

        /// <summary>
        /// Default ctor
        /// </summary>
        /// <param name="next"></param>
        protected DragDropHandler(DragDropHandler next)
        {
            this.next = next;
        }

        /// <summary>
        /// Occurs when a drag-and-drop operation is completed.
        /// </summary>
        /// <param name="e"></param>
        public virtual bool OnDragDrop(VCItem sender, ItemDragEventArgs e)
        {
            if (next != null) { return next.OnDragDrop(sender, e); }
            return false;
        }

        /// <summary>
        /// Occurs when an object is dragged over the item's bounds. 
        /// </summary>
        /// <param name="e"></param>
        public virtual bool OnDragOver(VCItem sender, ItemDragEventArgs e)
        {
            if (next != null) { return next.OnDragOver(sender, e); }
            return false;
        }

        /// <summary>
        /// Occurs when an object is dragged into the item's bounds. 
        /// </summary>
        /// <param name="e"></param>
        public virtual void OnDragEnter(VCItem sender, EventArgs e)
        {
            if (next != null) { next.OnDragEnter(sender, e); }
        }

        /// <summary>
        /// Occurs when an object is dragged out of the item's bounds. 
        /// </summary>
        /// <param name="e"></param>
        public virtual void OnDragLeave(VCItem sender, EventArgs e)
        {
            if (next != null) { next.OnDragLeave(sender, e); }
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

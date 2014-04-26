using System;
using System.Drawing;

namespace BinkyRailways.WinApp.Controls.VirtualCanvas.Handlers
{
    /// <summary>
    /// Virtual canvas item that is also an item container
    /// </summary>
    public class ContainerDragDropHandler : DragDropHandler 
    {
        private readonly VCItemContainer container;
        private VCItemPlacement dragOverPlacement;

        /// <summary>
        /// Default ctor
        /// </summary>
        /// <param name="container"></param>
        public ContainerDragDropHandler(VCItemContainer container, DragDropHandler next)
            : base(next)
        {
            this.container = container;
        }

        /// <summary>
        /// Occurs when a drag-and-drop operation is completed.
        /// </summary>
        /// <param name="e"></param>
        public override bool OnDragDrop(VCItem sender, ItemDragEventArgs e)
        {
            var pt = new Point(e.X, e.Y);
            VCItemPlacement placement = null;
            while ((placement = container.Items.Find(pt, placement)) != null)
            {
                Point localPt = placement.ToLocal(pt);
                if (placement.Item.OnDragDrop(new ItemDragEventArgs(e, localPt.X, localPt.Y)))
                {
                    return true;
                }
            }
            return base.OnDragDrop(sender, e); 
        }

        /// <summary>
        /// Occurs when an object is dragged over the item's bounds. 
        /// </summary>
        /// <param name="e"></param>
        public override bool OnDragOver(VCItem sender, ItemDragEventArgs e)
        {
            var pt = new Point(e.X, e.Y);
            VCItemPlacement placement = container.Items.Find(pt, null);

            // Handle drag leave
            if ((dragOverPlacement != null) && (dragOverPlacement != placement))
            {
                dragOverPlacement.Item.OnDragLeave(EventArgs.Empty);
                dragOverPlacement = null;
            }

            while (placement != null)
            {
                // Convert point to local
                Point localPt = placement.ToLocal(pt);

                // Handle drag enter
                if (dragOverPlacement == null)
                {
                    dragOverPlacement = placement;
                    dragOverPlacement.Item.OnDragEnter(EventArgs.Empty);
                }

                // Send drag over
                dragOverPlacement = placement;
                if (placement.Item.OnDragOver(new ItemDragEventArgs(e, localPt.X, localPt.Y)))
                {
                    return true;
                }

                placement = container.Items.Find(pt, placement);
                if ((placement != dragOverPlacement) && (placement != null))
                {
                    dragOverPlacement.Item.OnDragLeave(EventArgs.Empty);
                    dragOverPlacement = null;
                }
            }

            return base.OnDragOver(sender, e);
        }

        /// <summary>
        /// Occurs when an object is dragged out of the item's bounds. 
        /// </summary>
        /// <param name="e"></param>
        public override void OnDragLeave(VCItem sender, EventArgs e)
        {
            if (dragOverPlacement != null)
            {
                dragOverPlacement.Item.OnDragLeave(e);
                dragOverPlacement = null;
            }
            base.OnDragLeave(sender, e);
        }
    }
}

using System;
using System.Drawing;
using System.Windows.Forms;

namespace BinkyRailways.WinApp.Controls.VirtualCanvas.Handlers
{
    /// <summary>
    /// Mouse handler that implements selection of items in the given container.
    /// </summary>
    public class SelectMouseHandler : MouseHandler
    {
        private readonly IVCItemContainer container;
        private readonly SelectionManager manager;

        /// <summary>
        /// Default ctor
        /// </summary>
        public SelectMouseHandler(IVCItemContainer container, MouseHandler next)
            : this(container, null, next)
        {
        }

        /// <summary>
        /// Default ctor
        /// </summary>
        public SelectMouseHandler(IVCItemContainer container, SelectionManager manager, MouseHandler next)
            : base(next)
        {
            this.container = container;
            this.manager = manager ?? new SelectionManager(container, CanSelect);
        }

        /// <summary>
        /// Pass mouse down to appropriate item
        /// </summary>
        public override bool OnMouseDown(VCItem sender, ItemMouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
            {
                // Pass event on
                return base.OnMouseDown(sender, e);
            }
            
            var pt = e.Location;
            var clearSelection = (Control.ModifierKeys == Keys.None);
            manager.Start(pt, clearSelection);
            return true;
        }

        /// <summary>
        /// End of selection
        /// </summary>
        public override bool OnMouseUp(VCItem sender, ItemMouseEventArgs e)
        {
            if (manager.IsSelecting)
            {
                manager.Stop();
                return true;
            }
            return base.OnMouseUp(sender, e);
        }

        /// <summary>
        /// Pass mouse move to appropriate item
        /// </summary>
        public override bool OnMouseMove(VCItem sender, ItemMouseEventArgs e)
        {
            if (!manager.IsSelecting || (e.Button != MouseButtons.Left))
            {
                return base.OnMouseMove(sender, e);
            }
            
            // Record position
            var pt = e.Location;
            manager.Extend(pt);

            return true;
        }

        /// <summary>
        /// Stop resizing when mouse leaves the container
        /// </summary>
        public override void OnMouseLeave(VCItem sender, EventArgs e)
        {
            if (manager.IsSelecting)
            {
                manager.Stop();
            }
            base.OnMouseLeave(sender, e);
        }

        /// <summary>
        /// Draw selection
        /// </summary>
        public sealed override void OnPostPaint(VCItem sender, ItemPaintEventArgs e)
        {
            manager.Paint(sender, e);
            base.OnPostPaint(sender, e);
        }

        /// <summary>
        /// Is a selection being made?
        /// </summary>
        protected bool IsSelecting
        {
            get { return manager.IsSelecting; }
        }

        /// <summary>
        /// Calculate the selection rectangle
        /// </summary>
        protected RectangleF SelectionBounds
        {
            get { return manager.SelectionBounds; }
        }

        /// <summary>
        /// Can the given item placement be selected.
        /// Override this method to block items from being selected.
        /// </summary>
        protected virtual bool CanSelect(VCItemPlacement placement)
        {
            return true;
        }
    }
}

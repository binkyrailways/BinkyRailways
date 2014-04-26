using System;
using System.Drawing;

namespace BinkyRailways.WinApp.Controls.VirtualCanvas.Handlers
{
    /// <summary>
    /// Virtual canvas item that is also an item container
    /// </summary>
    public class ContainerMouseHandler : MouseHandler
    {
        private readonly VCItemContainer container;
        private VCItemPlacement mouseOverPlacement;
        private VCItemPlacement mouseDownPlacement;

        /// <summary>
        /// Default ctor
        /// </summary>
        /// <param name="container"></param>
        public ContainerMouseHandler(VCItemContainer container, MouseHandler next)
            : base(next)
        {
            this.container = container;
        }

        /// <summary>
        /// Pass mouse click to appropriate item
        /// </summary>
        /// <param name="e"></param>
        public override bool OnMouseClick(VCItem sender, ItemMouseEventArgs e)
        {
            var pt = e.Location;
            VCItemPlacement placement = null;
            while ((placement = container.Items.Find(pt, placement)) != null)
            {
                var localPt = placement.ToLocal(pt);
                if (placement.Item.OnMouseClick(new ItemMouseEventArgs(e, localPt.X, localPt.Y))) 
                {
                    return true; 
                }
            }
            return base.OnMouseClick(sender, e);
        }

        /// <summary>
        /// Pass mouse double click to appropriate item
        /// </summary>
        /// <param name="e"></param>
        public override bool OnMouseDoubleClick(VCItem sender, ItemMouseEventArgs e)
        {
            var pt = e.Location;
            VCItemPlacement placement = null;
            while ((placement = container.Items.Find(pt, placement)) != null)
            {
                var localPt = placement.ToLocal(pt);
                if (placement.Item.OnMouseDoubleClick(new ItemMouseEventArgs(e, localPt.X, localPt.Y)))
                {
                    return true;
                }
            }
            return base.OnMouseDoubleClick(sender, e);
        }

        /// <summary>
        /// Pass mouse down to appropriate item
        /// </summary>
        /// <param name="e"></param>
        public override bool OnMouseDown(VCItem sender, ItemMouseEventArgs e)
        {
            var pt = e.Location;
            VCItemPlacement placement = null;
            this.mouseDownPlacement = null;
            while ((placement = container.Items.Find(pt, placement)) != null)
            {
                var localPt = placement.ToLocal(pt);
                if (placement.Item.OnMouseDown(new ItemMouseEventArgs(e, localPt.X, localPt.Y)))
                {
                    this.mouseDownPlacement = placement;
                    return true;
                }
            }
            return base.OnMouseDown(sender, e);
        }

        /// <summary>
        /// Pass mouse up to appropriate item
        /// </summary>
        /// <param name="e"></param>
        public override bool OnMouseUp(VCItem sender, ItemMouseEventArgs e)
        {
            var pt = e.Location;
            if (mouseDownPlacement != null)
            {
                try
                {
                    var localPt = mouseDownPlacement.ToLocal(pt);
                    return mouseDownPlacement.Item.OnMouseUp(new ItemMouseEventArgs(e, localPt.X, localPt.Y));
                }
                finally
                {
                    mouseDownPlacement = null;
                }
            }
            else
            {
                VCItemPlacement placement = null;
                while ((placement = container.Items.Find(pt, placement)) != null)
                {
                    var localPt = placement.ToLocal(pt);
                    if (placement.Item.OnMouseUp(new ItemMouseEventArgs(e, localPt.X, localPt.Y)))
                    {
                        return true;
                    }
                }
                return base.OnMouseUp(sender, e);
            }
        }

        /// <summary>
        /// Pass mouse move to appropriate item
        /// </summary>
        /// <param name="e"></param>
        public override bool OnMouseMove(VCItem sender, ItemMouseEventArgs e)
        {
            var pt = e.Location;
            if (mouseDownPlacement != null)
            {
                // Mouse button down, always send events to same placement
                var localPt = mouseDownPlacement.ToLocal(pt);
                return mouseDownPlacement.Item.OnMouseMove(new ItemMouseEventArgs(e, localPt.X, localPt.Y));
            } 
            else 
            {
                VCItemPlacement placement = container.Items.Find(pt, null);

                // Handle mouse leave
                if ((mouseOverPlacement != null) && (mouseOverPlacement != placement))
                {
                    mouseOverPlacement.Item.OnMouseLeave(EventArgs.Empty);
                    mouseOverPlacement = null;
                }

                while (placement != null)
                {
                    // Convert point to local
                    var localPt = placement.ToLocal(pt);

                    // Handle mouse enter
                    if (mouseOverPlacement == null)
                    {
                        mouseOverPlacement = placement;
                        mouseOverPlacement.Item.OnMouseEnter(EventArgs.Empty);
                    }

                    // Send mouse move
                    mouseOverPlacement = placement;
                    if (placement.Item.OnMouseMove(new ItemMouseEventArgs(e, localPt.X, localPt.Y)))
                    {
                        return true;
                    }

                    placement = container.Items.Find(pt, placement);
                    if ((placement != mouseOverPlacement) && (placement != null))
                    {
                        mouseOverPlacement.Item.OnMouseLeave(EventArgs.Empty);
                        mouseOverPlacement = null;
                    }
                }
                return base.OnMouseMove(sender, e);
            }
        }

        /// <summary>
        /// Handle mouse leaves.
        /// </summary>
        /// <param name="e"></param>
        public override void OnMouseLeave(VCItem sender, EventArgs e)
        {
            if (mouseOverPlacement != null)
            {
                mouseOverPlacement.Item.OnMouseLeave(e);
                mouseOverPlacement = null;
            }
            base.OnMouseLeave(sender, e);
        }
    }
}

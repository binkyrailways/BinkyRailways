using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;

namespace BinkyRailways.WinApp.Controls.VirtualCanvas
{
    /// <summary>
    /// Virtual canvas item that is also an item container
    /// </summary>
    public class VCItemContainer : VCItem, IVCItemContainer
    {
        private readonly VCItemPlacementCollection items = new VCItemPlacementCollection();
        private SelectedVCItemPlacementCollection selection;
        private Layout.IVCLayoutManager layoutManager = new Layout.TopDownLayoutManager();
        private bool layoutRequested = false;
        private Size size;
        private List<VCItem> visibleItems = new List<VCItem>();

        /// <summary>
        /// Default ctor
        /// </summary>
        public VCItemContainer()
        {
            this.KeyboardHandler = new Handlers.ContainerKeyboardHandler(this, null);
            this.MouseHandler = new Handlers.ContainerMouseHandler(this, null);
            this.DragDropHandler = new Handlers.ContainerDragDropHandler(this, null);

            // Connect to itms
            items.Added += delegate(object sender, VCItemPlacementEventArgs e)
            {
                LayoutItems();
                //RaiseSizeChanged();
            };
            items.Removed += delegate(object sender, VCItemPlacementEventArgs e)
            {
                if (selection != null)
                {
                    selection.Remove(e.Placement);
                }
                LayoutItems();
                //RaiseSizeChanged();
            };
            items.ContainerEvent += delegate(object sender, ContainerEventArgs e)
            {
                switch (e.Message)
                {
                    case ContainerMessage.SizeChanged:
                        LayoutItems();
                        //RaiseSizeChanged();
                        break;
                    case ContainerMessage.ZoomToRectangle:
                        {
                            var placement = items.Find((VCItem)sender);
                            if (placement != null)
                            {
                                var rect = e.Rectangle;
                                rect.Location = placement.FromLocal(rect.Location);
                                ZoomToRectangle(rect);
                            }
                        }
                        break;
                    case ContainerMessage.EnsureVisible:
                        {
                            var placement = items.Find((VCItem)sender);
                            if (placement != null)
                            {
                                var pt = placement.FromLocal(e.Point);
                                EnsureVisible(pt);
                            }
                        }
                        break;
                    case ContainerMessage.Redraw:
                        if (!IsUpdating)
                        {
                            RaiseContainerEvent(e);
                        }
                        break;
                    case ContainerMessage.Local2ControlMatrix:
                        {
                            var placement = items.Find((VCItem)sender);
                            if (placement != null)
                            {
                                e.Transform.Multiply(placement.Transform, MatrixOrder.Append);
                                var args = new ContainerEventArgs(e.Message, this, e.Transform);
                                RaiseContainerEvent(args);
                            }
                        }
                        break;
                    default:
                        // Forward all other events
                        RaiseContainerEvent(e);
                        break;
                }
            };
        }

        /// <summary>
        /// Gets the item placements within this container.
        /// </summary>
        [Browsable(false), ReadOnly(true)]
        public VCItemPlacementCollection Items
        {
            get { return items; }
        }

        /// <summary>
        /// Gets all selected items within this container.
        /// </summary>
        [Browsable(false), ReadOnly(true)]
        public SelectedVCItemPlacementCollection SelectedItems
        {
            get
            {
                if (selection == null) { selection = new SelectedVCItemPlacementCollection(this); }
                return selection;
            }
        }

        /// <summary>
        /// Gets all items that have a visibility of true.
        /// </summary>
        public ICollection<VCItemPlacement> VisibleItems
        {
            get
            {
                List<VCItemPlacement> result = new List<VCItemPlacement>();
                foreach (VCItemPlacement placement in Items)
                {
                    if (placement.Item.Visible)
                    {
                        result.Add(placement);
                    }
                }
                return result;
            }
        }

        /// <summary>
        /// Gets / Sets layout manager used to layout items.
        /// </summary>
        [Browsable(false), ReadOnly(true)]
        public Layout.IVCLayoutManager LayoutManager
        {
            get { return layoutManager; }
            set
            {
                if (value == null) { throw new ArgumentNullException("LayoutManager"); }
                if (value != layoutManager)
                {
                    this.layoutManager = value;
                    LayoutItems();
                }
            }
        }

        /// <summary>
        /// Gets all items that will be layed out.
        /// Defaults to all visible items.
        /// </summary>
        protected virtual ICollection<VCItemPlacement> GetLayoutItems()
        {
            return VisibleItems; 
        }

        /// <summary>
        /// Gets the preferred size of this item, expressed in units chosen for the virtual canvas.
        /// </summary>
        public override Size PreferredSize
        {
            get { return layoutManager.PreferredSize; }
        }

        /// <summary>
        /// Gets the size takes by this item
        /// </summary>
        public override Size Size
        {
            get { return size; }
            set
            {
                if (size != value)
                {
                    size = value;
                    LayoutItems();
                }
            }
        }

        /// <summary>
        /// Gets the size that is available to this container.
        /// Overriding this property is only done in the root container.
        /// </summary>
        protected virtual Size AvailableSize
        {
            get { return Size; }
        }

        /// <summary>
        /// Gets the size that is available to this container.
        /// Overriding this property is only done in the root container.
        /// </summary>
        Size IVCItemContainer.AvailableSize { get { return AvailableSize; } }

        /// <summary>
        /// Zoom to a given rectangle.
        /// The rectangle is in local space.
        /// </summary>
        public virtual void ZoomToRectangle(RectangleF rectangle)
        {
            RaiseZoomToRectangle(rectangle);
        }

        /// <summary>
        /// Ensure the given point on this item is visible.
        /// </summary>
        public void EnsureVisible(PointF pt)
        {
            RaiseContainerEvent(new ContainerEventArgs(ContainerMessage.EnsureVisible, this, Local2Control(pt)));
        }

        /// <summary>
        /// Draw this item and all child items
        /// </summary>
        /// <param name="g"></param>
        public override void Draw(ItemPaintEventArgs e)
        {
            List<VCItem> nowVisible = new List<VCItem>();
            DrawItems(e, delegate(ItemPaintEventArgs ie, VCItemPlacement placement) { 
                placement.Item.Draw(ie);
                nowVisible.Add(placement.Item);
            });

            // Notify all items that are no longer visible
            foreach (VCItem item in this.visibleItems)
            {
                if (!nowVisible.Contains(item))
                {
                    item.OnLostVisibility();
                }
            }
            this.visibleItems = nowVisible;

            base.Draw(e);
        }

        /// <summary>
        /// Draw in the context of the container using the given draw handler.
        /// The graphics provided to the draw handler is adjust for the 
        /// zoom factor and position of the container.
        /// </summary>
        /// <param name="g"></param>
        public void DrawContainer(ItemPaintEventArgs e, DrawContainerHandler drawHandler)
        {
            var g = e.Graphics;
            var savedTx = g.Transform;
            try
            {
                drawHandler(new ItemPaintEventArgs(g, e.VisibleRectangle));
            }
            finally
            {
                g.Transform = savedTx;
            }
        }


        /// <summary>
        /// Draw this item and all child items
        /// </summary>
        /// <param name="g"></param>
        public void DrawItems(ItemPaintEventArgs e, DrawItemHandler drawHandler)
        {
            Graphics g = e.Graphics;
            GraphicsState saved = g.Save();
            try
            {
                var visibleRect = e.VisibleRectangle;
                SetupDrawItemsTransform(g, ref visibleRect);

                foreach (VCItemPlacement placement in items.OrderBy(x => x.Item.Priority))
                {
                    if (placement.Item.Visible)
                    {
                        var placementBounds = placement.Bounds;
                        if (placementBounds.IntersectsWith(visibleRect))
                        {
                            // Save transformation matrix
                            Matrix tx = g.Transform;
                            try
                            {
                                // Translate graphics
                                g.MultiplyTransform(placement.Transform);

                                // Calculate visible rectangle of placement
                                var placementVisRect = Rectangle.Intersect(placementBounds, visibleRect);
                                var localVisRect = placement.ToLocal(placementVisRect);

                                // Draw placement
                                drawHandler(new ItemPaintEventArgs(g, localVisRect), placement);
                            }
                            finally
                            {
                                g.Transform = tx;
                            }
                        }
                    }
                }
            }
            finally
            {
                if (null != saved) g.Restore(saved);
            }
        }

        /// <summary>
        /// Setup the graphics such that items can be drawn on it.
        /// Typically only override in the root container.
        /// </summary>
        protected virtual void SetupDrawItemsTransform(Graphics g, ref Rectangle visibleRectangle)
        {
        }

        /// <summary>
        /// Re-layout all items
        /// </summary>
        public void LayoutItems()
        {
            if (IsUpdating)
            {
                layoutRequested = true;
            }
            else
            {
                layoutRequested = false;
                SizeF available = AvailableSize;

                var oldPrefSize = layoutManager.PreferredSize;
                layoutManager.Layout(GetLayoutItems(), available);
                if (oldPrefSize != layoutManager.PreferredSize)
                {
                    RaiseSizeChanged();
                }
                RaiseRedraw();
            }
        }

        /// <summary>
        /// All updates are completed.
        /// </summary>
        protected override void OnUpdateCompleted()
        {
            base.OnUpdateCompleted();
            if (layoutRequested)
            {
                LayoutItems();
            }
        }

        /// <summary>
        /// This method is called when the item is no longer visible.
        /// </summary>
        /// <param name="visible"></param>
        public override void OnLostVisibility()
        {
            foreach (VCItem item in visibleItems)
            {
                item.OnLostVisibility();
            }
            visibleItems.Clear();
            base.OnLostVisibility();
        }
    }
}

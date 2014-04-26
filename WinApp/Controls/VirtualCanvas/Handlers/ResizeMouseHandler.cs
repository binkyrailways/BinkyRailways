using System;
using System.Drawing;
using System.Windows.Forms;

namespace BinkyRailways.WinApp.Controls.VirtualCanvas.Handlers
{
    /// <summary>
    /// Mouse handler that implements resizing of items in the given container.
    /// </summary>
    public class ResizeMouseHandler : MouseHandler
    {
        [Flags]
        private enum HitSide
        {
            None,
            Top = 0x01, 
            Bottom = 0x02, 
            Left = 0x04, 
            Right = 0x08
        }

        private const int DEFAULT_HANDLE_SIZE = 5;

        private readonly IVCItemContainer container;
        private VCItemPlacement activePlacement;
        private int handleSize = DEFAULT_HANDLE_SIZE;
        private PointF startMousePt;
        private Rectangle startBounds;
        private bool resizing = false;
        private HitSide startSide;

        /// <summary>
        /// Default ctor
        /// </summary>
        /// <param name="container"></param>
        public ResizeMouseHandler(IVCItemContainer container, MouseHandler next)
            : base(next)
        {
            this.container = container;
        }

        /// <summary>
        /// Pass mouse down to appropriate item
        /// </summary>
        /// <param name="e"></param>
        public override bool OnMouseDown(VCItem sender, ItemMouseEventArgs e)
        {
            var pt = e.Location;
            var placement = Find(pt, null);

            // Can placement be resized?
            if ((placement != null) && (placement != activePlacement) && !CanResize(placement))
            {
                // No, it cannot be resized
                placement = null;
            }

            HitSide side = HitSide.None;
            if (placement != null)
            {
                side = CalcHitSide(placement, pt);
                if (side == HitSide.None) { placement = null; }
            }

            if (placement != null)
            {
                activePlacement = placement;
                startBounds = placement.Bounds;
                startMousePt = pt;
                startSide = side;
                resizing = true;
                container.Invalidate();
                return true;
            }
            else
            {
                // Pass event on
                return base.OnMouseDown(sender, e);
            }
        }

        /// <summary>
        /// End of resize
        /// </summary>
        /// <param name="e"></param>
        public override bool OnMouseUp(VCItem sender, ItemMouseEventArgs e)
        {
            if (resizing)
            {
                resizing = false;
                return true;
            }
            else
            {
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

            if (resizing)
            {
                var rect = CalcResizedBounds(startBounds, startMousePt, LimitMoveToPosition(pt), startSide);
                Resize(activePlacement, rect);
                container.Invalidate();
                return true;
            }
            else if (e.Button == MouseButtons.None)
            {
                VCItemPlacement placement = Find(pt, null);

                // Can placement be resized?
                if ((placement != null) && (placement != activePlacement) && !CanResize(placement))
                {
                    // No, it cannot be resized
                    placement = null;
                }

                HitSide hitSide = HitSide.None;
                if ((placement != null) || (activePlacement != null))
                {
                    hitSide = CalcHitSide(placement, pt);
                    if (hitSide != HitSide.None)
                    {
                        e.Cursor = GetCursor(hitSide);
                    }
                }

                if (activePlacement != placement)
                {
                    activePlacement = placement;
                    container.Invalidate();
                }

                if (hitSide != HitSide.None)
                {
                    return true;
                }
            }

            return base.OnMouseMove(sender, e);
        }

        /// <summary>
        /// Stop resizing when mouse leaves the container
        /// </summary>
        /// <param name="e"></param>
        public override void OnMouseLeave(VCItem sender, EventArgs e)
        {
            base.OnMouseLeave(sender, e);
            resizing = false;

            if (activePlacement != null)
            {
                activePlacement = null;
                container.Invalidate();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        public override void OnPostPaint(VCItem sender, ItemPaintEventArgs e)
        {
            container.DrawItems(e, DrawResizeHandles);
            base.OnPostPaint(sender, e);
        }

        /// <summary>
        /// Gets the item placement that is being resized.
        /// </summary>
        protected VCItemPlacement ResizePlacement
        {
            get { return activePlacement; }
        }

        /// <summary>
        /// Is a placement being resized?
        /// </summary>
        public bool IsResizing
        {
            get { return resizing; }
        }

        /// <summary>
        /// Adjust the bounds of the given placement.
        /// </summary>
        /// <param name="placement"></param>
        /// <param name="newBounds"></param>
        protected virtual void Resize(VCItemPlacement placement, Rectangle newBounds)
        {
            activePlacement.Bounds = newBounds;
        }

        /// <summary>
        /// Find the placement to resize at the given virtual point.
        /// </summary>
        /// <param name="pt"></param>
        /// <returns></returns>
        private VCItemPlacement Find(PointF pt, VCItemPlacement after)
        {
            // This can be smarter so it also finds items the mouse is (a bit) outside of.
            return container.Items.Find(pt, after);
        }

        /// <summary>
        /// Adjust the current position.
        /// This is typically used to limit a position to within the container.
        /// </summary>
        /// <param name="pt"></param>
        /// <returns></returns>
        protected virtual PointF LimitMoveToPosition(PointF pt)
        {
            return pt;
        }

        /// <summary>
        /// Snap the given point to some other item.
        /// </summary>
        /// <param name="pt"></param>
        /// <returns></returns>
        protected virtual Point SnapPoint(Point pt)
        {
            return pt;
        }

        /// <summary>
        /// Calculate what side or corner or the placement the given point touches.
        /// </summary>
        /// <param name="placement"></param>
        /// <param name="pt"></param>
        /// <returns></returns>
        private HitSide CalcHitSide(VCItemPlacement placement, PointF pt)
        {
            if (placement == null) { return HitSide.None; }

            Rectangle bounds = placement.Bounds;
            int max = handleSize;

            var topDist = Math.Abs(bounds.Top - pt.Y);
            var bottomDist = Math.Abs(bounds.Bottom - pt.Y);
            var leftDist = Math.Abs(bounds.Left - pt.X);
            var rightDist = Math.Abs(bounds.Right - pt.X);

            if (topDist <= max)
            {
                if (leftDist <= max) { return HitSide.Top | HitSide.Left; }
                if (rightDist <= max) { return HitSide.Top | HitSide.Right; }
                return HitSide.Top;
            }
            else if (bottomDist <= max)
            {
                if (leftDist <= max) { return HitSide.Bottom | HitSide.Left; }
                if (rightDist <= max) { return HitSide.Bottom | HitSide.Right; }
                return HitSide.Bottom;
            }
            else if (leftDist <= max)
            {
                return HitSide.Left;
            }
            else if (rightDist <= max)
            {
                return HitSide.Right;
            }
            return HitSide.None;
        }

        /// <summary>
        /// Calculate a resized bounds
        /// </summary>
        /// <param name="startBounds">Start bounds of the placement</param>
        /// <param name="startPt">Start mouse point</param>
        /// <param name="curPt">Current mouse point</param>
        /// <param name="side">Starting hit side</param>
        /// <returns></returns>
        private Rectangle CalcResizedBounds(Rectangle startBounds, PointF startPt, PointF curPt, HitSide side)
        {
            var dx = (int) (curPt.X - startPt.X);
            var dy = (int) (curPt.Y - startPt.Y);
            var x = startBounds.X;
            var y = startBounds.Y;
            var width = startBounds.Width;
            var height = startBounds.Height;

            var origDy = dy;

            if ((side & HitSide.Top) != 0)
            {
                Point pt = SnapPoint(new Point(x, y + Math.Min(dy, height)));
                dy = Math.Min(pt.Y - y, height);
                y += dy;
                height -= dy;
            }
            if ((side & HitSide.Bottom) != 0)
            {                
                Point pt = SnapPoint(new Point(x, y + height + Math.Max(dy, -height)));
                dy = Math.Max(pt.Y - (y + height), -height);
                height += dy;
            }
            if ((side & HitSide.Left) != 0)
            {
                Point pt = SnapPoint(new Point(x + Math.Min(dx, width), y));
                dx = Math.Min(pt.X - x, width);
                x += dx;
                width -= dx;
            }
            if ((side & HitSide.Right) != 0)
            {
                Point pt = SnapPoint(new Point(x + width + Math.Max(dx, -width), y));
                dx = Math.Max(pt.X - (x + width), -width);
                width += dx;
            }

            return new Rectangle(x, y, width, height);
        }

        /// <summary>
        /// Gets the cursor for a given side.
        /// </summary>
        /// <param name="side"></param>
        /// <returns></returns>
        private static Cursor GetCursor(HitSide side)
        {
            switch (side)
            {
                case HitSide.Top:
                case HitSide.Bottom:
                    return Cursors.SizeNS;
                case HitSide.Left:
                case HitSide.Right:
                    return Cursors.SizeWE;
                case (HitSide.Top | HitSide.Left):
                case HitSide.Bottom | HitSide.Right:
                    return Cursors.SizeNWSE;
                case HitSide.Top | HitSide.Right:
                case HitSide.Bottom | HitSide.Left:
                    return Cursors.SizeNESW;
                default:
                    return Cursors.Default;
            }
        }

        /// <summary>
        /// Draw resize handles on the given placement
        /// </summary>
        /// <param name="e"></param>
        /// <param name="placement"></param>
        private void DrawResizeHandles(ItemPaintEventArgs e, VCItemPlacement placement)
        {
            if ((placement == activePlacement) && CanResize(placement))
            {
                Graphics g = e.Graphics;
                handleSize = (int)(DEFAULT_HANDLE_SIZE / e.ZoomFactor);
                int d = -handleSize / 2;
                int s = handleSize;
                Size sz = placement.Item.Size;

                Brush brush = Brushes.Red;

                // Top-left
                g.FillRectangle(brush, d, d, s, s);
                // Top-right
                g.FillRectangle(brush, sz.Width + d, d, s, s);
                // Bottom-left
                g.FillRectangle(brush, d, sz.Height + d, s, s);
                // Bottom-right
                g.FillRectangle(brush, sz.Width + d, sz.Height + d, s, s);

                // Top-center
                g.FillRectangle(brush, (sz.Width / 2) + d, d, s, s);
                // Bottom-center
                g.FillRectangle(brush, (sz.Width / 2) + d, sz.Height + d, s, s);
                // Left-middle
                g.FillRectangle(brush, d, (sz.Height / 2) + d, s, s);
                // Right-middle
                g.FillRectangle(brush, sz.Width + d, (sz.Height / 2) + d, s, s);
            }
        }

        /// <summary>
        /// Can the given item placement be resized.
        /// Override this method to block items from being resized.
        /// </summary>
        /// <param name="placement"></param>
        /// <returns></returns>
        protected virtual bool CanResize(VCItemPlacement placement)
        {
            return true;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;

namespace BinkyRailways.WinApp.Controls.VirtualCanvas.Handlers
{
    /// <summary>
    /// Mouse handler that implements resizing of items in the given container.
    /// </summary>
    public abstract class MoveMouseHandler : MouseHandler
    {
        private readonly IVCItemContainer container;
        private List<MovingPlacement> movingPlacements;
        private PointF startMousePt;
        private bool moveContents = false;

        /// <summary>
        /// Default ctor
        /// </summary>
        public MoveMouseHandler(IVCItemContainer container, MouseHandler next)
            : base(next)
        {
            this.container = container;
        }

        /// <summary>
        /// Pass mouse down to appropriate item
        /// </summary>
        public override bool OnMouseDown(VCItem sender, ItemMouseEventArgs e)
        {
            if (IsValidMoveButton(e.Button))
            {
                var pt = e.Location;
                startMousePt = pt;
                movingPlacements = null;

                if (container.SelectedItems.Find(pt, CanMove) != null)
                {
                    // Move all selected items
                    movingPlacements = container.SelectedItems.Where(CanMove).Select(x => new MovingPlacement(x)).ToList();
                }
                else 
                {
                    movingPlacements = null;
                    var placement = container.Items.Find(pt, null, CanMove);
                    if (placement != null)
                    {
                        // Move only 1 items
                        movingPlacements = new List<MovingPlacement> { new MovingPlacement(placement) };
                    }
                }
                if ((movingPlacements != null) && (movingPlacements.Count > 0))
                {
                    // Begin of the move
                    return true;
                }

                // Nothing to move
                movingPlacements = null;
            }

            return base.OnMouseDown(sender, e);
        }

        /// <summary>
        /// End of move
        /// </summary>
        public override bool OnMouseUp(VCItem sender, ItemMouseEventArgs e)
        {
            if (movingPlacements == null)
            {
                return base.OnMouseUp(sender, e);
            }

            foreach (var mPlacement in movingPlacements)
            {
                mPlacement.Commit(this);
            }
            movingPlacements = null;
            container.Invalidate();
            return true;
        }

        /// <summary>
        /// Pass mouse move to appropriate item
        /// </summary>
        public override bool OnMouseMove(VCItem sender, ItemMouseEventArgs e)
        {
            if (movingPlacements == null)
            {
                return base.OnMouseMove(sender, e);
            }

            var pt = e.Location;
            var dx = pt.X - startMousePt.X;
            var dy = pt.Y - startMousePt.Y;
            foreach (var mPlacement in movingPlacements)
            {
                mPlacement.Move(dx, dy, this);
                if (moveContents)
                {
                    mPlacement.Commit(this);
                }
            }
            container.Invalidate();
            return true;
        }

        /// <summary>
        /// Stop resizing when mouse leaves the container
        /// </summary>
        public override void OnMouseLeave(VCItem sender, EventArgs e)
        {
            base.OnMouseLeave(sender, e);
            movingPlacements = null;
        }

        /// <summary>
        /// Draw moving boxes
        /// </summary>
        public override void OnPostPaint(VCItem sender, ItemPaintEventArgs e)
        {
            if ((movingPlacements != null) && (!moveContents))
            {
                container.DrawContainer(e, DrawMovingBoxes);
            }
            base.OnPostPaint(sender, e);
        }

        /// <summary>
        /// If true, the location of placements is modified during the move.
        /// If false, the location of placements is modified on mouse up.
        /// </summary>
        public bool MoveContents
        {
            get { return moveContents; }
            set { moveContents = value; }
        }

        /// <summary>
        /// Adjust the bounds of the given placement.
        /// </summary>
        protected abstract void Move(VCItemPlacement placement, int dx, int dy);

        /// <summary>
        /// Adjust the current position.
        /// This is typically used to limit a position to within the container.
        /// </summary>
        protected virtual Point LimitMoveToPosition(Point pt)
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
        /// Are placements being moved?
        /// </summary>
        public bool IsMoving
        {
            get { return (movingPlacements != null); }
        }

        /// <summary>
        /// Gets a list of placements that are being moved.
        /// </summary>
        protected IEnumerable<VCItemPlacement> MovingPlacements
        {
            get {
                return (movingPlacements != null) ? 
                    movingPlacements.Select(x => x.Placement) : 
                    Enumerable.Empty<VCItemPlacement>();
            }
        }

        /// <summary>
        /// Draw boxes on the new location of the moving placements.
        /// </summary>
        /// <param name="e"></param>
        private void DrawMovingBoxes(ItemPaintEventArgs e)
        {
            using (var pen = new Pen(Color.Black, 2.0f / e.ZoomFactor))
            {
                pen.DashStyle = DashStyle.Dot;
                var g = e.Graphics;

                foreach (var mPlacement in movingPlacements)
                {
                    g.DrawRectangle(pen, mPlacement.GetNewBounds(this));
                }
            }
        }

        /// <summary>
        /// Is the given button the right button to move?
        /// </summary>
        protected virtual bool IsValidMoveButton(MouseButtons button)
        {
            return (button == MouseButtons.Left);
        }

        /// <summary>
        /// Can the given item placement be moved.
        /// Override this method to block items from being moved.
        /// </summary>
        /// <param name="placement"></param>
        /// <returns></returns>
        protected virtual bool CanMove(VCItemPlacement placement)
        {
            return true;
        }

        /// <summary>
        /// Object that holds a placement and its starting a current location.
        /// </summary>
        private sealed class MovingPlacement
        {
            private readonly Point startLocation;
            private Point lastCommitLocation;
            private Point newLocation;
            private readonly VCItemPlacement placement;

            /// <summary>
            /// Default ctor
            /// </summary>
            public MovingPlacement(VCItemPlacement placement)
            {
                this.placement = placement;
                startLocation = placement.Location;
                newLocation = startLocation;
                lastCommitLocation = startLocation;
            }

            public VCItemPlacement Placement
            {
                get { return placement; }
            }

            /// <summary>
            /// Move the placement
            /// </summary>
            public void Move(float dx, float dy, MoveMouseHandler handler)
            {
                var pt = handler.LimitMoveToPosition(new Point((int) (startLocation.X + dx), (int) (startLocation.Y + dy)));
                newLocation = pt;
            }

            /// <summary>
            /// Gets the new bounds of the placement.
            /// </summary>
            public Rectangle GetNewBounds(MoveMouseHandler handler)
            {
                var newBounds = new Rectangle(newLocation, placement.Item.Size);

                // Snap to other items

                // First try top-left
                var pt = new Point(newBounds.Left, newBounds.Top);
                var sPt = handler.SnapPoint(pt);
                var snappedX = false;
                var snappedY = false;
                if (pt.X != sPt.X)
                {
                    // Snap to left
                    newBounds.X = sPt.X;
                    snappedX = true;
                }
                if (pt.Y != sPt.Y)
                {
                    // Snap to top
                    newBounds.Y = sPt.Y;
                    snappedY = true;
                }

                // Now try bottom-right
                if (!(snappedX && snappedY))
                {
                    pt = new Point(newBounds.Right, newBounds.Bottom);
                    sPt = handler.SnapPoint(pt);
                    if ((!snappedX) && (pt.X != sPt.X))
                    {
                        // Snap to right
                        newBounds.X = sPt.X - newBounds.Width;
                    }
                    if ((!snappedY) && (pt.Y != sPt.Y))
                    {
                        // Snap to bottom
                        newBounds.Y = sPt.Y - newBounds.Height;
                    }
                }
                return newBounds;
            }

            /// <summary>
            /// Set the new position in the placement
            /// </summary>
            public void Commit(MoveMouseHandler handler)
            {
                var location = lastCommitLocation;
                var rect = GetNewBounds(handler);
                var dx = rect.X - location.X;
                var dy = rect.Y - location.Y;
                handler.Move(placement, dx, dy);
                lastCommitLocation = newLocation;
            }
        }
    }
}

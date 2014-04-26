using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using BinkyRailways.WinApp.Utils;

namespace BinkyRailways.WinApp.Controls.VirtualCanvas.Handlers
{
    /// <summary>
    /// Mouse handler that implements selection of a rectangle in a container.
    /// </summary>
    public class RectangleMouseHandler : MouseHandler, IMessageFilter
    {
        public event EventHandler<RectangleEventArgs> RectangleSelected;

        private const int WM_KEYDOWN = 0x100;
        private const int WM_KEYUP = 0x101;

        private readonly IVCItemContainer container;
        private bool selecting_ = false;
        private bool enabled = true;
        private PointF selStart;
        private PointF selEnd;

        /// <summary>
        /// Default ctor
        /// </summary>
        /// <param name="container"></param>
        public RectangleMouseHandler(IVCItemContainer container, MouseHandler next)
            : base(next)
        {
            this.container = container;
        }

        /// <summary>
        /// Is this mouse handler active?
        /// </summary>
        public bool Enabled
        {
            get { return enabled; }
            set 
            {
                if (enabled != value)
                {
                    enabled = value;
                    if (value)
                    {
                        Application.AddMessageFilter(this);
                    }
                    else
                    {
                        Application.RemoveMessageFilter(this);
                    }
                }
            }
        }

        /// <summary>
        /// Pass mouse down to appropriate item
        /// </summary>
        /// <param name="e"></param>
        public override bool OnMouseDown(VCItem sender, ItemMouseEventArgs e)
        {
            if (enabled || IsSelecting)
            {
                e.Cursor = Cursors.Cross;
            }
            if (enabled) 
            {
                if (e.Button == MouseButtons.Left)
                {
                    var pt = SnapPoint(LimitMoveToPosition(e.Location));
                    selStart = pt;
                    selEnd = pt;
                    IsSelecting = true;
                }

                return true;
            }
            else
            {
                // Pass event on
                return base.OnMouseDown(sender, e);
            }
        }

        /// <summary>
        /// End of selection
        /// </summary>
        /// <param name="e"></param>
        public override bool OnMouseUp(VCItem sender, ItemMouseEventArgs e)
        {
            if (enabled || IsSelecting)
            {
                if (IsSelecting)
                {
                    IsSelecting = false;
                    container.Invalidate();

                    if (RectangleSelected != null)
                    {
                        var bounds = SelectionBounds;
                        if (!bounds.IsEmpty)
                        {
                            RectangleSelected(this, new RectangleEventArgs(bounds.Round()));
                        }
                    }
                }

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
            if (enabled || IsSelecting)
            {
                e.Cursor = Cursors.Cross;

                if (IsSelecting)
                {
                    // Record position
                    selEnd = SnapPoint(LimitMoveToPosition(e.Location));

                    // Redraw selection box
                    container.Invalidate();
                }
                else
                {
                    // Record position
                    selStart = selEnd = SnapPoint(LimitMoveToPosition(e.Location));

                    // Redraw selection box
                    container.Invalidate();
                }

                return true;
            }
            else
            {
                return base.OnMouseMove(sender, e);
            }
        }

        /// <summary>
        /// Stop resizing when mouse leaves the container
        /// </summary>
        /// <param name="e"></param>
        public override void OnMouseLeave(VCItem sender, EventArgs e)
        {
            if (IsSelecting)
            {
                IsSelecting = false;
                container.Invalidate();
            }
            base.OnMouseLeave(sender, e);
        }

        /// <summary>
        /// Is a selection active?
        /// </summary>
        public bool IsSelecting
        {
            get { return selecting_; }
            private set { selecting_ = value; }
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
        protected virtual PointF SnapPoint(PointF pt)
        {
            return pt;
        }

        /// <summary>
        /// Draw selection
        /// </summary>
        /// <param name="e"></param>
        public override void OnPostPaint(VCItem sender, ItemPaintEventArgs e)
        {
            if (IsSelecting)
            {
                var rect = SelectionBounds;
                if (!rect.IsEmpty)
                {
                    using (Pen pen = new Pen(Color.Black))
                    {
                        pen.DashStyle = DashStyle.Dot;
                        e.Graphics.DrawRectangle(pen, SelectionBounds.Round());
                    }
                }
            }

            base.OnPostPaint(sender, e);
        }

        /// <summary>
        /// Calculate the selection rectangle
        /// </summary>
        protected RectangleF SelectionBounds
        {
            get
            {
                var x = Math.Min(selStart.X, selEnd.X);
                var y = Math.Min(selStart.Y, selEnd.Y);
                var width = Math.Abs(selStart.X - selEnd.X);
                var height = Math.Abs(selStart.Y - selEnd.Y);

                return new RectangleF(x, y, width, height);
            }
        }

        #region IMessageFilter Members

        bool IMessageFilter.PreFilterMessage(ref Message m)
        {
            Keys keyCode = (Keys)(int)m.WParam & Keys.KeyCode;
            if ((m.Msg == WM_KEYDOWN) && (keyCode == Keys.Escape))
            {
                Enabled = false;
                IsSelecting = false;
                container.Invalidate();
            }
            return false; 
        }

        #endregion
    }
}

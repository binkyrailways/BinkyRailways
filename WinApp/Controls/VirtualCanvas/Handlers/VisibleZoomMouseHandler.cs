using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using BinkyRailways.WinApp.Utils;

namespace BinkyRailways.WinApp.Controls.VirtualCanvas.Handlers
{
    /// <summary>
    /// Place a zoom control in the canvas and zoom with that.
    /// </summary>
    public class VisibleZoomMouseHandler : ZoomMouseHandler
    {
        private RectangleF zoomArea;
        private bool containsMouse = false;
        private bool zooming = false;

        /// <summary>
        /// Default ctor
        /// </summary>
        public VisibleZoomMouseHandler(IZoomableVCItemContainer container, MouseHandler next, params float[] zoomSteps)
            : base(container, zoomSteps, next)
        {
        }

        /// <summary>
        /// Default ctor
        /// </summary>
        public VisibleZoomMouseHandler(IZoomableVCItemContainer container, MouseHandler next)
            : base(container, DEFAULT_ZOOM_STEPS, next)
        {
        }

        /// <summary>
        /// Pass mouse down to appropriate item
        /// </summary>
        public override bool OnMouseDown(VCItem sender, ItemMouseEventArgs e)
        {
            var location = ZoomLayer.Local2Global(e.Location);
            if ((e.Button == MouseButtons.Left) && (zoomArea.Contains(location)))
            {
                zooming = true;
                ContainsMouse = true;
                ZoomLayer.ZoomFactor = GetZoomFactor(zoomArea, location);
                return true;
            }
            return base.OnMouseDown(sender, e);
        }

        /// <summary>
        /// Handle mouse moves
        /// </summary>
        public override bool OnMouseMove(VCItem sender, ItemMouseEventArgs e)
        {
            var location = ZoomLayer.Local2Global(e.Location);
            if (zooming)
            {
                ZoomLayer.ZoomFactor = GetZoomFactor(zoomArea, location);
                return true;
            }
            
            ContainsMouse = zoomArea.Contains(location);
            return base.OnMouseMove(sender, e);
        }

        /// <summary>
        /// Mouse button up
        /// </summary>
        public override bool OnMouseUp(VCItem sender, ItemMouseEventArgs e)
        {
            if (zooming)
            {
                zooming = false;
                var location = ZoomLayer.Local2Global(e.Location);
                ContainsMouse = zoomArea.Contains(location);
                ZoomLayer.Invalidate();
                return true;
            }
            
            return base.OnMouseUp(sender, e);
        }

        /// <summary>
        /// Mouse leaves the container
        /// </summary>
        public override void OnMouseLeave(VCItem sender, EventArgs e)
        {
            ContainsMouse = false;
            base.OnMouseLeave(sender, e);
        }

        /// <summary>
        /// Draw the zoom control
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnPostPaint(VCItem sender, ItemPaintEventArgs e)
        {
            float[] steps = ZoomSteps;
            int count = steps.Length;
            int current = (count - 1) - CurrentZoomStep;
            Graphics g = e.Graphics;
            SmoothingMode oldSmoothingMode = g.SmoothingMode;
            try
            {
                g.SmoothingMode = SmoothingMode.HighQuality;

                Color c = containsMouse ? Color.Gray : ControlPaint.Light(Color.Gray);
                using (GraphicsPath path = GraphicsUtil.CreateRoundedRectangle(20, 10, 3))
                using (GraphicsPath smallPath = GraphicsUtil.CreateRoundedRectangle(16, 8, 3))
                {
                    int x = ZoomLayer.AvailableSize.Width - 40;
                    int y = 20;
                    Matrix tx = new Matrix();
                    tx.Translate(x, y);
                    path.Transform(tx);
                    tx = new Matrix();
                    tx.Translate(x + 2, y + 1);
                    smallPath.Transform(tx);

                    tx = new Matrix();
                    tx.Translate(0, 20);

                    zoomArea = new Rectangle(x, 20, 20, (steps.Length - 1) * 20 + 10);
                    using (Pen pen = new Pen(c))
                    {
                        var pt1 = new PointF((zoomArea.Left + zoomArea.Right) / 2, zoomArea.Top + 2);
                        var pt2 = new PointF(pt1.X, zoomArea.Bottom - 2);
                        g.DrawLine(pen, pt1, pt2);
                    }

                    for (int i = 0; i < count; i++)
                    {
                        if (current == i)
                        {
                            using (SolidBrush brush = new SolidBrush(c))
                            {
                                g.FillPath(brush, path);
                            }
                        }
                        else if (containsMouse)
                        {
                            using (SolidBrush brush = new SolidBrush(ControlPaint.LightLight(c)))
                            {
                                g.FillPath(brush, smallPath);
                            }
                            //g.DrawPath(pen, path);
                        }
                        path.Transform(tx);
                        smallPath.Transform(tx);
                    }

                    if (containsMouse)
                    {
                        var rect = new RectangleF((zoomArea.Left + zoomArea.Right) / 2 - 20, zoomArea.Bottom + 2, 40, 20);
                        var text = string.Format("{0}%", Math.Round((ZoomLayer.ZoomFactor * 100.0f)));
                        var format = new StringFormat();
                        format.Alignment = StringAlignment.Center;

                        using (Font font = new Font(FontFamily.GenericSansSerif, 8.0f))
                        using (SolidBrush brush = new SolidBrush(c))
                        {
                            g.DrawString(text, font, brush, rect, format);
                        }
                    }
                }
            }
            finally
            {
                g.SmoothingMode = oldSmoothingMode;
            }
            base.OnPostPaint(sender, e);
        }

        /// <summary>
        /// Is the mouse pointer in the zoom area.
        /// </summary>
        private bool ContainsMouse
        {
            get { return containsMouse; }
            set
            {
                if (containsMouse != value)
                {
                    containsMouse = value;
                    ZoomLayer.Invalidate();
                }
            }
        }

        /// <summary>
        /// Gets the zoom steps the zoom factor is closest to.
        /// </summary>
        private int CurrentZoomStep
        {
            get
            {
                float[] steps = ZoomSteps;
                float factor = ZoomLayer.ZoomFactor;
                float bestDistance = Math.Abs(factor - steps[0]);
                int bestStep = 0;

                for (int i = 1; i < steps.Length; i++)
                {
                    float distance = Math.Abs(factor - steps[i]);
                    if (distance < bestDistance)
                    {
                        bestDistance = distance;
                        bestStep = i;
                    }
                }
                return bestStep;
            }
        }

        /// <summary>
        /// Gets the best zoom factor given the zoom area an current location
        /// </summary>
        private float GetZoomFactor(RectangleF zoomArea, PointF location)
        {
            float[] steps = ZoomSteps;

            if (location.Y <= zoomArea.Top) { return steps[steps.Length - 1]; }
            if (location.Y >= zoomArea.Bottom) { return steps[0]; }

            var invIndex = (int)((location.Y - zoomArea.Top) / (zoomArea.Height / steps.Length));
            var index = (steps.Length - 1) - invIndex;
            return steps[Math.Max(0, Math.Min(index, steps.Length - 1))];
        }
    }
}

using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Windows.Forms;
using BinkyRailways.WinApp.Utils;

namespace BinkyRailways.WinApp.Controls.VirtualCanvas.Items
{
    /// <summary>
    /// Item that shows a button that can be clicked on.
    /// </summary>
    public class VCButtonItem : VCBaseItem
    {
        /// <summary>
        /// This event is fired when the button is clicked on by the mouse of the keyboard.
        /// </summary>
        public event EventHandler Click;

        private Image image;
        private ButtonPainter painter = ButtonPainter.Default;

        /// <summary>
        /// Default ctor
        /// </summary>
        public VCButtonItem()
        {
            this.MouseHandler = new ButtonMouseHandler(this, this.MouseHandler);
        }

        /// <summary>
        /// Default ctor
        /// </summary>
        public VCButtonItem(string text) : this()
        {
            this.Text = text;
        }

        /// <summary>
        /// Default ctor
        /// </summary>
        public VCButtonItem(Image image)
            : this()
        {
            this.Image = image;
        }

        /// <summary>
        /// Calculate the preferred size of this label.
        /// </summary>
        public override Size PreferredSize
        {
            get
            {
                Size sz = new Size(4, 4);
                if (!string.IsNullOrEmpty(Text))
                {
                    sz += TextRenderer.MeasureText(Text, Font);
                }
                if (image != null)
                {
                    Size imgSz = image.Size;
                    sz.Width += imgSz.Width + 4;
                    sz.Height = Math.Max(sz.Height, imgSz.Height + 4);
                }
                return sz;
            }
        }

        /// <summary>
        /// Image drawn in front of text
        /// </summary>
        public Image Image
        {
            get { return image; }
            set { if (image != value) { image = value; RaiseSizeChanged(); } }
        }

        /// <summary>
        /// Gets / sets the paint hander of this button.
        /// </summary>
        public ButtonPainter Painter
        {
            get { return painter; }
            set 
            {
                if (painter != value)
                {
                    painter = value;
                    RaiseRedraw();
                }
            }
        }

        protected override void OnMouseOverChanged()
        {
            RaiseRedraw();
            base.OnMouseOverChanged();
        }

        protected override void OnMouseDownChanged()
        {
            RaiseRedraw();
            base.OnMouseDownChanged();
        }

        /// <summary>
        /// Draw this label
        /// </summary>
        /// <param name="e"></param>
        public override void Draw(ItemPaintEventArgs e)
        {
            Size sz = this.Size;
            Graphics g = e.Graphics;
            SmoothingMode oldSmoothingMode = g.SmoothingMode;
            try
            {
                g.SmoothingMode = SmoothingMode.HighQuality;

                using (GraphicsPath path = GraphicsUtil.CreateRoundedRectangle(sz, 3))
                {
                    // Draw background
                    painter.DrawBackground(this, e, path);

                    if (image != null)
                    {
                        Point pt = new Point(1 + (sz.Width - image.Width) / 2, 1 + (sz.Height - image.Height) / 2);
                        Rectangle imgBounds = new Rectangle(pt, image.Size);
                        painter.DrawImage(this, e, imgBounds);
                    }

                    if (!string.IsNullOrEmpty(Text))
                    {
                        // Calculate rectangle
                        Rectangle rect = new Rectangle(Point.Empty, sz);
                        painter.DrawText(this, e, rect);
                    }

                    if (IsMouseOver)
                    {
                        painter.DrawMouseOver(this, e, path);
                    }
                }
            }
            finally
            {
                g.SmoothingMode = oldSmoothingMode;
            }

            base.Draw(e);
        }

        /// <summary>
        /// Raise the Click event.
        /// </summary>
        protected void RaiseClick()
        {
            if (Click != null)
            {
                Click(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Specialized mouse handler for buttons.
        /// </summary>
        public class ButtonMouseHandler : Handlers.MouseHandler
        {
            private readonly VCButtonItem button;

            /// <summary>
            /// Default ctor
            /// </summary>
            /// <param name="button"></param>
            /// <param name="next"></param>
            public ButtonMouseHandler(VCButtonItem button, Handlers.MouseHandler next)
                : base(next)
            {
                this.button = button;
            }

            /// <summary>
            /// Raise click on button
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            public override bool OnMouseClick(VCItem sender, Handlers.ItemMouseEventArgs e)
            {
                if ((e.Button == MouseButtons.Left) && (button.Enabled))
                {
                    button.IsMouseDown = false;
                    button.RaiseClick();
                    return true;
                }
                else
                {
                    return base.OnMouseClick(sender, e);
                }
            }

            /// <summary>
            /// Is enabled, consume the event
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            /// <returns></returns>
            public override bool OnMouseMove(VCItem sender, Handlers.ItemMouseEventArgs e)
            {
                if (button.Enabled)
                {
                    button.IsMouseOver = true;
                    return true;
                }
                else
                {
                    return base.OnMouseMove(sender, e);
                }
            }

            /// <summary>
            /// Mouse enters button
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            public override void OnMouseEnter(VCItem sender, EventArgs e)
            {
                if (button.Enabled)
                {
                    button.IsMouseOver = true;
                }
                base.OnMouseEnter(sender, e);
            }

            /// <summary>
            /// Mouse leaves button
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            public override void OnMouseLeave(VCItem sender, EventArgs e)
            {
                button.IsMouseOver = false;
                base.OnMouseLeave(sender, e);
            }

            /// <summary>
            /// Mouse button down
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            public override bool OnMouseDown(VCItem sender, Handlers.ItemMouseEventArgs e)
            {
                if ((e.Button == MouseButtons.Left) && (button.Enabled))
                {
                    button.IsMouseDown = true;
                    return true;
                }
                else
                {
                    return base.OnMouseDown(sender, e);
                }
            }

            /// <summary>
            /// Mouse button up
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            public override bool OnMouseUp(VCItem sender, Handlers.ItemMouseEventArgs e)
            {
                button.IsMouseDown = false;
                return base.OnMouseUp(sender, e);
            }
        }

        /// <summary>
        /// Painter for button items.
        /// </summary>
        public class ButtonPainter
        {
            public static readonly ButtonPainter Default = new ButtonPainter();

            /// <summary>
            /// Draw the background of the button
            /// </summary>
            /// <param name="e"></param>
            /// <param name="bounds"></param>
            public virtual void DrawBackground(VCButtonItem button, ItemPaintEventArgs e, GraphicsPath bounds)
            {
                if (button.IsMouseDown)
                {
                    using (Brush brush = new SolidBrush(Color.Yellow))
                    {
                        e.Graphics.FillPath(brush, bounds);
                    }
                }
            }

            /// <summary>
            /// Draw the text of the button
            /// </summary>
            /// <param name="e"></param>
            /// <param name="bounds"></param>
            public virtual void DrawText(VCButtonItem button, ItemPaintEventArgs e, Rectangle textBounds)
            {
                // Calculate formatting
                StringFormat format = new StringFormat();
                format.Alignment = StringAlignment.Center;
                format.LineAlignment = StringAlignment.Center;

                // Draw text
                Color c = button.Enabled ? button.TextColor : ControlPaint.Light(button.TextColor);
                using (Brush brush = new SolidBrush(c))
                {
                    e.Graphics.DrawString(button.Text, button.Font, brush, textBounds, format);
                }
            }

            /// <summary>
            /// Draw the image of the button
            /// </summary>
            /// <param name="e"></param>
            /// <param name="bounds"></param>
            public virtual void DrawImage(VCButtonItem button, ItemPaintEventArgs e, Rectangle imgBounds)
            {
                Image image = button.Image;
                if (button.Enabled)
                {
                    e.Graphics.DrawImage(image, imgBounds);
                }
                else
                {
                    float[][] array = new float[5][];
                    array[0] = new float[5] { 0.2125f, 0.2125f, 0.2125f, 0, 0 };
                    array[1] = new float[5] { 0.5f, 0.5f, 0.5f, 0, 0 };
                    array[2] = new float[5] { 0.0361f, 0.0361f, 0.0361f, 0, 0 };
                    array[3] = new float[5] { 0, 0, 0, 1, 0 };
                    array[4] = new float[5] { 0.2f, 0.2f, 0.2f, 0, 1 };
                    ColorMatrix grayMatrix = new ColorMatrix(array);
                    ImageAttributes att = new ImageAttributes();
                    att.SetColorMatrix(grayMatrix);
                    e.Graphics.DrawImage(image, imgBounds, 0, 0, imgBounds.Width, imgBounds.Height, GraphicsUnit.Pixel, att);
                }
            }

            /// <summary>
            /// Draw the mouse over markers of the button
            /// </summary>
            /// <param name="e"></param>
            /// <param name="bounds"></param>
            public virtual void DrawMouseOver(VCButtonItem button, ItemPaintEventArgs e, GraphicsPath bounds)
            {
                e.Graphics.DrawPath(Pens.Azure, bounds);
            }
        }
    }
}

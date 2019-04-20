using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace BinkyRailways.WinApp.Controls.VirtualCanvas.Items
{
    /// <summary>
    /// Item that shows a text string.
    /// </summary>
    public class VCLabelItem : VCBaseItem
    {
        private ContentAlignment textAlign = ContentAlignment.TopLeft;

        /// <summary>
        /// Default ctor
        /// </summary>
        public VCLabelItem()
        {
        }

        /// <summary>
        /// Default ctor
        /// </summary>
        public VCLabelItem(string text)
        {
            this.Text = text;
        }

        /// <summary>
        /// Default ctor
        /// </summary>
        public VCLabelItem(string text, ContentAlignment textAlign)
        {
            this.Text = text;
            this.textAlign = textAlign;
        }

        /// <summary>
        /// Gets / sets alignment of text within the item
        /// </summary>
        public ContentAlignment TextAlign
        {
            get { return textAlign; }
            set { textAlign = value; }
        }

        /// <summary>
        /// Calculate the preferred size of this label.
        /// </summary>
        public override Size PreferredSize
        {
            get
            {
                return TextRenderer.MeasureText(Text, Font);
            }
        }

        /// <summary>
        /// Draw this label
        /// </summary>
        /// <param name="e"></param>
        public override void Draw(ItemPaintEventArgs e)
        {
            Graphics g = e.Graphics;
            SmoothingMode oldSmoothingMode = g.SmoothingMode;
            try
            {
                g.SmoothingMode = SmoothingMode.HighQuality;

                // Calculate formatting
                StringFormat format = new StringFormat();
                format.Alignment = Alignment;
                format.LineAlignment = LineAlignment;

                // Calculate rectangle
                RectangleF rect = new RectangleF(PointF.Empty, Size);

                // Draw text
                using (Brush brush = new SolidBrush(TextColor))
                {
                    g.DrawString(Text, TextFont, brush, rect, format);
                }
            }
            finally
            {
                g.SmoothingMode = oldSmoothingMode;
            }
            base.Draw(e);
        }

        /// <summary>
        /// Gets the font used to draw the text.
        /// </summary>
        protected virtual Font TextFont
        {
            get { return Font; }
        }

        /// <summary>
        /// Gets line alignment from TextAlign
        /// </summary>
        private StringAlignment LineAlignment
        {
            get
            {
                // Top
                if (((int)textAlign & 0x000F) != 0) { return StringAlignment.Near; }
                // Middle
                if (((int)textAlign & 0x00F0) != 0) { return StringAlignment.Center; }
                // Bottom
                return StringAlignment.Far;
            }
        }

        /// <summary>
        /// Gets alignment from TextAlign
        /// </summary>
        private StringAlignment Alignment
        {
            get
            {
                // Left
                if (((int)textAlign & 0x0111) != 0) { return StringAlignment.Near; }
                // Center
                if (((int)textAlign & 0x0222) != 0) { return StringAlignment.Center; }
                // Right
                return StringAlignment.Far;
            }
        }
    }
}

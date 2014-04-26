using System.Drawing;

namespace BinkyRailways.WinApp.Controls.VirtualCanvas.Items
{
    /// <summary>
    /// Item that shows an image.
    /// </summary>
    public class VCImageItem : VCItem
    {
        private Image image;
        private Size size = Size.Empty;

        /// <summary>
        /// Default ctor
        /// </summary>
        public VCImageItem()
        {
        }

        /// <summary>
        /// Default ctor
        /// </summary>
        public VCImageItem(Image image)
        {
            this.image = image;
        }

        /// <summary>
        /// Gets / sets image shown in this item
        /// </summary>
        public Image Image
        {
            get { return image; }
            set
            {
                if (image != value)
                {
                    image = value;
                    RaiseSizeChanged();
                }
            }
        }

        /// <summary>
        /// Calculate the preferred size of this label.
        /// </summary>
        public override Size PreferredSize
        {
            get { return (image != null) ? image.Size : Size.Empty; }
        }

        /// <summary>
        /// Gets / Sets item size
        /// </summary>
        public override Size Size
        {
            get { return size; }
            set
            {
                if (size != value)
                {
                    size = value;
                    RaiseSizeChanged();
                }
            }
        }

        /// <summary>
        /// Draw this label
        /// </summary>
        /// <param name="e"></param>
        public override void Draw(ItemPaintEventArgs e)
        {
            if (image != null)
            {
                Rectangle imgBounds = new Rectangle(Point.Empty, image.Size);
                e.Graphics.DrawImage(image, imgBounds);
            }

            base.Draw(e);
        }
    }
}

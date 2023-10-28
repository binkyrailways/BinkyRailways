using System.Drawing;
using System.Windows.Forms;
using BinkyRailways.Core.State;
using BinkyRailways.Core.Util;
using BinkyRailways.WinApp.TypeConverters;

namespace BinkyRailways.WinApp.Controls.Run
{
    public partial class LocImage : UserControl
    {
        private ILocState loc;
        private Image currentImage;

        /// <summary>
        /// Default ctor
        /// </summary>
        public LocImage()
        {
            InitializeComponent();
            pictureBox1.Paint += PictureBox1_Paint;
        }

        private void PictureBox1_Paint(object sender, PaintEventArgs e)
        {
            if (loc != null)
            {
                var text = loc.Description + " (" + loc.Owner + ")";
                e.Graphics.DrawString(text, SystemFonts.DefaultFont, SystemBrushes.ControlText, 0, 0);
            }
        }

        /// <summary>
        /// Set the current loc
        /// </summary>
        internal ILocState Loc
        {
            get { return loc; }
            set
            {
                if (loc != value)
                {
                    loc = value;
                    // Update image
                    var imageStream = (loc != null) ? loc.Image : null;
                    var image = (Image)Default<ImageTypeConverter>.Instance.ConvertTo(imageStream, typeof(Image));
                    pictureBox1.Image = image;
                    if (currentImage != null)
                    {
                        currentImage.Dispose();
                    }
                    currentImage = image;
                }
            }
        }
    }
}

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

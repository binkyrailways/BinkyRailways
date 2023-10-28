using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;

namespace BinkyRailways.WinApp.Forms
{
    public partial class ImageEditorForm : AppForm
    {
        private Image image;

        /// <summary>
        /// Default ctor
        /// </summary>
        public ImageEditorForm()
            : this(null)
        {
        }

        /// <summary>
        /// Default ctor
        /// </summary>
        public ImageEditorForm(Image image)
        {
            InitializeComponent();
            Image = image;
            UpdateToolbar();
        }

        /// <summary>
        /// Gets/sets the image
        /// </summary>
        public Image Image
        {
            get { return image; }
            set
            {
                image = value;
                pictureBox.Image = value;
                pictureBox.Visible = (value != null);
                pictureBox.Size = (value != null) ? value.Size : Size.Empty;
            }
        }

        /// <summary>
        /// Open new image
        /// </summary>
        private void OnOpenClick(object sender, EventArgs e)
        {
            using (var dialog = new OpenFileDialog())
            {
                dialog.Filter = Filters.ImageFilter;
                if (dialog.ShowDialog() != DialogResult.OK)
                    return;

                // Try to load the image
                try
                {
                    var newImage = Image.FromFile(dialog.FileName);
                    var stream = new MemoryStream();
                    newImage.Save(stream, ImageFormat.Png);
                    stream.Position = 0;
                    Image = newImage;
                }
                catch (Exception ex)
                {
                    Notifications.ShowError(ex, Strings.FailedToOpenImageBecauseX, ex.Message);
                }
                finally
                {
                    UpdateToolbar();
                }
            }
        }

        /// <summary>
        /// Save image to disk
        /// </summary>
        private void OnSaveAsClick(object sender, EventArgs e)
        {
            if (image == null)
                return;
            using (var dialog = new SaveFileDialog())
            {
                dialog.Filter = Filters.PngFilter;
                dialog.DefaultExt = ".png";
                if (dialog.ShowDialog() != DialogResult.OK)
                    return;

                // Try to save the image
                try
                {
                    var stream = new MemoryStream();
                    image.Save(stream, ImageFormat.Png);
                    stream.Position = 0;

                    var path = dialog.FileName;
                    var folder = Path.GetDirectoryName(path);
                    if (!Directory.Exists(folder))
                        Directory.CreateDirectory(folder);
                    if (File.Exists(path))
                        File.Delete(path);
                    File.WriteAllBytes(path, stream.ToArray());
                }
                catch (Exception ex)
                {
                    Notifications.ShowError(ex, Strings.FailedToSaveImageBecauseX, ex.Message);
                }
                finally
                {
                    UpdateToolbar();
                }
            }
        }

        /// <summary>
        /// Remove image
        /// </summary>
        private void OnRemoveClick(object sender, EventArgs e)
        {
            Image = null;
            UpdateToolbar();
        }

        /// <summary>
        /// Update the state of the buttons
        /// </summary>
        private void UpdateToolbar()
        {
            cmdRemove.Enabled = (image != null);
            cmdSaveAs.Enabled = (image != null);
        }
    }
}

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;
using BinkyRailways.Core.Util;
using BinkyRailways.WinApp.Forms;
using BinkyRailways.WinApp.TypeConverters;

namespace BinkyRailways.WinApp.UIEditors
{
    /// <summary>
    /// UI type editor for stream typed images
    /// </summary>
    internal class ImageEditor : UITypeEditor
    {
        /// <summary>
        /// Gets the editor style used by the <see cref="M:System.Drawing.Design.UITypeEditor.EditValue(System.IServiceProvider,System.Object)"/> method.
        /// </summary>
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }

        /// <summary>
        /// Edit an address
        /// </summary>
        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            var converter = Default<ImageTypeConverter>.Instance;
            var image = (Image)converter.ConvertTo(value, typeof(Image));

            using (var dialog = new ImageEditorForm(image))
            {
                if (dialog.ShowDialog() != DialogResult.OK)
                    return value;

                return converter.ConvertFrom(dialog.Image);
            }
        }
    }
}

using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.IO;
using System.Windows.Forms;

namespace BinkyRailways.WinApp.UIEditors
{
    /// <summary>
    /// UI type editor for stream typed sounds
    /// </summary>
    internal class SoundEditor : UITypeEditor
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
            using (var dialog = new OpenFileDialog())
            {
                dialog.Filter = Filters.SoundFilter;
                if (dialog.ShowDialog() != DialogResult.OK)
                    return value;

                // Try to load the image
                try
                {
                    return File.OpenRead(dialog.FileName);
                }
                catch (Exception ex)
                {
                    Notifications.ShowError(ex, Strings.FailedToOpenSoundBecauseX, ex.Message);
                    return value;
                }
            }
        }
    }
}

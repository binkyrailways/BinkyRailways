using System;
using System.ComponentModel;
using System.Drawing.Design;
using BinkyRailways.Core.Model;
using BinkyRailways.WinApp.Controls.Edit.Settings;
using BinkyRailways.WinApp.Forms;

namespace BinkyRailways.WinApp.UIEditors
{
    /// <summary>
    /// UI type editor for IEntitySet3[ILoc].
    /// </summary>
    internal class LocSetEditor : UITypeEditor
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
            var locs = value as IEntitySet3<ILoc>;
            var settings = context.GetFirstEntitySettings<IGatherProperties>();
            if ((settings == null) || (locs == null))
                return value;
            using (var dialog = new LocSetEditorForm(settings.Railway, locs))
            {
                dialog.ShowDialog();
            }
            return locs;
        }
    }
}

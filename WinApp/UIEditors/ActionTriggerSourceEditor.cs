using System;
using System.ComponentModel;
using System.Drawing.Design;
using BinkyRailways.Core.Model;
using BinkyRailways.WinApp.Controls.Edit.Settings;
using BinkyRailways.WinApp.Forms;

namespace BinkyRailways.WinApp.UIEditors
{
    /// <summary>
    /// UI type editor for <see cref="IActionTriggerSource"/>.
    /// </summary>
    internal class ActionTriggerSourceEditor : UITypeEditor
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
            var source = value as IActionTriggerSource;
            var settings = context.GetFirstEntitySettings<IEntitySettings>();
            if ((settings == null) || (source == null))
                return value;
            using (var dialog = new ActionsEditorForm(source, settings.Railway, settings.Context))
            {
                dialog.ShowDialog();
            }
            return source;
        }
    }
}

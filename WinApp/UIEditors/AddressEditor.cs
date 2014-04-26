using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;
using BinkyRailways.Core.Model;
using BinkyRailways.WinApp.Forms;

namespace BinkyRailways.WinApp.UIEditors
{
    /// <summary>
    /// UI type editor for addresses
    /// </summary>
    internal class AddressEditor : UITypeEditor
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
            var address = (Address) value;
            using (var dialog = new AddressEditorForm())
            {
                dialog.Address = address;
                switch (dialog.ShowDialog())
                {
                    case DialogResult.OK:
                        return dialog.Address;
                    default:
                        return value;
                }
            }
        }
    }
}

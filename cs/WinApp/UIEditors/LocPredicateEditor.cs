using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;
using BinkyRailways.Core.Model;
using BinkyRailways.WinApp.Controls.Edit.Settings;
using BinkyRailways.WinApp.Forms;

namespace BinkyRailways.WinApp.UIEditors
{
    /// <summary>
    /// UI type editor for loc predicates
    /// </summary>
    internal class LocPredicateEditor : UITypeEditor
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
            var entity = context.GetFirstEntitySettings<IGatherProperties>();
            if (entity == null)
                return value;
            var predicate = (ILocPredicate)value;
            using (var dialog = new PredicateEditorForm(predicate, entity.Railway))
            {
                dialog.Text = "Edit permissions";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    if (provider != null)
                    {
                        var listener = (IPredicateChangedListener)provider.GetService(typeof (IPredicateChangedListener));
                        if (listener != null)
                        {
                            listener.PredicateChanged(predicate);
                        }
                    }
                }
                return predicate;
            }
        }
    }
}

using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using BinkyRailways.Core.Model;
using BinkyRailways.Core.Util;
using BinkyRailways.WinApp.Controls.Edit.Settings;

namespace BinkyRailways.WinApp.UIEditors
{
    /// <summary>
    /// UI type editor for locs
    /// </summary>
    internal class LocEditor : UITypeEditor
    {
        /// <summary>
        /// Gets the editor style used by the <see cref="M:System.Drawing.Design.UITypeEditor.EditValue(System.IServiceProvider,System.Object)"/> method.
        /// </summary>
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.DropDown;
        }

        /// <summary>
        /// Edit an endpoint
        /// </summary>
        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            if ((context != null) && (context.Instance != null) && (provider != null))
            {
                var entity = context.GetFirstEntitySettings<IGatherProperties>();
                if (entity == null)
                    return value;
                var railway = entity.Railway;

                var editorService = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));
                if (editorService != null)
                {
                    // Create a listbox
                    var lb = new ListBox();
                    lb.Items.AddRange(railway.Locs.Select(x =>
                                                              {
                                                                  ILoc loc;
                                                                  return x.TryResolve(out loc) ? loc : null;
                                                              }).Where(x => x != null).OrderBy(x => x.Description, NameWithNumbersComparer.Instance).ToArray());
                    lb.SelectedIndexChanged += (s, _) => editorService.CloseDropDown();

                    editorService.DropDownControl(lb);
                    return lb.SelectedItem;
                }
            }

            return value;
        }
    }
}

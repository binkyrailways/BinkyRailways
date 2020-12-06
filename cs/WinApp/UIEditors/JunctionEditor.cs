using System;
using System.Collections.Generic;
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
    /// UI type editor for junctions
    /// </summary>
    internal class JunctionEditor : UITypeEditor
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
                var entity = context.GetFirstEntitySettings<IEntitySettings>();
                if (entity == null)
                    return value;
                var module = entity.Module;

                var editorService = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));
                if (editorService != null)
                {
                    // Create a listbox
                    var lb = new ListBox();
                    LoadJunctions(module.Junctions, lb);
                    lb.SelectedIndexChanged += (s, _) => editorService.CloseDropDown();

                    editorService.DropDownControl(lb);
                    var selection = (Entry) lb.SelectedItem;
                    return (selection != null) ? selection.Junction : null;
                }
            }

            return value;
        }

        /// <summary>
        /// Load the given junctions into the list.
        /// </summary>
        protected virtual void LoadJunctions(IEnumerable<IJunction> junctions, ListBox target)
        {
            target.Items.AddRange(junctions.OrderBy(x => x.Description, NameWithNumbersComparer.Instance).Select(x => new Entry(x, x.Description)).ToArray());            
        }

        protected class Entry
        {
            private readonly IJunction junction;
            private readonly string description;

            public Entry(IJunction junction, string description)
            {
                this.junction = junction;
                this.description = description;
            }

            public IJunction Junction { get { return junction; } }

            public override string ToString()
            {
                return description;
            }
        }
    }
}

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
    /// UI type editor for edges in a module connection
    /// </summary>
    internal class EdgeEditor : UITypeEditor
    {
        /// <summary>
        /// Gets the editor style used by the <see cref="M:System.Drawing.Design.UITypeEditor.EditValue(System.IServiceProvider,System.Object)"/> method.
        /// </summary>
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.DropDown;
        }

        /// <summary>
        /// Edit an edge
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
                    var allEdges = railway.GetModules().SelectMany(x => x.Edges);
                    lb.Items.AddRange(allEdges.Select(x => new EdgeItem(x)).OrderBy(x => x.ToString(), NameWithNumbersComparer.Instance).ToArray());
                    lb.SelectedIndexChanged += (s, _) => editorService.CloseDropDown();

                    editorService.DropDownControl(lb);
                    var item = lb.SelectedItem as EdgeItem;
                    return (item != null) ? item.Edge : null;
                }
            }

            return value;
        }

        private sealed class EdgeItem
        {
            private readonly IEdge edge;

            public EdgeItem(IEdge edge)
            {
                this.edge = edge;
            }

            public IEdge Edge
            {
                get { return edge; }
            }

            public override string ToString()
            {
                return edge.GlobalDescription();
            }
        }
    }
}

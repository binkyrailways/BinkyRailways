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
    /// UI type editor for block groups
    /// </summary>
    internal class BlockGroupEditor : UITypeEditor
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
                    LoadBlockGroups(module.BlockGroups, lb);
                    lb.SelectedIndexChanged += (s, _) => editorService.CloseDropDown();

                    editorService.DropDownControl(lb);
                    var selection = (Entry) lb.SelectedItem;
                    return (selection != null) ? selection.BlockGroup : null;
                }
            }

            return value;
        }

        /// <summary>
        /// Load the given block groups into the list.
        /// </summary>
        protected virtual void LoadBlockGroups(IEnumerable<IBlockGroup> blockGroups, ListBox target)
        {
            target.Items.AddRange(blockGroups.OrderBy(x => x.Description, NameWithNumbersComparer.Instance).Select(x => new Entry(x, x.Description)).ToArray());            
        }

        protected class Entry
        {
            private readonly IBlockGroup blockGroup;
            private readonly string description;

            public Entry(IBlockGroup blockGroup, string description)
            {
                this.blockGroup = blockGroup;
                this.description = description;
            }

            public IBlockGroup BlockGroup { get { return blockGroup; } }

            public override string ToString()
            {
                return description;
            }
        }
    }

    /// <summary>
    /// Block group editor where "None" is added.
    /// </summary>
    internal class OptionalBlockGroupEditor : BlockGroupEditor
    {
        /// <summary>
        /// Load the given block groups into the list.
        /// </summary>
        protected override void LoadBlockGroups(IEnumerable<IBlockGroup> blockGroups, ListBox target)
        {
            target.Items.Add(new Entry(null, Strings.None));
            base.LoadBlockGroups(blockGroups, target);
        }
    }
}

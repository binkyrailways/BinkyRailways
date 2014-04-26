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
    /// UI type editor for blocks
    /// </summary>
    internal class BlockEditor : UITypeEditor
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
                    LoadBlocks(module.Blocks, lb);
                    lb.SelectedIndexChanged += (s, _) => editorService.CloseDropDown();

                    editorService.DropDownControl(lb);
                    var selection = (Entry) lb.SelectedItem;
                    return (selection != null) ? selection.Block : null;
                }
            }

            return value;
        }

        /// <summary>
        /// Load the given blocks into the list.
        /// </summary>
        protected virtual void LoadBlocks(IEnumerable<IBlock> blocks, ListBox target)
        {
            target.Items.AddRange(blocks.OrderBy(x => x.Description, NameWithNumbersComparer.Instance).Select(x => new Entry(x, x.Description)).ToArray());            
        }

        protected class Entry
        {
            private readonly IBlock block;
            private readonly string description;

            public Entry(IBlock block, string description)
            {
                this.block = block;
                this.description = description;
            }

            public IBlock Block { get { return block; } }

            public override string ToString()
            {
                return description;
            }
        }
    }

    /// <summary>
    /// Block editor where "None" is added.
    /// </summary>
    internal class OptionalBlockEditor : BlockEditor
    {
        /// <summary>
        /// Load the given blocks into the list.
        /// </summary>
        protected override void LoadBlocks(IEnumerable<IBlock> blocks, ListBox target)
        {
            target.Items.Add(new Entry(null, Strings.None));
            base.LoadBlocks(blocks, target);
        }
    }
}

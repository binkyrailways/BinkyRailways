using System;
using System.ComponentModel;
using System.Drawing.Design;
using BinkyRailways.Core.Model;
using BinkyRailways.WinApp.Controls.Edit.Settings;
using BinkyRailways.WinApp.Forms;

namespace BinkyRailways.WinApp.UIEditors
{
    /// <summary>
    /// UI type editor for editing patterns in a <see cref="IBlockSignal"/>.
    /// </summary>
    internal abstract class BlockSignalPatternEditor : UITypeEditor
    {
        private readonly Func<IBlockSignal, int> getPattern;

        protected BlockSignalPatternEditor(Func<IBlockSignal, int> getPattern)
        {
            this.getPattern = getPattern;
        }

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
            var settings = context.Instance as BlockSignalSettings;
            if (settings == null)
                return value;
            using (var dialog = new BlockSignalPatternEditorForm(settings.Entity))
            {
                dialog.ShowDialog();
            }
            return getPattern(settings.Entity);
        }
    }

    internal sealed class BlockSignalRedPatternEditor : BlockSignalPatternEditor
    {
        public BlockSignalRedPatternEditor()
            : base(x => x.RedPattern)
        {
        }
    }

    internal sealed class BlockSignalGreenPatternEditor : BlockSignalPatternEditor
    {
        public BlockSignalGreenPatternEditor()
            : base(x => x.GreenPattern)
        {
        }
    }

    internal sealed class BlockSignalYellowPatternEditor : BlockSignalPatternEditor
    {
        public BlockSignalYellowPatternEditor()
            : base(x => x.YellowPattern)
        {
        }
    }

    internal sealed class BlockSignalWhitePatternEditor : BlockSignalPatternEditor
    {
        public BlockSignalWhitePatternEditor()
            : base(x => x.WhitePattern)
        {
        }
    }
}

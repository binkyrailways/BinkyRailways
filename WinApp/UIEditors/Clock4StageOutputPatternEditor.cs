using System;
using System.ComponentModel;
using System.Drawing.Design;
using BinkyRailways.Core.Model;
using BinkyRailways.WinApp.Controls.Edit.Settings;
using BinkyRailways.WinApp.Forms;

namespace BinkyRailways.WinApp.UIEditors
{
    /// <summary>
    /// UI type editor for editing patterns in a <see cref="IClock4StageOutput"/>.
    /// </summary>
    internal abstract class Clock4StageOutputPatternEditor : UITypeEditor
    {
        private readonly Func<IClock4StageOutput, int> getPattern;

        protected Clock4StageOutputPatternEditor(Func<IClock4StageOutput, int> getPattern)
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
            var settings = context.Instance as Clock4StageOutputSettings;
            if (settings == null)
                return value;
            using (var dialog = new Clock4StageOutputPatternEditorForm(settings.Entity))
            {
                dialog.ShowDialog();
            }
            return getPattern(settings.Entity);
        }
    }

    internal sealed class Clock4StageOutputMorningPatternEditor : Clock4StageOutputPatternEditor
    {
        public Clock4StageOutputMorningPatternEditor()
            : base(x => x.MorningPattern)
        {
        }
    }

    internal sealed class Clock4StageOutputAfternoonPatternEditor : Clock4StageOutputPatternEditor
    {
        public Clock4StageOutputAfternoonPatternEditor()
            : base(x => x.AfternoonPattern)
        {
        }
    }

    internal sealed class Clock4StageOutputEveningPatternEditor : Clock4StageOutputPatternEditor
    {
        public Clock4StageOutputEveningPatternEditor()
            : base(x => x.EveningPattern)
        {
        }
    }

    internal sealed class Clock4StageOutputNightPatternEditor : Clock4StageOutputPatternEditor
    {
        public Clock4StageOutputNightPatternEditor()
            : base(x => x.NightPattern)
        {
        }
    }
}

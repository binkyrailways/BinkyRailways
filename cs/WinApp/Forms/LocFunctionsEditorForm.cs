using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using BinkyRailways.Core.Model;
using BinkyRailways.WinApp.Controls.Edit.Settings;

namespace BinkyRailways.WinApp.Forms
{
    public partial class LocFunctionsEditorForm : AppForm
    {
        private readonly ILocFunctions source;
        private readonly Dictionary<LocFunction, TextBox> editors = new Dictionary<LocFunction, TextBox>();

        /// <summary>
        /// Default ctor
        /// </summary>
        public LocFunctionsEditorForm()
            : this(null, null)
        {
        }

        /// <summary>
        /// Default ctor
        /// </summary>
        internal LocFunctionsEditorForm(ILocFunctions source, GridContext gridContext)
        {
            this.source = source;
            InitializeComponent();
            if (source != null)
            {
                tlpMain.SuspendLayout();
                tlpMain.RowStyles.Clear();
                var range = Enum.GetValues(typeof (LocFunction));
                var row = 0;
                foreach (LocFunction iterator in range)
                {
                    var function = iterator;
                    tlpMain.RowStyles.Add(new RowStyle(SizeType.AutoSize));
                    var label = new Label { Text = string.Format("F{0}", (int)function), AutoSize = true, TextAlign = ContentAlignment.MiddleLeft, Anchor = AnchorStyles.Left };
                    tlpMain.Controls.Add(label, 0, row);
                    var textbox = new TextBox { Tag = function, Dock = DockStyle.Top };
                    var item = source.FirstOrDefault(x => x.Function == function);
                    textbox.Text = (item != null) ? item.Description : "-";
                    textbox.GotFocus += (s, x) => textbox.SelectAll();
                    tlpMain.Controls.Add(textbox, 1, row);
                    editors[function] = textbox;
                    row++;
                }
                tlpMain.ResumeLayout(true);

                var prefHeight = tlpMain.GetPreferredSize(new Size(0, 0)).Height;
                var panelClientSize = scrollPanel.ClientSize;
                if (panelClientSize.Height > prefHeight)
                {
                    Height -= (panelClientSize.Height - prefHeight);
                    scrollPanel.ClientSize = new Size(panelClientSize.Width, prefHeight);
                }
            }
        }

        /// <summary>
        /// Save the actions
        /// </summary>
        private void cmdOk_Click(object sender, System.EventArgs e)
        {
            foreach (var pair in editors)
            {
                var function = pair.Key;
                var name = pair.Value.Text.Trim();
                if (string.IsNullOrEmpty(name) || (name == "-"))
                {
                    // Function does not exist, remove when needed
                    var item = source.FirstOrDefault(x => x.Function == function);
                    if (item != null)
                        source.Remove(item);
                }
                else
                {
                    // Function does exist, add it
                    source.Add(function).Description = name;
                }
            }
        }
    }
}

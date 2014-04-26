using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using BinkyRailways.Import;

namespace BinkyRailways.WinApp.Forms
{
    /// <summary>
    /// Show import messages.
    /// </summary>
    public partial class ImportMessagesForm : AppForm
    {
        /// <summary>
        /// Designer ctor
        /// </summary>
        [Obsolete("Designer only")]
        public ImportMessagesForm()
            : this(null, null)
        {
        }

        /// <summary>
        /// Default ctor
        /// </summary>
        public ImportMessagesForm(string result, IEnumerable<ImportMessage> messages)
        {
            InitializeComponent();

            if (result != null)
            {
                lbInfo.Text = result;
            }
            if (messages != null)
            {
                lbMessages.BeginUpdate();
                lbMessages.Items.AddRange(messages.Select(x => new ListViewItem(x.Message)).ToArray());
                if (lbMessages.Items.Count == 0)
                    lbMessages.Items.Add(Strings.NoMessages);
                lbMessages.EndUpdate();
            }
        }

    }
}

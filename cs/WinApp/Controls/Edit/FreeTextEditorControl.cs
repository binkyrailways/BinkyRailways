using System;
using System.Windows.Forms;
using BinkyRailways.Core.Util;

namespace BinkyRailways.WinApp.Controls.Edit
{
    public partial class FreeTextEditorControl : UserControl
    {
        public event EventHandler Save;
        public event EventHandler Cancel;

        private string editedText;

        /// <summary>
        /// Default ctor
        /// </summary>
        public FreeTextEditorControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Text to edit
        /// </summary>
        public new string Text
        {
            get { return textBox.Text; }
            set { textBox.Text = editedText = value; }
        }

        /// <summary>
        /// Text to edit
        /// </summary>
        public string EditedText
        {
            get { return editedText; }
        }

        private void cmdOk_Click(object sender, EventArgs e)
        {
            editedText = textBox.Text;
            Save.Fire(this);
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            Cancel.Fire(this);
        }

        private void textBox_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.IsInputKey = true;
            }
        }
    }
}

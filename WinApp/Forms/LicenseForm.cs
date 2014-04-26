using System;
using System.Windows.Forms;
using BinkyRailways.KeyGen;
using BinkyRailways.WinApp.Preferences;

namespace BinkyRailways.WinApp.Forms
{
    public partial class LicenseForm : AppForm
    {
        private readonly AppState appState;

        /// <summary>
        /// Designer ctor
        /// </summary>
        public LicenseForm()
            : this(null)
        {
        }

        /// <summary>
        /// Default ctor
        /// </summary>
        internal LicenseForm(AppState appState)
        {
            this.appState = appState;
            InitializeComponent();
            OnTextChanged(null, null);
        }

        /// <summary>
        /// Set button state
        /// </summary>
        private void OnTextChanged(object sender, EventArgs e)
        {
            cmdOk.Enabled = !string.IsNullOrEmpty(tbLicense.Text.Trim());
        }

        /// <summary>
        /// Check license
        /// </summary>
        private void cmdOk_Click(object sender, EventArgs e)
        {
            var xmlData = tbLicense.Text.Trim();
            var license = new License(xmlData);
            string name;
            if (license.Verify(out name))
            {
                // License valid
                UserPreferences.Preferences.License = xmlData;
                UserPreferences.SaveNow();
                appState.UserName = name;
                DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show(Strings.NotAValidLicense, Strings.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

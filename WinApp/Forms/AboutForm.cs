using System;
using System.ComponentModel;
using System.Windows.Forms;
using BinkyRailways.WinApp.Preferences;
using BinkyRailways.WinApp.Update;

namespace BinkyRailways.WinApp.Forms
{
    public sealed partial class AboutForm : AppForm
    {
        private bool checkStarted;
        private readonly UpdateChecker checker = new UpdateChecker();

        /// <summary>
        /// Default ctor
        /// </summary>
        public AboutForm()
            : this(null)
        {
        }

        /// <summary>
        /// Default ctor
        /// </summary>
        internal AboutForm(AppState appState)
        {
            InitializeComponent();
            if (appState != null)
            {
                Text = string.Format(Strings.AboutX, Strings.ProductName);
                lbProductName.Text = Strings.ProductName;
                lbVersion.Text = string.Format(Strings.VersionX, GetType().Assembly.GetName().Version);
                lbLicense.Text = string.Format(Strings.LicensedToX, appState.UserName);
            }
        }

        /// <summary>
        /// Start checking for updates on becoming visible.
        /// </summary>
        protected override void OnVisibleChanged(System.EventArgs e)
        {
            base.OnVisibleChanged(e);
            if ((Visible) && !checkStarted)
            {
                checkStarted = true;
                upgradeCheckWorker.RunWorkerAsync();
            }
        }

        /// <summary>
        /// Check for available updates.
        /// </summary>
        private void OnUpdateCheckDoWork(object sender, DoWorkEventArgs e)
        {
            var result = checker.Check();
            if (result != UpdateCheckResult.CheckFailed)
            {
                UserPreferences.Preferences.LastUpdateCheck = DateTime.Now.ToBinary();
                UserPreferences.SaveNow();
            }
            e.Result = result;
        }

        /// <summary>
        /// Show the results.
        /// </summary>
        private void OnUpdateCheckCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            var result = (UpdateCheckResult) e.Result;
            switch (result)
            {
                case UpdateCheckResult.Up2Date:
                    lbChecking.Text = Strings.UpdateLatestVersion;
                    break;
                case UpdateCheckResult.UpdateAvailable:
                    lbChecking.Text = string.Format(Strings.UpdateVersionXAvailable, checker.LatestVersion);
                    lbChecking.LinkArea = new LinkArea(0, lbChecking.Text.Length);
                    break;
                default:
                    lbChecking.Text = Strings.UpdateCheckFailed;
                    break;
            }
            lbChecking.Enabled = true;
        }

        /// <summary>
        /// Open download URL.
        /// </summary>
        private void lbChecking_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (!string.IsNullOrEmpty(checker.SetupUrl))
            {
                checker.InstallUpdate();
            }
        }
    }
}

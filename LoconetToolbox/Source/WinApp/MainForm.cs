/*
Loconet toolbox
Copyright (C) 2010 Modelspoorgroep Venlo, Ewout Prangsma

This program is free software; you can redistribute it and/or
modify it under the terms of the GNU General Public License
as published by the Free Software Foundation; either version 2
of the License, or (at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program; if not, write to the Free Software
Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA.
*/
using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using LocoNetToolBox.Configuration;
using LocoNetToolBox.Protocol;
using LocoNetToolBox.WinApp.Controls;
using LocoNetToolBox.WinApp.Preferences;

namespace LocoNetToolBox.WinApp
{
    public partial class MainForm : Form
    {
        private const string Title = "MGV LocoNet ToolBox";
        private readonly AppState state;

        /// <summary>
        /// Default ctor
        /// </summary>
        public MainForm()
        {
            state = new AppState(this);
            InitializeComponent();
            lbVersion.Text = string.Format("Version: {0}", GetType().Assembly.GetName().Version);
            state.Setup(new SerialPortLocoBuffer(), new LocoNetConfiguration());

            locoBufferView1.AppState = state;
            commandControl1.AppState = state;
            locoIOList1.AppState = state;
            locoNetMonitor.AppState = state;
            state.PathChanged += (_, x) => UpdateTitle();
            state.Idle += OnLocoNetIdle;
            UpdateTitle();
        }

        /// <summary>
        /// Load time initialization
        /// </summary>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            var prefs = UserPreferences.Preferences;
            var path = prefs.LocoNetConfigurationPath;
            if (!string.IsNullOrEmpty(path))
            {
                state.OpenConfiguration(path);
            }

            var task = Task<VersionCheckResult>.Factory.StartNew(VersionCheckResult.Load);
            task.ContinueWith(x => ProcessVersionCheckResult(x.Result), TaskScheduler.FromCurrentSynchronizationContext());
        }

        /// <summary>
        /// Version check has been done, show results.
        /// </summary>
        private void ProcessVersionCheckResult(VersionCheckResult result)
        {
            if ((result == null) || (!result.Succeeded))
                return;

            var currentVersion = GetType().Assembly.GetName().Version;
            if (result.LatestVersion > currentVersion)
            {
                lbVersion.Text += " NEW VERSION AVAILABLE";
            }
        }

        /// <summary>
        /// Close locobuffer on form close
        /// </summary>
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (!state.SaveConfigurationIfDirty())
            {
                e.Cancel = true;
                return;
            }
            state.Dispose();
            base.OnFormClosing(e);
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            AdjustSizes();
        }

        /// <summary>
        /// Locobuffer has changed.
        /// </summary>
        private void LocoBufferView1LocoBufferChanged(object sender, EventArgs e)
        {
            state.Setup(locoBufferView1.ConfiguredLocoBuffer, null);
        }

        /// <summary>
        /// LocoNet seems idle.
        /// Perform some checks.
        /// </summary>
        private void OnLocoNetIdle(object sender, EventArgs e)
        {
            if (ActiveForm != this)
                return;

            if (state.LocoNet.HasNewLocoIOs && false)
            {
                using (var dialog = new ReadNewLocoIOsForm(state))
                {
                    dialog.ShowDialog(this);
                }
            }
        }

        /// <summary>
        /// Fix size of controls
        /// </summary>
        private void AdjustSizes()
        {
            var height = Math.Max(locoBufferView1.Height, commandControl1.Height);
            locoIOList1.Height = height;
        }

        /// <summary>
        /// Open a new configuration.
        /// </summary>
        private void miOpen_Click(object sender, EventArgs e)
        {
            using (var dialog = new OpenFileDialog())
            {
                dialog.Title = "Open loconet configuration";
                dialog.DefaultExt = Constants.LocoNetConfigurationExt;
                dialog.Filter = Constants.LocoNetConfigurationFilter;
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    state.OpenConfiguration(dialog.FileName);
                }
            }
        }

        /// <summary>
        /// Save configuration now.
        /// </summary>
        private void miSave_Click(object sender, EventArgs e)
        {
            state.SaveConfiguration();
        }

        /// <summary>
        /// Save configuration using different filename
        /// </summary>
        private void miSaveAs_Click(object sender, EventArgs e)
        {
            state.SaveConfigurationAs();
        }

        /// <summary>
        /// Update the title of this form
        /// </summary>
        private void UpdateTitle()
        {
            var path = state.Configuration.Path;
            path = string.IsNullOrEmpty(path) ? "New" : Path.GetFileNameWithoutExtension(path);
            Text = string.Format("{0} - {1}", path, Title);
        }

        /// <summary>
        /// Go to download website.
        /// </summary>
        private void lbVersion_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start("http://code.google.com/p/mgv/downloads");
            }
            catch (Exception ex)
            {
                MessageBox.Show(Application.ProductName, "Failed to open url because: " + ex.Message,
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

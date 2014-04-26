using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using BinkyRailways.Core.Reporting;
using BinkyRailways.Core.State;
using BinkyRailways.Core.Storage;
using BinkyRailways.Core.Util;
using BinkyRailways.WinApp.Preferences;
using BinkyRailways.WinApp.Sound;
using BinkyRailways.WinApp.Update;

namespace BinkyRailways.WinApp.Forms
{
    public partial class MainForm : AppForm, IStateUserInterface 
    {
        private const int WM_SYSCOMMAND = 0x112;
        private const int SC_SCREENSAVE = 0xF140;
        private const int SC_MONITORPOWER = 0xF170;

        private readonly string[] args;
        private readonly AppState appState;
        private readonly SoundPlayer soundPlayer = new SoundPlayer();

        /// <summary>
        /// Designer ctor
        /// </summary>
        public MainForm() : this(new string[0])
        {
        }

        /// <summary>
        /// Default ctor
        /// </summary>
        public MainForm(string[] args)
        {
            this.args = args;
            appState = new AppState(this, this);
            InitializeComponent();
            appState.RailwayStateChanged += (s, _) => SelectControls();
            appState.PackageChanged += (s, _) => SelectControls();
            appState.PackageSaved += (s, _) => UpdateTitle();
            appState.PackageSavedAs += (s, _) => AfterSaveAs();
            appState.TimeChanged += AppStateOnTimeChanged;
            editControl.Initialize(appState);
            runControl.Initialize(appState);
            editControl.UpdateAppTitle += (s, _) => UpdateTitle();

            // Build language menu
            AddLanguageMenuItem("Dutch", "nl-NL");
            AddLanguageMenuItem("English", "en-US");

            // Merge toolbar
            editControl.ToolStripMerge.MergeInto(menuStrip1, 2);
            runControl.ToolStripMerge.MergeInto(menuStrip1, 2);
        }

        /// <summary>
        /// Add an item to the language menu
        /// </summary>
        private void AddLanguageMenuItem(string text, string locale)
        {
            var item = new ToolStripMenuItem(text);
            item.Tag = locale;
            miLanguage.DropDownItems.Add(item);
            item.Click += (s, x) =>
                              {
                                  UserPreferences.Preferences.Locale = locale;
                                  UserPreferences.SaveNow();
                                  MessageBox.Show(this, Strings.RestartToUseNewLanguage, Strings.ProductName);
                              };
        }

        /// <summary>
        /// Gets the state of the application.
        /// </summary>
        internal AppState AppState
        {
            get { return appState; }
        }

        /// <summary>
        /// Save on close
        /// </summary>
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (!appState.SaveIfDirty())
            {
                e.Cancel = true;
            }
            else
            {
                appState.RailwayState = null;
                base.OnFormClosing(e);
            }
        }

        /// <summary>
        /// Load time initialization
        /// </summary>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            // Fill open recent menu
            LoadRecentPlans();

            // Load preferences
            var prefs = UserPreferences.Preferences;

            // Open file from command line
            var activeFile = (args.Length > 0) && (args[0].EndsWith(Package.DefaultExt, StringComparison.OrdinalIgnoreCase))
                                 ? args[0] : null;
            if (activeFile == null)
            {
                // Open last used file
                activeFile = prefs.RecentFileList.FirstOrDefault(x => !string.IsNullOrEmpty(x) && File.Exists(x));
            }
            if (activeFile != null)
            {
                // Open the last opened file
                Open(activeFile);

                // Switch to run mode if needed
                if ((prefs.RunMode) && (appState.Package != null))
                {
                    appState.CreateRailwayState(prefs.VirtualMode);
                }
            }

            // Now select the applicable control
            SelectControls();

            // Start update check
            updateCheckWorker.RunWorkerAsync();
        }

        /// <summary>
        /// Load the state of this form from user preferences
        /// </summary>
        protected override void LoadFormPreferences(out Size formSize, out Point formLocation, out bool isMaximized)
        {
            var prefs = UserPreferences.Preferences;
            formSize = prefs.AppWindowSize;
            formLocation = prefs.AppWindowLocation;
            isMaximized = prefs.AppWindowMaximized;
        }

        /// <summary>
        /// Save the location, size and state of this form in user preferences.
        /// </summary>
        protected override void SaveFormPreferences()
        {
            UserPreferences.Preferences.AppWindowSize = Size;
            UserPreferences.Preferences.AppWindowMaximized = (WindowState == FormWindowState.Maximized);
            UserPreferences.Preferences.AppWindowLocation = Location;
            UserPreferences.SaveNow();
        }

        /// <summary>
        /// Override some system commands to avoid screensavers / powerdown.
        /// </summary>
        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case WM_SYSCOMMAND:
                    switch ((int)m.WParam)
                    {
                        case SC_MONITORPOWER:
                            // Avoid monitor power off
                            return;
                        case SC_SCREENSAVE:
                            // Avoid screensaver
                            return;
                    }
                    break;
            }
            base.WndProc(ref m);
        }

        /// <summary>
        /// Fill recent menu item
        /// </summary>
        private void LoadRecentPlans()
        {
            // Cleanup first
            miRecentFiles.DropDownItems.Clear();
            miRecentFiles.Visible = false;

            var prefs = UserPreferences.Preferences;
            var validFolders = new List<string>();
            foreach (var folder in prefs.RecentFileList)
            {
                if (!string.IsNullOrEmpty(folder) && File.Exists(folder))
                {
                    var fileToOpen = folder;
                    validFolders.Add(fileToOpen);
                    var item = miRecentFiles.DropDownItems.Add(fileToOpen);
                    item.Click += (s, e) => Open(fileToOpen);
                    miRecentFiles.Visible = true;
                }
            }
            prefs.RecentFileList = validFolders.ToArray();
        }

        /// <summary>
        /// Create a new railway package.
        /// </summary>
        private void miNew_Click(object sender, EventArgs e)
        {
            using (var dialog = new SaveFileDialog())
            {
                dialog.DefaultExt = Package.DefaultExt;
                dialog.Title = Strings.NewPackageDialogTitle;
                dialog.Filter = Filters.PackagesFilter;
                if (dialog.ShowDialog(this) == DialogResult.OK)
                {
                    try
                    {
                        var package = Package.Create();
                        var path = dialog.FileName;
                        package.Save(path);
                        Open(path);
                    }
                    catch (Exception ex)
                    {
                        Notifications.ShowError(ex, Strings.NewPackageFailedBecauseX, ex.Message);
                    }
                }
            }
        }

        /// <summary>
        /// Open a railway package
        /// </summary>
        private void miOpen_Click(object sender, EventArgs e)
        {
            using (var dialog = new OpenFileDialog())
            {
                dialog.DefaultExt = Package.DefaultExt;
                dialog.Title = Strings.OpenPackageDialogTitle;
                dialog.Filter = Filters.PackagesFilter;
                if (dialog.ShowDialog(this) == DialogResult.OK)
                {
                    Open(dialog.FileName);
                }
            }
        }

        /// <summary>
        /// Save the current package
        /// </summary>
        private void miSave_Click(object sender, EventArgs e)
        {
            appState.Save();
        }

        /// <summary>
        /// Save the current package under a different path.
        /// </summary>
        private void miSaveAs_Click(object sender, EventArgs e)
        {
            appState.SaveAs();
        }

        /// <summary>
        /// Close now
        /// </summary>
        private void miExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Open a railway package
        /// </summary>
        private void Open(string path)
        {
            try
            {
                if (!appState.SaveIfDirty())
                    return;

                var mutex = AppState.CreatePackageMutex(path);
                if (!mutex.WaitOne(TimeSpan.Zero, true))
                {
                    MessageBox.Show(Strings.This_railway_is_already_open_in_another_instance, Application.ProductName,
                                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                var package = Package.Load(path);
                AppState.SetPackage(package, path, mutex);

                // Save location
                var prefs = UserPreferences.Preferences;
                var recentFolders = prefs.RecentFileList.ToList();
                recentFolders.Remove(path);
                recentFolders.Insert(0, path);
                prefs.RecentFileList = recentFolders.ToArray();
                UserPreferences.SaveNow();
                LoadRecentPlans();

                // Update UI
                UpdateTitle();
            }
            catch (Exception ex)
            {
                Notifications.ShowError(ex, Strings.OpenPackageFailedBecauseX, ex.Message);
            }
        }

        /// <summary>
        /// Current package has been saved under a different path.
        /// </summary>
        private void AfterSaveAs()
        {
            try
            {
                // Save location
                var path = appState.PackagePath;
                var prefs = UserPreferences.Preferences;
                var recentFolders = prefs.RecentFileList.ToList();
                recentFolders.Remove(path);
                recentFolders.Insert(0, path);
                prefs.RecentFileList = recentFolders.ToArray();
                UserPreferences.SaveNow();
                LoadRecentPlans();

                // Update UI
                UpdateTitle();
            }
            catch (Exception ex)
            {
                Notifications.ShowError(ex, Strings.UnexpectedError, ex.Message);
            }            
        }

        /// <summary>
        /// Update the window title
        /// </summary>
        private void UpdateTitle()
        {
            var path = AppState.PackagePath;
            if (string.IsNullOrEmpty(path))
            {
                Text = Strings.ProductName;
            }
            else
            {
                var package = AppState.Package;
                var railway = (package != null) ? package.Railway : null;
                if ((railway != null) && !string.IsNullOrEmpty(railway.Description))
                {
                    path = railway.Description;
                }
                if ((package != null) && (package.IsDirty))
                {
                    path += "*";
                }
                var railwayState = AppState.RailwayState;
                var virtualMode = ((railwayState != null) && (railwayState.VirtualMode.Enabled))
                                      ? Strings.VirtualModeTitlePostfix : string.Empty;
                var time = (railwayState != null) ? string.Format(" ({0})", railwayState.ModelTime.Actual) : "";
                Text = string.Format("{0}{1}{2} - {3}", path, virtualMode, time, Strings.ProductName);
            }
        }

        /// <summary>
        /// Select the control to display depending on the current app state
        /// </summary>
        private void SelectControls()
        {
            if (appState.RailwayState != null)
            {
                // Run state
                runControl.ToolStripMerge.ShowAll();
                runControl.Visible = true;
                editControl.Visible = false;
                editControl.ToolStripMerge.HideAll();
            }
            else
            {
                // Edit state
                editControl.ToolStripMerge.ShowAll();
                editControl.Enabled = (appState.Package != null);
                editControl.Visible = true;
                runControl.Visible = false;
                runControl.ToolStripMerge.HideAll();
            }
            UpdateTitle();
        }

        /// <summary>
        /// Check for available updates.
        /// </summary>
        private void OnUpdateCheckDoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            var prefs = UserPreferences.Preferences;
            var lastCheck = DateTime.FromBinary(prefs.LastUpdateCheck);
            if (DateTime.Now.Subtract(lastCheck).TotalMinutes > 15)
            {
                var checker = new UpdateChecker();
                checker.UpdateAvailable += this.Synchronize<PropertyEventArgs<Version>>((s, x) => UpdateAvailable(checker, x.Value));
                checker.Check();
                prefs.LastUpdateCheck = DateTime.Now.ToBinary();
                UserPreferences.SaveNow();
            }
        }

        /// <summary>
        /// An update is available.
        /// </summary>
        private static void UpdateAvailable(UpdateChecker checker, Version version)
        {
            var msg = string.Format(Strings.UpdateXAvailableDownloadNow, version);
            if (MessageBox.Show(msg, Strings.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                checker.InstallUpdate();                
            }
        }

        /// <summary>
        /// Show about dialog
        /// </summary>
        private void miAbout_Click(object sender, EventArgs e)
        {
            using (var dialog = new AboutForm(appState))
            {
                dialog.ShowDialog();
            }
        }

        /// <summary>
        /// The COM port for the given command station is invalid.
        /// Choose a new one.
        /// </summary>
        string IStateUserInterface.ChooseComPortName(ICommandStationState cs)
        {
            using (var dialog = new ChooseComPortForm(cs))
            {
                if (dialog.ShowDialog(this) == DialogResult.OK)
                {
                    return dialog.SelectedPortName;
                }
            }
            return null;
        }

        /// <summary>
        /// Gets a sound player implementation.
        /// </summary>
        ISoundPlayer IStateUserInterface.SoundPlayer
        {
            get { return soundPlayer; }
        }

        /// <summary>
        /// Generate a locs report.
        /// </summary>
        private void miLocsReport_Click(object sender, EventArgs e)
        {
            GenerateReport(new LocReportBuilder(appState.Package.Railway), "LocReport");
        }

        /// <summary>
        /// Generate report.
        /// </summary>
        private void GenerateReport(IReportBuilder builder, string title)
        {
            try
            {
                using (var dialog = new SaveFileDialog())
                {
                    dialog.Title = title;
                    dialog.DefaultExt = builder.ReportExtension;
                    dialog.Filter = "Reports|*" + builder.ReportExtension;
                    if (dialog.ShowDialog(this) == DialogResult.OK)
                    {
                        // Generate now
                        builder.Generate(dialog.FileName);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Application.ProductName, ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Model time has changed.
        /// </summary>
        private void AppStateOnTimeChanged(object sender, EventArgs eventArgs)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new EventHandler(AppStateOnTimeChanged), sender, eventArgs);
            }
            else
            {
                UpdateTitle();
            }
        }
    }
}

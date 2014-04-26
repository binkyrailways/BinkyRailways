using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using BinkyRailways.Core.Model;
using BinkyRailways.Core.Server;
using BinkyRailways.Core.State;
using BinkyRailways.Core.State.Impl;
using BinkyRailways.Core.Util;
using BinkyRailways.WinApp.Forms;
using BinkyRailways.WinApp.Preferences;

namespace BinkyRailways.WinApp
{
    internal class AppState
    {
        private const string PackageMutexPrefix = "3FC9EE3A-5D01-40F4-A116-6876E78BDB6F-";

        public event EventHandler PackageChanged;
        public event EventHandler RailwayStateChanged;
        public event EventHandler PackageSaved;
        public event EventHandler PackageSavedAs;
        public event EventHandler TimeChanged; // Model time has changed (can be fired on non-ui thread)

        private readonly IStateUserInterface ui;
        private readonly IStatePersistence statePersistence;
        private readonly IWebServerPersistence webServerPersistence;
        private readonly MainForm mainForm;
        private IPackage package;
        private IRailwayState railwayState;
        private StateInspectionForm stateInspectionForm;
        private RestoreBlockAssignmentsForm restoreBlockAssignmentsForm;
        private bool updateRestoreBlockAssignmentsForm;
        private Mutex packageMutex;
        private WebServer webServer;

        /// <summary>
        /// Default ctor
        /// </summary>
        internal AppState(IStateUserInterface ui, MainForm mainForm)
        {
            Application.ThreadException += (s, x) => OnErrorPowerDown(x.Exception);
            Application.Idle += (s, x) => OnApplicationIdle();
            this.ui = ui;
            this.mainForm = mainForm;
            var persistence = new StatePersistence();
            statePersistence = persistence;
            webServerPersistence = persistence;
        }

        /// <summary>
        /// Save the current package if it's dirty
        /// </summary>
        /// <returns>True when it's save to change package</returns>
        public bool SaveIfDirty()
        {
            if ((package == null) || (!package.IsDirty))
                return true;
            var msg = Strings.PackageDirtySaveNow;
            switch (MessageBox.Show(mainForm, msg, Strings.ProductName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1))
            {
                case DialogResult.Yes:
                    return Save();
                case DialogResult.No:
                    return true;
                default:
                    return false;
            }
        }

        /// <summary>
        /// Save the current package under a new path.
        /// </summary>
        /// <returns>On success</returns>
        public bool SaveAs()
        {
            var path = PackagePath;
            if ((package != null) && !string.IsNullOrEmpty(path))
            {
                try
                {
                    // Check for errors
                    if (!VerifyPackageIntegrity())
                        return false;

                    // Show Save As dialog
                    using (var dialog = new SaveFileDialog())
                    {
                        dialog.DefaultExt = Core.Storage.Package.DefaultExt;
                        dialog.Title = Strings.SaveAsTitle;
                        dialog.Filter = Filters.PackagesFilter;
                        if (dialog.ShowDialog(mainForm) != DialogResult.OK)
                        {
                            return false;
                        }
                        path = dialog.FileName;
                    }

                    // Save now
                    package.Save(path);
                    PackagePath = path;
                    PackageSavedAs.Fire(this);
                    return true;
                }
                catch (Exception ex)
                {
                    Notifications.ShowError(ex, Strings.SavePackageFailedBecauseX, ex.Message);
                    return false;
                }
            }
            // No package, no need to save
            return true;
        }

        /// <summary>
        /// Save the current package under the current path.
        /// </summary>
        /// <returns>On success</returns>
        public bool Save()
        {
            var path = PackagePath;
            if ((package != null) && !string.IsNullOrEmpty(path))
            {
                try
                {
                    // Check for errors
                    if (!VerifyPackageIntegrity())
                        return false;

                    // Save now
                    package.Save(path);
                    PackageSaved.Fire(this);
                    return true;
                }
                catch (Exception ex)
                {
                    Notifications.ShowError(ex, Strings.SavePackageFailedBecauseX, ex.Message);
                    return false;
                }
            }
            // No package, no need to save
            return true;
        }

        /// <summary>
        /// Verify the integrity of the package.
        /// </summary>
        /// <returns>False to avoid saving the package, true otherwise.</returns>
        private bool VerifyPackageIntegrity()
        {
            // Check for errors
            var results = new ValidationResults();
            package.Validate(results);
            if (results.ErrorCount > 0)
            {
                // Oops, sure to save
                var msg = Strings.PackageContainsIntegrityErrorsSureToSave;
                if (MessageBox.Show(msg, Strings.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) != DialogResult.Yes)
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Full path of current package
        /// </summary>
        internal string PackagePath { get; private set; }

        /// <summary>
        /// Gets the railway package which is currently open.
        /// Null if nothing is open
        /// </summary>
        internal IPackage Package
        {
            get { return package; }
        }

        /// <summary>
        /// Create a mutex for accessing a package with given path.
        /// </summary>
        internal static Mutex CreatePackageMutex(string path)
        {
            var id = string.IsNullOrEmpty(path) ? Guid.NewGuid().ToString() : Hash(PackageMutexPrefix + path);
            return new Mutex(true, id);
        }

        /// <summary>
        /// Create a hash of the given value.
        /// </summary>
        private static string Hash(string value)
        {
            var hash = SHA1.Create();
            var data = hash.ComputeHash(Encoding.UTF8.GetBytes(value));
            return string.Join("", data.Select(x => x.ToString("X2")).ToArray());
        }

        /// <summary>
        /// Make the given package the current package.
        /// </summary>
        internal void SetPackage(IPackage value, string packagePath, Mutex valueMutex)
        {
            if (package == value) 
                return;

            // Release old
            RailwayState = null;
            if (packageMutex != null)
            {
                packageMutex.ReleaseMutex();
                packageMutex.Dispose();
            }
            package = value;
            packageMutex = valueMutex;
            PackageChanged.Fire(this);
            PackagePath = (value == null) ? null : packagePath;
        }

        /// <summary>
        /// State of the live railway (null if there is no live railway)
        /// </summary>
        internal IRailwayState RailwayState
        {
            get { return railwayState; }
            set
            {
                if (railwayState != value)
                {
                    if (webServer != null)
                    {
                        webServer.Dispose();
                        webServer = null;
                    }
                    if (railwayState != null)
                    {
                        railwayState.Dispose();
                    }
                    railwayState = value;
                    if (value != null)
                    {
                        value.PrepareForUse(ui, statePersistence);
                        value.ModelTime.ActualChanged += OnModelTimeChanged;
                        webServer = WebServer.StartNew(value, webServerPersistence);
                    }
                    RailwayStateChanged.Fire(this);
                    if (stateInspectionForm != null)
                    {
                        if (value == null)
                        {
                            stateInspectionForm.Hide();
                        }
                        stateInspectionForm.State = value;
                    }
                    updateRestoreBlockAssignmentsForm = true;
                }
            }
        }

        /// <summary>
        /// Gets the current webserver.
        /// Can be null.
        /// </summary>
        public WebServer WebServer
        {
            get { return webServer; }
        }

        /// <summary>
        /// Time changed
        /// </summary>
        private void OnModelTimeChanged(object sender, EventArgs eventArgs)
        {
            TimeChanged.Fire(this);
        }

        /// <summary>
        /// Application is idle.
        /// </summary>
        private void OnApplicationIdle()
        {
            if (updateRestoreBlockAssignmentsForm)
            {
                updateRestoreBlockAssignmentsForm = false;
                UpdateRestoreBlockAssignmentsForm();
            }
        }

        /// <summary>
        /// Hide existing restore block assignments form and show one if railwayState exists
        /// </summary>
        private void UpdateRestoreBlockAssignmentsForm()
        {
            // Show/hide restore block assignment form
            if (restoreBlockAssignmentsForm != null)
            {
                restoreBlockAssignmentsForm.Close();
                restoreBlockAssignmentsForm = null;
            }

            if (railwayState != null)
            {
                // Show restore block assignment form
                restoreBlockAssignmentsForm = new RestoreBlockAssignmentsForm();
                restoreBlockAssignmentsForm.Closed += (s, x) =>
                                                          {
                                                              restoreBlockAssignmentsForm = null;
                                                              if (railwayState != null)
                                                              {
                                                                  foreach (var locState in railwayState.LocStates)
                                                                  {
                                                                      locState.PersistState();
                                                                  }
                                                              }
                                                          };
                if (restoreBlockAssignmentsForm.Initialize(railwayState, StatePersistence))
                {
                    restoreBlockAssignmentsForm.Show(mainForm);
                }
            }            
        }

        /// <summary>
        /// State persistence provider
        /// </summary>
        internal IStatePersistence StatePersistence { get { return statePersistence; } }

        /// <summary>
        /// Instantiate a new railway state.
        /// </summary>
        internal void CreateRailwayState(bool virtualMode)
        {
            RailwayState = new RailwayState(Package.Railway, virtualMode);
        }

        /// <summary>
        /// Show a form used to inspect the current railway state
        /// </summary>
        internal void ShowStateInspectionForm()
        {
            if (stateInspectionForm == null)
            {
                stateInspectionForm = new StateInspectionForm();
                stateInspectionForm.FormClosed += (s, x) => stateInspectionForm = null;
            }
            stateInspectionForm.State = RailwayState;
            if (!stateInspectionForm.Visible)
            {
                stateInspectionForm.Show();
            }
        }

        /// <summary>
        /// Power down on errors
        /// </summary>
        private void OnErrorPowerDown(Exception exception)
        {
            try
            {
                if (RailwayState != null)
                {
                    RailwayState.Power.Requested = false;
                }
                UnhandledExceptionForm.ShowExceptionDialog(exception);
            }
            catch (Exception ex)
            {
            }
        }

        /// <summary>
        /// Licensed user
        /// </summary>
        internal string UserName { get; set; }

        /// <summary>
        /// Gets the common sound player
        /// </summary>
        internal ISoundPlayer SoundPlayer
        {
            get { return ui.SoundPlayer; }
        }
    }
}

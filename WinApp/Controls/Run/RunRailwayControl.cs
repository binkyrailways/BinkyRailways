using System;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using BinkyRailways.Core.Model;
using BinkyRailways.Core.Services;
using BinkyRailways.Core.State;
using BinkyRailways.Core.Util;
using BinkyRailways.WinApp.Forms;
using BinkyRailways.WinApp.Preferences;
using BinkyRailways.WinApp.Utils;

namespace BinkyRailways.WinApp.Controls.Run
{
    /// <summary>
    /// Outer control used when running the railway
    /// </summary>
    public partial class RunRailwayControl : UserControl
    {
        private readonly ToolStripMerge toolStripMerge;
        private AppState appState;
        private ModuleBuilder moduleBuilder;
        private QuickEditForm quickEditForm;
        private LocoIOInspectorForm locoIOInspectorForm;
        private RouteInspectionForm routeInspectorForm;
        private LogForm logForm;
 
        /// <summary>
        /// Default ctor
        /// </summary>
        public RunRailwayControl()
        {
            InitializeComponent();
            tbShowDescriptions.Checked = viewControl.ShowDescriptions;
            toolStripMerge = new ToolStripMerge(toolStrip1);
            toolStrip1.Hide();
            viewControl.SelectLoc += (s, x) => {
                locsControl.SelectLoc(x.Value);
            };
        }

        /// <summary>
        /// Items to merge into the toolbar.
        /// </summary>
        internal ToolStripMerge ToolStripMerge { get { return toolStripMerge; } }

        /// <summary>
        /// Connect to the given application state
        /// </summary>
        internal void Initialize(AppState appState)
        {
            this.appState = appState;
            appState.RailwayStateChanged += (s, _) => ReloadState();
            ReloadState();
        }

        /// <summary>
        /// Reload the railway state into this control
        /// </summary>
        private void ReloadState()
        {
            // Cleanup first
            if (moduleBuilder != null)
            {
                moduleBuilder.Dispose();
                moduleBuilder = null;
            }

            var railway = appState.RailwayState;
            locsControl.Initialize(appState, railway);
            viewControl.Initialize(appState);
            if (railway != null)
            {
                railway.AutomaticLocController.UnexpectedSensorActivated += this.ASynchronize<UnexpectedSensorActivatedEventArgs>((s, x) => OnUnexpectedSensorActivated(x));
                if (tbLearn.Checked)
                {
                    var firstModuleRef = railway.Model.Modules.FirstOrDefault();
                    if (firstModuleRef != null)
                    {
                        IModule module;
                        if (firstModuleRef.TryResolve(out module))
                        {
                            moduleBuilder = new ModuleBuilder(railway, module);
                        }
                    }
                }
            }
            tbbVirtualMode.Visible = (railway != null) && railway.VirtualMode.Enabled;
        }

        /// <summary>
        /// Handle keyboard shortcuts
        /// </summary>
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.F5:
                    OnPowerOnClick();
                    break;
                case Keys.F6:
                    OnPowerOffClick();
                    break;
                case Keys.F12:
                    OnStopAllClick(this, EventArgs.Empty);
                    break;
                case Keys.Control | Keys.Alt | Keys.I:
                    appState.ShowStateInspectionForm();
                    break;
                case Keys.Control | Keys.Alt | Keys.E:
                    ShowQuickEditor(null);
                    break;
                case Keys.Control | Keys.Alt | Keys.R:
                    ShowRouteInspector();
                    break;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        /// <summary>
        /// Hide the quick edit for when hidden.
        /// </summary>
        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);
            if (!Visible)
            {
                if (quickEditForm != null)
                {
                    quickEditForm.Close();
                }
                if (locoIOInspectorForm != null)
                {
                    locoIOInspectorForm.Close();
                }
                if (routeInspectorForm != null)
                {
                    routeInspectorForm.Close();
                }
            }
        }

        /// <summary>
        /// Show the quick editor.
        /// </summary>
        private void ShowQuickEditor(IEntityState selection)
        {
            if (quickEditForm == null)
            {
                quickEditForm = new QuickEditForm();
                quickEditForm.FormClosed += (s, x) => quickEditForm = null;
            }
            quickEditForm.Initialize(appState, appState.Package.Railway, viewControl.Context);
            quickEditForm.Select(selection);
            quickEditForm.Show(this);
        }

        /// <summary>
        /// Show the LocoIO inspector form
        /// </summary>
        private void ShowLocoIOInspector()
        {
            if (locoIOInspectorForm == null)
            {
                locoIOInspectorForm = new LocoIOInspectorForm();
                locoIOInspectorForm.FormClosed += (s, x) => locoIOInspectorForm = null;
            }
            locoIOInspectorForm.Initialize(appState);
            locoIOInspectorForm.Show(this);
        }

        /// <summary>
        /// Show the route inspector form
        /// </summary>
        public void ShowRouteInspector()
        {
            if (routeInspectorForm == null)
            {
                routeInspectorForm = new RouteInspectionForm();
                routeInspectorForm.FormClosed += (s, x) => routeInspectorForm = null;
            }
            routeInspectorForm.Initialize(appState);
            routeInspectorForm.Show(this);
        }

        /// <summary>
        /// Show the LocoIO inspector form
        /// </summary>
        private void ShowLog()
        {
            if (logForm == null)
            {
                logForm = new LogForm();
                logForm.FormClosed += (s, x) => logForm = null;
            }
            logForm.Initialize(appState.RailwayState);
            logForm.Show(this);
        }

        /// <summary>
        /// Close this view and return to edit mode
        /// </summary>
        private void OnCloseAndEditClick(object sender, EventArgs e)
        {
            appState.RailwayState = null;
            UserPreferences.Preferences.RunMode = false;
            UserPreferences.SaveNow();
        }

        /// <summary>
        /// Power the railway
        /// </summary>
        private void OnPowerOnClick()
        {
            var railway = appState.RailwayState;
            if (railway != null)
            {
                railway.Power.Requested = true;
            }
        }

        /// <summary>
        /// Power off the raiway
        /// </summary>
        private void OnPowerOffClick()
        {
            var railway = appState.RailwayState;
            if (railway != null)
            {
                railway.Power.Requested = false;
            }
        }

        /// <summary>
        /// Stop all locs
        /// </summary>
        private void OnStopAllClick(object sender, EventArgs e)
        {
            var railway = appState.RailwayState;
            if (railway != null)
            {
                foreach (var loc in railway.LocStates)
                {
                    loc.SpeedInSteps.Requested = 0;
                }
            }
        }

        /// <summary>
        /// Reload when learn is modified
        /// </summary>
        private void OnLearnCheckedChanged(object sender, EventArgs e)
        {
            ReloadState();
        }

        /// <summary>
        /// An unexpected sensor was activated.
        /// Show UI.
        /// </summary>
        private void OnUnexpectedSensorActivated(UnexpectedSensorActivatedEventArgs e)
        {
            var railway = appState.RailwayState;
            if (railway != null) 
            {
                // Perhaps assign to a loc
                locsControl.OnUnexpectedSensorActivated(e);
            }
        }

        /// <summary>
        /// Show descriptions option changed.
        /// </summary>
        private void OnShowDescriptionsCheckedChanged(object sender, EventArgs e)
        {
            var context = viewControl.Context;
            if (context != null)
            {
                context.ShowDescriptions = tbShowDescriptions.Checked;
                UserPreferences.Preferences.RunShowDescriptions = context.ShowDescriptions;
                UserPreferences.SaveNow();
            }
        }

        /// <summary>
        /// Show the quick editor window.
        /// </summary>
        private void OnQuickEditorClick(object sender, EventArgs e)
        {
            ShowQuickEditor(null);
        }

        /// <summary>
        /// Show the loco IO inspector.
        /// </summary>
        private void OnLocoIoInspectorClick(object sender, EventArgs e)
        {
            ShowLocoIOInspector();
        }

        /// <summary>
        /// Show the log form
        /// </summary>
        private void OnShowLogClick(object sender, EventArgs e)
        {
            ShowLog();
        }

        /// <summary>
        /// Show properties of the given loc state.
        /// </summary>
        private void OnShowLocProperties(object sender, ObjectEventArgs<ILocState> e)
        {
            ShowQuickEditor(e.Object);
        }

        /// <summary>
        /// Virtual mode autorun changed.
        /// </summary>
        private void OnVirtualModeAutoRunCheckStateChanged(object sender, EventArgs e)
        {
            var railway = appState.RailwayState;
            if (railway != null)
            {
                railway.VirtualMode.AutoRun = tbVirtualModeAutoRun.Checked;
            }
        }

        /// <summary>
        /// Show the route inspector form.
        /// </summary>
        private void tbRouteInspector_Click(object sender, EventArgs e)
        {
            ShowRouteInspector();
        }
    }
}

using System.Drawing;
using BinkyRailways.WinApp.Preferences;

namespace BinkyRailways.WinApp.Forms
{
    public partial class RouteInspectionForm : AppForm
    {
        /// <summary>
        /// Default ctor
        /// </summary>
        public RouteInspectionForm()
        {
            InitializeComponent();
        }


        /// <summary>
        /// Connect to the given application state
        /// </summary>
        internal void Initialize(AppState appState)
        {
            locListView.Initialize(appState, appState.RailwayState);
            locListView.UpdateTimerEnabled = false;
        }

        /// <summary>
        /// Loc selection changed
        /// </summary>
        private void OnSelectionChanged(object sender, System.EventArgs e)
        {
            var selection = locListView.SelectedItem;
            inspectionControl.Loc = (selection != null) ? selection.LocState : null;
        }

        /// <summary>
        /// Load the state of this form from user preferences
        /// </summary>
        protected override void LoadFormPreferences(out Size formSize, out Point formLocation, out bool isMaximized)
        {
            var prefs = UserPreferences.Preferences;
            formSize = prefs.RouteInspectorWindowSize;
            formLocation = prefs.RouteInspectorWindowLocation;
            isMaximized = false;
        }

        /// <summary>
        /// Save the location, size and state of this form in user preferences.
        /// </summary>
        protected override void SaveFormPreferences()
        {
            UserPreferences.Preferences.RouteInspectorWindowSize = Size;
            UserPreferences.Preferences.RouteInspectorWindowLocation = Location;
            UserPreferences.SaveNow();
        }
    }
}

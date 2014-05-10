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
    }
}

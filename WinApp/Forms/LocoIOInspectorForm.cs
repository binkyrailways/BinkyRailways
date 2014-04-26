namespace BinkyRailways.WinApp.Forms
{
    public partial class LocoIOInspectorForm : AppForm
    {
        private bool devicesLoaded;

        /// <summary>
        /// Default ctor
        /// </summary>
        public LocoIOInspectorForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Connect to the given application state
        /// </summary>
        internal void Initialize(AppState appState)
        {
            inspectorControl.RailwayState = appState.RailwayState;
        }

        /// <summary>
        /// Load devices when becoming visible.
        /// </summary>
        protected override void OnVisibleChanged(System.EventArgs e)
        {
            base.OnVisibleChanged(e);
            if (Visible && !devicesLoaded)
            {
                devicesLoaded = true;
                inspectorControl.RefreshDevices();
            }
        }
    }
}

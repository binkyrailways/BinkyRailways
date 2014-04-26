using System;
using System.Linq;
using System.Windows.Forms;
using BinkyRailways.Core.Model;
using BinkyRailways.WinApp.Items;
using BinkyRailways.WinApp.Items.Run;
using BinkyRailways.WinApp.Preferences;

namespace BinkyRailways.WinApp.Controls.Run
{
    public partial class RailwayViewControl : UserControl
    {
        private readonly ItemContext context;
        private AppState appState;
        private bool saveZoomFactor;

        /// <summary>
        /// Default ctor
        /// </summary>
        public RailwayViewControl()
        {
            context = new ItemContext(null, () => Railway, Enumerable.Empty<IEntity>, ReloadApp);
            InitializeComponent();
            context.ShowDescriptions = UserPreferences.Preferences.RunShowDescriptions;
            context.Changed += OnContextChanged;
        }

        /// <summary>
        /// Show descriptions of entities?
        /// </summary>
        internal bool ShowDescriptions
        {
            get { return (context == null) || context.ShowDescriptions; }
        }

        /// <summary>
        /// Gets the context passed to all items.
        /// </summary>
        internal ItemContext Context
        {
            get { return context; }
        }

        /// <summary>
        /// Connect to the given application state
        /// </summary>
        internal void Initialize(AppState appState)
        {
            this.appState = appState;
            ReloadApp();
        }

        /// <summary>
        /// Item context has changed.
        /// </summary>
        private void OnContextChanged(object sender, EventArgs e)
        {
            canvas.Refresh();
        }

        /// <summary>
        /// Reload the canvas.
        /// </summary>
        private void ReloadApp()
        {
            saveZoomFactor = false;
            using (canvas.BeginUpdate())
            {
                canvas.Items.Clear();
                var railwayState = appState.RailwayState;
                if (railwayState != null)
                {
                    var item = new RailwayRunItem(railwayState, context);
                    canvas.Items.Add(item, null);
                }
            }
            canvas.ZoomFactor = UserPreferences.Preferences.RunViewZoomFactor;
            saveZoomFactor = true;
        }

        /// <summary>
        /// Save zoom factor
        /// </summary>
        private void canvas_ZoomFactorChanged(object sender, System.EventArgs e)
        {
            if (saveZoomFactor)
            {
                UserPreferences.Preferences.RunViewZoomFactor = canvas.ZoomFactor;
                UserPreferences.SaveNow();
            }
        }

        /// <summary>
        /// Gets the railway
        /// </summary>
        private IRailway Railway
        {
            get
            {
                var package = (appState != null) ? appState.Package : null;
                return (package != null) ? package.Railway : null;
            }
        }
    }
}

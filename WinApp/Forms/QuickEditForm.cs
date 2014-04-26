using System.Drawing;
using BinkyRailways.Core.Model;
using BinkyRailways.Core.State;
using BinkyRailways.WinApp.Items;
using BinkyRailways.WinApp.Preferences;

namespace BinkyRailways.WinApp.Forms
{
    public partial class QuickEditForm : AppForm
    {
        /// <summary>
        /// Default ctor
        /// </summary>
        public QuickEditForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Connect to the given application state
        /// </summary>
        internal void Initialize(AppState appState, IRailway railway, ItemContext context)
        {
            quickEditControl.Initialize(appState, railway, context);
        }

        /// <summary>
        /// Select the given state
        /// </summary>
        internal void Select(IEntityState selection)
        {
            quickEditControl.Select(selection);
        }

        /// <summary>
        /// Load the state of this form from user preferences
        /// </summary>
        protected override void LoadFormPreferences(out Size formSize, out Point formLocation, out bool isMaximized)
        {
            var prefs = UserPreferences.Preferences;
            formSize = prefs.QuickEditorWindowSize;
            formLocation = prefs.QuickEditorWindowLocation;
            isMaximized = false;
        }

        /// <summary>
        /// Save the location, size and state of this form in user preferences.
        /// </summary>
        protected override void SaveFormPreferences()
        {
            UserPreferences.Preferences.QuickEditorWindowSize = Size;
            UserPreferences.Preferences.QuickEditorWindowLocation = Location;
            UserPreferences.SaveNow();
        }
    }
}

using System;
using System.Drawing;
using System.Windows.Forms;
using BinkyRailways.Core.Model;
using BinkyRailways.WinApp.Items;
using BinkyRailways.WinApp.Preferences;

namespace BinkyRailways.WinApp.Forms
{
    public sealed partial class EditModuleForm : AppForm
    {
        private readonly AppState appState;
        private readonly IModule module;

        /// <summary>
        /// Designer ctor
        /// </summary>
        [Obsolete("Designer only")]
        public EditModuleForm()
            : this(null, null, null)
        {
        }

        /// <summary>
        /// Designer ctor
        /// </summary>
        internal EditModuleForm(AppState appState, IModule module, ItemContext context)
        {
            InitializeComponent();
            this.appState = appState;
            this.module = module;
            if ((appState != null) && (module != null))
            {
                editControl.Initialize(appState, module, context);
                UpdateTitle();
            }
            editControl.UpdateFormTitle += (s, _) => UpdateTitle();
        }

        /// <summary>
        /// Load the state of this form from user preferences
        /// </summary>
        protected override void LoadFormPreferences(out Size formSize, out Point formLocation, out bool isMaximized)
        {
            var prefs = UserPreferences.Preferences;
            formSize = prefs.ModuleEditWindowSize;
            formLocation = prefs.ModuleEditWindowLocation;
            isMaximized = prefs.ModuleEditWindowMaximized;
        }

        /// <summary>
        /// Save the location, size and state of this form in user preferences.
        /// </summary>
        protected override void SaveFormPreferences()
        {
            UserPreferences.Preferences.ModuleEditWindowSize = Size;
            UserPreferences.Preferences.ModuleEditWindowMaximized = (WindowState == FormWindowState.Maximized);
            UserPreferences.Preferences.ModuleEditWindowLocation = Location;
            UserPreferences.SaveNow();
        }

        /// <summary>
        /// Process keys.
        /// </summary>
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Control | Keys.S:
                    cmdSave_Click(this, EventArgs.Empty);
                    break;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        /// <summary>
        /// Close this dialog
        /// </summary>
        private void cmdClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Save changes
        /// </summary>
        private void cmdSave_Click(object sender, EventArgs e)
        {
            if (appState != null)
            {
                appState.Save();
                UpdateTitle();
            }
        }

        /// <summary>
        /// Update the window title
        /// </summary>
        private void UpdateTitle()
        {
            var title = Strings.EditModuleTitle;
            if (module != null)
            {
                var prefix = module.ToString();
                if ((appState.Package != null) && (appState.Package.IsDirty))
                {
                    prefix += "*";
                }
                title = prefix + " - " + title;
            }
            Text = title;
        }
    }
}

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using BinkyRailways.WinApp.Preferences;
using BinkyRailways.WinApp.Utils;

namespace BinkyRailways.WinApp.Forms
{
    /// <summary>
    /// Base class for all application forms.
    /// </summary>
    public class AppForm : Form
    {
        private bool initialized;

        /// <summary>
        /// Default ctor
        /// </summary>
        public AppForm()
        {
            base.Icon = Strings.train;
        }

        /// <summary>
        /// Override
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new Icon Icon { get; set; }

        /// <summary>
        /// Load time initialization
        /// </summary>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            // Load windows size
            Size formSize;
            Point formLocation;
            bool isMaximized;
            LoadFormPreferences(out formSize, out formLocation, out isMaximized);
            if (isMaximized)
            {
                WindowState = FormWindowState.Maximized;
            }
            else
            {
                var bounds = new Rectangle(formLocation, formSize);
                if (ScreenUtil.IsVisibleOnScreens(bounds))
                {
                    Bounds = bounds;
                }
            }
            initialized = true;
        }

        /// <summary>
        /// Save window location
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLocationChanged(EventArgs e)
        {
            base.OnLocationChanged(e);
            if (initialized)
            {
                SaveFormPreferences();
            }
        }

        /// <summary>
        /// Save window size
        /// </summary>
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            if (initialized)
            {
                SaveFormPreferences();
            }
        }

        /// <summary>
        /// Save the location, size and state of this form in user preferences.
        /// </summary>
        protected virtual void SaveFormPreferences()
        {
            // Overwrite me
        }

        /// <summary>
        /// Load the state of this form from user preferences
        /// </summary>
        protected virtual void LoadFormPreferences(out Size formSize, out Point formLocation, out bool isMaximized)
        {
            formSize = Size;
            formLocation = Location;
            isMaximized = (WindowState == FormWindowState.Maximized);
        }
    }
}

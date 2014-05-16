using System.Configuration;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using Microsoft.Win32;

namespace BinkyRailways.WinApp.Preferences
{
    /// <summary>
    /// User settings
    /// </summary>
    [SettingsProvider(typeof(CustomSettingsProvider))]
    internal sealed class UserPreferences : ApplicationSettingsBase
    {
        internal const string RegistryPrefsBase = @"Software\BinkyRailways\Preferences";

        private static UserPreferences instance;

        #region Public statics
        /// <summary>
        /// Gets the global instance.
        /// </summary>
        public static UserPreferences Preferences
        {
            get
            {
                if (instance == null)
                {
                    lock (typeof(UserPreferences))
                    {
                        if (instance == null)
                        {
                            instance = (UserPreferences)SettingsBase.Synchronized(new UserPreferences());
                        }
                    }
                }
                return instance;
            }
        }

        /// <summary>
        /// Save the preferences now
        /// </summary>
        public static void SaveNow()
        {
            if (instance != null)
            {
                instance.Save();
            }
        }
        #endregion

        /// <summary>
        /// Selected package path's as array of path's.
        /// Do not use directly
        /// </summary>
        [UserScopedSetting(),
        DefaultSettingValue(""),
        EditorBrowsable(EditorBrowsableState.Never)]
        public string RecentFiles
        {
            get { return (string)this["RecentFiles"]; }
            set { this["RecentFiles"] = value; }
        }

        /// <summary>
        /// Recently opened files.
        /// </summary>
        public string[] RecentFileList
        {
            get
            {
                var data = this.RecentFiles;
                return (data != null) ? data.Split(Path.PathSeparator) : new string[0]; 
            }
            set { this.RecentFiles = (value != null) ? string.Join(Path.PathSeparator.ToString(), value) : string.Empty; }
        }

        /// <summary>
        /// Top-left location of application window
        /// </summary>
        [UserScopedSetting, DefaultSettingValue("50,50")]
        public Point AppWindowLocation
        {
            get { return (Point)this["AppWindowLocation"]; }
            set { this["AppWindowLocation"] = value; }
        }

        /// <summary>
        /// Size of application window
        /// </summary>
        [UserScopedSetting, DefaultSettingValue("950,700")]
        public Size AppWindowSize
        {
            get { return (Size)this["AppWindowSize"]; }
            set { this["AppWindowSize"] = value; }
        }

        /// <summary>
        /// Is application window maximized?
        /// </summary>
        [UserScopedSetting, DefaultSettingValue("false")]
        public bool AppWindowMaximized
        {
            get { return (bool)this["AppWindowMaximized"]; }
            set { this["AppWindowMaximized"] = value; }
        }

        /// <summary>
        /// Top-left location of module editor window
        /// </summary>
        [UserScopedSetting, DefaultSettingValue("50,50")]
        public Point ModuleEditWindowLocation
        {
            get { return (Point)this["ModuleEditWindowLocation"]; }
            set { this["ModuleEditWindowLocation"] = value; }
        }

        /// <summary>
        /// Size of module editor window
        /// </summary>
        [UserScopedSetting, DefaultSettingValue("950,700")]
        public Size ModuleEditWindowSize
        {
            get { return (Size)this["ModuleEditWindowSize"]; }
            set { this["ModuleEditWindowSize"] = value; }
        }

        /// <summary>
        /// Is module editor window maximized?
        /// </summary>
        [UserScopedSetting, DefaultSettingValue("false")]
        public bool ModuleEditWindowMaximized
        {
            get { return (bool)this["ModuleEditWindowMaximized"]; }
            set { this["ModuleEditWindowMaximized"] = value; }
        }

        /// <summary>
        /// Top-left location of module editor window
        /// </summary>
        [UserScopedSetting, DefaultSettingValue("50,50")]
        public Point QuickEditorWindowLocation
        {
            get { return (Point)this["QuickEditorWindowLocation"]; }
            set { this["QuickEditorWindowLocation"] = value; }
        }

        /// <summary>
        /// Size of module editor window
        /// </summary>
        [UserScopedSetting, DefaultSettingValue("950,700")]
        public Size QuickEditorWindowSize
        {
            get { return (Size)this["QuickEditorWindowSize"]; }
            set { this["QuickEditorWindowSize"] = value; }
        }

        /// <summary>
        /// Top-left location of module editor window
        /// </summary>
        [UserScopedSetting, DefaultSettingValue("50,50")]
        public Point RouteInspectorWindowLocation
        {
            get { return (Point)this["RouteInspectorWindowLocation"]; }
            set { this["RouteInspectorWindowLocation"] = value; }
        }

        /// <summary>
        /// Size of module editor window
        /// </summary>
        [UserScopedSetting, DefaultSettingValue("863, 490")]
        public Size RouteInspectorWindowSize
        {
            get { return (Size)this["RouteInspectorWindowSize"]; }
            set { this["RouteInspectorWindowSize"] = value; }
        }

        /// <summary>
        /// Is application in run mode?
        /// </summary>
        [UserScopedSetting, DefaultSettingValue("false")]
        public bool RunMode
        {
            get { return (bool)this["RunMode"]; }
            set { this["RunMode"] = value; }
        }

        /// <summary>
        /// Show descriptions of items during run mode?
        /// </summary>
        [UserScopedSetting, DefaultSettingValue("false")]
        public bool RunShowDescriptions
        {
            get { return (bool)this["RunShowDescriptions"]; }
            set { this["RunShowDescriptions"] = value; }
        }

        /// <summary>
        /// Is application in virtual mode?
        /// This setting in only relevant if <see cref="RunMode"/> is true.
        /// </summary>
        [UserScopedSetting, DefaultSettingValue("false")]
        public bool VirtualMode
        {
            get { return (bool)this["VirtualMode"]; }
            set { this["VirtualMode"] = value; }
        }

        /// <summary>
        /// Zoom factor of the railway view in run mode?
        /// </summary>
        [UserScopedSetting, DefaultSettingValue("1")]
        public float RunViewZoomFactor
        {
            get { return (float)this["RunViewZoomFactor"]; }
            set { this["RunViewZoomFactor"] = value; }
        }

        /// <summary>
        /// Timestamp of last update check.
        /// </summary>
        [UserScopedSetting, DefaultSettingValue("0")]
        public long LastUpdateCheck
        {
            get { return (long)this["LastUpdateCheck"]; }
            set { this["LastUpdateCheck"] = value; }
        }

        /// <summary>
        /// Last pattern used in rename entities dialog.
        /// </summary>
        [UserScopedSetting, DefaultSettingValue("")]
        public string LastRenamePattern
        {
            get { return (string)this["LastRenamePattern"]; }
            set { this["LastRenamePattern"] = value; }
        }

        /// <summary>
        /// Language used.
        /// </summary>
        [UserScopedSetting, DefaultSettingValue("")]
        public string Locale
        {
            get { return (string)this["Locale"]; }
            set { this["Locale"] = value; }
        }

        /// <summary>
        /// License data.
        /// </summary>
        [UserScopedSetting, DefaultSettingValue("")]
        public string License
        {
            get { return (string)this["License"]; }
            set { this["License"] = value; }
        }

        /// <summary>
        /// Blocks node state in module editor.
        /// </summary>
        [UserScopedSetting, DefaultSettingValue("true")]
        public bool EditBlocksOpen
        {
            get { return (bool)this["EditBlocksOpen"]; }
            set { this["EditBlocksOpen"] = value; }
        }

        /// <summary>
        /// Block groups node state in module editor.
        /// </summary>
        [UserScopedSetting, DefaultSettingValue("true")]
        public bool EditBlockGroupsOpen
        {
            get { return (bool)this["EditBlockGroupsOpen"]; }
            set { this["EditBlockGroupsOpen"] = value; }
        }

        /// <summary>
        /// Junctions node state in module editor.
        /// </summary>
        [UserScopedSetting, DefaultSettingValue("true")]
        public bool EditJunctionsOpen
        {
            get { return (bool)this["EditJunctionsOpen"]; }
            set { this["EditJunctionsOpen"] = value; }
        }

        /// <summary>
        /// Sensors node state in module editor.
        /// </summary>
        [UserScopedSetting, DefaultSettingValue("true")]
        public bool EditSensorsOpen
        {
            get { return (bool)this["EditSensorsOpen"]; }
            set { this["EditSensorsOpen"] = value; }
        }

        /// <summary>
        /// Signals node state in module editor.
        /// </summary>
        [UserScopedSetting, DefaultSettingValue("true")]
        public bool EditSignalsOpen
        {
            get { return (bool)this["EditSignalsOpen"]; }
            set { this["EditSignalsOpen"] = value; }
        }

        /// <summary>
        /// Edges node state in module editor.
        /// </summary>
        [UserScopedSetting, DefaultSettingValue("true")]
        public bool EditEdgesOpen
        {
            get { return (bool)this["EditEdgesOpen"]; }
            set { this["EditEdgesOpen"] = value; }
        }

        /// <summary>
        /// Routes node state in module editor.
        /// </summary>
        [UserScopedSetting, DefaultSettingValue("true")]
        public bool EditRoutesOpen
        {
            get { return (bool)this["EditRoutesOpen"]; }
            set { this["EditRoutesOpen"] = value; }
        }

        /// <summary>
        /// Outputs node state in module editor.
        /// </summary>
        [UserScopedSetting, DefaultSettingValue("true")]
        public bool EditOutputsOpen
        {
            get { return (bool)this["EditOutputsOpen"]; }
            set { this["EditOutputsOpen"] = value; }
        }

        /// <summary>
        /// Locs node state in railway editor.
        /// </summary>
        [UserScopedSetting, DefaultSettingValue("true")]
        public bool EditLocsOpen
        {
            get { return (bool)this["EditLocsOpen"]; }
            set { this["EditLocsOpen"] = value; }
        }

        /// <summary>
        /// Loc groups node state in railway editor.
        /// </summary>
        [UserScopedSetting, DefaultSettingValue("true")]
        public bool EditLocGroupsOpen
        {
            get { return (bool)this["EditLocGroupsOpen"]; }
            set { this["EditLocGroupsOpen"] = value; }
        }

        /// <summary>
        /// Modules node state in railway editor.
        /// </summary>
        [UserScopedSetting, DefaultSettingValue("true")]
        public bool EditModulesOpen
        {
            get { return (bool)this["EditModulesOpen"]; }
            set { this["EditModulesOpen"] = value; }
        }

        /// <summary>
        /// Modules connections node state in railway editor.
        /// </summary>
        [UserScopedSetting, DefaultSettingValue("true")]
        public bool EditModuleConnectionsOpen
        {
            get { return (bool)this["EditModuleConnectionsOpen"]; }
            set { this["EditModuleConnectionsOpen"] = value; }
        }

        /// <summary>
        /// Command stations node state in railway editor.
        /// </summary>
        [UserScopedSetting, DefaultSettingValue("true")]
        public bool EditCommandStationsOpen
        {
            get { return (bool)this["EditCommandStationsOpen"]; }
            set { this["EditCommandStationsOpen"] = value; }
        }

        /// <summary>
        /// Sorting of routes in module editor.
        /// </summary>
        [UserScopedSetting, DefaultSettingValue("true")]
        public bool SortRoutesByFrom
        {
            get { return (bool)this["SortRoutesByFrom"]; }
            set { this["SortRoutesByFrom"] = value; }
        }

        /// <summary>
        /// Settings provider for user prefs.
        /// </summary>
        private sealed class CustomSettingsProvider : RegistryAssemblySettingsProvider
        {
            private static readonly string[] KEYS = new[] { RegistryPrefsBase };

            /// <summary>
            /// Try to read the latest path
            /// </summary>
            /// <param name="writable"></param>
            /// <returns></returns>
            protected override string GetSubKeyPath(bool writable)
            {
                // Always write to latest key
                if (writable) { return KEYS[0]; }

                // Read from the latest available key
                foreach (var key in KEYS)
                {
                    var regKey = Registry.CurrentUser.OpenSubKey(key);
                    if (regKey != null) { regKey.Close(); return key; }
                }

                // No key found, use latest
                return KEYS[0];
            }
        }
    }
}

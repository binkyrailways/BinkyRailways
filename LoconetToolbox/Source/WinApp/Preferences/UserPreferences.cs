/*
Loconet toolbox
Copyright (C) 2010 Modelspoorgroep Venlo, Ewout Prangsma

This program is free software; you can redistribute it and/or
modify it under the terms of the GNU General Public License
as published by the Free Software Foundation; either version 2
of the License, or (at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program; if not, write to the Free Software
Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA.
*/
using System;
using System.Configuration;
using System.ComponentModel;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Text;
using Microsoft.Win32;

namespace LocoNetToolBox.WinApp.Preferences
{
    /// <summary>
    /// User settings
    /// </summary>
    [SettingsProvider(typeof(CustomSettingsProvider))]
    internal sealed class UserPreferences : ApplicationSettingsBase
    {
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
        /// Portname to which the locobuffer is attached
        /// </summary>
        [UserScopedSetting(),
        DefaultSettingValue("")]
        public string PortName
        {
            get { return (string)this["PortName"]; }
            set { this["PortName"] = value; }
        }

        /// <summary>
        /// Path of LocoNetConfiguration file.
        /// </summary>
        [UserScopedSetting(),
        DefaultSettingValue("")]
        public string LocoNetConfigurationPath
        {
            get { return (string)this["LocoNetConfigurationPath"]; }
            set { this["LocoNetConfigurationPath"] = value; }
        }

        /// <summary>
        /// Settings provider for user prefs.
        /// </summary>
        private sealed class CustomSettingsProvider : RegistryAssemblySettingsProvider
        {
            private static readonly string[] KEYS = new string[] { @"Software\MGV\LoconetToolbox\1.0\Preferences" };

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

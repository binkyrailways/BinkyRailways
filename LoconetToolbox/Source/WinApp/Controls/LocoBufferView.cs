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
using System.Windows.Forms;

using LocoNetToolBox.Protocol;
using LocoNetToolBox.WinApp.Communications;

namespace LocoNetToolBox.WinApp.Controls
{
    public partial class LocoBufferView : UserControl
    {
        public event EventHandler LocoBufferChanged;

        private AppState appState;

        /// <summary>
        /// View on the locobuffer.
        /// </summary>
        public LocoBufferView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Connect to the locobuffer.
        /// </summary>
        internal LocoBuffer ConfiguredLocoBuffer
        {
            get { return locoBufferSettings.LocoBuffer; }
        }

        /// <summary>
        /// Attach to the given application.
        /// </summary>
        internal AppState AppState
        {
            set
            {
                if (appState != null)
                {
                    appState.LocoNetChanged -= AppStateLocoNetChanged;
                }
                appState = value;
                if (appState != null)
                {
                    appState.LocoNetChanged += AppStateLocoNetChanged;
                }
                powerCommandControl1.AppState = value;
                AppStateLocoNetChanged(null, null);
            }
        }

        private void AppStateLocoNetChanged(object sender, EventArgs e)
        {
            locoBufferSettings.LocoBuffer = (appState != null) ? appState.ConfiguredLocoBuffer : null;
        }

        /// <summary>
        /// Locobuffer has changed.
        /// Propagate the event.
        /// </summary>
        private void LocoBufferSettingsLocoBufferChanged(object sender, EventArgs e)
        {
            LocoBufferChanged.Fire(this);
        }
    }
}

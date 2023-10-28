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
using LocoNetToolBox.Devices.LocoIO;
using LocoNetToolBox.Model;
using LocoNetToolBox.Protocol;

namespace LocoNetToolBox.WinApp.Controls
{
    public partial class LocoIOList : UserControl
    {
        /// <summary>
        /// Selected address has changed.
        /// </summary>
        public event EventHandler SelectionChanged;

        private AppState appState;

        /// <summary>
        /// Default ctor
        /// </summary>
        public LocoIOList()
        {
            InitializeComponent();
            UpdateUI();
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
                    appState.LocoIOQuery -= LnStateLocoIoQuery;
                    appState.LocoIOFound -= LnStateLocoIoFound;
                }
                appState = value;
                lbModules.Items.Clear();
                if (appState != null)
                {
                    appState.LocoIOQuery += LnStateLocoIoQuery;
                    appState.LocoIOFound += LnStateLocoIoFound;
                }
            }
        }

        /// <summary>
        /// Gets the selected address.
        /// Returns null if there is no selection.
        /// </summary>
        public LocoNetAddress SelectedAddress
        {
            get
            {
                if (lbModules.SelectedItems.Count > 0)
                {
                    var item = lbModules.SelectedItems[0];
                    return (LocoNetAddress)item.Tag;
                }
                return null;
            }
        }

        /// <summary>
        /// New LocoIO unit found
        /// </summary>
        void LnStateLocoIoFound(object sender, LocoIOEventArgs e)
        {
            var item = new ListViewItem();
            var entry = e.LocoIO;
            item.Text = entry.Address.ToString();
            item.Tag = entry.Address;
            item.SubItems.Add(entry.Version);
            lbModules.Items.Add(item);
            lbModules.Sort();
            item.Selected = true;
        }

        /// <summary>
        /// Query for locoIO units.
        /// </summary>
        void LnStateLocoIoQuery(object sender, EventArgs e)
        {
            // Query request
            lbModules.Items.Clear();
        }

        /// <summary>
        /// Selection has changed
        /// </summary>
        private void LbModulesSelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateUI();
            SelectionChanged.Fire(this);
        }

        /// <summary>
        /// Program active address
        /// </summary>
        private void LbModulesItemActivate(object sender, EventArgs e)
        {
            CmdConfigureMgv50Click(sender, e);
        }

        /// <summary>
        /// Configure the selected MGV50.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CmdConfigureMgv50Click(object sender, EventArgs e)
        {
            var currentAddress = SelectedAddress;
            if (currentAddress != null)
            {
                var dialog = new LocoIOConfigurationForm();
                dialog.Initialize(appState.LocoBuffer, currentAddress);
                dialog.Show(this);
            }
        }

        /// <summary>
        /// Advanced MGV50 configuration
        /// </summary>
        private void CmdConfigMgv50AdvancedClick(object sender, EventArgs e)
        {
            var currentAddress = SelectedAddress;
            if (currentAddress != null)
            {
                var dialog = new LocoIOAdvancedConfigurationForm();
                dialog.Initialize(appState.LocoBuffer, currentAddress);
                dialog.Show(this);
            }
        }

        /// <summary>
        /// Change address of selected MGV50
        /// </summary>
        private void cmdChangeAddress_Click(object sender, EventArgs e)
        {
            var currentAddress = SelectedAddress;
            if (currentAddress != null)
            {
                var dialog = new LocoIOChangeAddressForm();
                dialog.Initialize(appState.LocoBuffer, currentAddress);
                dialog.Show(this);
            }
        }

        /// <summary>
        /// Update the controls
        /// </summary>
        private void UpdateUI()
        {
            var hasAddress = (SelectedAddress != null);
            cmdConfigureMgv50.Enabled = hasAddress;
            cmdConfigMgv50Advanced.Enabled = hasAddress;
            cmdChangeAddress.Enabled = hasAddress && (lbModules.Items.Count == 1);
        }
    }
}

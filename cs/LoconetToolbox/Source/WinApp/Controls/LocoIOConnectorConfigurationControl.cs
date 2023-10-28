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
using System.Linq;
using System.Windows.Forms;

using LocoNetToolBox.Devices.LocoIO;

namespace LocoNetToolBox.WinApp.Controls
{
    public partial class LocoIOConnectorConfigurationControl : UserControl
    {
        private readonly Label[] labels;
        private readonly NumericUpDown[] addressControls;
        private readonly AddressList addresses;
        private Connector connector;
        private int oldAddress;

        /// <summary>
        /// Default ctor
        /// </summary>
        public LocoIOConnectorConfigurationControl()
        {
            InitializeComponent();

            labels = new[] { lbPin1, lbPin2, lbPin3, lbPin4, lbPin5, lbPin6, lbPin7, lbPin8 };
            addressControls = new[] { tbAddr1, tbAddr2, tbAddr3, tbAddr4, tbAddr5, tbAddr6, tbAddr7, tbAddr8 };
            addresses = new AddressList(8);         

            for (int i = 0; i < 8; i++)
            {
                var tbAddr = addressControls[i];
                tbAddr.Value = addresses[i];
                var index = i;
                tbAddr.GotFocus += (s, x) => { 
                    tbAddr.Select(0, tbAddr.Value.ToString().Length);
                    oldAddress = (int)tbAddr.Value;
                };
                tbAddr.ValueChanged += (s, x) => {
                    addresses[index] = (int) tbAddr.Value;
                    var newAddress = (int)tbAddr.Value;
                    var tbNextAddr = (index + 1 < 8) ? addressControls[index + 1] : null;
                    if ((tbNextAddr != null) && (tbNextAddr.Value == oldAddress +1))
                    {
                        // Adjust next address
                        oldAddress = (int)tbNextAddr.Value;
                        tbNextAddr.Value = newAddress + 1;
                    }
                };
            }

            Connector = Connector.First;
            lvModes.Items.AddRange(ConnectorMode.All.Select(x => new ListViewItem(x.Name) { Tag = x }).ToArray());
            UpdateUI(true);
        }

        /// <summary>
        /// Create a configuration from the settings found in this control.
        /// </summary>
        public ConnectorConfig CreateConfig()
        {
            var mode = SelectedMode ?? ConnectorMode.None;
            var pinOffset = mode.GetPinOffset(SelectedSubMode);
            var usedAddresses = new AddressList(8 - pinOffset);
            for (var i = pinOffset; i < 8; i++)
            {
                usedAddresses[i - pinOffset] = addresses[i];
            }
            return mode.CreateConfig(connector, usedAddresses, SelectedSubMode);
        }

        /// <summary>
        /// Connect this control to the given configuration
        /// </summary>
        public void Connect(ConnectorConfig config)
        {
        }

        /// <summary>
        /// Sets the connector, used to set the pins.
        /// </summary>
        public Connector Connector
        {
            set
            {
                connector = value;
                var firstPin = (value == Connector.First) ? 1 : 9;
                for (int i = 0; i < 8; i++)
                {
                    labels[i].Text = (firstPin + i).ToString();
                }
            }
        }

        /// <summary>
        /// Gets the currently selected connector mode.
        /// </summary>
        private ConnectorMode SelectedMode
        {
            get 
            { 
                if (lvModes.SelectedItems.Count == 0)
                    return null;
                var selection = lvModes.SelectedItems[0];
                return (ConnectorMode) selection.Tag;
            }
        }

        /// <summary>
        /// Gets the currentlty selected sub mode
        /// </summary>
        private int SelectedSubMode
        {
            get { return Math.Max(0, cbSubMode.SelectedIndex); }
        }

        /// <summary>
        /// Update the ui controls
        /// </summary>
        private void UpdateUI(bool updateSubModes)
        {
            SuspendLayout();
            tlpMain.SuspendLayout();
            try
            {
                var selection = SelectedMode;
                var addressCount = (selection != null) ? selection.AddressCount : 0;
                var firstPin = (connector == Connector.First) ? 1 : 9;
                var pinOffset = (selection != null) ? selection.GetPinOffset(SelectedSubMode) : 0;

                for (int i = 0; i < 8; i++)
                {
                    var visible = (i >= pinOffset) && (i < addressCount + pinOffset);
                    labels[i].Enabled = visible;
                    addressControls[i].Enabled = visible;

                    var text = (firstPin + i).ToString();
                    if (visible && (selection != null))
                    {
                        text = selection.GetAddressName(i - pinOffset);
                    }
                    labels[i].Text = text;
                }

                if (updateSubModes)
                {
                    var index = cbSubMode.SelectedIndex;
                    cbSubMode.Items.Clear();
                    if (selection != null)
                    {
                        cbSubMode.Items.AddRange(selection.SubModes);
                    }
                    var hasSubModes = cbSubMode.Items.Count > 0;
                    cbSubMode.Enabled = hasSubModes;
                    if (hasSubModes)
                    {
                        cbSubMode.SelectedIndex = Math.Max(0, index < cbSubMode.Items.Count ? index : 0);
                    }
                }
            }
            finally
            {
                tlpMain.PerformLayout();
                tlpMain.ResumeLayout(true);
                ResumeLayout(true);
            }
        }

        /// <summary>
        /// Selected connector mode has changed.
        /// </summary>
        private void lvModes_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateUI(true);
        }

        /// <summary>
        /// Selected submit has changed
        /// </summary>
        private void cbSubMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateUI(false);
        }
    }
}

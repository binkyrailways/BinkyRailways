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

namespace LocoNetToolBox.WinApp.Controls
{
    /// <summary>
    /// LocoIO Single port editor.
    /// </summary>
    public partial class LocoIOPinConfigurationControl : UserControl
    {
        public event EventHandler Read;
        public event EventHandler Write;

        private bool initialized;

        /// <summary>
        /// Default ctor
        /// </summary>
        public LocoIOPinConfigurationControl()
        {
            InitializeComponent();
            Pin = 1;
            initialized = true;
        }

        /// <summary>
        /// My pin number (1..16)
        /// </summary>
        public int Pin { get; set; }

        /// <summary>
        /// Connect this control to the given config.
        /// </summary>
        public void LoadFrom(PinConfig config)
        {
            try
            {
                initialized = false;
                tbAddr.Value = config.Address;
                modeControl.Mode = config.Mode;
            }
            finally
            {
                initialized = true;
                UpdateUI();
            }
        }

        /// <summary>
        /// Create a configuration
        /// </summary>
        /// <returns></returns>
        public PinConfig CreateConfig()
        {
            var config = new PinConfig(Pin);
            config.Address = (int)tbAddr.Value;
            config.Mode = modeControl.Mode;
            return config;
        }

        /// <summary>
        /// Update the visibility of the UI components.
        /// </summary>
        private void UpdateUI()
        {
            if (initialized)
            {
                var addr = (int)tbAddr.Value;
                var mode = modeControl.Mode;
                tbConfig.Text = (mode != null) ? mode.GetConfig().ToString() : string.Empty;
                tbValue1.Text = (mode != null) ? mode.GetValue1(addr).ToString() : string.Empty;
                tbValue2.Text = (mode != null) ? mode.GetValue2(addr).ToString() : string.Empty;
                cmdNotUsed.Enabled = (mode != PinMode.BlockOccupied) || (addr != 2048);
            }
        }

        /// <summary>
        /// Store address.
        /// </summary>
        private void TbAddressValueChanged(object sender, EventArgs e)
        {
            if (initialized)
            {
                UpdateUI();
            }
        }

        /// <summary>
        /// Input settings have changed.
        /// </summary>
        private void OnConfigChanged(object sender, EventArgs e)
        {
            if (initialized)
            {
                UpdateUI();
            }
        }

        private void cmdRead_Click(object sender, EventArgs e)
        {
            Read.Fire(this);
        }

        private void cmdWrite_Click(object sender, EventArgs e)
        {
            Write.Fire(this);
        }

        private void cmdNotUsed_Click(object sender, EventArgs e)
        {
            modeControl.Mode = PinMode.BlockOccupied;
            tbAddr.Value = 2048;
            Write.Fire(this);
            UpdateUI();
        }
    }
}

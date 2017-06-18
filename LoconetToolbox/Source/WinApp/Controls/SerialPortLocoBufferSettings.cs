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
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using LocoNetToolBox.WinApp.Preferences;

namespace LocoNetToolBox.WinApp.Controls
{
    public partial class SerialPortLocoBufferSettings : UserControl
    {
        private Protocol.SerialPortLocoBuffer locoBuffer;
        private bool initialized = false;

        /// <summary>
        /// Default ctor
        /// </summary>
        public SerialPortLocoBufferSettings()
        {
            InitializeComponent();

            // First ports
            var ports = SerialPort.GetPortNames();
            cbPort.Items.AddRange(ports);
            if (ports.Length > 0) { cbPort.SelectedIndex = 0; }
            rbRate57K.Checked = true;

            var port = UserPreferences.Preferences.PortName;
            if (ports.Contains(port))
            {
                cbPort.SelectedItem = port;
            }
            initialized = true;
        }

        /// <summary>
        /// Set data
        /// </summary>
        internal Protocol.SerialPortLocoBuffer LocoBuffer
        {
            set
            {
                if (this.locoBuffer != null)
                {
                    locoBuffer.Closed -= LocoBufferClosed;
                    locoBuffer.Opened -= LocoBufferOpened;
                }
                locoBuffer = value;
                Save();
                if (locoBuffer != null)
                {
                    locoBuffer.Closed += LocoBufferClosed;
                    locoBuffer.Opened += LocoBufferOpened;
                }
            }
        }

        void LocoBufferOpened(object sender, EventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke(new EventHandler(LocoBufferOpened), sender, e);
            }
            else
            {
                Enabled = false;
            }
        }

        void LocoBufferClosed(object sender, EventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke(new EventHandler(LocoBufferClosed), sender, e);
            }
            else
            {
                Enabled = true;
            }
        }

        /// <summary>
        /// Save settings into lb.
        /// </summary>
        private void Save()
        {
            var lb = this.locoBuffer;
            if (lb != null) {
                // Close first
                lb.Close();
                // Baudrate
                if (rbRate57K.Checked) { lb.BaudRate = LocoNetToolBox.Protocol.BaudRate.Rate57K; }
                else if (rbRate19K.Checked) { lb.BaudRate = LocoNetToolBox.Protocol.BaudRate.Rate19K; }
                // Port
                if (cbPort.SelectedIndex >= 0)
                {
                    lb.PortName = (string)cbPort.SelectedItem;
                }
            }
        }

        /// <summary>
        /// Baudrate changed
        /// </summary>
        private void rbRate_CheckedChanged(object sender, EventArgs e)
        {
            Save();
        }

        private void cbPort_SelectedIndexChanged(object sender, EventArgs e)
        {
            Save();
            if (initialized)
            {
                var port = (string)cbPort.SelectedItem;
                if (port != null)
                {
                    UserPreferences.Preferences.PortName = port;
                    UserPreferences.SaveNow();
                }
            }
        }
    }
}

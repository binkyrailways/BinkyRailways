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
using System.ComponentModel;
using System.Net;
using System.Windows.Forms;

namespace LocoNetToolBox.WinApp.Controls
{
    public partial class TcpLocoBufferSettings : UserControl
    {
        private Protocol.TcpLocoBuffer locoBuffer;
        private bool initialized = false;

        /// <summary>
        /// Default ctor
        /// </summary>
        public TcpLocoBufferSettings()
        {
            InitializeComponent();

            // First ports
            initialized = true;
        }

        /// <summary>
        /// Set data
        /// </summary>
        internal Protocol.TcpLocoBuffer LocoBuffer
        {
            set
            {
                if (locoBuffer != null)
                {
                    locoBuffer.Closed -= LocoBufferClosed;
                    locoBuffer.Opened -= LocoBufferOpened;
                }
                locoBuffer = value;
                if (value != null)
                {
                    tbIpAddress.Text = value.IpAddress;
                    tbPort.Text = value.Port.ToString();
                }
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
            var lb = locoBuffer;
            if (lb != null) {
                // Close first
                lb.Close();
                // copy settings to lb.
                lb.IpAddress = tbIpAddress.Text.Trim();
                lb.Port = int.Parse(tbPort.Text);
            }
        }

        private void tbIpAddress_Validated(object sender, EventArgs e)
        {
            Save();
        }

        private void tbPort_Validated(object sender, EventArgs e)
        {
            Save();
        }

        private void tbIpAddress_Validating(object sender, CancelEventArgs e)
        {
            var text = tbIpAddress.Text;
            IPAddress addr;
            if (!IPAddress.TryParse(text, out addr))
                e.Cancel = true;
        }

        private void tbPort_Validating(object sender, CancelEventArgs e)
        {
            var text = tbPort.Text;
            int port;
            if (!int.TryParse(text, out port))
                e.Cancel = true;
        }
    }
}

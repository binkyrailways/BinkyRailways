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

namespace LocoNetToolBox.WinApp.Controls
{
    public partial class LocoBufferSettings : UserControl
    {
        public event EventHandler LocoBufferChanged;

        private LocoBuffer lb;

        /// <summary>
        /// Default ctor
        /// </summary>
        public LocoBufferSettings()
        {
            InitializeComponent();
            serialPortSettings.Visible = false;
            tcpSettings.Visible = false;
        }

        /// <summary>
        /// Set data
        /// </summary>
        internal LocoBuffer LocoBuffer
        {
            get { return lb; }
            set
            {
                if (lb != value)
                {
                    lb = value;
                    serialPortSettings.LocoBuffer = value as SerialPortLocoBuffer;
                    tcpSettings.LocoBuffer = value as TcpLocoBuffer;
                    udpSettings.LocoBuffer = value as UdpLocoBuffer;
                    serialPortSettings.Visible = (value is SerialPortLocoBuffer);
                    tcpSettings.Visible = (value is TcpLocoBuffer);
                    udpSettings.Visible = (value is UdpLocoBuffer);
                    LocoBufferChanged.Fire(this);
                }
            }
        }

        /// <summary>
        /// Change the type of locobuffer.
        /// </summary>
        private void OnChangeLocoBufferType(object sender, EventArgs e)
        {
            if (rbSerialPort.Checked)
            {
                if (!(LocoBuffer is SerialPortLocoBuffer))
                    LocoBuffer = new SerialPortLocoBuffer();
            }
            else if (rbTcp.Checked)
            {
                if (!(LocoBuffer is TcpLocoBuffer))
                    LocoBuffer = new TcpLocoBuffer();
            }
            else if (rbUdp.Checked)
            {
                if (!(LocoBuffer is UdpLocoBuffer))
                    LocoBuffer = new UdpLocoBuffer();
            }
        }
    }
}

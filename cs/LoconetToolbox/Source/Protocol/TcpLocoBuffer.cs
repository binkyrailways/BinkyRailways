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
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.IO.Ports;

namespace LocoNetToolBox.Protocol
{
    /// <summary>
    /// LocoBuffer communication based on TCP/IP
    /// </summary>
    public sealed class TcpLocoBuffer : LocoBuffer 
    {
        private const int CTS_ATTEMPTS = 20;

        private readonly object portLock = new object();
        private TcpClient tcpClient;

        /// <summary>
        /// Default ctor
        /// </summary>
        internal TcpLocoBuffer()
        {
            IpAddress = "192.168.0.200";
            Port = 1235;
        }

        /// <summary>
        /// IP address of locobuffer.
        /// </summary>
        public string IpAddress { get; set; }

        /// <summary>
        /// Port number.
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// Open the connection (if needed)
        /// </summary>
        private TcpClient Open()
        {
            lock (portLock)
            {
                if (tcpClient == null)
                {
                    try
                    {
                        var addr = IPAddress.Parse(IpAddress);
                        tcpClient = new TcpClient();
                        tcpClient.NoDelay = true;
                        tcpClient.Connect(new IPEndPoint(addr, Port));
                        OnOpened();
                    }
                    catch
                    {
                        tcpClient = null;
                        throw;
                    }
                }
                return tcpClient;
            }
        }

        /// <summary>
        /// Close the connection (if any)
        /// </summary>
        public override void Close()
        {
            lock (portLock)
            {
                if (tcpClient != null)
                {
                    tcpClient.Close();
                    tcpClient = null;
                    OnClosed();
                }
            }
        }

        /// <summary>
        /// Send the given message
        /// </summary>
        protected override void Send(byte[] msg, int length)
        {
            var port = Open();
            var stream = tcpClient.GetStream();
            stream.Write(msg, 0, length);
        }

        /// <summary>
        /// Read a single message
        /// </summary>
        protected override byte[] ReadMessage()
        {
            // Check for an open connection
            var port = this.tcpClient;
            if (port == null) { return null; }

            // Try to read the message
            var data = new byte[16];
            while (true)
            {
                // Read a potential opcode

                Read(port, data, 0, 1);
                if (Message.IsOpcode(data[0]))
                {
                    break;
                }
            }

            // Now read further data
            Read(port, data, 1, 1); // Read at least the second byte

            var length = Message.GetMessageLength(data);
            if (length > 2)
            {
                // Read remaining data
                Read(port, data, 2, length - 2);
            }

            return data;
        }

        private static void Read(TcpClient port, byte[] data, int offset, int count)
        {
            var stream = port.GetStream();
            while (count > 0)
            {

                var read = stream.Read(data, offset, count);
                if (read > 0)
                {
                    count -= read;
                    offset += read;
                } 
            }
        }
    }
}

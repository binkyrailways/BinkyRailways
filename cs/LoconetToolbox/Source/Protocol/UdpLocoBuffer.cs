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
using System.Net;
using System.Net.Sockets;

namespace LocoNetToolBox.Protocol
{
    /// <summary>
    /// LocoBuffer communication based on UDP (MGV101 v2.1)
    /// </summary>
    public sealed class UdpLocoBuffer : LocoBuffer 
    {
        private const int CTS_ATTEMPTS = 20;

        private readonly object portLock = new object();
        private Socket udpSocket;

        /// <summary>
        /// Default ctor
        /// </summary>
        internal UdpLocoBuffer()
        {
            IpAddress = "224.0.0.1";
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
        private Socket Open()
        {
            lock (portLock)
            {
                if (udpSocket == null)
                {
                    try
                    {
                        var addr = IPAddress.Parse(IpAddress);
                        var localAddr = IPAddress.Any;//.Parse("192.168.0.10");

                        var iPEndPoint = new IPEndPoint(localAddr, Port);
                        udpSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                        udpSocket.SetSocketOption(SocketOptionLevel.Udp, SocketOptionName.Debug, 1);
                        udpSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, 1);
                        udpSocket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.MulticastTimeToLive, 1);
                        udpSocket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.MulticastLoopback, 0);
                        udpSocket.Bind(iPEndPoint);
                        udpSocket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.AddMembership, new MulticastOption(addr, localAddr));
                        OnOpened();
                    }
                    catch
                    {
                        udpSocket = null;
                        throw;
                    }
                }
                return udpSocket;
            }
        }

        /// <summary>
        /// Close the connection (if any)
        /// </summary>
        public override void Close()
        {
            lock (portLock)
            {
                if (udpSocket != null)
                {
                    udpSocket.Close();
                    udpSocket = null;
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
            var remoteEndPoint = new IPEndPoint(IPAddress.Parse(IpAddress), Port);
            udpSocket.SendTo(msg, length, SocketFlags.None, remoteEndPoint);
        }

        /// <summary>
        /// Read a single message
        /// </summary>
        protected override byte[] ReadMessage()
        {
            // Check for an open connection
            var port = this.udpSocket;
            if (port == null) { return null; }

            // Try to read the message
            var data = new byte[16];
            while (true)
            {
                // Read a potential opcode
                udpSocket.Receive(data);
                if (Message.IsOpcode(data[0]))
                {
                    break;
                }

                if (data[0] == 113)
                {
                    
                }
            }

            /*var length = Message.GetMessageLength(data);
            if (length > 2)
            {
                // Read remaining data
                Read(port, data, 2, length - 2);
            }*/

            return data;
        }
    }
}

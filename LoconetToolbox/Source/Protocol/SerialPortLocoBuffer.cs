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
using System.Linq;
using System.Text;
using System.IO.Ports;
using System.Threading;

namespace LocoNetToolBox.Protocol
{
    /// <summary>
    /// LocoBuffer communication.
    /// </summary>
    public sealed class SerialPortLocoBuffer : LocoBuffer 
    {
        private const int CTS_ATTEMPTS = 20;

        private readonly object portLock = new object();
        private SerialPort port;

        /// <summary>
        /// Default ctor
        /// </summary>
        public SerialPortLocoBuffer()
        {
            this.PortName = "COM1";
            this.BaudRate = BaudRate.Rate57K;
        }

        /// <summary>
        /// Name of the serial port (e.g. COM1)
        /// </summary>
        public string PortName { get; set; }

        /// <summary>
        /// Baud rate of the port
        /// </summary>
        public BaudRate BaudRate { get; set; }

        /// <summary>
        /// Open the connection (if needed)
        /// </summary>
        private SerialPort Open()
        {
            lock (portLock)
            {
                if (port == null)
                {
                    try
                    {
                        port = new SerialPort(PortName);
                        port.BaudRate = (int)BaudRate;
                        port.DtrEnable = false;
                        port.RtsEnable = true;
                        //port.Handshake = Handshake.RequestToSend;
                        port.DataBits = 8;
                        port.StopBits = StopBits.One;
                        port.Parity = Parity.None;
                        port.Open();
                        port.DiscardInBuffer();
                        port.DiscardOutBuffer();
                        OnOpened();
                    }
                    catch
                    {
                        port = null;
                        throw;
                    }
                }
                return port;
            }
        }

        /// <summary>
        /// Close the connection (if any)
        /// </summary>
        public override void Close()
        {
            lock (portLock)
            {
                var p = port;
                port = null;
                if (p != null)
                {
                    p.Close();
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
            for (int i = 0; i < length; i++)
            {
                WaitForCTS(port);
                port.Write(msg, i, 1);
            }
        }

        /// <summary>
        /// Wait for CTS to be enabled.
        /// </summary>
        private static void WaitForCTS(SerialPort port)
        {
            int attempts = 0;
            while (!port.CtsHolding)
            {
                Thread.Sleep(10);
                attempts++;
                if (attempts > CTS_ATTEMPTS)
                {
                    throw new InvalidOperationException("CTS timeout");
                }
            }
        }

        /// <summary>
        /// Read a single message
        /// </summary>
        protected override byte[] ReadMessage()
        {
            // Check for an open connection
            var port = this.port;
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

        private static void Read(SerialPort port, byte[] data, int offset, int count)
        {
            while (count > 0)
            {
                var read = port.Read(data, offset, count);
                if (read > 0)
                {
                    count -= read;
                    offset += read;
                } 
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BinkyRailways.Protocols.P50x
{
    /// <summary>
    /// P50x protocol connection
    /// </summary>
    public class Connection : IDisposable
    {
        public delegate void Callback();

        public event Callback Opened;
        public event Callback Closed;

        private const int CTS_ATTEMPTS = 20;

        private readonly object portLock = new object();
        private SerialPort port;

        /// <summary>
        /// Default ctor
        /// </summary>
        public Connection()
        {
            this.PortName = "COM1";
            this.BaudRate = BaudRate.Rate9K6;
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
                        if (Opened != null)
                        {
                            Opened();
                        }
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
        public void Close()
        {
            lock (portLock)
            {
                var p = port;
                port = null;
                if (p != null)
                {
                    p.Close();
                    if (Closed != null)
                    {
                        Closed();
                    }
                }
            }
        }

        /// <summary>
        /// Send the given message
        /// </summary>
        protected void Send(byte[] msg, int length)
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
        protected byte[] ReadMessage(int len)
        {
            // Check for an open connection
            var port = this.port;
            if (port == null) { return null; }

            // Try to read the message
            var data = new byte[len];
            Read(port, data, 0, len);

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

        public void Dispose()
        {
            Close();
        }
    }
}

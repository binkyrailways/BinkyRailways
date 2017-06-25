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
        /// <summary>
        /// Idle is set to true by this class, you must set it to false.
        /// </summary>
        public bool Idle { get; set; }

        public delegate void Callback();

        public event Callback Opened;
        public event Callback Closed;

        protected enum ReplyLength
        {
            // Reply has fixed length
            Fixed,
            // High bit of byte set if another byte follows
            Bit7,
            // First byte contains length of response
            FirstByte
        }

        private const int CTS_ATTEMPTS = 50;

        private readonly object portLock = new object();
        private SerialPort port;

        /// <summary>
        /// Default ctor
        /// </summary>
        public Connection()
        {
            this.PortName = "COM1";
            this.BaudRate = BaudRate.Rate19K;
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
                        port.WriteTimeout = 1000;
                        port.ReadTimeout = 1000;
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

        protected byte[] FixedTransaction(byte[] msg, int fixedReplyLength)
        {
            return Transaction(msg, ReplyLength.Fixed, fixedReplyLength);
        }

        protected byte[] Bit7Transaction(byte[] msg)
        {
            return Transaction(msg, ReplyLength.Bit7, 0);
        }

        protected byte[] Transaction(byte[] msg, ReplyLength replyLength, int fixedReplyLength)
        {
            Idle = false;
            var port = Open();
            try
            {
                flushAvailableData(port);
                Send(port, msg, msg.Length);
                return ReadMessage(port, replyLength, fixedReplyLength);
            }
            catch
            {
                Close();
                throw;
            }
        }

        /// <summary>
        /// Send the given message
        /// </summary>
        private static void Send(SerialPort port, byte[] msg, int length)
        {
            for (int i = 0; i < length; i++)
            {
                WaitForCTS(port);
                port.Write(msg, i, 1);
            }
        }

        private static void flushAvailableData(SerialPort port)
        {
            port.DiscardInBuffer();
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
        private static byte[] ReadMessage(SerialPort port, ReplyLength replyLength, int fixedReplyLength)
        {
            // Try to read the message
            switch (replyLength)
            {
                case ReplyLength.Fixed:
                    {
                        var data = new byte[fixedReplyLength];
                        Read(port, data, 0, fixedReplyLength);
                        return data;
                    }
                case ReplyLength.Bit7:
                    {
                        var data = new byte[1];
                        var offset = 0;
                        while (true)
                        {
                            Read(port, data, offset, 1);
                            if ((data[offset] & 0x80) == 0)
                            {
                                return data;
                            }
                            var oldData = data;
                            data = new byte[data.Length + 1];
                            Array.Copy(oldData, data, oldData.Length);
                            offset++;
                        }
                    }
                case ReplyLength.FirstByte:
                    throw new NotImplementedException();
                default:
                    throw new ArgumentException("unknown ReplyLength");
            }
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
            Closed = null;
            Opened = null;
        }
    }
}

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
    /// P50x protocol client
    /// </summary>
    public class Client : Connection
    {
        /// <summary>
        /// Send power on.
        /// </summary>
        public void PowerOn()
        {
            Send(new byte[] { 0xA7 }, 1);
            var resp = ReadMessage(1);
            checkResultOK(resp);
        }

        /// <summary>
        /// Send power off.
        /// </summary>
        public void PowerOff()
        {
            Send(new byte[] { 0xA6 }, 1);
            var resp = ReadMessage(1);
            checkResultOK(resp);
        }

        private void checkResultOK(byte[] response)
        {
        }
    }
}

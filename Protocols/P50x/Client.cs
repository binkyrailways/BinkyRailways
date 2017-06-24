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
            var resp = Transaction(new byte[] { (byte)'x', 0xA7 }, 1);
            checkResultOK(resp);
        }

        /// <summary>
        /// Send power off.
        /// </summary>
        public void PowerOff()
        {
            var resp = Transaction(new byte[] { (byte)'x', 0xA6 }, 1);
            checkResultOK(resp);
        }

        public void LocCommand(int address, int speed, bool forward, bool lights, bool f1, bool f2, bool f3, bool f4)
        {
            const byte ChgF = 0x80;
            const byte Force = 0x40;
            const byte Dir = 0x20;
            const byte FL = 0x10;
            const byte F4 = 0x08;
            const byte F3 = 0x04;
            const byte F2 = 0x02;
            const byte F1 = 0x01;
            var cmd = new byte[] { 
            (byte)'x',
            0x80,
            (byte)(address & 0xFF),
            (byte)((address >> 8) & 0xFF),
            (byte)speed,
            (byte)(ChgF | Force | (forward ? Dir : 0) | (lights ? FL : 0) | 
                (f4 ? F4 : 0) | (f3 ? F3 : 0) | (f2 ? F2 : 0)| (f1 ? F1 : 0)),
            };
            var resp = Transaction(cmd, 1);
            checkResultOK(resp);
        }

        private void checkResultOK(byte[] response)
        {
        }
    }
}

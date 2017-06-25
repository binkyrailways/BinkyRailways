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
        /// Set the CTS 'off' time in case of non-PC requested Power Off.
        /// </summary>
        /// <param name="ctime">
        /// 0 = the CTS line is permanently disabled: only a
        /// manual Power On (e.g. pressing the 'Go' key on the IB)
        /// would reactivate the CTS line. This is what would happen
        /// in case of a 6050 (actually: after a Power Off, a 6050
        /// accepts at least one more cmd. If the 6050 is connected
        /// to a 6021, then only a turnout cmd 'blocks' the 6050).
        /// 255 = the CTS line is not disabled at all (excluding, of course, IB RS-232 input buffer status).
        /// For other values, the CTS line is disabled for the specified amount of 50 ms units.
        /// </param>
        public void SetCTime(int ctime)
        {
            var cmd = "xRT " + ctime + "\r";
            FixedTransaction(Encoding.ASCII.GetBytes(cmd), 0);   
        }

        /// <summary>
        /// Send power on.
        /// </summary>
        public void PowerOn()
        {
            var resp = FixedTransaction(new byte[] { (byte)'x', 0xA7 }, 1);
            checkResultOK(resp);
        }

        /// <summary>
        /// Send power off.
        /// </summary>
        public void PowerOff()
        {
            var resp = FixedTransaction(new byte[] { (byte)'x', 0xA6 }, 1);
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
            var resp = FixedTransaction(cmd, 1);
            checkResultOK(resp);
        }

        public XStatus Status() {
            var resp = Bit7Transaction(new byte[]{(byte)'x', 0xA2 });
            return new XStatus(){
                VReg = (resp[0] & 0x40) != 0,
                ExtCU = (resp[0] & 0x20) != 0,
                Halt = (resp[0] & 0x10) != 0,
                Pwr = (resp[0] & 0x08) != 0,
                Hot = (resp[0] & 0x04) != 0,
                Go = (resp[0] & 0x02) != 0,
                Stop = (resp[0] & 0x01) != 0
            };
        }

        private void checkResultOK(byte[] response)
        {
        }
    }
}

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
namespace LocoNetToolBox.Protocol
{
    /// <summary>
    /// OPC_LOCO_DIRF
    /// </summary>
    public class LocoDirFuncRequest : Request
    {
        /// <summary>
        /// Default ctor
        /// </summary>
        public LocoDirFuncRequest(int slot, bool forward, bool f0, bool f1, bool f2, bool f3, bool f4)
        {
            Slot = slot;
            Forward = forward;
            F0 = f0;
            F1 = f1;
            F2 = f2;
            F3 = f3;
            F4 = f4;
        }

        /// <summary>
        /// Parse ctor
        /// </summary>
        public LocoDirFuncRequest(byte[] msg)
        {
            Slot = msg[1];
            var dirf = msg[2];
            F1 = (dirf & 0x01) != 0;
            F2 = (dirf & 0x02) != 0;
            F3 = (dirf & 0x04) != 0;
            F4 = (dirf & 0x08) != 0;
            F0 = (dirf & 0x10) != 0;
            Forward = (dirf & 0x20) == 0;
        }

        /// <summary>
        /// Accept a visit by the given visitor.
        /// </summary>
        public override TReturn Accept<TReturn, TData>(MessageVisitor<TReturn, TData> visitor, TData data)
        {
            return visitor.Visit(this, data);
        }

        /// <summary>
        /// Slot number 
        /// </summary>
        public int Slot { get; set; }

        /// <summary>
        /// Direction (true is forward)
        /// </summary>
        public bool Forward { get; set; }

        /// <summary>
        /// Function 0
        /// </summary>
        public bool F0 { get; set; }

        /// <summary>
        /// Function 1
        /// </summary>
        public bool F1 { get; set; }

        /// <summary>
        /// Function 2
        /// </summary>
        public bool F2 { get; set; }

        /// <summary>
        /// Function 3
        /// </summary>
        public bool F3 { get; set; }

        /// <summary>
        /// Function 4
        /// </summary>
        public bool F4 { get; set; }

        public DirFunc DirF
        {
            get
            {
                var dirf = 0;
                if (F1) dirf |= 0x01;
                if (F2) dirf |= 0x02;
                if (F3) dirf |= 0x04;
                if (F4) dirf |= 0x08;
                if (F0) dirf |= 0x10;
                if (!Forward) dirf |= 0x20;
                return (DirFunc) dirf;
            }
        }

        /// <summary>
        /// Create the byte level message
        /// </summary>
        private byte[] CreateMessage()
        {
            var msg = new byte[4];
            msg[0] = 0xA1; // Opcode
            msg[1] = (byte)(Slot & 0x7F);
            var dirf = 0;
            if (F1) dirf |= 0x01;
            if (F2) dirf |= 0x02;
            if (F3) dirf |= 0x04;
            if (F4) dirf |= 0x08;
            if (F0) dirf |= 0x10;
            if (!Forward) dirf |= 0x20;
            msg[2] = (byte)(dirf & 0x7F);
            UpdateChecksum(msg, msg.Length);
            return msg;
        }

        /// <summary>
        /// Execute the message on the given buffer
        /// </summary>
        public override void Execute(LocoBuffer lb)
        {
            lb.Send(this, CreateMessage());
        }

        /// <summary>
        /// Convert to string
        /// </summary>
        public override string ToString()
        {
            return string.Format("OPC_LOCO_DIRF slot:{0} dir:{1}, f0:{2}, f1:{3}, f2:{4}, f3:{5}, f4:{6}", 
                Slot, Forward, F0, F1, F2, F3, F4);
        }
    }
}

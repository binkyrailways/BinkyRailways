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
    /// OPC_SW_STATE
    /// </summary>
    public class SwitchStateRequest : Request
    {
        /// <summary>
        /// Default ctor
        /// </summary>
        public SwitchStateRequest(int address, bool direction, bool on)
        {
            Address = address;
            Direction = direction;
            On = on;
        }

        /// <summary>
        /// Parse ctor
        /// </summary>
        public SwitchStateRequest(byte[] msg)
        {
            var sw2 = msg[2];
            Address = (msg[1] & 0x7F) | ((sw2 & 0x0F) << 7);
            Direction = (sw2 & 0x20) != 0;
            On = (sw2 & 0x10) != 0;
        }

        /// <summary>
        /// Accept a visit by the given visitor.
        /// </summary>
        public override TReturn Accept<TReturn, TData>(MessageVisitor<TReturn, TData> visitor, TData data)
        {
            return visitor.Visit(this, data);
        }

        /// <summary>
        /// Switch address
        /// </summary>
        public int Address { get; set; }

        /// <summary>
        /// 1 for Closed,/GREEN, 
        /// 0 for Thrown/RED
        /// </summary>
        public bool Direction { get; set; }

        /// <summary>
        /// 1 for Output ON, 
        /// 0 FOR output OFF
        /// </summary>
        public bool On { get; set; }

        /// <summary>
        /// Create the byte level message
        /// </summary>
        private byte[] CreateMessage()
        {
            var msg = new byte[4];
            msg[0] = 0xBC; // Opcode
            msg[1] = (byte)(Address & 0x7F);
            var sw2 = (Address >> 7) & 0x0F;
            if (On) sw2 |= 0x10;
            if (Direction) sw2 |= 0x20;
            msg[2] = (byte)sw2;
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
            return string.Format("OPC_SW_STATE adr:{0} dir:{1}, on:{2}", Address, Direction, On);
        }
    }
}

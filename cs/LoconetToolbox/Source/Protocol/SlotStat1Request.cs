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
    /// OPC_LINK_SLOTS
    /// </summary>
    public class SlotStat1Request : Request
    {
        /// <summary>
        /// Default ctor
        /// </summary>
        public SlotStat1Request(int slot, SlotStatus1 stat1)
        {
            Slot = slot;
            Status = stat1;
        }

        /// <summary>
        /// Parse ctor
        /// </summary>
        public SlotStat1Request(byte[] msg)
        {
            Slot = msg[1];
            Status = (SlotStatus1) msg[2];
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
        /// Status 1
        /// </summary>
        public SlotStatus1 Status { get; set; }

        /// <summary>
        /// Create the byte level message
        /// </summary>
        private byte[] CreateMessage()
        {
            var msg = new byte[4];
            msg[0] = 0xB5; // Opcode
            msg[1] = (byte)(Slot & 0x7F);
            msg[2] = (byte)((int)Status & 0x7F);
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
            return string.Format("OPC_SLOT_STAT1 sl:{0} stat1:{1}", Slot, Status);
        }
    }
}

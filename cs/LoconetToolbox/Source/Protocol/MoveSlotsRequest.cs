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
    /// OPC_MOVE_SLOTS
    /// </summary>
    public class MoveSlotsRequest : Request
    {
        /// <summary>
        /// Default ctor
        /// </summary>
        public MoveSlotsRequest(int sourceSlot, int destinationSlot)
        {
            SourceSlot = sourceSlot;
            DestinationSlot = destinationSlot;
        }

        /// <summary>
        /// Parse ctor
        /// </summary>
        public MoveSlotsRequest(byte[] msg)
        {
            SourceSlot = msg[1];
            DestinationSlot = msg[2];
        }

        /// <summary>
        /// Accept a visit by the given visitor.
        /// </summary>
        public override TReturn Accept<TReturn, TData>(MessageVisitor<TReturn, TData> visitor, TData data)
        {
            return visitor.Visit(this, data);
        }

        /// <summary>
        /// Source slot number
        /// </summary>
        public int SourceSlot { get; set; }

        /// <summary>
        /// Destination slot number
        /// </summary>
        public int DestinationSlot { get; set; }

        /// <summary>
        /// Create the byte level message
        /// </summary>
        private byte[] CreateMessage()
        {
            var msg = new byte[4];
            msg[0] = 0xBA; // Opcode
            msg[1] = (byte)(SourceSlot & 0x7F);
            msg[2] = (byte)(DestinationSlot & 0x7F);
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
            return string.Format("OPC_MOVE_SLOTS src:{0} dst:{1}", SourceSlot, DestinationSlot);
        }
    }
}

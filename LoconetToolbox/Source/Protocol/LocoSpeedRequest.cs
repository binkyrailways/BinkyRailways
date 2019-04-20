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
    /// OPC_LOCO_SPD
    /// </summary>
    public class LocoSpeedRequest : Request
    {
        /// <summary>
        /// Default ctor
        /// </summary>
        public LocoSpeedRequest(int slot, int speed)
        {
            Slot = slot;
            Speed = speed;
        }

        /// <summary>
        /// Parse ctor
        /// </summary>
        public LocoSpeedRequest(byte[] msg)
        {
            Slot = msg[1];
            Speed = msg[2];
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
        /// Speed value
        /// </summary>
        public int Speed { get; set; }

        /// <summary>
        /// Create the byte level message
        /// </summary>
        private byte[] CreateMessage()
        {
            var msg = new byte[4];
            msg[0] = 0xA0; // Opcode
            msg[1] = (byte)(Slot & 0x7F);
            msg[2] = (byte)(Speed & 0x7F);
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
            return string.Format("OPC_LOCO_SPD slot:{0} speed:{1}", Slot, Speed);
        }
    }
}

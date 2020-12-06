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
    /// OPC_SL_RD_DATA
    /// </summary>
    public class SlotDataResponse : Request
    {
        /// <summary>
        /// Default ctor
        /// </summary>
        public SlotDataResponse(int slot)
        {
            Slot = slot;
        }

        /// <summary>
        /// Parse ctor
        /// </summary>
        public SlotDataResponse(byte[] msg)
        {
            Slot = msg[2];
            Status1 = (SlotStatus1) msg[3];
            Address = msg[4] | (msg[9] << 7);
            Speed = msg[5];
            DirF = (DirFunc) msg[6];
            TrackStatus = (TrackStatus) msg[7];
            Status2 = (SlotStatus2) msg[8];
            Sound = (Sound) msg[10];
            Id1 = msg[11];
            Id2 = msg[12];
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
        /// First status
        /// </summary>
        public SlotStatus1 Status1 { get; set; }

        /// <summary>
        /// Second status
        /// </summary>
        public SlotStatus2 Status2 { get; set; }

        /// <summary>
        /// Address
        /// </summary>
        public int Address { get; set; }

        /// <summary>
        /// Speed
        /// </summary>
        public int Speed { get; set; }

        /// <summary>
        /// Direction and F0 - F4
        /// </summary>
        public DirFunc DirF { get; set; }

        /// <summary>
        /// Track status
        /// </summary>
        public TrackStatus TrackStatus { get; set; }

        /// <summary>
        /// Sound/F5-F8 functions
        /// </summary>
        public Sound Sound { get; set; }

        /// <summary>
        /// First ID
        /// </summary>
        public int Id1 { get; set; }

        /// <summary>
        /// Second ID
        /// </summary>
        public int Id2 { get; set; }

        /// <summary>
        /// Create the byte level message
        /// </summary>
        private byte[] CreateMessage()
        {
            var msg = new byte[14];
            msg[0] = 0xE7; // Opcode
            msg[1] = 0x0E; // Size
            msg[2] = (byte)Slot;
            msg[3] = (byte)Status1;
            msg[4] = (byte)(Address & 0x7F); // low
            msg[5] = (byte)(Speed & 0x7F);
            msg[6] = (byte)DirF;
            msg[7] = (byte)TrackStatus;
            msg[8] = (byte)Status2;
            msg[9] = (byte)((Address >> 7) & 0x7F);
            msg[10] = (byte)Sound;
            msg[11] = (byte)(Id1 & 0x7F);
            msg[12] = (byte)(Id2 & 0x7F);
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
            return string.Format("OPC_SL_RD_DATA slot:{0}, stat1:{1}, addr:{2}, spd:{3}, dirf:{4}, trk:{5}, stat2:{6}, snd:{7}, id1:{8}, id2:{9}",
                Slot, Status1, Address, Speed, DirF, TrackStatus, Status2, Sound, Id1, Id2);
        }
    }
}

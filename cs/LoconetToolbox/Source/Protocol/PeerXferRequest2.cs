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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LocoNetToolBox.Protocol
{
    public class PeerXferRequest2 : PeerXferRequest
    {
        /// <summary>
        /// Accept a visit by the given visitor.
        /// </summary>
        public override TReturn Accept<TReturn, TData>(MessageVisitor<TReturn, TData> visitor, TData data)
        {
            return visitor.Visit(this, data);
        }

        /// <summary>
        /// Create the byte level message
        /// </summary>
        private byte[] CreateMessage()
        {
            int sv_adrl = SvAddress & 0xFF;
            int sv_adrh = (SvAddress >> 2) & 0xFF;

            byte svx1 = (byte)(0x10 | GetBit7(sv_adrh, 3) | GetBit7(sv_adrl, 2) | GetBit7(DestinationHigh, 1) | GetBit7(DestinationLow, 0));
            byte svx2 = (byte)(0x10 | GetBit7(Data4, 3) | GetBit7(Data3, 2) | GetBit7(Data2, 1) | GetBit7(Data1, 0));
            var msg = new byte[16];
            msg[0] = 0xE5; // Opcode
            msg[1] = 0x10; // Message length
            msg[2] = 0x50; // Source address low (PC os 01 80)
            msg[3] = (byte)Command;
            msg[4] = 0x02; // SV_TYPE
            msg[5] = svx1;
            msg[6] = (byte)(DestinationLow & 0x7F);
            msg[7] = (byte)(DestinationHigh & 0x7F);
            msg[8] = (byte)(sv_adrl & 0x7F);
            msg[9] = (byte)(sv_adrh & 0x7F);
            msg[10] = svx2;
            msg[11] = (byte)(Data1 & 0x7F);
            msg[12] = (byte)(Data1 & 0x7F);
            msg[13] = (byte)(Data1 & 0x7F);
            msg[14] = (byte)(Data1 & 0x7F);
            // Remaining all null
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
    }
}

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
    public class PeerXferRequest1 : PeerXferRequest
    {
        public PeerXferRequest1()
        {
            DestinationHigh = 1;
        }

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
            var msg = new byte[16];
            msg[0] = 0xE5; // Opcode
            msg[1] = 0x10; // Message length
            msg[2] = PC_ADDRESS; // Source address low (PC os 01 80)
            msg[3] = (byte)DestinationLow;
            msg[4] = (byte)DestinationHigh;
            //msg[5] = pxct1;
            msg[6] = (byte)Command;
            msg[7] = (byte)SvAddress;
            msg[8] = 0;
            msg[9] = (byte)Data1;
            //msg[10] = pxct2;
            msg[11] = (byte)SubAddress;
            msg[12] = 0;
            msg[13] = 0;
            msg[14] = 0;

            int pxct1 = 0;
            int pxct2 = 0;
            for (int i = 0; i < 4; i++)
            {
                if ((msg[6 + i] & 0x80) != 0)
                {
                    pxct1 |= 1 << i;
                    msg[6 + i] &= 0x7F;
                }
                if ((msg[11 + i] & 0x80) != 0)
                {
                    pxct2 |= 1 << i;
                    msg[11 + i] &= 0x7F;
                }
            }
            msg[5] = (byte)pxct1;
            msg[10] = (byte)pxct2;

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

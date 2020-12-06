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
    public class SwitchRequest : Request
    {
        /// <summary>
        /// Default ctor
        /// </summary>
        public SwitchRequest()
        {
        }

        /// <summary>
        /// Accept a visit by the given visitor.
        /// </summary>
        public override TReturn Accept<TReturn, TData>(MessageVisitor<TReturn, TData> visitor, TData data)
        {
            return visitor.Visit(this, data);
        }

        /// <summary>
        /// Address
        /// </summary>
        public int Address { get; set; }

        /// <summary>
        /// True for closed/Green, False for Thrown/Red
        /// </summary>
        public bool Direction { get; set; }

        /// <summary>
        /// True for on, false for off
        /// </summary>
        public bool Output { get; set; }

        /// <summary>
        /// Convert to readable string
        /// </summary>
        public override string ToString()
        {
            return string.Format("SW_REQ addr:{0}, dir:{1}, output:{2}", Address, Direction, Output);
        }

        /// <summary>
        /// Create the byte level message
        /// </summary>
        private byte[] CreateMessage()
        {
            var dir = Direction ? 0x20 : 0x00;
            var output = Output ? 0x10 : 0x00;
            var msg = new byte[4];
            msg[0] = 0xB0; // Opcode
            msg[1] = (byte)(Address & 0x7F);
            msg[2] = (byte)(((Address >> 7) & 0x0F) | dir | output);
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

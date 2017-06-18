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
    public abstract class PeerXferResponse : Response
    {
        /// <summary>
        /// Default ctor
        /// </summary>
        protected PeerXferResponse(byte[] data) 
        {
            if (data.Length < 16) { throw new ArgumentException("Invalid message length"); }
            if (data[0] != 0xE5) { throw new ArgumentException("Invalid opcode"); }
            if (data[1] != 0x10) { throw new ArgumentException("Invalid message length " + data[1]); }
        }

        /// <summary>
        /// Create a response object if the given data is recognized.
        /// </summary>
        internal static Response TryDecode(byte[] data)
        {
            if ((data[0] == 0xE5) && (data[1] == 0x10)) { return new PeerXferResponse1(data); }
            return null;
        }

        /// <summary>
        /// Is the PC the source of this message.
        /// </summary>
        public bool IsSourcePC
        {
            get { return (SourceLow == PeerXferRequest.PC_ADDRESS); }
        }

        /// <summary>
        /// Source address
        /// </summary>
        public LocoNetAddress Source
        {
            get { return new LocoNetAddress(SourceLow, SourceHigh); }
        }

        public bool IsDestinationBroadcast
        {
            get { return (DestinationLow == 0); }
        }

        /// <summary>
        /// Destination address
        /// </summary>
        public LocoNetAddress Destination
        {
            get { return new LocoNetAddress(DestinationLow, DestinationHigh); }
        }

        /// <summary>
        /// Low byte source address
        /// </summary>
        protected abstract byte SourceLow { get; }

        /// <summary>
        /// High byte source address
        /// </summary>
        protected abstract byte SourceHigh { get; }

        /// <summary>
        /// Low byte of destination address
        /// </summary>
        protected abstract byte DestinationLow { get; }

        /// <summary>
        /// High byte of destination address
        /// </summary>
        protected abstract byte DestinationHigh { get; }

        /// <summary>
        /// Command to send
        /// </summary>
        public abstract PeerXferRequest.Commands OriginalCommand { get; }

        /// <summary>
        /// 16-bit EEPROM address for read/write
        /// </summary>
        public abstract int SvAddress { get; }

        public abstract int LocoIOVersion { get; }

        /// <summary>
        /// D1
        /// </summary>
        public abstract byte Data1 { get; }

        /// <summary>
        /// D2
        /// </summary>
        public abstract byte Data2 { get; }

        /// <summary>
        /// D3
        /// </summary>
        public abstract byte Data3 { get; }

        public override string ToString()
        {
            return string.Format("PEER_XFER src:{0} dst:{1} cmd:{2} sv:{3} ver:{4:000} data:{5},{6},{7}",
                Source, Destination, OriginalCommand, SvAddress, LocoIOVersion, Data1, Data2, Data3);
        }
    }
}

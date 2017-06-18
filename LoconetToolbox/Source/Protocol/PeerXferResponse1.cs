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
    public class PeerXferResponse1 : PeerXferResponse
    {
        private readonly byte[] data;

        /// <summary>
        /// Default ctor
        /// </summary>
        public PeerXferResponse1(byte[] data)
            : base(data)
        {
            this.data = data;
        }

        /// <summary>
        /// Accept a visit by the given visitor.
        /// </summary>
        public override TReturn Accept<TReturn, TData>(MessageVisitor<TReturn, TData> visitor, TData data)
        {
            return visitor.Visit(this, data);
        }

        /// <summary>
        /// Low byte source address
        /// </summary>
        protected override byte SourceLow { get { return data[2]; } }

        /// <summary>
        /// High byte source address
        /// </summary>
        protected override byte SourceHigh { get { return data[11]; } }

        /// <summary>
        /// Low byte of destination address
        /// </summary>
        protected override byte DestinationLow { get { return data[3]; } }

        /// <summary>
        /// High byte of destination address
        /// </summary>
        protected override byte DestinationHigh { get { return data[4]; } }

        /// <summary>
        /// Command to send
        /// </summary>
        public override PeerXferRequest.Commands OriginalCommand
        {
            get { return (PeerXferRequest.Commands)D1; }
        }

        /// <summary>
        /// 16-bit EEPROM address for read/write
        /// </summary>
        public override int SvAddress { get { return D2; } }

        public override int LocoIOVersion { get { return D3; }}

        /// <summary>
        /// D1
        /// </summary>
        public override byte Data1
        {
            get
            {
                if (IsSourcePC)
                    return D4;
                if (OriginalCommand == PeerXferRequest.Commands.Write)
                    return D8;
                else
                    return D6;
            }
        }

        /// <summary>
        /// D2
        /// </summary>
        public override byte Data2 { get { return GetSplitValue(data[13], data[10], 1); } }

        /// <summary>
        /// D3
        /// </summary>
        public override byte Data3 { get { return GetSplitValue(data[14], data[10], 0); } }

        private byte D1 { get { return GetSplitValue(data[6], data[5], 0); } }
        private byte D2 { get { return GetSplitValue(data[7], data[5], 1); } }
        private byte D3 { get { return GetSplitValue(data[8], data[5], 2); } }
        private byte D4 { get { return GetSplitValue(data[9], data[5], 3); } }
        private byte D5 { get { return GetSplitValue(data[11], data[10], 0); } }
        private byte D6 { get { return GetSplitValue(data[12], data[10], 1); } }
        private byte D7 { get { return GetSplitValue(data[13], data[10], 2); } }
        private byte D8 { get { return GetSplitValue(data[14], data[10], 3); } }
    }
}

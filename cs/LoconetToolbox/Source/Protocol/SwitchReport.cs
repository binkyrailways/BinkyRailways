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
    public class SwitchReport : Response
    {
        /// <summary>
        /// Message ctor
        /// </summary>
        internal SwitchReport(byte[] data)
        {
            var low = data[1] & 0x7F;
            var high = data[2] & 0x0F;
            Address = (low | (high << 7)) + 1;
            InputSource = ((data[2] & 0x20) == 0) ? InputSource.Aux : InputSource.Switch;
            SensorLevel = ((data[2] & 0x10) != 0);
        }

        /// <summary>
        /// Accept a visit by the given visitor.
        /// </summary>
        public override TReturn Accept<TReturn, TData>(MessageVisitor<TReturn, TData> visitor, TData data)
        {
            return visitor.Visit(this, data);
        }

        /// <summary>
        /// Create a response object if the given data is recognized.
        /// </summary>
        internal static Response TryDecode(byte[] data)
        {
            if (data[0] == 0xB1) { return new SwitchReport(data); }
            return null;
        }

        /// <summary>
        /// Address
        /// </summary>
        public int Address { get; set; }

        /// <summary>
        /// Source
        /// </summary>
        public InputSource InputSource { get; set; }

        /// <summary>
        /// Output: true = straight, false = off
        /// </summary>
        public bool SensorLevel { get; set; }

        public override string ToString()
        {
            return string.Format("SW_REP addr:{0}, dir:{1}, output:{2}", 
                Address,
                InputSource,
                SensorLevel ? "straight" : "off");
        }
    }
}

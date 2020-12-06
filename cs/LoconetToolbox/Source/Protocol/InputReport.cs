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
    public class InputReport : Response
    {
        /// <summary>
        /// Message ctor
        /// </summary>
        internal InputReport(byte[] data)
        {
            var low = data[1] & 0x7F;
            var high = data[2] & 0x0F;
            var bit0 = (data[2] & 0x20) >> 5;
            this.Address = (low << 1) | (high << 8) | bit0;
            SensorLevel = (data[2] & 0x10) != 0;
            InputSource = ((data[2] & 0x20) == 0) ? InputSource.Aux : InputSource.Switch;
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
            if (data[0] == 0xB2) { return new InputReport(data); }
            return null;
        }

        /// <summary>
        /// Address
        /// </summary>
        public int Address { get; set; }

        /// <summary>
        /// Level (true=high, false=low)
        /// </summary>
        public bool SensorLevel { get; set; }

        /// <summary>
        /// Source
        /// </summary>
        public InputSource InputSource { get; set; }

        public override string ToString()
        {
            return string.Format("INPUT_REP addr:{0}", Address);
        }
    }
}

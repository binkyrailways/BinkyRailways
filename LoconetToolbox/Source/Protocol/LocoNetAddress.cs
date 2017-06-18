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
    public class LocoNetAddress : IEquatable<LocoNetAddress>
    {
        /// <summary>
        /// Default ctor
        /// </summary>
        public LocoNetAddress(byte address, byte subAddress)
        {
            this.Address = address;
            this.SubAddress = subAddress;
        }

        /// <summary>
        /// Primary (also low) address
        /// </summary>
        public byte Address { get; private set; }

        /// <summary>
        /// Sub (also high) address
        /// </summary>
        public byte SubAddress { get; private set; }

        /// <summary>
        /// Compare to other address.
        /// </summary>
        public bool Equals(LocoNetAddress other)
        {
            return (other != null) && (other.Address == this.Address) && (other.SubAddress == this.SubAddress);
        }

        /// <summary>
        /// Compare to other address.
        /// </summary>
        public override bool Equals(object obj)
        {
            return Equals(obj as LocoNetAddress);
        }

        /// <summary>
        /// Convert to human readable string.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("{0}/{1}", Address, SubAddress);
        }

        /// <summary>
        /// Hashing
        /// </summary>
        public override int GetHashCode()
        {
            return Address ^ SubAddress;
        }
    }
}

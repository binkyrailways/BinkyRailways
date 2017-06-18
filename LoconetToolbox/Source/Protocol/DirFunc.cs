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

namespace LocoNetToolBox.Protocol
{
    /// <summary>
    /// DIRF bits
    /// </summary>
    [Flags]
    public enum DirFunc
    {
        None = 0,

        /// <summary>
        /// 1=F1 ON
        /// </summary>
        F1 = 0x01,

        /// <summary>
        /// 1=F2 ON
        /// </summary>
        F2= 0x02,

        /// <summary>
        /// 1=F3 ON
        /// </summary>
        F3 = 0x04,

        /// <summary>
        /// 1=F4 ON
        /// </summary>
        F4 = 0x08,

        /// <summary>
        /// 1=Directional lighting ON
        /// </summary>
        F0 = 0x10,

        /// <summary>
        /// 1=loco direction FORWARD ... so says the spec.
        /// But this seems wrong
        /// </summary>
        Direction = 0x20,
    }
}

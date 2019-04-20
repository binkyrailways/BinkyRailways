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
    /// STAT2 bits
    /// </summary>
    [Flags]
    public enum SlotStatus2
    {
        /// <summary>
        /// 1=this slot has SUPPRESSED ADV consist-
        /// </summary>
        SuppressedAbdConsist = 0x01,

        /// <summary>
        /// 1=Expansion ID1/2 is NOT ID usage
        /// </summary>
        ExpansionIdNotIdUsage = 0x04, 

        /// <summary>
        /// 1=expansion IN ID1/2, 0=ENCODED alias
        /// </summary>
        ExpansionId = 0x08,
    }
}

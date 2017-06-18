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
    /// TrackStatus bits
    /// </summary>
    [Flags]
    public enum TrackStatus
    {
        /// <summary>
        /// 1=DCC packets are ON in MASTER, Global POWER up
        /// </summary>
        Power = 0x01,

        /// <summary>
        /// 0=TRACK is PAUSED, B'cast EMERG STOP.
        /// </summary>
        Idle = 0x02,

        /// <summary>
        /// 1=This Master IMPLEMENTS LocoNet 1.1 capability,
        /// 0=Master is DT200
        /// </summary>
        LocoNet11 = 0x04,

        /// <summary>
        /// 1=Programming TRACK in this Master is BUSY.
        /// </summary>
        ProgrammingTrackBusy = 0x08
    }
}

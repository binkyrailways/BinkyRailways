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
    /// STAT1 bits
    /// </summary>
    [Flags]
    public enum SlotStatus1
    {
        None = 0,

        /// <summary>
        /// Busy, Active:
        /// 11=IN_USE loco adr in SLOT -REFRESHED
        /// 10=IDLE loco adr in SLOT -NOT refreshed
        /// 01=COMMON loco adr IN SLOT -refreshed
        /// 00=FREE SLOT, no valid DATA -not refreshed
        /// </summary>
        xBusy = 0x20,
        xActive = 0x10,
        BusyActiveMask = xBusy | xActive,
        InUse = xBusy | xActive,
        Idle = xBusy,
        Common = xActive,
        FreeSlot = 0,

        /// <summary>
        /// CONDN/CONUP: bit encoding-Control double linked Consist List
        /// 11=LOGICAL MID CONSIST , Linked up AND down
        /// 10=LOGICAL CONSIST TOP, Only linked downwards
        /// 01=LOGICAL CONSIST SUB-MEMBER, Only linked upwards
        /// 00=FREE locomotive, no CONSIST indirection/linking
        /// ALLOWS "CONSISTS of CONSISTS". Uplinked means that Slot SPD
        /// number is now SLOT adr of SPD/DIR and STATUS of consist. i.e. is
        /// an Indirect pointer. This Slot has same BUSY/ACTIVE bits as TOP of
        /// Consist. TOP is loco with SPD/DIR for whole consist. (top of list).
        /// </summary>
        ConsistDown = 0x08,
        ConsistUp = 0x40,

        xSlSpdEx = 0x04,
        xSlSpd14 = 0x02,
        xSlSpd28 = 0x01,

        DecoderTypeMask = 0x07,
        // Motorola
        DecoderType28Tri = xSlSpd28, // 001=28 step. Generate Trinary packets for this Mobile ADR (motorola)
        // DCC
        DecoderType14 = xSlSpd14, // 010=14 step MODE
        DecoderType28 = 0x00, // 000=28 step/ 3 BYTE PKT regular mode
        DecoderType128 = xSlSpd14 | xSlSpd28, // 011=send 128 speed mode packets
        // Advanced consisting
        DecoderType28AdvC = xSlSpdEx, // 100=28 Step decoder ,Allow Advanced DCC consisting
        DecoderType128AdvC = xSlSpd14 | xSlSpd28 | xSlSpdEx // 111=128 Step decoder, Allow Advanced DCC consisting
    }
}

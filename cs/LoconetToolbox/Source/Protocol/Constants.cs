/*
Loconet toolbox
Copyright (C) 2010 Modelspoorgroep Venlo, Ewout Prangsma

Thanks to JMRI project.

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
    public static class Constants
    {
        public const int OPC_GPBUSY = 0x81;
        public const int OPC_GPOFF = 0x82;
        public const int OPC_GPON = 0x83;
        public const int OPC_IDLE = 0x85;
        public const int OPC_LOCO_SPD = 0xa0;
        public const int OPC_LOCO_DIRF = 0xa1;
        public const int OPC_LOCO_SND = 0xa2;
        public const int OPC_SW_REQ = 0xb0;
        public const int OPC_SW_REP = 0xb1;
        public const int OPC_INPUT_REP = 0xb2;
        public const int OPC_UNKNOWN = 0xb3;
        public const int OPC_LONG_ACK = 0xb4;
        public const int OPC_SLOT_STAT1 = 0xb5;
        public const int OPC_CONSIST_FUNC = 0xb6;
        public const int OPC_UNLINK_SLOTS = 0xb8;
        public const int OPC_LINK_SLOTS = 0xb9;
        public const int OPC_MOVE_SLOTS = 0xba;
        public const int OPC_RQ_SL_DATA = 0xbb;
        public const int OPC_SW_STATE = 0xbc;
        public const int OPC_SW_ACK = 0xbd;
        public const int OPC_LOCO_ADR = 0xbf;
        public const int OPC_MULTI_SENSE = 0xd0;
        public const int OPC_PEER_XFER = 0xe5;
        public const int OPC_SL_RD_DATA = 0xe7;
        public const int OPC_IMM_PACKET = 0xed;
        public const int OPC_IMM_PACKET_2 = 0xee;
        public const int OPC_WR_SL_DATA = 0xef;
        public const int OPC_WR_SL_DATA_EXP = 0xee;
        public const int OPC_MASK = 0x7f;  /* mask for acknowledge opcodes */

        public const int OPC_SW_ACK_CLOSED = 0x20;  /* command switch closed/open bit   */
        public const int OPC_SW_ACK_OUTPUT = 0x10;  /* command switch output on/off bit */

        public const int OPC_INPUT_REP_CB = 0x40;  /* control bit, reserved otherwise      */
        public const int OPC_INPUT_REP_SW = 0x20;  /* input is switch input, aux otherwise */
        public const int OPC_INPUT_REP_HI = 0x10;  /* input is HI, LO otherwise            */

        public const int OPC_SW_REP_SW = 0x20;  /* switch input, aux input otherwise    */
        public const int OPC_SW_REP_HI = 0x10;  /* input is HI, LO otherwise            */
        public const int OPC_SW_REP_CLOSED = 0x20;  /* 'Closed' line is ON, OFF otherwise   */
        public const int OPC_SW_REP_THROWN = 0x10;  /* 'Thrown' line is ON, OFF otherwise   */
        public const int OPC_SW_REP_INPUTS = 0x40;  /* sensor inputs, outputs otherwise     */

        public const int OPC_SW_REQ_DIR = 0x20;  /* switch direction - closed/thrown     */
        public const int OPC_SW_REQ_OUT = 0x10;  /* output On/Off                        */

        public const int OPC_LOCO_SPD_ESTOP = 0x01; /* emergency stop command               */

        public const int OPC_MULTI_SENSE_MSG = 0x60; // byte 1
        public const int OPC_MULTI_SENSE_PRESENT = 0x20; // MSG field: transponder seen
        public const int OPC_MULTI_SENSE_ABSENT = 0x00; // MSG field: transponder lost
        public const int OPC_MULTI_SENSE_POWER = 0x60; // MSG field: Power message
    }
}

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
    public abstract class PeerXferRequest : Request
    {
        public const int PC_ADDRESS = 80;

        public enum Commands : uint
        {
            Write = 0x01,    // Write 1 byte of data from D1
            Read = 0x02,    // Initiate read 1 byte of data into D1
            MaskedWrite = 0x03,    // Write 1 byte of masked data from D1. D2 contains the mask to be used.
            Write4 = 0x05,    // Write 4 bytes of data from D1..D4
            Read4 = 0x06,    // Initiate read 4 bytes of data into D1..D4
            Discover = 0x07,    // Causes all devices to identify themselves by their MANUFACTURER_ID, DEVELOPER_ID, PRODUCT_ID and Serial Number
            Identify = 0x08,    // Causes an individual device to identify itself by its MANUFACTURER_ID, DEVELOPER_ID, PRODUCT_ID and Serial Number
            ChangeAddress= 0x09,    // Changes the device address to the values specified in <DST_L> + <DST_H> in the device that matches the values specified in <SV_ADRL> + <SV_ADRH> + <D1>..<D4> that we in the reply to the Discover or Identify command issued previously
            Reconfigure = 0x4F,    // Initiates a device reconfiguration or reset so that any new device configuration becomes active
        }

        /// <summary>
        /// Default ctor
        /// </summary>
        public PeerXferRequest()
        {
            this.Command = Commands.Read;
        }

        /// <summary>
        /// Low byte of destination address
        /// </summary>
        public byte DestinationLow { get; set; }

        /// <summary>
        /// High byte of destination address (typically 1)
        /// </summary>
        public byte DestinationHigh { get; set; }

        /// <summary>
        /// Sub address of LocoIO module
        /// </summary>
        public byte SubAddress { get; set; }

        /// <summary>
        /// Command to send
        /// </summary>
        public Commands Command { get; set; }

        /// <summary>
        /// 16-bit EEPROM address for read/write
        /// </summary>
        public int SvAddress { get; set; }

        /// <summary>
        /// D1
        /// </summary>
        public byte Data1 { get; set; }

        /// <summary>
        /// D2
        /// </summary>
        public byte Data2 { get; set; }

        /// <summary>
        /// D3
        /// </summary>
        public byte Data3 { get; set; }

        /// <summary>
        /// D4
        /// </summary>
        public byte Data4 { get; set; }
    }
}

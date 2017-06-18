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

using LocoNetToolBox.Protocol;

namespace LocoNetToolBox.Devices.LocoIO
{
    public sealed partial class PinMode
    {
        /// <summary>
        /// Directions for a single port.
        /// </summary>
        public enum Directions { Input, Output };

        /// <summary>
        /// Default ctor
        /// </summary>
        private PinMode(Directions direction, int opcode, int sv0, int sv2, string name)
        {
            this.Direction = direction;
            this.Opcode = opcode;
            this.SV0 = sv0;
            this.SV2 = sv2;
            this.Name = name;
        }

        /// <summary>
        /// Input or output
        /// </summary>
        public Directions Direction { get; private set; }

        /// <summary>
        /// Direction is Input?
        /// </summary>
        public bool IsInput { get { return (Direction == Directions.Input); } }

        /// <summary>
        /// Direction is Output?
        /// </summary>
        public bool IsOutput { get { return (Direction == Directions.Output); } }

        /// <summary>
        /// Loconet opcode
        /// </summary>
        private int Opcode { get; set; }

        /// <summary>
        /// Value 1
        /// </summary>
        private int SV0 { get; set; }

        /// <summary>
        /// Value 2
        /// </summary>
        private int SV2 { get; set; }

        /// <summary>
        /// Human readable name of this mode
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets the SV0 value
        /// </summary>
        public int GetConfig()
        {
            return SV0;
        }

        /// <summary>
        /// Gets Low bits
        /// </summary>
        public int GetValue1(int address)
        {
            return addressToValues(this.Opcode, this.SV0, this.SV2, address) & 0x7F;
        }

        /// <summary>
        /// Gets high configuration byte
        /// </summary>
        public int GetValue2(int address)
        {
            return (addressToValues(this.Opcode, this.SV0, this.SV2, address) / 256) & 0x7F;
        }

        /**
         * Convert bytes from LocoNet packet into a 1-based address for
         * a sensor or turnout.
         * @param a1 Byte containing the upper bits
         * @param a2 Byte containing the lower bits
         * @return 1-4096 address
         */
        private static int SENSOR_ADR(int a1, int a2) { return (((a2 & 0x0f) * 128) + (a1 & 0x7f)) + 1; }

        private static int addressToValues(int opcode, int sv, int v2mask, int address)
        {
            int v1 = 0;
            int v2 = 0;

            address--;

            if (opcode == Constants.OPC_INPUT_REP)
            {
                v1 = ((address / 2) & 0x7F);
                v2 = ((address / 256) & 0x0F);
                if ((address & 0x01) == 0x01)
                {
                    v2 |= Constants.OPC_INPUT_REP_SW;
                }
                v2 |= v2mask;
            }
            else if (opcode == Constants.OPC_SW_REQ)
            {
                v1 = (address & 0x7F);
                v2 = (address / 128) & 0x0F;
                v2 &= ~(0x40);
                v2 |= v2mask;
            }
            else if (opcode == Constants.OPC_SW_REP)
            {
                v1 = (address & 0x7F);
                v2 = (address / 128) & 0x0F;
                v2 &= ~(0x40);
                v2 |= v2mask;
            }
            return v2 * 256 + v1;
        }

        /// <summary>
        /// Gets the address from the given SV values
        /// </summary>
        public int GetAddress(int sv, int v1, int v2)
        {
            return valuesToAddress(Opcode, sv, v1, v2);
        }

        internal static int valuesToAddress(int opcode, int sv, int v1, int v2)
        {
            //int hi = 0;
            //int lo = 0;
            if (opcode == Constants.OPC_INPUT_REP)
            {  /* return 1-4096 address */
                return ((SENSOR_ADR(v1, v2) - 1) * 2 + ((v2 & Constants.OPC_INPUT_REP_SW) != 0 ? 2 : 1));
            }
            else if (opcode == Constants.OPC_SW_REQ)
            {
                // if ( ((v2 & 0xCF) == 0x0F)  && ((v1 & 0xFC) == 0x78) ) { // broadcast address LPU V1.0 page 12
                // "Request Switch to broadcast address with bits "+
                // "a="+ ((sw2&0x20)>>5)+((sw2 & LnConstants.OPC_SW_REQ_DIR)!=0 ? " (Closed)" : " (Thrown)")+
                // " c="+ ((sw1 & 0x02)>>1) +
                // " b="+ ((sw1 & 0x01)) +
                // "\n\tOutput "+
                // ((sw2 & LnConstants.OPC_SW_REQ_OUT)!=0 ? "On"     : "Off")+"\n";
                // } else if ( ((v2 & 0xCF) == 0x07)  && ((v1 & 0xFC) == 0x78) ) { // broadcast address LPU V1.0 page 13
                // "Request switch command is Interrogate LocoNet with bits "+
                // "a="+ ((sw2 & 0x20)>>5) +
                // " c="+ ((sw1&0x02)>>1) +
                // " b="+ ((sw1&0x01)) +
                // "\n\tOutput "+
                // ((sw2 & LnConstants.OPC_SW_REQ_OUT)!=0 ? "On"     : "Off")+"\n"+
                // ( ( (sw2&0x10) == 0 ) ? "" : "\tNote 0x10 bit in sw2 is unexpectedly 0\n");
                // } else { // normal command
                return (SENSOR_ADR(v1, v2));
                //}
            }
            else if (opcode == Constants.OPC_SW_REP)
            {
                return (SENSOR_ADR(v1, v2));
            }
            return -1;
        }

        public override string ToString()
        {
            var prefix = IsInput ? "[in] " : "[out] ";
            return prefix + Name;
        }
    }
}

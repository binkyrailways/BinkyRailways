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
    public abstract class Message
    {
        /// <summary>
        /// Is this byte the first byte of a message?
        /// </summary>
        internal static bool IsOpcode(byte value)
        {
            return ((value & 0x80) == 0x80);
        }
       
        /// <summary>
        /// Create a response object if the given data is recognized.
        /// </summary>
        public static Message Decode(byte[] data)
        {
            // TODO Decode Requests also
            Message result = Response.Decode(data);
            if (result != null)
                return result;
            switch (data[0])
            {
                case 0x81:
                    return new Busy();
                case 0x82:
                    return new GlobalPowerOff();
                case 0x83:
                    return new GlobalPowerOn();
                case 0x85:
                    return new Idle();
                case 0xBF:
                    return new LocoAddressRequest(data);
                case 0xBC:
                    return new SwitchStateRequest(data);
                case 0xBB:
                    return new SlotDataRequest(data);
                case 0xBA:
                    return new MoveSlotsRequest(data);
                case 0xB9:
                    return new LinkSlotsRequest(data);
                case 0xB8:
                    return new UnlinkSlotsRequest(data);
                case 0xB5:
                    return new SlotStat1Request(data);
                case 0xB4:
                    return new LongAck(data);
                case 0xA0:
                    return new LocoSpeedRequest(data);
                case 0xA1:
                    return new LocoDirFuncRequest(data);
                case 0xE7:
                    return new SlotDataResponse(data);
                case 0xEF:
                    return new WriteSlotData(data);

            }
            return result;
        }

        /// <summary>
        /// Convert message to string.
        /// </summary>
        public static string ToString(byte[] msg)
        {
            var sb = new StringBuilder();
            for (int i = 0; i < GetMessageLength(msg); i++)
            {
                if (i > 0) { sb.Append(' '); }
                sb.Append(msg[i].ToString("X2"));
            }
            return sb.ToString();
        }

        /// <summary>
        /// Gets the length of the given message (including checksum)
        /// </summary>
        internal static int GetMessageLength(byte[] data)
        {
            switch (data[0] & 0xE0)
            {
                case 0x80:
                    return 2;
                case 0xA0:
                    return 4;
                case 0xC0:
                    return 6;
                case 0xE0:
                    return data[1] & 0x3F;
                default:
                    throw new ArgumentException("Not a valid message");
            }
        }

        /// <summary>
        /// Calculate the checksum of the given messages.
        /// </summary>
        /// <param name="message">Message data (including checksum byte)</param>
        /// <param name="length">Total length including checksum byte</param>
        internal static void UpdateChecksum(byte[] message, int length)
        {
            int chsum = 0xFF;
            for (int i = 0; i < length - 1; i++)
            {
                chsum ^= message[i];
            }
            message[length - 1] = (byte)chsum;
        }

        /// <summary>
        /// Gets the value of bit 7, shifted to the left.
        /// </summary>
        protected static int GetBit7(int value, int shiftLeft)
        {
            return ((value & 0x80) >> 7) << shiftLeft;
        }

        /// <summary>
        /// Combine a value.
        /// </summary>
        /// <param name="lowValue">7 lsb bits</param>
        /// <param name="highValue">1 msb bit at shiftRight</param>
        protected static byte GetSplitValue(int lowValue, int highValue, int shiftRight)
        {
            return (byte)(lowValue | (((highValue >> shiftRight) & 0x01) << 7));
        }

        /// <summary>
        /// Accept a visit by the given visitor.
        /// </summary>
        public abstract TReturn Accept<TReturn, TData>(MessageVisitor<TReturn, TData> visitor, TData data);
    }
}

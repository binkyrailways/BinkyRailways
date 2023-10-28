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
using System.Threading;

using LocoNetToolBox.Protocol;

namespace LocoNetToolBox.Devices.MgvServo
{
    /// <summary>
    /// Class used to program and MGV81, MGV84 and MGV136
    /// </summary>
    public sealed class ServoProgrammer
    {
        private bool programmingMode;
        private static readonly int[] SpeedMasks = { 0x01, 0x05, 0x0D, 0x0F };

        /// <summary>
        /// Address of first bit
        /// </summary>
        public int Address1 { get; set; }

        /// <summary>
        /// Address of second bit
        /// </summary>
        public int Address2 { get; set; }

        /// <summary>
        /// Address of third bit
        /// </summary>
        public int Address3 { get; set; }

        /// <summary>
        /// Address of fourth bit
        /// </summary>
        public int Address4 { get; set; }

        /// <summary>
        /// Turnout number (1-4)
        /// </summary>
        public int Turnout { get; set; }

        /// <summary>
        /// Set all bits to 0.
        /// </summary>
        internal void BitsToZero(LocoBuffer lb)
        {
            if (programmingMode)
            {
                throw new InvalidOperationException("Only valid in normal mode");
            }
            SetB4(lb, false);
            SetB3(lb, false);
            SetB2(lb, false);
            SetB1(lb, false);
        }

        /// <summary>
        /// Go into programming mode.
        /// </summary>
        internal void EnterProgrammingMode(LocoBuffer lb)
        {
            if (!programmingMode)
            {
                // Turn all bits off
                SetB4(lb, false);
                SetB3(lb, false);
                SetB2(lb, false);
                SetB1(lb, false);
                Thread.Sleep(1000);

                // Turn bit 1 on
                SetB1(lb, true);
                Thread.Sleep(6);
                // Turn bit 2 on
                SetB2(lb, true);
                Thread.Sleep(6);
                // Turn bit 3 on
                SetB3(lb, true);
                Thread.Sleep(6);
                // Turn bit 4 on
                SetB4(lb, true);
                Thread.Sleep(6);
                // Turn bit 1 off
                SetB1(lb, false);
                Thread.Sleep(6);
                // Turn bit 2 off
                SetB2(lb, false);
                Thread.Sleep(6);
                // Turn bit 3 off
                SetB3(lb, false);
                Thread.Sleep(6);
                // Turn bit 4 off
                SetB4(lb, false);
                Thread.Sleep(6);

                programmingMode = true;
            }
        }

        /// <summary>
        /// Set the target turnout
        /// </summary>
        internal void SetTarget(LocoBuffer lb)
        {
            EnterProgrammingMode(lb);

            SendNibble(lb, 0x01); // Command
            SendNibble(lb, Turnout - 1); // Turnout encoded as 0-3
        }

        /// <summary>
        /// Set the left edge position in degrees
        /// </summary>
        /// <param name="value">Maximum left position (in time units) from 1 - 100</param>
        internal void SetLeftDegrees(LocoBuffer lb, int value)
        {
            EnterProgrammingMode(lb);

            SendNibble(lb, 0x02); // Command
            SendNibble(lb, (value >> 1) & 0x07); // value bit 4,3,2
            SendNibble(lb, (value >> 4) & 0x07); // value bit 7,6,5
        }

        /// <summary>
        /// Set the right edge position in degrees
        /// </summary>
        /// <param name="value">Maximum right position (in time units) from 1 - 100</param>
        internal void SetRightDegrees(LocoBuffer lb, int value)
        {
            EnterProgrammingMode(lb);

            SendNibble(lb, 0x03); // Command
            SendNibble(lb, (value >> 1) & 0x07); // value bit 4,3,2
            SendNibble(lb, (value >> 4) & 0x07); // value bit 7,6,5
        }

        /// <summary>
        /// Set the direction of the relays. 
        /// </summary>
        /// <param name="leftLsb">If true, left is the LSB and right is MSB, otherwise left is MSB and right is LSB</param>
        internal void SetRelaisPosition(LocoBuffer lb, bool leftLsb)
        {
            EnterProgrammingMode(lb);

            SendNibble(lb, 0x04); // Command
            SendNibble(lb, leftLsb ? 0 : 1);
        }

        /// <summary>
        /// Set the speed of a switch.
        /// </summary>
        /// <param name="value">Between 1 and 4</param>
        internal void SetSpeed(LocoBuffer lb, int value)
        {
            if ((value < 1) || (value > 4))
            {
                throw new ArgumentException("Value must be between 1 and 4");
            }

            EnterProgrammingMode(lb);

            SendNibble(lb, 0x05); // Command
            SendNibble(lb, SpeedMasks[value - 1] & 0x07); // LSB
            SendNibble(lb, (SpeedMasks[value - 1] >> 3) & 0x07); // MSB
        }

        /// <summary>
        /// Set the duration from left to right
        /// </summary>
        internal void ExitProgrammingMode(LocoBuffer lb)
        {
            if (programmingMode)
            {
                SendNibble(lb, 0x07); // Command
                programmingMode = false;

                // Reset all 4 bits
                SetB4(lb, false);
                SetB3(lb, false);
                SetB2(lb, false);
                SetB1(lb, false);
            }
        }

        /// <summary>
        /// Send a 3-bits nibble.
        /// </summary>
        private void SendNibble(LocoBuffer lb, int value)
        {
            // Lower b4 first
            SetB4(lb, false);

            // Set b1
            SetB1(lb, (value & 0x01) != 0);
            // Set b2
            SetB2(lb, (value & 0x02) != 0);
            // Set b3
            SetB3(lb, (value & 0x04) != 0);

            // Raise B4 to transmit
            SetB4(lb, true);

            // Wait a while
            Thread.Sleep(20);

            // Lower B4 to avoid duplication
            SetB4(lb, false);

            // Wait a while
            Thread.Sleep(5);
        }

        private void SetB1(LocoBuffer lb, bool on) { Execute(lb, new SwitchRequest { Address = Address1, Direction = on, Output = true }); }
        private void SetB2(LocoBuffer lb, bool on) { Execute(lb, new SwitchRequest { Address = Address2, Direction = on, Output = true }); }
        private void SetB3(LocoBuffer lb, bool on) { Execute(lb, new SwitchRequest { Address = Address3, Direction = on, Output = true }); }
        private void SetB4(LocoBuffer lb, bool on) { Execute(lb, new SwitchRequest { Address = Address4, Direction = on, Output = true }); }

        /// <summary>
        /// Execute the given request.
        /// </summary>
        private static void Execute(LocoBuffer lb, Request request)
        {
            request.Execute(lb);
        }
    }
}

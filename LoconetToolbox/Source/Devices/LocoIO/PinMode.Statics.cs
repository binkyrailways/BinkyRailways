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
    partial class PinMode
    {
        public static readonly PinMode BlockActiveHigh = new PinMode(PinMode.Directions.Input, Constants.OPC_INPUT_REP, 0x5F, 0x00, "Block Detector, Active High");
        public static readonly PinMode BlockActiveLow = new PinMode(PinMode.Directions.Input, Constants.OPC_INPUT_REP, 0x1F, 0x10, "Block Detector, Active Low");
        public static readonly PinMode BlockActiveHighDelay = new PinMode(PinMode.Directions.Input, Constants.OPC_INPUT_REP, 0x5B, 0x00, "Block Detector, Reset Delay, Active High");
        public static readonly PinMode BlockActiveLowDelay = new PinMode(PinMode.Directions.Input, Constants.OPC_INPUT_REP, 0x1B, 0x10, "Block Detector, Reset Delay, Active Low");
        public static readonly PinMode ToggleSwitchDirect = new PinMode(PinMode.Directions.Input, Constants.OPC_SW_REQ, 0x0F, 0x10, "Toggle Switch, Direct Control");
        public static readonly PinMode ToggleSwitchIndirect = new PinMode(PinMode.Directions.Input, Constants.OPC_SW_REP, 0x07, 0x10, "Toggle Switch, Indirect Control");
        public static readonly PinMode PushButtonActiveHighDirect = new PinMode(PinMode.Directions.Input, Constants.OPC_SW_REQ, 0x6F, 0x00, "Push Button, Active High, Direct Control");
        public static readonly PinMode PushButtonActiveHighIndirect = new PinMode(PinMode.Directions.Input, Constants.OPC_SW_REP, 0x67, 0x00, "Push Button, Active High, Indirect Control");
        public static readonly PinMode PushButtonActiveLowDirect = new PinMode(PinMode.Directions.Input, Constants.OPC_SW_REQ, 0x2F, 0x10, "Push Button, Active Low, Direct Control");
        public static readonly PinMode PushButtonActiveLowIndirect = new PinMode(PinMode.Directions.Input, Constants.OPC_SW_REP, 0x27, 0x10, "Push Button, Active Low, Indirect Control");
        public static readonly PinMode TurnoutFeedbackSingle = new PinMode(PinMode.Directions.Input, Constants.OPC_SW_REP, 0x17, 0x70, "Turnout Feedback, single sensor");
        public static readonly PinMode TurnoutFeedbackDual1 = new PinMode(PinMode.Directions.Input, Constants.OPC_SW_REP, 0x37, 0x70, "Turnout Feedback, dual sensor, #1");
        public static readonly PinMode TurnoutFeedbackDual2 = new PinMode(PinMode.Directions.Input, Constants.OPC_SW_REP, 0x37, 0x60, "Turnout Feedback, dual sensor, #2");

        public static readonly PinMode BlockOccupied = new PinMode(PinMode.Directions.Output, Constants.OPC_INPUT_REP, 0xC0, 0x00, "Block Occupied Indication");
        public static readonly PinMode BlockOccupiedBlinking = new PinMode(PinMode.Directions.Output, Constants.OPC_INPUT_REP, 0xD0, 0x00, "Block Occupied Indication, Blinking");
        public static readonly PinMode SteadyStateSingleOn = new PinMode(PinMode.Directions.Output, Constants.OPC_SW_REQ, 0x81, 0x10, "Steady State, single output, On at Power up");
        public static readonly PinMode SteadyStateSingleOff = new PinMode(PinMode.Directions.Output, Constants.OPC_SW_REQ, 0x80, 0x10, "Steady State, single output, Off at Power up");
        public static readonly PinMode SteadyStatePairedOn = new PinMode(PinMode.Directions.Output, Constants.OPC_SW_REQ, 0x81, 0x30, "Steady State, paired output, On at Power up");
        public static readonly PinMode SteadyStatePairedOff = new PinMode(PinMode.Directions.Output, Constants.OPC_SW_REQ, 0x80, 0x30, "Steady State, paired output, Off at Power up");
        public static readonly PinMode SteadyStateSingleOnBlinking = new PinMode(PinMode.Directions.Output, Constants.OPC_SW_REQ, 0x91, 0x10, "Steady State, single output, On at Power up, Blinking");
        public static readonly PinMode SteadyStateSingleOffBlinking = new PinMode(PinMode.Directions.Output, Constants.OPC_SW_REQ, 0x90, 0x10, "Steady State, single output, Off at Power up, Blinking");
        public static readonly PinMode SteadyStatePairedOnBlinking = new PinMode(PinMode.Directions.Output, Constants.OPC_SW_REQ, 0x91, 0x30, "Steady State, paired output, On at Power up, Blinking");
        public static readonly PinMode SteadyStatePairedOffBlinking = new PinMode(PinMode.Directions.Output, Constants.OPC_SW_REQ, 0x90, 0x30, "Steady State, paired output, Off at Power up, Blinking");
        public static readonly PinMode PulseSoftwareSingle = new PinMode(PinMode.Directions.Output, Constants.OPC_SW_REQ, 0x88, 0x20, "Pulsed, software controlled on time, single output");
        public static readonly PinMode PulseFirmwareSingle = new PinMode(PinMode.Directions.Output, Constants.OPC_SW_REQ, 0x8C, 0x20, "Pulsed, firmware controlled on time, single output");
        public static readonly PinMode PulseSoftwarePaired = new PinMode(PinMode.Directions.Output, Constants.OPC_SW_REQ, 0x88, 0x00, "Pulsed, software controlled on time, paired output");
        public static readonly PinMode PulseFirmwarePaired = new PinMode(PinMode.Directions.Output, Constants.OPC_SW_REQ, 0x8C, 0x00, "Pulsed, firmware controlled on time, paired output");

        private static readonly PinMode[] modes = new PinMode[] {

            // Inputs
            new PinMode(PinMode.Directions.Input, Constants.OPC_SW_REQ,    0x0F, 0x00, "Toggle Switch, LocoIO 1.3.2"),
            BlockActiveHigh,
            BlockActiveLow,
            BlockActiveHighDelay,
            BlockActiveLowDelay,
            ToggleSwitchDirect,
            ToggleSwitchIndirect,
            PushButtonActiveHighDirect,
            PushButtonActiveHighIndirect,
            PushButtonActiveLowDirect,
            PushButtonActiveLowIndirect,
            TurnoutFeedbackSingle,
            TurnoutFeedbackDual1,
            TurnoutFeedbackDual2,

            // Outputs
            BlockOccupied,
            BlockOccupiedBlinking,
            SteadyStateSingleOn,
            SteadyStateSingleOff,
            SteadyStatePairedOn,
            SteadyStatePairedOff,
            SteadyStateSingleOnBlinking,
            SteadyStateSingleOffBlinking,
            SteadyStatePairedOnBlinking,
            SteadyStatePairedOffBlinking,
            PulseSoftwareSingle,
            PulseFirmwareSingle,
            PulseSoftwarePaired,
            PulseFirmwarePaired 
        };

        /// <summary>
        /// Gets all modes
        /// </summary>
        public static IEnumerable<PinMode> All
        {
            get { return modes; }
        }

        /// <summary>
        /// Gets all inputs modes
        /// </summary>
        public static IEnumerable<PinMode> Inputs
        {
            get { return modes.Where(x => x.Direction == Directions.Input); }
        }

        /// <summary>
        /// Gets all outputs modes
        /// </summary>
        public static IEnumerable<PinMode> Outputs
        {
            get { return modes.Where(x => x.Direction == Directions.Output); }
        }

        /// <summary>
        /// Find a matching mode for the given values.
        /// Returns null if not found
        /// </summary>
        public static PinMode Find(int cv, int v1, int v2)
        {
            // v2 &= 0x0F;
            foreach (var m in modes)
            {
                if (m.SV0 == cv)
                {
                    if ((m.Opcode == Constants.OPC_INPUT_REP) &&
                        (m.SV2 == (v2 & 0xD0)))
                    {
                        return m;
                    }
                    else if (((cv == 0x6F) || (cv == 0x67) || (cv == 0x2F) || (cv == 0x27))
                            && (m.SV2 == (v2 & 0x50)))
                    {
                        return m;
                    }
                    else if ((m.SV2 == (v2 & 0xB0)))
                    {
                        return m;
                    }
                    else if (((cv & 0x90) == 0x10)
                           && ((cv & 0x80) == 0)
                           && (m.SV2 == (v2 & 0x70)))
                    {
                        return m;
                    }
                }
            }
            return null;
        }
    }
}

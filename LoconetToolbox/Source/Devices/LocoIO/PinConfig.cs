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

namespace LocoNetToolBox.Devices.LocoIO
{
    /// <summary>
    /// SV settings for a single port of a LocoIO module.
    /// </summary>
    public class PinConfig : IEnumerable<SVConfig>
    {
        public event EventHandler AddressChanged;
        public event EventHandler ModeChanged;

        // Helper
        private bool updating = false;

        // mode
        private PinMode mode;
        private int address;

        // SV values
        private readonly SVConfig config;
        private readonly SVConfig value1;
        private readonly SVConfig value2;

        /// <summary>
        /// Default ctor
        /// </summary>
        internal PinConfig(int pin)
        {
            if ((pin < 1) || (pin > 16))
            {
                throw new ArgumentException("Invalid pin " + pin);
            }

            int index = pin * 3;
            this.config = new SVConfig(index);
            this.value1 = new SVConfig(index + 1);
            this.value2 = new SVConfig(index + 2);

            config.ValueChanged += (s, x) => UpdateFunctionalSettings();
            value1.ValueChanged += (s, x) => UpdateFunctionalSettings();
            value2.ValueChanged += (s, x) => UpdateFunctionalSettings();
        }

        /// <summary>
        /// Mode of the pin.
        /// </summary>
        public PinMode Mode
        {
            get { return mode; }
            set { mode = value; UpdateValues(); ModeChanged.Fire(this); }
        }

        /// <summary>
        /// Loconet address of this port.
        /// </summary>
        public int Address
        {
            get { return address; }
            set { address = value; UpdateValues(); AddressChanged.Fire(this); }
        }

        // SV values

        /// <summary>
        /// First SV for this pin.
        /// </summary>
        public byte Config
        {
            get { return config.Value; }
            set { config.Value = value; config.Valid = true; UpdateFunctionalSettings(); }
        }

        /// <summary>
        /// Second SV for this pin.
        /// </summary>
        public byte Value1
        {
            get { return value1.Value; }
            set { value1.Value = value; value1.Valid = true; UpdateFunctionalSettings(); }
        }

        /// <summary>
        /// Third SV for this pin.
        /// </summary>
        public byte Value2
        {
            get { return value2.Value; }
            set { value2.Value = value; value2.Valid = true; UpdateFunctionalSettings(); }
        }

        /// <summary>
        /// Update functional settings fom SV values
        /// </summary>
        private void UpdateFunctionalSettings()
        {
            if (!updating)
            {
                try
                {
                    updating = true;
                    this.mode = PinMode.Find(config.Value, value1.Value, value2.Value);
                    if (this.mode != null)
                    {
                        this.address = mode.GetAddress(config.Value, value1.Value, value2.Value);
                    }
                    else
                    {
                        this.address = 0;
                    }
                    ModeChanged.Fire(this);
                    AddressChanged.Fire(this);
                }
                finally
                {
                    updating = false;
                }
            }
        }

        /// <summary>
        /// Calculate config, value1 and value2 from functional settings.
        /// </summary>
        private void UpdateValues()
        {
            if (!updating)
            {
                try
                {
                    updating = true;
                    if (mode == null)
                    {
                        config.Value = 0;
                        value1.Value = 0;
                        value2.Value = 0;
                    }
                    else
                    {
                        config.Value = (byte)mode.GetConfig();
                        value1.Value = (byte)mode.GetValue1(address);
                        value2.Value = (byte)mode.GetValue2(address);
                    }
                }
                finally
                {
                    updating = false;
                }
            }
        }

        /// <summary>
        /// Enumerate Config, Value1, Value2
        /// </summary>
        public IEnumerator<SVConfig> GetEnumerator()
        {
            yield return config;
            yield return value1;
            yield return value2;
        }

        /// <summary>
        /// Enumerate Config, Value1, Value2
        /// </summary>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}

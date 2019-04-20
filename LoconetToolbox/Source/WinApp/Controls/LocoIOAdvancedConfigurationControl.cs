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
using System.Windows.Forms;

using LocoNetToolBox.Devices.LocoIO;
using LocoNetToolBox.WinApp.Communications;

namespace LocoNetToolBox.WinApp.Controls
{
    public partial class LocoIOAdvancedConfigurationControl : UserControl
    {
        public event EventHandler BusyChanged;
        public event EventHandler WriteSucceeded;

        private AsyncLocoBuffer lb;
        private Programmer programmer;
        private int busy;

        private readonly Label[] labels;
        private readonly LocoIOPinConfigurationControl[] pinControls;

        /// <summary>
        /// Default ctor
        /// </summary>
        public LocoIOAdvancedConfigurationControl()
        {
            InitializeComponent();

            labels = new[] { lbPin1, lbPin2, lbPin3, lbPin4, lbPin5, lbPin6, lbPin7, lbPin8,
                lbPin9, lbPin10, lbPin11, lbPin12, lbPin13, lbPin14, lbPin15, lbPin16 };
            pinControls = new[] { pin1, pin2, pin3, pin4, pin5, pin6, pin7, pin8,
                pin9, pin10, pin11, pin12, pin13, pin14, pin15, pin16 };

            for (int i = 0; i < 16; i++)
            {
                labels[i].Text = (1 + i).ToString();
                pinControls[i].Pin = 1 + i;
                pinControls[i].Read += ReadPin;
                pinControls[i].Write += WritePin;
            }
        }

        /// <summary>
        /// Is a read/write action busy?
        /// </summary>
        internal bool Busy
        {
            get { return (busy > 0); }
            set
            {
                var oldValue = Busy;
                if (value) { busy++; }
                else { busy--; }
                if (oldValue != Busy)
                {
                    BusyChanged.Fire(this);
                }
            }
        }

        /// <summary>
        /// Initialize for a specific module
        /// </summary>
        internal void Initialize(AsyncLocoBuffer lb, Programmer programmer)
        {
            this.lb = lb;
            this.programmer = programmer;
        }

        /// <summary>
        /// Create a configuration from the settings found in this control.
        /// </summary>
        public PinConfigList CreateConfig()
        {
            var list = new PinConfig[16];
            for (int i = 0; i < 16; i++)
            {
                list[i] = pinControls[i].CreateConfig();
            }
            return new PinConfigList(list);
        }

        /// <summary>
        /// Read all pins
        /// </summary>
        internal void Read()
        {
            Busy = true;
            var config = new LocoIOConfig();
            Enabled = false;
            lb.BeginRequest(
                x => programmer.Read(x, config),
                x =>
                {
                    Enabled = true;
                    Busy = false;
                    if (x.HasError)
                    {
                        MessageBox.Show(x.Error.Message);
                    }
                    else
                    {
                        for (int i = 0; i < 16; i++)
                        {
                            pinControls[i].LoadFrom(config.Pins[i]);
                        }
                    }
                });
        }

        /// <summary>
        /// Write all pins
        /// </summary>
        internal void Write()
        {
            Busy = true;
            var settings = CreateConfig();
            Enabled = false;
            lb.BeginRequest(
                x => programmer.Write(x, settings.GetSVConfigs()),
                x =>
                {
                    Enabled = true;
                    Busy = false;
                    if (x.HasError)
                    {
                        MessageBox.Show(x.Error.Message);
                    }
                    else
                    {
                        WriteSucceeded.Fire(this);
                    }
                });
        }

        /// <summary>
        /// Read a single pin.
        /// </summary>
        private void ReadPin(object sender, EventArgs e)
        {
            Busy = true;
            var control = (LocoIOPinConfigurationControl) sender;
            var config = control.CreateConfig();
            control.Enabled = false;
            lb.BeginRequest(
                x => programmer.Read(x, config),
                x =>
                {
                    control.Enabled = true;
                    Busy = false;
                    if (x.HasError)
                    {
                        MessageBox.Show(x.Error.Message);
                    }
                    else
                    {
                        control.LoadFrom(config);
                    }
                });
        }

        /// <summary>
        /// Write a single pin
        /// </summary>
        internal void WritePin(object sender, EventArgs e)
        {
            Busy = true;
            var control = (LocoIOPinConfigurationControl)sender;
            var config = control.CreateConfig();
            control.Enabled = false;
            lb.BeginRequest(
                x => programmer.Write(x, config),
                x =>
                {
                    control.Enabled = true;
                    Busy = false;
                    if (x.HasError)
                    {
                        MessageBox.Show(x.Error.Message);
                    }
                    else
                    {
                        WriteSucceeded.Fire(this);
                    }
                });
        }
    }
}

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
using LocoNetToolBox.Protocol;
using LocoNetToolBox.WinApp.Communications;

namespace LocoNetToolBox.WinApp.Controls
{
    public partial class LocoIOConfigurationControl : UserControl
    {
        public event EventHandler BusyChanged;
        public event EventHandler WriteSucceeded;

        private LocoIOConfig config;
        private AsyncLocoBuffer lb;
        private Programmer programmer;
        private int busy;

        /// <summary>
        /// Default ctor
        /// </summary>
        public LocoIOConfigurationControl()
        {
            InitializeComponent();
            connector1.Connector = Connector.First;
            connector2.Connector = Connector.Second;
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
        /// Connect to the given config
        /// </summary>
        public void Connect(LocoIOConfig config)
        {
            this.config = config;
            connector1.Connect(config.ConnectorA);
            connector2.Connect(config.ConnectorB);
        }

        /// <summary>
        /// Read all settings
        /// </summary>
        internal void ReadAll(LocoBuffer lb, LocoNetAddress address)
        {
            // Create a set of all SV's that are relevant
            /*var configs = LocoIOConfig.GetAllSVs();

            // Create the programmer
            var programmer = new Programmer(lb, address);

            // Read all SV's
            programmer.Read(configs);

            // Get all properly read configs
            var validConfigs = configs.Where(x => x.Valid).ToArray();
            */
        }

        private void Write(LocoIOConnectorConfigurationControl connector, Button cmdWrite)
        {
            Busy = true;
            var settings = connector.CreateConfig();
            connector.Enabled = false;
            cmdWrite.Enabled = false;
            lb.BeginRequest(
                x => programmer.Write(x, settings),
                x =>
                {
                    connector.Enabled = true;
                    cmdWrite.Enabled = true;
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
        /// Write to the first connector
        /// </summary>
        private void CmdWrite1Click(object sender, EventArgs e)
        {
            Write(connector1, cmdWrite1);
        }

        /// <summary>
        /// Write to the second connector
        /// </summary>
        private void CmdWrite2Click(object sender, EventArgs e)
        {
            Write(connector2, cmdWrite2);
        }
    }
}

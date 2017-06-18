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
    public partial class LocoIOConfigurationForm : Form
    {
        private readonly LocoIOConfig config = new LocoIOConfig();
        private Programmer programmer;
        private AsyncLocoBuffer lb;

        /// <summary>
        /// Default ctor
        /// </summary>
        public LocoIOConfigurationForm()
        {
            InitializeComponent();
            configurationControl.Connect(config);

            cmdReadAll.Enabled = false;
            cmdWriteAll.Enabled = false;
            lbResetWarning.Visible = false;
        }

        /// <summary>
        /// Initialize for a specific module
        /// </summary>
        internal void Initialize(AsyncLocoBuffer lb, LocoNetAddress address)
        {
            this.lb = lb;
            this.programmer = new Programmer(address);
            configurationControl.Initialize(lb, programmer);
            Text += string.Format(" [{0}]", address);
        }

        /// <summary>
        /// Form is about to close.
        /// </summary>
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            if (configurationControl.Busy)
                e.Cancel = true;
        }

        /// <summary>
        /// Close me.
        /// </summary>
        private void CmdCloseClick(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Programmer busy?
        /// </summary>
        private void ConfigurationControlBusyChanged(object sender, EventArgs e)
        {
            var busy = configurationControl.Busy;
            cmdClose.Enabled = !busy;
            if (busy)
                lbResetWarning.Visible = false;
        }

        /// <summary>
        /// Writing of locoIO has succeeded.
        /// </summary>
        private void configurationControl_WriteSucceeded(object sender, EventArgs e)
        {
            lbResetWarning.Visible = true;
        }
    }
}

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
using System.ComponentModel;
using System.Threading;
using System.Windows.Forms;
using LocoNetToolBox.Model;
using LocoNetToolBox.Protocol;
using LocoNetToolBox.WinApp.Communications;

namespace LocoNetToolBox.WinApp.Controls
{
    public partial class ServoTester : Form
    {
        private readonly AsyncLocoBuffer lb;
        private readonly ILocoNetState lnState;

        /// <summary>
        /// Designer ctor
        /// </summary>
        public ServoTester() : this(null, null)
        {
        }

        /// <summary>
        /// Default ctor
        /// </summary>
        public ServoTester(AsyncLocoBuffer lb, ILocoNetState lnState)
        {
            this.lb = lb;
            this.lnState = lnState;
            InitializeComponent();
        }

        /// <summary>
        /// Execute the given request.
        /// </summary>
        private void Execute(Request request)
        {
            lb.BeginRequest(request, e =>
            {
                if (e.HasError)
                {
                    MessageBox.Show(e.Error.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            });
        }

        /// <summary>
        /// Go left.
        /// </summary>
        private void CmdLeftClick(object sender, EventArgs e)
        {
            Execute(new SwitchRequest
                        {
                Address = (int)udAddress.Value - 1,
                Direction = true,
                Output = true
            });
        }

        /// <summary>
        /// Go right
        /// </summary>
        private void CmdRightClick(object sender, EventArgs e)
        {
            Execute(new SwitchRequest
                        {
                Address = (int)udAddress.Value - 1,
                Direction = false,
                Output = true
            });
        }

        /// <summary>
        /// Start a duration test
        /// </summary>
        private void CmdStartClick(object sender, EventArgs e)
        {
            cmdStart.Enabled = false;
            cmdStop.Enabled = true;
            testWorker.RunWorkerAsync((int) udAddress.Value);
        }

        /// <summary>
        /// Stop the duration test
        /// </summary>
        private void CmdStopClick(object sender, EventArgs e)
        {
            cmdStop.Enabled = false;
            testWorker.CancelAsync();
        }

        /// <summary>
        /// Run duration test
        /// </summary>
        private void TestWorkerDoWork(object sender, DoWorkEventArgs e)
        {
            var address = (int) e.Argument;
            var direction = true;
            while (!testWorker.CancellationPending)
            {
                // Run the test
                Execute(new SwitchRequest
                            {
                    Address = address - 1,
                    Direction = direction,
                    Output = true
                });
                if (!lnState.WaitForSwitchDirection(address, !direction, 7000))
                {
                    return;
                }

                Thread.Sleep(500);
                direction = !direction;
            }
        }

        /// <summary>
        /// Test stopped
        /// </summary>
        private void TestWorkerRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            cmdStart.Enabled = true;
            cmdStop.Enabled = false;
        }
    }
}

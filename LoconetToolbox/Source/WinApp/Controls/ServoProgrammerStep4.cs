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
using LocoNetToolBox.WinApp.Communications;

namespace LocoNetToolBox.WinApp.Controls
{
    public partial class ServoProgrammerStep4 : UserControl
    {
        private Devices.MgvServo.ServoProgrammer programmer;
        private AsyncLocoBuffer lb;

        /// <summary>
        /// Default ctor
        /// </summary>
        public ServoProgrammerStep4()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Default ctor
        /// </summary>
        internal void Initialize(AsyncLocoBuffer lb, Devices.MgvServo.ServoProgrammer programmer)
        {
            this.lb = lb;
            this.programmer = programmer;
            programmer.Turnout = turnoutSelection.Turnout;
        }

        /// <summary>
        /// Set left position
        /// </summary>
        private void cmdSetLeft_Click(object sender, EventArgs e)
        {
            lb.BeginRequest(
                x => programmer.SetLeftDegrees(x, (int)udLeft.Value), 
                CompletedHandler);
        }

        /// <summary>
        /// Set right position
        /// </summary>
        private void cmdSetRight_Click(object sender, EventArgs e)
        {
            lb.BeginRequest(
                x => programmer.SetRightDegrees(x, (int) udRight.Value),
                CompletedHandler);
        }

        /// <summary>
        /// Update target
        /// </summary>
        private void turnoutSelection_TurnoutChanged(object sender, EventArgs e)
        {
            lb.BeginRequest(
                x =>
                    {
                        programmer.Turnout = turnoutSelection.Turnout;
                        programmer.SetTarget(x);
                    },
                CompletedHandler);
        }

        /// <summary>
        /// Set turnout speed
        /// </summary>
        private void cmdSetSpeed_Click(object sender, EventArgs e)
        {
            lb.BeginRequest(
                x => programmer.SetSpeed(x, (int) udSpeed.Value),
                CompletedHandler);
        }

        private void cmdSetRelay_Click(object sender, EventArgs e)
        {
            lb.BeginRequest(
                x => programmer.SetRelaisPosition(x, cbLeftLSB.Checked),
                CompletedHandler);
        }

        private static void CompletedHandler(AsyncRequestCompletedEventArgs e)
        {
            if (e.HasError)
            {
                MessageBox.Show(e.Error.Message);
            }
        }
    }
}

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
    public partial class ServoProgrammer : Form
    {
        private AsyncLocoBuffer lb;
        private readonly Devices.MgvServo.ServoProgrammer programmer;

        /// <summary>
        /// Default ctor
        /// </summary>
        [Obsolete("Designer only")]
        public ServoProgrammer()
            : this(null)
        {
        }

        /// <summary>
        /// Default ctor
        /// </summary>
        internal ServoProgrammer(AsyncLocoBuffer lb)
        {
            this.lb = lb;
            this.programmer = new Devices.MgvServo.ServoProgrammer();
            InitializeComponent();
            step1.Initialize(lb, programmer);
        }

        /// <summary>
        /// Exit programmer mode on close.
        /// </summary>
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            programmer.ExitProgrammingMode(lb.LocoBuffer);
            base.OnFormClosing(e);
        }

        private void step1_Continue(object sender, EventArgs e)
        {
            this.SuspendLayout();
            step1.Visible = false;
            step2.Visible = true;
            step2.Initialize(lb, programmer);
            lbStep.Text = "Step 2/4";
            this.ResumeLayout(true);
        }

        private void step2_Continue(object sender, EventArgs e)
        {
            this.SuspendLayout();
            step2.Visible = false;
            step3.Visible = true;
            lbStep.Text = "Step 3/4";
            this.ResumeLayout(true);
        }

        private void step3_Continue(object sender, EventArgs e)
        {
            this.SuspendLayout();
            step3.Visible = false;
            step4.Visible = true;
            step4.Initialize(lb, programmer);
            lbStep.Text = "Step 4/4";
            this.ResumeLayout(true);
        }
    }
}

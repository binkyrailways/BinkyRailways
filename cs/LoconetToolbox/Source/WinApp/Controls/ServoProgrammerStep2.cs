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
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

using LocoNetToolBox.Protocol;
using LocoNetToolBox.WinApp.Communications;
using Message = LocoNetToolBox.Protocol.Message;

namespace LocoNetToolBox.WinApp.Controls
{
    public partial class ServoProgrammerStep2 : UserControl
    {
        public event EventHandler Continue;

        private Devices.MgvServo.ServoProgrammer programmer;
        private AsyncLocoBuffer lb;

        /// <summary>
        /// Default ctor
        /// </summary>
        public ServoProgrammerStep2()
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
        }

        /// <summary>
        /// Go into programming mode.
        /// </summary>
        private void cmdEnterProgramMode_Click(object sender, EventArgs e)
        {
            lb.BeginRequest(
                x => programmer.EnterProgrammingMode(x),
                x =>
                    {
                        if (Continue != null)
                        {
                            Continue(this, EventArgs.Empty);
                        }
                    });
        }
    }
}

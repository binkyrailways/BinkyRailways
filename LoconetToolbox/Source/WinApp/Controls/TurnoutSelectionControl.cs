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
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LocoNetToolBox.WinApp.Controls
{
    public partial class TurnoutSelectionControl : UserControl
    {
        /// <summary>
        /// Turnlout selection has changed
        /// </summary>
        public event EventHandler TurnoutChanged;

        /// <summary>
        /// Default ctor
        /// </summary>
        public TurnoutSelectionControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Gets the selected turnout (1,2,3,4)
        /// </summary>
        public int Turnout
        {
            get
            {
                if (rb1.Checked) { return 1; }
                if (rb2.Checked) { return 2; }
                if (rb3.Checked) { return 3; }
                if (rb4.Checked) { return 4; }
                return 1;
            }
        }

        /// <summary>
        /// Selection has changed.
        /// </summary>
        private void OnCheckedChanged(object sender, EventArgs e)
        {
            // No need to do anything now.
            if (TurnoutChanged != null) { TurnoutChanged(this, EventArgs.Empty); }
        }
    }
}

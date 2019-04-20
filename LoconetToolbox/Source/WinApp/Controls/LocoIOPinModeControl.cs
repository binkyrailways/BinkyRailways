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
using System.Linq;
using System.Windows.Forms;

using LocoNetToolBox.Devices.LocoIO;

namespace LocoNetToolBox.WinApp.Controls
{
    public partial class LocoIOPinModeControl : UserControl
    {
        /// <summary>
        /// Fired when settings are changed.
        /// </summary>
        public event EventHandler Changed;

        private int updating = 0;
        private bool isInput = true;

        /// <summary>
        /// Default ctor
        /// </summary>
        public LocoIOPinModeControl()
        {
            InitializeComponent();
            cbModes.Items.AddRange(PinMode.All.ToArray());
        }

        /// <summary>
        /// Gets / sets the current mode
        /// </summary>
        public PinMode Mode
        {
            get { return cbModes.SelectedItem as PinMode; }
            set
            {
                try
                {
                    updating++;
                    if (value != null)
                    {
                        cbModes.SelectedIndex = cbModes.Items.IndexOf(value);
                    }
                    else
                    {
                        cbModes.SelectedIndex = -1;
                    }
                }
                finally
                {
                    updating--;
                }
            }
        }

        /// <summary>
        /// Selection has changed.
        /// </summary>
        private void cbModes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (updating == 0)
            {
                Changed.Fire(this);
            }
        }
    }
}

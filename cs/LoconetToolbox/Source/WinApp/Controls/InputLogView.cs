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

using System.Drawing;
using System.Windows.Forms;

using LocoNetToolBox.Protocol;

namespace LocoNetToolBox.WinApp.Controls
{
    public partial class InputLogView : UserControl
    {
        /// <summary>
        /// Default ctor
        /// </summary>
        public InputLogView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Process the given response.
        /// </summary>
        internal void ProcessResponse(Response value)
        {
            var inpRep = value as InputReport;
            if (inpRep != null)
            {
                var item = new ListViewItem(string.Format("Feedback {0} turned {1}", inpRep.Address + 1, inpRep.SensorLevel ? "On" : "Off"));
                item.ForeColor = inpRep.SensorLevel ? Color.Red : Color.Green;
                try
                {
                    lvInputs.BeginUpdate();
                    lvInputs.Items.Add(item);
                }
                finally
                {
                    lvInputs.EndUpdate();
                    item.EnsureVisible();
                }
            }
        }

        /// <summary>
        /// Update column width
        /// </summary>
        protected override void OnSizeChanged(System.EventArgs e)
        {
            base.OnSizeChanged(e);
            chMessage.Width = ClientSize.Width - 32;
        }

        /// <summary>
        /// Remove all entries.
        /// </summary>
        private void miClear_Click(object sender, System.EventArgs e)
        {
            lvInputs.Items.Clear();
        }
    }
}

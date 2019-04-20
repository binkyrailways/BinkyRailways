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
    public partial class DirectionControl : UserControl
    {
        public event EventHandler Changed;

        /// <summary>
        /// Default ctor
        /// </summary>
        public DirectionControl()
        {
            InitializeComponent();
            cbDirection.Items.Add(Strings.Input);
            cbDirection.Items.Add(Strings.Output);
            cbDirection.SelectedIndex = 0;
        }

        /// <summary>
        /// Is input selected?
        /// </summary>
        public bool IsInput
        {
            get { return (cbDirection.SelectedIndex == 0); }
            set { if (value) { cbDirection.SelectedIndex = 0; } }
        }

        /// <summary>
        /// Is output selected?
        /// </summary>
        public bool IsOutput
        {
            get { return (cbDirection.SelectedIndex == 1); }
            set { if (value) { cbDirection.SelectedIndex = 1; } }
        }

        /// <summary>
        /// Setting has changed
        /// </summary>
        private void cbDirection_SelectedIndexChanged(object sender, EventArgs e)
        {
            Changed.Fire(this);
        }
    }
}

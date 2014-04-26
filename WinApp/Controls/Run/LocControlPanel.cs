using System;
using System.Drawing;
using System.Windows.Forms;
using BinkyRailways.Core.Model;
using BinkyRailways.Core.State;

namespace BinkyRailways.WinApp.Controls.Run
{
    public partial class LocControlPanel : UserControl
    {
        private ILocState loc;
        private EventHandler actualStateChangedHandler;

        /// <summary>
        /// Default ctor
        /// </summary>
        public LocControlPanel()
        {
            InitializeComponent();
            Enabled = false;
        }

        /// <summary>
        /// Set the current loc
        /// </summary>
        internal ILocState Loc
        {
            get { return loc; }
            set
            {
                if (loc != value)
                {
                    if ((loc != null) && (actualStateChangedHandler != null))
                    {
                        loc.ActualStateChanged -= actualStateChangedHandler;
                    }
                    loc = value;
                    actualStateChangedHandler = null;
                    if (loc != null)
                    {
                        actualStateChangedHandler = this.ASynchronize(OnLocActualStateChanged);
                        loc.ActualStateChanged += actualStateChangedHandler;
                        OnLocActualStateChanged(null, null);
                    }
                    Enabled = (loc != null);
                    locImage.Loc = loc;
                    LoadFunctionNames();
                }
            }
        }

        /// <summary>
        /// Set the names of the various functions
        /// </summary>
        private void LoadFunctionNames()
        {
            if (loc != null)
            {
                bool isCustom;
                cbF0.Text = loc.GetFunctionName(LocFunction.Light, out isCustom);
                cbF0.Enabled = isCustom;
                cbF1.Text = loc.GetFunctionName(LocFunction.F1, out isCustom);
                cbF1.Enabled = isCustom;
                cbF2.Text = loc.GetFunctionName(LocFunction.F2, out isCustom);
                cbF2.Enabled = isCustom;
                cbF3.Text = loc.GetFunctionName(LocFunction.F3, out isCustom);
                cbF3.Enabled = isCustom;
                cbF4.Text = loc.GetFunctionName(LocFunction.F4, out isCustom);
                cbF4.Enabled = isCustom;
                cbF5.Text = loc.GetFunctionName(LocFunction.F5, out isCustom);
                cbF5.Enabled = isCustom;
                cbF6.Text = loc.GetFunctionName(LocFunction.F6, out isCustom);
                cbF6.Enabled = isCustom;
                cbF7.Text = loc.GetFunctionName(LocFunction.F7, out isCustom);
                cbF7.Enabled = isCustom;
                cbF8.Text = loc.GetFunctionName(LocFunction.F8, out isCustom);
                cbF8.Enabled = isCustom;
            }
            else
            {
                cbF0.Text = "Light";
                cbF0.Enabled = false;
                cbF1.Text = "F1";
                cbF1.Enabled = false;
                cbF2.Text = "F2";
                cbF2.Enabled = false;
                cbF3.Text = "F3";
                cbF3.Enabled = false;
                cbF4.Text = "F4";
                cbF4.Enabled = false;
                cbF5.Text = "F5";
                cbF5.Enabled = false;
                cbF6.Text = "F6";
                cbF6.Enabled = false;
                cbF7.Text = "F7";
                cbF7.Enabled = false;
                cbF8.Text = "F8";
                cbF8.Enabled = false;
            }
        }

        /// <summary>
        /// Actual state of this loc has changed
        /// </summary>
        private void OnLocActualStateChanged(object sender, EventArgs e)
        {
            if (loc == null)
                return;
            tbSpeed.Value = loc.Speed.Actual;
            var direction = loc.Direction.Actual;
            var activeColor = Color.GreenYellow;
            var inActiveColor = SystemColors.Control;
            cmdForward.Enabled = (direction == LocDirection.Reverse);
            cmdForward.BackColor = (direction == LocDirection.Forward) ? activeColor : inActiveColor;
            cmdReverse.Enabled = (direction == LocDirection.Forward);
            cmdReverse.BackColor = (direction == LocDirection.Reverse) ? activeColor : inActiveColor;
            cbF0.Checked = loc.F0.Actual;
            cbF1.Checked = loc.F1.Actual;
            cbF2.Checked = loc.F2.Actual;
            cbF3.Checked = loc.F3.Actual;
            cbF4.Checked = loc.F4.Actual;
            cbF5.Checked = loc.F5.Actual;
            cbF6.Checked = loc.F6.Actual;
            cbF7.Checked = loc.F7.Actual;
            cbF8.Checked = loc.F8.Actual;
        }

        /// <summary>
        /// Set direction to reverse
        /// </summary>
        private void cmdReverse_Click(object sender, EventArgs e)
        {
            if (loc != null)
            {
                loc.Direction.Requested = LocDirection.Reverse;
            }
        }

        /// <summary>
        /// Set direction to forward
        /// </summary>
        private void cmdForward_Click(object sender, EventArgs e)
        {
            if (loc != null)
            {
                loc.Direction.Requested = LocDirection.Forward;
            }
        }

        /// <summary>
        /// Change speed
        /// </summary>
        private void tbSpeed_Scroll(object sender, EventArgs e)
        {
            if (loc != null)
            {
                loc.Speed.Requested = tbSpeed.Value;
            }
        }

        /// <summary>
        /// Set requested speed to 0.
        /// </summary>
        private void cmdStop_Click(object sender, EventArgs e)
        {
            if (loc != null)
            {
                loc.Speed.Requested = 0;
            }
        }

        /// <summary>
        /// Update F0
        /// </summary>
        private void cbF0_CheckedChanged(object sender, EventArgs e)
        {
            if (loc != null)
            {
                loc.F0.Requested = cbF0.Checked;
            }
        }

        /// <summary>
        /// Update F1
        /// </summary>
        private void cbF1_CheckedChanged(object sender, EventArgs e)
        {
            if (loc != null)
            {
                loc.F1.Requested = cbF1.Checked;
            }
        }

        /// <summary>
        /// Update F2
        /// </summary>
        private void cbF2_CheckedChanged(object sender, EventArgs e)
        {
            if (loc != null)
            {
                loc.F2.Requested = cbF2.Checked;
            }
        }

        /// <summary>
        /// Update F3
        /// </summary>
        private void cbF3_CheckedChanged(object sender, EventArgs e)
        {
            if (loc != null)
            {
                loc.F3.Requested = cbF3.Checked;
            }
        }

        /// <summary>
        /// Update F4
        /// </summary>
        private void cbF4_CheckedChanged(object sender, EventArgs e)
        {
            if (loc != null)
            {
                loc.F4.Requested = cbF4.Checked;
            }
        }

        /// <summary>
        /// Update F5
        /// </summary>
        private void cbF5_CheckedChanged(object sender, EventArgs e)
        {
            if (loc != null)
            {
                loc.F5.Requested = cbF5.Checked;
            }
        }

        /// <summary>
        /// Update F6
        /// </summary>
        private void cbF6_CheckedChanged(object sender, EventArgs e)
        {
            if (loc != null)
            {
                loc.F6.Requested = cbF6.Checked;
            }
        }

        /// <summary>
        /// Update F7
        /// </summary>
        private void cbF7_CheckedChanged(object sender, EventArgs e)
        {
            if (loc != null)
            {
                loc.F7.Requested = cbF7.Checked;
            }
        }

        /// <summary>
        /// Update F8
        /// </summary>
        private void cbF8_CheckedChanged(object sender, EventArgs e)
        {
            if (loc != null)
            {
                loc.F8.Requested = cbF8.Checked;
            }
        }
    }
}

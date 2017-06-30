using System;
using System.Drawing;
using System.Windows.Forms;
using BinkyRailways.Core.Model;
using BinkyRailways.Core.State;
using System.Collections.Generic;

namespace BinkyRailways.WinApp.Controls.Run
{
    public partial class LocControlPanel : UserControl
    {
        private ILocState loc;
        private EventHandler actualStateChangedHandler;
        private readonly Dictionary<LocFunction, CheckBox> functionCheckboxes = new Dictionary<LocFunction,CheckBox>();

        /// <summary>
        /// Default ctor
        /// </summary>
        public LocControlPanel()
        {
            InitializeComponent();
            functionCheckboxes[LocFunction.F1] = cbF1;
            functionCheckboxes[LocFunction.F2] = cbF2;
            functionCheckboxes[LocFunction.F3] = cbF3;
            functionCheckboxes[LocFunction.F4] = cbF4;
            functionCheckboxes[LocFunction.F5] = cbF5;
            functionCheckboxes[LocFunction.F6] = cbF6;
            functionCheckboxes[LocFunction.F7] = cbF7;
            functionCheckboxes[LocFunction.F8] = cbF8;
            functionCheckboxes[LocFunction.F9] = cbF9;
            functionCheckboxes[LocFunction.F10] = cbF10;
            functionCheckboxes[LocFunction.F11] = cbF11;
            functionCheckboxes[LocFunction.F12] = cbF12;
            functionCheckboxes[LocFunction.F13] = cbF13;
            functionCheckboxes[LocFunction.F14] = cbF14;
            functionCheckboxes[LocFunction.F15] = cbF15;
            functionCheckboxes[LocFunction.F16] = cbF16;
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
                foreach (var pair in functionCheckboxes)
                {
                    pair.Value.Text = loc.GetFunctionName(pair.Key, out isCustom);
                    pair.Value.Enabled = isCustom;
                    //pair.Value.Visible = isCustom;
                }
            }
            else
            {
                cbF0.Text = "Light";
                cbF0.Enabled = false;
                foreach (var pair in functionCheckboxes)
                {
                    pair.Value.Text = pair.Key.ToString();
                    pair.Value.Enabled = false;
                }
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
            foreach (var pair in functionCheckboxes)
            {
                pair.Value.Checked = loc.GetFunctionActualState(pair.Key);
            }
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
        /// Update F1..16
        /// </summary>
        private void cbFX_CheckedChanged(object sender, EventArgs e)
        {
            if (loc != null)
            {
                var cb = (CheckBox)sender;
                foreach (var pair in functionCheckboxes)
                {
                    if (pair.Value == cb)
                    {
                        loc.SetFunctionRequestedState(pair.Key, cb.Checked);
                        break;
                    }
                }
            }
        }
    }
}

using System;
using System.Drawing;
using System.Windows.Forms;
using BinkyRailways.Core.State;
using BinkyRailways.Properties;

namespace BinkyRailways.WinApp.Controls.Run
{
    public partial class RailwayStateControlPanel : UserControl
    {
        private IRailwayState railway;

        /// <summary>
        /// Default ctor
        /// </summary>
        public RailwayStateControlPanel()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Set the current railway
        /// </summary>
        internal void Initialize(IRailwayState railway)
        {
            this.railway = railway;
            if (railway != null)
            {
                railway.Power.ActualChanged += this.Synchronize((s, x) => UpdateToolbar());
                railway.Power.RequestedChanged += this.Synchronize((s, x) => UpdateToolbar());
                railway.AutomaticLocController.Enabled.ActualChanged += this.Synchronize((s, x) => UpdateToolbar());
            }
            UpdateToolbar();
        }

        private Color powerButtonColor(bool on)
        {
            var power = (railway != null) ? railway.Power : null;
            if (power == null)
            {
                return SystemColors.GrayText;
            }
            if (!power.IsConsistent)
            {
                return (on == power.Requested) ? Color.Red : SystemColors.ControlText;
            }
            return (on == power.Actual) ? SystemColors.GrayText : SystemColors.ControlText;
        }

        /// <summary>
        /// Update the state.
        /// </summary>
        private void UpdateToolbar()
        {
            lbPower.Text = GetPowerInfo(railway);
            var power = (railway != null) ? railway.Power : null;
            cmdPowerOn.ForeColor = powerButtonColor(true);
            cmdPowerOff.ForeColor = powerButtonColor(false);
            pbPower.Image = (power != null) && power.Actual ? Resources.apply_22 : Resources.system_log_out_22;

            lbAutoControl.Text = GetAutoLocControllerInfo(railway);
            var autoLocs = (railway != null) ? railway.AutomaticLocController : null;
            cmdAutoOn.Enabled = (railway != null) && autoLocs.Enabled.IsConsistent && !autoLocs.Enabled.Actual;
            cmdAutoOff.Enabled = (railway != null) && autoLocs.Enabled.IsConsistent && autoLocs.Enabled.Actual;
            pbAuto.Image = (autoLocs != null) && autoLocs.Enabled.Actual
                               ? Resources.games_solve_22
                               : Resources.joystick_22;
        }

        /// <summary>
        /// Describe the power state
        /// </summary>
        private static string GetPowerInfo(IRailwayState railway)
        {
            if (railway == null)
                return string.Empty;
            var power = railway.Power;
            if (power.IsConsistent)
            {
                return power.Actual ? Strings.PowerIsOn : Strings.PowerIsOff;
            }
            return power.Requested ? Strings.PowerWillTurnOn : Strings.PowerWillTurnOff;
        }

        /// <summary>
        /// Describe the automatic loc controller
        /// </summary>
        private static string GetAutoLocControllerInfo(IRailwayState railway)
        {
            if (railway == null)
                return string.Empty;
            var autoLocs = railway.AutomaticLocController.Enabled;
            if (autoLocs.IsConsistent)
            {
                return autoLocs.Actual ? Strings.IsAuto : Strings.IsManual;
            }
            return autoLocs.Requested ? Strings.WillTurnAuto : Strings.WillTurnManual;
        }

        /// <summary>
        /// Power the railway
        /// </summary>
        private void OnPowerOnClick(object sender, EventArgs e)
        {
            if (railway != null)
            {
                railway.Power.Requested = true;
            }
        }

        /// <summary>
        /// Power off the raiway
        /// </summary>
        private void OnPowerOffClick(object sender, EventArgs e)
        {
            if (railway != null)
            {
                railway.Power.Requested = false;
            }
        }

        /// <summary>
        /// Start automatic behavior
        /// </summary>
        private void tbAutomatic_Click(object sender, EventArgs e)
        {
            if (railway != null)
            {
                railway.AutomaticLocController.Enabled.Requested = true;
            }
        }

        /// <summary>
        /// Stop automatic behavior
        /// </summary>
        private void tbManual_Click(object sender, EventArgs e)
        {
            if (railway != null)
            {
                railway.AutomaticLocController.Enabled.Requested = false;
            }
        }
    }
}

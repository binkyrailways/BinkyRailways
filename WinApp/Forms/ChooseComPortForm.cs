using System;
using System.IO.Ports;
using System.Windows.Forms;
using BinkyRailways.Core.Model;
using BinkyRailways.Core.State;

namespace BinkyRailways.WinApp.Forms
{
    public partial class ChooseComPortForm : AppForm
    {
        private readonly ICommandStationState cs;

        /// <summary>
        /// Designer ctor
        /// </summary>
        public ChooseComPortForm() : this(null)
        {
        }

        /// <summary>
        /// Default ctor
        /// </summary>
        public ChooseComPortForm(ICommandStationState cs)
        {
            this.cs = cs;
            InitializeComponent();
            if (cs != null)
            {
                lbInfo.Text = string.Format(lbInfo.Text, cs.Description);
                lbPortNames.Items.AddRange(SerialPort.GetPortNames());
                if (lbPortNames.Items.Count > 0)
                {
                    lbPortNames.SelectedIndex = 0;
                }
            }
            UpdateControls();
        }

        /// <summary>
        /// Gets the selected COM port name
        /// </summary>
        public string SelectedPortName
        {
            get { return lbPortNames.SelectedItem as string; }
        }

        /// <summary>
        /// Update the state of the controls.
        /// </summary>
        private void UpdateControls()
        {
            cmdOk.Enabled = (SelectedPortName != null);
        }

        /// <summary>
        /// Selection has changed.
        /// </summary>
        private void lbPortNames_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateControls();
        }

        /// <summary>
        /// Save changes
        /// </summary>
        private void cmdOk_Click(object sender, EventArgs e)
        {
            if ((cs != null) && cbChangeEntity.Checked)
            {
                var entity = (ISerialPortCommandStation) cs.Model;
                entity.ComPortName = SelectedPortName;
            }
            DialogResult = DialogResult.OK;
        }
    }
}

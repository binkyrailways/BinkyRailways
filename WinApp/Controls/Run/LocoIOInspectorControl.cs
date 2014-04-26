using System;
using System.Linq;
using System.Windows.Forms;
using BinkyRailways.Core.State;
using BinkyRailways.Core.Util;

namespace BinkyRailways.WinApp.Controls.Run
{
    public partial class LocoIOInspectorControl : UserControl
    {
        private IRailwayState railwayState;
        private readonly EventHandler<PropertyEventArgs<ILocoIO>> locoIoFoundHandler;

        /// <summary>
        /// Default ctor
        /// </summary>
        public LocoIOInspectorControl()
        {
            InitializeComponent();
            locoIoFoundHandler = this.ASynchronize<PropertyEventArgs<ILocoIO>>(OnLocoIoFound);
        }

        /// <summary>
        /// State of railway we're connecting to.
        /// </summary>
        public IRailwayState RailwayState
        {
            get { return railwayState;  }
            set
            {
                if (railwayState != value)
                {
                    if (railwayState != null)
                    {
                        // Disconnect
                        var lnStations = railwayState.CommandStationStates.OfType<ILocoNetCommandStationState>();
                        foreach (var cs in lnStations)
                        {
                            cs.LocoIOFound -= locoIoFoundHandler;
                        }
                    }
                    railwayState = value;
                    if (railwayState != null)
                    {
                        // Connect
                        var lnStations = railwayState.CommandStationStates.OfType<ILocoNetCommandStationState>();
                        foreach (var cs in lnStations)
                        {
                            cs.LocoIOFound += locoIoFoundHandler;
                        }
                    }
                    lvDevices.Items.Clear();
                    lvPorts.Items.Clear();
                    UpdateUI();
                }
            }
        }

        /// <summary>
        /// Update the state of the controls
        /// </summary>
        private void UpdateUI()
        {
            var selectedDevice = SelectedDevice;
            cmdRead.Enabled = (selectedDevice != null);
            lbConfig.Text = (selectedDevice != null)
                           ? string.Format(Strings.LocoIOConfigurationOfX, selectedDevice.Address)
                           : Strings.LocoIOConfiguration;
        }

        /// <summary>
        /// Reload the list of devices.
        /// </summary>
        internal void RefreshDevices()
        {
            lvDevices.Items.Clear();
            if (railwayState != null)
            {
                var lnStations = railwayState.CommandStationStates.OfType<ILocoNetCommandStationState>();
                foreach (var cs in lnStations)
                {
                    cs.QueryLocoIOs();
                }
            }
            UpdateUI();
        }

        /// <summary>
        /// Gets the selected device.
        /// </summary>
        private ILocoIO SelectedDevice
        {
            get
            {
                if (lvDevices.SelectedItems.Count == 0)
                    return null;
                return (ILocoIO) lvDevices.SelectedItems[0].Tag;
            }
        }

        /// <summary>
        /// LocoIO device found.
        /// </summary>
        private void OnLocoIoFound(object sender, PropertyEventArgs<ILocoIO> e)
        {
            lvDevices.BeginUpdate();
            var item = new ListViewItem(e.Value.Address.ToString());
            item.SubItems.Add(e.Value.Version);
            item.Tag = e.Value;

            // Remove matching items
            var matching = lvDevices.Items.Cast<ListViewItem>().Where(x => x.Text == item.Text).ToList();
            foreach (var x in matching)
            {
                lvDevices.Items.Remove(x);
            }

            // Add item
            lvDevices.Items.Add(item);

            // Sort
            lvDevices.Sort();
            lvDevices.EndUpdate();
            UpdateUI();
        }

        /// <summary>
        /// New device selected.
        /// </summary>
        private void lvDevices_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateUI();
        }

        /// <summary>
        /// Refresh the device list
        /// </summary>
        private void cmdRefresh_Click(object sender, EventArgs e)
        {
            RefreshDevices();
        }

        /// <summary>
        /// Read the ports of the selected device.
        /// </summary>
        private void cmdRead_Click(object sender, EventArgs e)
        {
            var device = SelectedDevice;
            if (device != null)
            {
                cmdRead.Enabled = false;
                try
                {
                    lvPorts.Items.Clear();
                    foreach (var port in device.ReadPorts())
                    {
                        var item = new ListViewItem(port.PortNr.ToString());
                        item.SubItems.Add(port.Address.ToString());
                        item.SubItems.Add(port.Configuration);
                        lvPorts.Items.Add(item);
                    }
                }
                catch (Exception ex)
                {
                    var msg = string.Format(Strings.CannotReadLocoIOConfigBecauseX, ex.Message);
                    MessageBox.Show(msg, Strings.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    UpdateUI();
                }
            }
        }
    }
}

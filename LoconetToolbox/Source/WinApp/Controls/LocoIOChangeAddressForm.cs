using System.Windows.Forms;
using LocoNetToolBox.Devices.LocoIO;
using LocoNetToolBox.Protocol;
using LocoNetToolBox.WinApp.Communications;

namespace LocoNetToolBox.WinApp.Controls
{
    /// <summary>
    /// Form used to change the address of a LocoIO
    /// </summary>
    public partial class LocoIOChangeAddressForm : Form
    {
        private int busy;
        private AsyncLocoBuffer lb;
        private Programmer programmer;

        /// <summary>
        /// Default ctor
        /// </summary>
        public LocoIOChangeAddressForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initialize for a specific module
        /// </summary>
        internal void Initialize(AsyncLocoBuffer lb, LocoNetAddress currentAddress)
        {
            this.lb = lb;
            programmer = new Programmer(new LocoNetAddress(0, 0));
            Text = string.Format("Change address of {0}/{1}", currentAddress.Address, currentAddress.SubAddress);
            upAddress.Value = currentAddress.Address;
            upSubAddress.Value = currentAddress.SubAddress;
        }

        /// <summary>
        /// Is a read/write action busy?
        /// </summary>
        internal bool Busy
        {
            get { return (busy > 0); }
            set
            {
                if (value) { busy++; }
                else { busy--; }
            }
        }

        /// <summary>
        /// Prevent close when busy.
        /// </summary>
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (Busy)
                e.Cancel = true;
            base.OnFormClosing(e);            
        }

        /// <summary>
        /// Send change address now.
        /// </summary>
        private void cmdChange_Click(object sender, System.EventArgs e)
        {
            Busy = true;
            var newAddress = (byte)upAddress.Value;
            var newSubAddress = (byte) upSubAddress.Value;
            lbStatus.Text = "Changing address...";
            upAddress.Enabled = false;
            upSubAddress.Enabled = false;
            cmdChange.Enabled = false;
            lb.BeginRequest(
                x => programmer.ChangeAddress(x, newAddress, newSubAddress),
                x =>
                {
                    upAddress.Enabled = false;
                    upSubAddress.Enabled = false;
                    cmdChange.Enabled = true;
                    Busy = false;
                    if (x.HasError)
                    {
                        lbStatus.Text = "Changing address failed.";
                        MessageBox.Show(x.Error.Message);
                    } else
                    {
                        lbStatus.Text = "Changing address succeeded. Reset the LocoIO.";                        
                    }
                });            

        }

        private void cmdClose_Click(object sender, System.EventArgs e)
        {
            Close();
        }
    }
}

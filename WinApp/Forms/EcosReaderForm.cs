using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using BinkyRailways.Core.Model;
using BinkyRailways.Core.State.Impl.Ecos;
using BinkyRailways.Protocols;
using BinkyRailways.Protocols.Ecos;
using BinkyRailways.Protocols.Ecos.Objects;
using RawLoc = BinkyRailways.Protocols.Ecos.Objects.Loc;
using RawSwitch = BinkyRailways.Protocols.Ecos.Objects.Switch;

namespace BinkyRailways.WinApp.Forms
{
    /// <summary>
    /// Read the Ecos.
    /// </summary>
    public partial class EcosReaderForm : Form
    {
        private readonly IPackage package;
        private readonly IEcosCommandStation entity;

        /// <summary>
        /// Designer ctor
        /// </summary>
        public EcosReaderForm()
            : this(null, null)
        {
        }

        /// <summary>
        /// Default ctor
        /// </summary>
        public EcosReaderForm(IPackage package, IEcosCommandStation entity)
        {
            this.package = package;
            this.entity = entity;
            InitializeComponent();
        }

        /// <summary>
        /// Start reading ...
        /// </summary>
        protected override void OnLoad(System.EventArgs e)
        {
            base.OnLoad(e);
            if (package != null)
            {
                lbStatus.Text = "Reading objects from " + entity.HostName;
                cmdClose.Enabled = false;
                var ui = TaskScheduler.FromCurrentSynchronizationContext();
                Task.Factory.StartNew(ReadObjects).ContinueWith(ReadObjectsCompleted, ui);
            }
        }

        /// <summary>
        /// Read all Ecos objects.
        /// </summary>
        private void ReadObjects()
        {
            if (string.IsNullOrEmpty(entity.HostName))
                throw new ArgumentException("Set hostname first");
            var client = new Client(entity.HostName);

            // Read locs
            var locMgr = new ReadLocManager(client, package, Log);
            var locTask = locMgr.QueryObjects();

            // Read switches
            var swMgr = new ReadSwitchManager(client, package, Log);
            var swTask = swMgr.QueryObjects();

            // Read feedbacks
            var fbMgr = new ReadFeedbackManager(client, package, Log);
            var fbTask = fbMgr.QueryObjects();

            // Wait for all to complete
            Task.WaitAll(new[] {locTask, swTask, fbTask}, TimeSpan.FromSeconds(15));
        }

        /// <summary>
        /// Done reading (back on UI thread).
        /// </summary>
        private void ReadObjectsCompleted(Task task)
        {
            if (task.IsCompleted)
            {
                lbStatus.Text = "Done";
            }
            else if (task.IsFaulted)
            {
                if (task.Exception != null)
                {
                    lbStatus.Text = task.Exception.Message;
                }
                else
                {
                    lbStatus.Text = "Unknown error";
                }
            }
            cmdClose.Enabled = true;
        }

        /// <summary>
        /// Add a log message
        /// </summary>
        private void Log(string message)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<string>(Log), message);
            }
            else
            {
                tbLog.Text += message + Environment.NewLine;
            }
        }

        /// <summary>
        /// Close the dialog
        /// </summary>
        private void OnCloseClick(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Specific loc manager used to create loc's
        /// </summary>
        private class ReadLocManager : LocManager
        {
            private readonly IPackage package;
            private readonly Action<string> log;

            /// <summary>
            /// Default ctor
            /// </summary>
            public ReadLocManager(Client client, IPackage package, Action<string> log)
                : base(client)
            {
                this.package = package;
                this.log = log;
            }

            /// <summary>
            /// Process the result
            /// </summary>
            protected override void OnQueryObjects(Reply reply)
            {
                if (!reply.IsSucceeded)
                    throw new ProtocolException(reply.ErrorMessage);
                var railway = package.Railway;
                var existingLocs = railway.GetLocs().ToList();

                foreach (var row in reply.Rows)
                {
                    var loc = new RawLoc(Client, row.Id);
                    var name = loc.GetName();
                    var address = loc.GetAddress();
                    var protocol = loc.GetProtocol();

                    var existingLoc = existingLocs.FirstOrDefault(x => x.Description == name);
                    if (existingLoc != null)
                        continue;

                    int addressNr;
                    if (!int.TryParse(address, out addressNr))
                        continue;

                    existingLoc = existingLocs.FirstOrDefault(x => x.Address.ValueAsInt == addressNr);
                    if (existingLoc != null)
                        continue;

                    AddressType addressType;
                    if (!EcosUtility.TryGetAddressType(protocol, out addressType))
                    {
                        log("Unsupported protocol: " + protocol);
                        continue;                        
                    }

                    // Add the new loc
                    var newLoc = package.AddNewLoc();
                    newLoc.Description = name;
                    newLoc.Address = new Address(addressType, null, addressNr);
                    package.Railway.Locs.Add(newLoc);

                    log("Imported new loc: " + name);
                }
            }
        }

        /// <summary>
        /// Specific feedback manager used to create feedbacks
        /// </summary>
        private class ReadFeedbackManager : FeedbackManager
        {
            private readonly IPackage package;
            private readonly Action<string> log;

            /// <summary>
            /// Default ctor
            /// </summary>
            public ReadFeedbackManager(Client client, IPackage package, Action<string> log)
                : base(client)
            {
                this.package = package;
                this.log = log;
            }

            /// <summary>
            /// Process the result
            /// </summary>
            protected override void OnQueryObjects(Reply reply)
            {
                if (!reply.IsSucceeded)
                    throw new ProtocolException(reply.ErrorMessage);
                var railway = package.Railway;
                //var existingLocs = railway.GetLocs().ToList();

                foreach (var row in reply.Rows)
                {

                    log("Feedback row: " + row.Id);
                }
            }
        }


        /// <summary>
        /// Specific switch manager used to create junctions
        /// </summary>
        private class ReadSwitchManager : SwitchManager
        {
            private readonly IPackage package;
            private readonly Action<string> log;

            /// <summary>
            /// Default ctor
            /// </summary>
            public ReadSwitchManager(Client client, IPackage package, Action<string> log)
                : base(client)
            {
                this.package = package;
                this.log = log;
            }

            /// <summary>
            /// Process the result
            /// </summary>
            protected override void OnQueryObjects(Reply reply)
            {
                if (!reply.IsSucceeded)
                    throw new ProtocolException(reply.ErrorMessage);
                var railway = package.Railway;
                //var existingLocs = railway.GetLocs().ToList();

                foreach (var row in reply.Rows)
                {
                    var raw = new RawSwitch(Client, row.Id);

                    log("Switch: " + raw.GetFullName() + ", addr: " + raw.GetProtocol() +" " + raw.GetAddress() + ", mode: " + raw.GetMode() + ", symbol: " + raw.GetSymbol());
                }
            }
        }
    }
}

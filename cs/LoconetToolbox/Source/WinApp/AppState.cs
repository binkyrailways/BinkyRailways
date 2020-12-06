using System;
using System.Windows.Forms;
using LocoNetToolBox.Configuration;
using LocoNetToolBox.Model;
using LocoNetToolBox.Protocol;
using LocoNetToolBox.WinApp.Communications;
using LocoNetToolBox.WinApp.Preferences;

namespace LocoNetToolBox.WinApp
{
    /// <summary>
    /// State of the application.
    /// </summary>
    internal sealed class AppState : IDisposable
    {
        public event EventHandler LocoNetChanged;
        public event EventHandler PathChanged;

        // LocoBuffer events synchronized on UI thread.
        internal event MessageHandler SendMessage;
        internal event MessageHandler PreviewMessage;

        // LocoNet (state) events synchronized on UI thread.
        public event EventHandler Idle;
        public event EventHandler<StateMessage> StateChanged;
        public event EventHandler LocoIOQuery;
        public event EventHandler<LocoIOEventArgs> LocoIOFound;

        private readonly Form ui;
        private LocoNet locoNet;
        private AsyncLocoBuffer asyncLb;

        /// <summary>
        /// Default ctor
        /// </summary>
        internal AppState(Form ui)
        {
            this.ui = ui;
        }

        /// <summary>
        /// Gets the current network
        /// </summary>
        internal LocoNet LocoNet { get { return locoNet; } }

        /// <summary>
        /// Gets the current configuration.
        /// </summary>
        internal LocoNetConfiguration Configuration { get { return (locoNet != null) ? locoNet.Configuration : null; } }

        /// <summary>
        /// Gets the synchronous locobuffer
        /// </summary>
        internal LocoBuffer ConfiguredLocoBuffer { get { return (locoNet != null) ? locoNet.LocoBuffer : null; } }

        /// <summary>
        /// Gets the locobuffer communicator
        /// </summary>
        internal AsyncLocoBuffer LocoBuffer { get { return asyncLb; } }

        /// <summary>
        /// Pass the given locobuffer on to all components.
        /// </summary>
        internal void Setup(LocoBuffer lb, LocoNetConfiguration configuration)
        {
            // Allow for null arguments
            lb = lb ?? ConfiguredLocoBuffer;
            configuration = configuration ?? Configuration;

            if ((ConfiguredLocoBuffer != lb) || (Configuration != configuration))
            {
                CloseLocoBuffer();
                locoNet = new LocoNet(lb, configuration);
                asyncLb = new AsyncLocoBuffer(ui, lb);

                lb.SendMessage += LbForwardSendMessage;
                lb.PreviewMessage += LbForwardPreviewMessage;

                var lnState = locoNet.State;
                lnState.StateChanged += LnStateStateChanged;
                lnState.LocoIOQuery += LnStateLocoIoQuery;
                lnState.LocoIOFound += LnStateLocoIoFound;
                lnState.Idle += LnStateIdle;

                LocoNetChanged.Fire(this);
            }
        }

        /// <summary>
        /// Forward the PreviewMessage event on the UI thread.
        /// </summary>
        private bool LbForwardPreviewMessage(byte[] message, Protocol.Message decoded)
        {
            if (PreviewMessage != null)
            {
                if (ui.InvokeRequired)
                {
                    return (bool)ui.Invoke(PreviewMessage, message, decoded);
                }
                return PreviewMessage(message, decoded);
            }
            return false;
        }

        /// <summary>
        /// Forward the SendMessage event on the UI thread.
        /// </summary>
        private bool LbForwardSendMessage(byte[] message, Protocol.Message decoded)
        {
            if (SendMessage != null)
            {
                if (ui.InvokeRequired)
                {
                    return (bool)ui.Invoke(SendMessage, message, decoded);
                }
                return SendMessage(message, decoded);
            }
            return false;
        }

        void LnStateIdle(object sender, EventArgs e)
        {
            if (Idle != null)
            {
                if (ui.InvokeRequired)
                {
                    ui.BeginInvoke(new EventHandler(LnStateIdle), sender, e);
                }
                else
                {
                    Idle.Fire(this);
                }
            }
        }

        private void LnStateLocoIoFound(object sender, LocoIOEventArgs e)
        {
            if (LocoIOFound != null)
            {
                if (ui.InvokeRequired)
                {
                    ui.BeginInvoke(new EventHandler<LocoIOEventArgs>(LnStateLocoIoFound), sender, e);
                }
                else
                {
                    LocoIOFound.Fire(this, e);
                }
            }
        }

        private void LnStateLocoIoQuery(object sender, EventArgs e)
        {
            if (LocoIOQuery != null)
            {
                if (ui.InvokeRequired)
                {
                    ui.BeginInvoke(new EventHandler(LnStateLocoIoQuery), sender, e);
                }
                else
                {
                    LocoIOQuery.Fire(this);
                }
            }
        }

        private void LnStateStateChanged(object sender, StateMessage e)
        {
            if (StateChanged != null)
            {
                if (ui.InvokeRequired)
                {
                    ui.BeginInvoke(new EventHandler<StateMessage>(LnStateStateChanged), sender, e);
                }
                else
                {
                    StateChanged.Fire(this, e);
                }
            }
        }

        /// <summary>
        /// Close any active locobuffer connection
        /// </summary>
        private void CloseLocoBuffer()
        {
            if (locoNet == null)
                return;

            var lb = ConfiguredLocoBuffer;
            if (lb != null)
            {
                lb.SendMessage -= LbForwardSendMessage;
                lb.PreviewMessage -= LbForwardPreviewMessage;
            }

            if (asyncLb != null)
            {
                asyncLb.Dispose();
                asyncLb = null;
            }

            var lnState = locoNet.State;
            if (lnState != null)
            {
                lnState.StateChanged -= LnStateStateChanged;
                lnState.LocoIOQuery -= LnStateLocoIoQuery;
                lnState.LocoIOFound -= LnStateLocoIoFound;
                lnState.Idle -= LnStateIdle;
            }

            locoNet.Dispose();
            locoNet = null;
        }

        /// <summary>
        /// Open a configuration from the given path.
        /// </summary>
        internal void OpenConfiguration(string path)
        {
            try
            {
                // Open
                var tmp = LocoNetConfiguration.Load(path);

                // Can we close?
                if (SaveConfigurationIfDirty())
                {
                    Setup(null, tmp);
                    UserPreferences.Preferences.LocoNetConfigurationPath = path;
                    UserPreferences.SaveNow();
                    PathChanged.Fire(this);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Save the current configuration if it is dirty.
        /// </summary>
        /// <returns>True if it is save to overwrite the current configuration.</returns>
        internal bool SaveConfigurationIfDirty()
        {
            var configuration = Configuration;
            if ((configuration == null) || (!configuration.Dirty))
                return true;

            // Save configuration
            switch (MessageBox.Show("Save the loconet configuration now?", Application.ProductName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1))
            {
                case DialogResult.Yes:
                    return SaveConfiguration();
                case DialogResult.No:
                    return true;
                default:
                    return false;
            }
        }

        /// <summary>
        /// Save the current configuration.
        /// </summary>
        internal bool SaveConfiguration()
        {
            var configuration = Configuration;
            if (configuration == null)
                return true;

            if (string.IsNullOrEmpty(configuration.Path))
            {
                return SaveConfigurationAs();
            }
            try
            {
                configuration.Save(configuration.Path);
                UserPreferences.Preferences.LocoNetConfigurationPath = configuration.Path;
                UserPreferences.SaveNow();
                PathChanged.Fire(this);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Save the current configuration.
        /// </summary>
        internal bool SaveConfigurationAs()
        {
            var configuration = Configuration;
            if (configuration == null)
                return true;

            using (var dialog = new SaveFileDialog())
            {
                dialog.Title = "Save loconet configuration as";
                dialog.DefaultExt = Constants.LocoNetConfigurationExt;
                dialog.Filter = Constants.LocoNetConfigurationFilter;
                if (dialog.ShowDialog() != DialogResult.OK)
                    return false;
                configuration.Path = dialog.FileName;
            }
            return SaveConfiguration();
        }

        /// <summary>
        /// Cleanup
        /// </summary>
        public void Dispose()
        {
            CloseLocoBuffer();
        }
    }
}

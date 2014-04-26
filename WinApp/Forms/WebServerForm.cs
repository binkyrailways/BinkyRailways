using System;
using System.Diagnostics;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using BinkyRailways.Core.Server;
using Gma.QrCodeNet.Encoding;
using Gma.QrCodeNet.Encoding.Windows.Render;

namespace BinkyRailways.WinApp.Forms
{
    public partial class WebServerForm : AppForm
    {
        private readonly WebServer webServer;
        private readonly string url;

        /// <summary>
        /// Designer ctor
        /// </summary>
        public WebServerForm()
            : this(null)
        {
        }

        /// <summary>
        /// Default ctor
        /// </summary>
        public WebServerForm(WebServer webServer)
        {
            this.webServer = webServer;
            url = webServer.HttpUrl;
            InitializeComponent();
        }

        /// <summary>
        /// Load time initialization
        /// </summary>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (DesignMode) return;

            webServer.HttpIndexRequest += OnHttpRequest;

            if (url == null) return;
            var ui = TaskScheduler.FromCurrentSynchronizationContext();
            Task.Factory.StartNew(() => CreateQrCode()).ContinueWith(t => {
                if (t.IsCompleted)
                {
                    pictureBox.Image = t.Result;
                }
            }, ui);
        }

        /// <summary>
        /// The form is closing.
        /// </summary>
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (webServer != null)
                webServer.HttpIndexRequest -= OnHttpRequest;
            base.OnFormClosing(e);
        }

        /// <summary>
        /// A HTTP request has been received.
        /// </summary>
        private void OnHttpRequest(object sender, EventArgs e)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new EventHandler(OnHttpRequest), sender, e);
            }
            else
            {
                Close();
            }           
        }

        /// <summary>
        /// Create a QR code for the URL.
        /// </summary>
        private Bitmap CreateQrCode()
        {
            var encoder = new QrEncoder();
            var qrcode = encoder.Encode(url);
            var renderer = new GraphicsRenderer(new FixedCodeSize(256, QuietZoneModules.Two));

            var bitmap = new Bitmap(256, 256);
            using (var graphics = Graphics.FromImage(bitmap))
            {
                renderer.Draw(graphics, qrcode.Matrix);
            }

            return bitmap;
        }

        /// <summary>
        /// Open in a browser.
        /// </summary>
        private void OnOpenBrowserClick(object sender, System.EventArgs e)
        {
            if (url == null)
                return;            
            try
            {
                Process.Start(url);
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Cannot open webbrowser because: {0}", ex.Message));
            }
        }
    }
}

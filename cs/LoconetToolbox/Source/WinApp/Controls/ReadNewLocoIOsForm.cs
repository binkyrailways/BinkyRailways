using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LocoNetToolBox.Devices.LocoIO;

namespace LocoNetToolBox.WinApp.Controls
{
    public partial class ReadNewLocoIOsForm : Form
    {
        private readonly AppState appState;
        private bool started;

        /// <summary>
        /// Designer ctor
        /// </summary>
        public ReadNewLocoIOsForm()
            : this(null)
        {
        }

        /// <summary>
        /// Default ctor
        /// </summary>
        internal ReadNewLocoIOsForm(AppState appState)
        {
            this.appState = appState;
            InitializeComponent();
        }

        /// <summary>
        /// Start reading loco IO's
        /// </summary>
        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);
            if (Visible && !started && (appState != null))
            {
                started = true;
                var list = appState.LocoNet.NewLocoIOs.ToList();
                progressBar1.Maximum = list.Count + 1;
                progressBar1.Minimum = 0;
                foreach (var io in list)
                {
                    var index = list.IndexOf(io);
                    var programmer = new Programmer(io.Address);
                    var config = new LocoIOConfig();
                    appState.LocoBuffer.BeginRequest(
                        x => programmer.Read(x, config),
                        x =>
                            {
                                if (!x.HasError)
                                {
                                    appState.Configuration.LocoIOs[config.Address] = config;
                                }
                                progressBar1.Value = index + 1;
                                if (index == list.Count-1)
                                {
                                    Close();
                                }
                            });
                }
            }
        }
    }
}

using System.Windows.Forms;
using BinkyRailways.Core.Logging;
using BinkyRailways.Core.State;
using System.Collections.Generic;

namespace BinkyRailways.WinApp.Controls.Run
{
    public partial class LogsControl : UserControl
    {
        private readonly List<Control> logControls;

        /// <summary>
        /// Default ctor
        /// </summary>
        public LogsControl()
        {
            InitializeComponent();
            if (!DesignMode)
            {
                logControls = new List<Control>();
            }
        }

        /// <summary>
        /// Initialize all logs.
        /// </summary>
        public void Initialize(IRailwayState state)
        {
            logsPane.Controls.Clear();
            logControls.Clear();
            try
            {
                logControls.Add(CreateLog("Common", "*", NLog.LogLevel.Warn));
                logControls.Add(CreateLog("Automatic", LogNames.AutoLocController, NLog.LogLevel.Trace));
                logControls.Add(CreateLog("Sensors", LogNames.Sensors, NLog.LogLevel.Trace));
                logControls.Add(CreateLog("LocoNet", LogNames.LocoNet, NLog.LogLevel.Trace));

                if (state != null)
                {
                    foreach (var cs in state.CommandStationStates)
                    {
                        logControls.Add(CreateLog(cs.Model.Description, LogNames.CommandStationPrefix + cs.Model.Description, NLog.LogLevel.Trace));
                    }
                }

                ShowLog(logControls[0], (ToolStripButton)toolStrip1.Items[0]);
            }
            catch
            {

            }
        }

        private Control CreateLog(string text, string logNamePattern, NLog.LogLevel minLogLevel)
        {
            var control = new LogViewControl();
            control.Dock = DockStyle.Fill;
            control.Visible = false;
            logsPane.Controls.Add(control);
            control.Configure(logNamePattern, minLogLevel);

            var button = new ToolStripButton(text);
            toolStrip1.Items.Add(button);
            button.Click += (s, x) => ShowLog(control, button);
            return control;
        }

        private void ShowLog(Control control, ToolStripButton button)
        {
            SuspendLayout();
            foreach (var x in logControls)
            {
                x.Visible = (x == control);
                x.Dock = DockStyle.Fill;
            }
            foreach (ToolStripButton x in toolStrip1.Items)
            {
                x.Checked = (x == button);
            }
            ResumeLayout(true);
        }
    }
}

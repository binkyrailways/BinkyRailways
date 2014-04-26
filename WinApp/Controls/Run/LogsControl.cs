using System.Windows.Forms;
using BinkyRailways.Core.Logging;

namespace BinkyRailways.WinApp.Controls.Run
{
    public partial class LogsControl : UserControl
    {
        private readonly Control[] logControls;

        /// <summary>
        /// Default ctor
        /// </summary>
        public LogsControl()
        {
            InitializeComponent();
            if (!DesignMode)
            {
                try
                {
                    logControls = new[]
                                      {
                                          CreateLog("Common", "*", NLog.LogLevel.Warn),
                                          CreateLog("Automatic", LogNames.AutoLocController, NLog.LogLevel.Trace),
                                          CreateLog("Sensors", LogNames.Sensors, NLog.LogLevel.Trace),
                                          CreateLog("LocoNet", LogNames.LocoNet, NLog.LogLevel.Trace),
                                      };
                    ShowLog(logControls[0], (ToolStripButton) toolStrip1.Items[0]);
                } catch
                {
                    
                }
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

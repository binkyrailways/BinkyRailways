using System;
using System.Windows.Forms;
using NLog;
using NLog.Common;
using NLog.Config;
using NLog.Targets;

namespace BinkyRailways.WinApp.Controls.Run
{
    public partial class LogViewControl : UserControl
    {
        private readonly LogViewTarget target;

        /// <summary>
        /// Default ctor
        /// </summary>
        public LogViewControl()
        {
            InitializeComponent();
            target = new LogViewTarget(this);
        }

        /// <summary>
        /// Configure this control for a specific log output.
        /// </summary>
        public void Configure(string loggerNamePattern, LogLevel minLogLevel)
        {
            if (!DesignMode)
            {
                var rule = new LoggingRule(loggerNamePattern, minLogLevel, target);
                LogManager.Configuration.LoggingRules.Add(rule);
                LogManager.ReconfigExistingLoggers();
            }
        }

        /// <summary>
        /// Add a log entry
        /// </summary>
        private void WriteLogEvent(AsyncLogEventInfo logEvent)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action<AsyncLogEventInfo>(WriteLogEvent), logEvent);
            }
            else
            {
                var item = new LogViewItem(logEvent.LogEvent);
                lvLog.Items.Add(item);
                item.EnsureVisible();
            }
        }

        /// <summary>
        /// Target implementation
        /// </summary>
        private class LogViewTarget : Target
        {
            private readonly LogViewControl control;

            /// <summary>
            /// Default ctor
            /// </summary>
            public LogViewTarget(LogViewControl control)
            {
                this.control = control;
            }

            /// <summary>
            /// Write the given log entry
            /// </summary>
            /// <param name="logEvent"></param>
            protected override void Write(AsyncLogEventInfo logEvent)
            {
                control.WriteLogEvent(logEvent);
            }
        }

        /// <summary>
        /// Remove all entries
        /// </summary>
        private void ctxClear_Click(object sender, EventArgs e)
        {
            lvLog.Items.Clear();
        }
    }
}

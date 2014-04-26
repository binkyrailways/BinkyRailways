using System.Windows.Forms;
using NLog;

namespace BinkyRailways.WinApp.Controls.Run
{
    public class LogViewItem : ListViewItem
    {
        /// <summary>
        /// Default ctor
        /// </summary>
        public LogViewItem(LogEventInfo eventInfo)
        {
            Text = eventInfo.FormattedMessage;
            SubItems.Add(eventInfo.LoggerName);
            SubItems.Add((eventInfo.Exception != null) ? eventInfo.Exception.Message : string.Empty);
        }
    }
}

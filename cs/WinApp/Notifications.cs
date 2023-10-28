using System;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace BinkyRailways.WinApp
{
    internal static class Notifications
    {
        /// <summary>
        /// Show a notification for the given error.
        /// </summary>
        internal static void ShowError(Exception ex, string messageFormat, params object[] args)
        {
            var initialMessage = string.Format(messageFormat, args);
            var msg = initialMessage;
            var inner = ex.InnerException;
            while (inner != null)
            {
                msg += Environment.NewLine;
                msg += "----------------------------------------";
                msg += Environment.NewLine;
                msg += inner.Message;
                inner = inner.InnerException;
            }
            msg += Environment.NewLine;
            msg += Environment.NewLine;
            msg += Strings.LikeToCopyToClipboard;
            var rc = MessageBox.Show(msg, Strings.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2);
            if (rc == DialogResult.Yes)
            {
                try
                {
                    var sb = new StringBuilder();
                    var name = typeof(Notifications).Assembly.GetName();
                    sb.AppendFormat("Product   : {0} {1}", name.Name, name.Version);
                    sb.AppendLine();
                    sb.AppendFormat("Message   : {0}", initialMessage);
                    sb.AppendLine();
                    sb.AppendFormat("OS        : {0}", Environment.OSVersion);
                    sb.AppendLine();
                    sb.AppendFormat("Culture   : {0}", Thread.CurrentThread.CurrentCulture);
                    sb.AppendLine();
                    sb.AppendFormat("UI Culture: {0}", Thread.CurrentThread.CurrentUICulture);
                    sb.AppendLine();
                    sb.AppendLine();
                    sb.AppendFormat("Error     : {0}", ex.Message);
                    sb.AppendLine();
                    sb.AppendFormat("Type      : {0}", ex.GetType().FullName);
                    sb.AppendLine();
                    sb.AppendLine(ex.StackTrace);
                    sb.AppendLine();
                    inner = ex.InnerException;
                    while (inner != null)
                    {
                        sb.AppendLine("---- Inner ----");
                        sb.AppendFormat("Error     : {0}", inner.Message);
                        sb.AppendLine();
                        sb.AppendFormat("Type      : {0}", inner.GetType().FullName);
                        sb.AppendLine();
                        sb.AppendLine(inner.StackTrace);
                        sb.AppendLine();
                        inner = inner.InnerException;
                    }
                    Clipboard.SetText(sb.ToString(), TextDataFormat.UnicodeText);
                }
                catch (Exception ex2)
                {
                    var text = string.Format(Strings.FailedToCopyToClipboardBecauseX, ex2.Message);
                    MessageBox.Show(text, Strings.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}

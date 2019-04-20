using System;
using System.Diagnostics;
using System.Text;
using System.Threading;

namespace BinkyRailways.WinApp.Forms
{
    public partial class UnhandledExceptionForm : AppForm
    {
        /// <summary>
        /// Register an unhandled exception error handler.
        /// </summary>
        public static void Register()
        {
            AppDomain.CurrentDomain.UnhandledException += CurrentDomainUnhandledException;
        }

        /// <summary>
        /// Unhandled exception has occurred.
        /// </summary>
        private static void CurrentDomainUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            ASyncShowError(e.ExceptionObject as Exception);
        }

        /// <summary>
        /// Unhandled exception has occurred.
        /// </summary>
        private static void ASyncShowError(Exception exception)
        {
            try
            {
                var thread = new Thread(() => ShowExceptionDialog(exception));
                thread.SetApartmentState(ApartmentState.STA);
                thread.Start();
            }
            catch (Exception myException)
            {
                Debug.WriteLine("Exception in appdomain exception handler: " + myException.Message);
            }
        }

        /// <summary>
        /// Show the exception dialog.
        /// This method is called on the thread pool to avoid blocking other handlers.
        /// </summary>
        internal static void ShowExceptionDialog(object exception)
        {
            try
            {
                using (var dialog = new UnhandledExceptionForm(exception as Exception))
                {
                    dialog.TopMost = true;
                    dialog.ShowDialog();
                }
            }
            catch (Exception myException)
            {
                Debug.WriteLine("Exception in exception handler: " + myException.Message);
            }
        }

        /// <summary>
        /// Designer ctor
        /// </summary>
        public UnhandledExceptionForm() : this(null)
        {
        }

        /// <summary>
        /// Default ctor
        /// </summary>
        private UnhandledExceptionForm(Exception ex)
        {
            InitializeComponent();
            if (ex != null)
            {
                var msg = new StringBuilder();
                msg.Append("Message   : ");
                msg.AppendLine(ex.Message);
                msg.Append("Exception : ");
                msg.AppendLine(ex.GetType().FullName);
                msg.AppendLine();

                msg.Append("Stacktrace: ");
                msg.AppendLine(ex.StackTrace);
                msg.AppendLine();

                var inner = ex.InnerException;
                while (inner != null)
                {
                    msg.AppendLine("-- Inner exception --");
                    msg.Append("Message   : ");
                    msg.AppendLine(inner.Message);
                    msg.Append("Exception : ");
                    msg.AppendLine(inner.GetType().FullName);
                    msg.AppendLine();

                    msg.Append("Stacktrace: ");
                    msg.AppendLine(inner.StackTrace);
                    msg.AppendLine();

                    inner = inner.InnerException;
                }

                tbError.Text = msg.ToString();
            }
        }

        /// <summary>
        /// Close the dialog
        /// </summary>
        private void cmdClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}

using System;
using LocoNetToolBox.Protocol;

namespace LocoNetToolBox.WinApp.Communications
{
    /// <summary>
    /// Event arguments for request completed notification
    /// </summary>
    public class AsyncRequestCompletedEventArgs : EventArgs
    {
        private readonly Exception error;

        /// <summary>
        /// Default ctor
        /// </summary>
        public AsyncRequestCompletedEventArgs(Exception error)
        {
            this.error = error;
        }

        /// <summary>
        /// Has the request resulted in an error?
        /// </summary>
        public bool HasError
        {
            get { return (error != null); }
        }

        /// <summary>
        /// Error thrown when executing the request
        /// </summary>
        public Exception Error
        {
            get { return error; }
        }
    }
}

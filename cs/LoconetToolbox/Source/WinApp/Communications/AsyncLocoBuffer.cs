using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;
using LocoNetToolBox.Protocol;

namespace LocoNetToolBox.WinApp.Communications
{
    /// <summary>
    /// Asynchronous locobuffer utility.
    /// It enables asynchronous message sending/receiving.
    /// Feedback it synchronized on the UI thread.
    /// </summary>
    public class AsyncLocoBuffer : IDisposable
    {
        private readonly Control ui;
        private readonly LocoBuffer lb;
        private readonly List<RequestItem> requests = new List<RequestItem>();
        private readonly object requestsLock = new object();
        private bool disposed;

        /// <summary>
        /// Default ctor
        /// </summary>
        internal AsyncLocoBuffer(Control ui, LocoBuffer lb)
        {
            this.ui = ui;
            this.lb = lb;
            var thread = new Thread(Run);
            thread.Start();
        }

        /// <summary>
        /// Get the locobuffer to which requests are send.
        /// </summary>
        internal LocoBuffer LocoBuffer
        {
            get { return lb; }
        }

        /// <summary>
        /// Start executing the given request.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="completed">Handler called when the request has completed (ok or error). This handler is called on the UI thread.</param>
        internal void BeginRequest(Request request, Action<AsyncRequestCompletedEventArgs> completed)
        {
            BeginRequest(request.Execute, completed);
        }

        /// <summary>
        /// Start executing the given request.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="completed">Handler called when the request has completed (ok or error). This handler is called on the UI thread.</param>
        internal void BeginRequest(Action<LocoBuffer> request, Action<AsyncRequestCompletedEventArgs> completed)
        {
            lock (requestsLock)
            {
                requests.Add(new RequestItem(request, completed));
                Monitor.PulseAll(requestsLock);
            }
        }

        /// <summary>
        /// Stop processing requests
        /// </summary>
        public void Dispose()
        {
            if (disposed)
                return;
            disposed = true;
            lock (requestsLock)
            {
                requests.Clear();
                Monitor.PulseAll(requestsLock);
            }
        }

        /// <summary>
        /// Run the requests
        /// </summary>
        private void Run()
        {
            while (!disposed)
            {
                RequestItem request = null;
                lock (requestsLock)
                {
                    if (requests.Count > 0)
                    {
                        request = requests[0];
                        requests.RemoveAt(0);
                    }
                    else
                    {
                        Monitor.Wait(requestsLock);
                    }
                }
                if (request != null)
                {
                    request.Execute(ui, lb);
                }
            }
        }

        private sealed class RequestItem
        {
            private readonly Action<LocoBuffer> request;
            private readonly Action<AsyncRequestCompletedEventArgs> completed;

            public RequestItem(Action<LocoBuffer> request, Action<AsyncRequestCompletedEventArgs> completed)
            {
                this.request = request;
                this.completed = completed;
            }

            /// <summary>
            /// Execute this request.
            /// </summary>
            internal void Execute(Control ui, LocoBuffer lb)
            {
                try
                {
                    request(lb);
                    OnCompleted(ui, null);
                }
                catch (Exception ex)
                {
                    OnCompleted(ui, ex);
                }
            }

            /// <summary>
            /// Fire the completed handler.
            /// </summary>
            private void OnCompleted(Control ui, Exception error)
            {
                if (completed == null)
                    return;
                try
                {
                    var args = new AsyncRequestCompletedEventArgs(error);
                    if (ui.InvokeRequired)
                    {
                        ui.Invoke(completed, args);
                    }
                    else
                    {
                        completed(args);
                    }
                }
                catch (Exception ex)
                {
                    // Ignore
                }
            }
        }
    }
}

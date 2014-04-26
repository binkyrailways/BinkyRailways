using System;
using System.Windows.Forms;

namespace BinkyRailways.WinApp
{
    /// <summary>
    /// Helper method for synchronization of the UI thread.
    /// </summary>
    internal static class UIThread
    {
        /// <summary>
        /// Create an event handler that invokes the given handler on the UI thread 
        /// via Invoke.
        /// </summary>
        internal static EventHandler Synchronize(this Control ui, Action<object, EventArgs> handler)
        {
            var dispatcher = new Dispatcher<EventArgs>(ui, handler);
            return dispatcher.Invoke;
        }

        /// <summary>
        /// Create an event handler that invokes the given handler on the UI thread 
        /// via Invoke.
        /// </summary>
        internal static EventHandler<T> Synchronize<T>(this Control ui, Action<object, T> handler)
            where T : EventArgs
        {
            var dispatcher = new Dispatcher<T>(ui, handler);
            return dispatcher.Invoke;
        }

        /// <summary>
        /// Create an event handler that invokes the given handler on the UI thread 
        /// via BeginInvoke.
        /// </summary>
        internal static EventHandler ASynchronize(this Control ui, Action<object, EventArgs> handler)
        {
            var dispatcher = new Dispatcher<EventArgs>(ui, handler);
            return dispatcher.BeginInvoke;
        }

        /// <summary>
        /// Create an event handler that invokes the given handler on the UI thread 
        /// via BeginInvoke.
        /// </summary>
        internal static EventHandler<T> ASynchronize<T>(this Control ui, Action<object, T> handler)
            where T : EventArgs
        {
            var dispatcher = new Dispatcher<T>(ui, handler);
            return dispatcher.BeginInvoke;
        }

        private class Dispatcher<T>
            where T : EventArgs
        {
            private readonly Control ui;
            private readonly Action<object, T> handler;

            /// <summary>
            /// Default ctor
            /// </summary>
            public Dispatcher(Control ui, Action<object, T> handler)
            {
                this.ui = ui;
                this.handler = handler;
            }

            /// <summary>
            /// The actual return-after-handled handler
            /// </summary>
            public void Invoke(object sender, T args)
            {
                if (ui.InvokeRequired)
                {
                    ui.Invoke(handler, sender, args);
                }
                else
                {
                    handler(sender, args);
                }
            }

            /// <summary>
            /// The actual fire-and-forget handler
            /// </summary>
            public void BeginInvoke(object sender, T args)
            {
                ui.BeginInvoke(handler, sender, args);
            }
        }
    }
}

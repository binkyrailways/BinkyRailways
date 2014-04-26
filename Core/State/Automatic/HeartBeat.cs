using System;
using System.Threading;

namespace BinkyRailways.Core.State.Automatic
{
    /// <summary>
    /// Thread that frequently posts an action on the state dispatcher thread
    /// to update the state of the automatically controlled locs.
    /// </summary>
    internal sealed class HeartBeat : IDisposable
    {
        private static readonly TimeSpan MaxDelay = TimeSpan.FromMinutes(1);
        private readonly AutomaticLocController automaticLocController;
        private readonly IStateDispatcher dispatcher;
        private readonly object waitLock = new object();
        private bool stop;

        /// <summary>
        /// Default ctor
        /// </summary>
        public HeartBeat(AutomaticLocController automaticLocController, IStateDispatcher dispatcher)
        {
            this.automaticLocController = automaticLocController;
            this.dispatcher = dispatcher;

            var thread = new Thread(Run);
            thread.Name = "heartbeat";
            thread.Start();
        }

        /// <summary>
        /// Notify the automatic loc controller of an update.
        /// </summary>
        internal void NotifyUpdate()
        {
            lock (waitLock)
            {
                Monitor.PulseAll(waitLock);
            }
        }

        /// <summary>
        /// Run loop
        /// </summary>
        private void Run()
        {
            TimeSpan delay = TimeSpan.Zero;
            while (!stop)
            {
                if (delay > TimeSpan.Zero)
                {
                    lock (waitLock)
                    {
                        if (delay > MaxDelay)
                        {
                            delay = MaxDelay;
                        }
                        Monitor.Wait(waitLock, delay);
                    }
                }
                if (!stop)
                {
                    delay = dispatcher.PostActionAndWait<TimeSpan>(automaticLocController.UpdateLocStates);
                }
            }
        }

        /// <summary>
        /// Stop the heartbeat.
        /// </summary>
        public void Dispose()
        {
            lock (waitLock)
            {
                stop = true;
                Monitor.PulseAll(waitLock);
            }
        }
    }
}

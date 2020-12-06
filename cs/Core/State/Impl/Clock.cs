using System;
using System.Threading;
using BinkyRailways.Core.Model;
using BinkyRailways.Core.Util;

namespace BinkyRailways.Core.State.Impl
{
    internal class Clock : IActualStateProperty<Time>, IDisposable
    {
        private readonly IRailwayState railwayState;
        private Time actual;
        private bool stop;

        /// <summary>
        /// Default ctor
        /// </summary>
        public Clock(IRailwayState railwayState)
        {
            this.railwayState = railwayState;
            var thread = new Thread(Run) { IsBackground = true };
            thread.Start();
        }

        /// <summary>
        /// Fired when the actual value has changed.
        /// </summary>
        public event EventHandler ActualChanged;

        /// <summary>
        /// Gets / sets the actual value
        /// </summary>
        public Time Actual
        {
            get { return actual; }
            set { /* ignore */ }
        }

        /// <summary>
        /// Close
        /// </summary>
        public void Dispose()
        {
            stop = true;
            ActualChanged = null;
        }

        /// <summary>
        /// Run a timer thread.
        /// </summary>
        private void Run()
        {
            while (!stop)
            {
                actual = new Time(DateTime.Now, railwayState.Model.ClockSpeedFactor);
                FireActualChanged();
                Thread.Sleep((int) (1000.0 * (60.0 / (double)railwayState.Model.ClockSpeedFactor)));
            }
        }

        /// <summary>
        /// Fire the ActualChanged event
        /// </summary>
        private void FireActualChanged()
        {
            try
            {
                ActualChanged.Fire(this);
            }
            catch (Exception ex)
            {
                // Ignore
            }
        }
    }
}

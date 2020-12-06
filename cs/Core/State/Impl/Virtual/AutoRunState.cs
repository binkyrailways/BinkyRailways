using System.Collections.Generic;
using System.Linq;
using System.Timers;

namespace BinkyRailways.Core.State.Impl.Virtual
{
    /// <summary>
    /// Auto run state of entire railway.
    /// </summary>
    internal sealed class AutoRunState
    {
        private readonly IRailwayState railway;
        private bool enabled;
        private List<AutoRunLocState> locStates;
        private Timer timer;

        /// <summary>
        /// Default ctor
        /// </summary>
        public AutoRunState(IRailwayState railway)
        {
            this.railway = railway;
        }

        /// <summary>
        /// Is auto run enabled?
        /// </summary>
        public bool Enabled
        {
            get { return enabled; }
            set
            {
                if (enabled != value)
                {
                    enabled = value;
                    if (value)
                    {
                        locStates = railway.LocStates.Select(x => new AutoRunLocState(x)).ToList();
                        timer = new Timer();
                        timer.Interval = 2000;
                        timer.Elapsed += TimerOnElapsed;
                        timer.Enabled = true;
                    }
                    else
                    {
                        locStates = null;
                        var t = timer;
                        timer = null;
                        if (t != null)
                        {
                            t.Dispose();
                        }
                    }
                }
            }
        }

        private void TimerOnElapsed(object sender, ElapsedEventArgs elapsedEventArgs)
        {
            var states = locStates;
            if (states != null)
            {
                states.ForEach(x => x.Tick());
            }
        }
    }
}

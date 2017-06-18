using System;
using System.Threading;

namespace LocoNetToolBox.Model
{
    /// <summary>
    /// Thread used to detect idle moments on the loconet.
    /// </summary>
    partial class LocoNetState
    {
        /// <summary>
        /// Fired when the loconet is idle for more then 1 second.
        /// </summary>
        public event EventHandler Idle;

        private Thread idleThread;

        /// <summary>
        /// Start a thread used to detect idle moments.
        /// </summary>
        private void StartIdleDetection()
        {
            idleThread = new Thread(DetectIdle);
            idleThread.Start();
        }

        /// <summary>
        /// Stop the idle detection thread.
        /// </summary>
        private void StopIdleDetection()
        {
            idleThread = null;
        }

        /// <summary>
        /// Run and wait for idle moments
        /// </summary>
        private void DetectIdle()
        {
            while (idleThread != null)
            {
                var delta = DateTime.Now.Subtract(lastPreviewMessageTime);
                if (delta.TotalSeconds >= 1)
                {
                    // We're idle
                    var idle = Idle; // Copy for better thread safety.
                    idle.Fire(this);
                }
                Thread.Sleep(1000);
            }
        }
    }
}

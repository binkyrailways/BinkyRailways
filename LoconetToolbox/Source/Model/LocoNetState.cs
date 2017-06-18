using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using LocoNetToolBox.Protocol;

namespace LocoNetToolBox.Model
{
    /// <summary>
    /// Maintains the state of all inputs and switches in the loconet.
    /// </summary>
    public partial class LocoNetState : ILocoNetState
    {
        /// <summary>
        /// Event is fired when an input or switch state has changed.
        /// </summary>
        public event EventHandler<StateMessage> StateChanged;

        /// <summary>
        /// Event is fired when a query for loco-io units is detected.
        /// </summary>
        public event EventHandler LocoIOQuery;

        /// <summary>
        /// Event is fired when a response from a loco-io unit on a query for such units is detected.
        /// </summary>
        public event EventHandler<LocoIOEventArgs> LocoIOFound;

        private readonly AddressStateMap<SwitchState> switches = new AddressStateMap<SwitchState>();
        private readonly Dictionary<LocoNetAddress, LocoIO> locoIOs = new Dictionary<LocoNetAddress, LocoIO>();
        private readonly object stateLock = new object();
        private readonly LocoBuffer lb;
        private DateTime lastPreviewMessageTime = DateTime.Now;

        /// <summary>
        /// Default ctor
        /// </summary>
        /// <param name="lb"></param>
        internal LocoNetState(LocoBuffer lb)
        {
            this.lb = lb;
            lb.PreviewMessage += ProcessMessage;
            StartIdleDetection();
        }

        /// <summary>
        /// Wait for the switch with the given address to have the given direction.
        /// </summary>
        /// <param name="address">Loconet address of the switch</param>
        /// <param name="direction">Intended direction</param>
        /// <param name="timeout">Timeout in ms</param>
        /// <returns>True if state is correct, false on timeout</returns>
        public bool WaitForSwitchDirection(int address, bool direction, int timeout)
        {
            lock (stateLock)
            {
                var sw = GetSwitch(address);
                var start = Environment.TickCount;
                while (true)
                {
                    if (sw.Direction == direction)
                        return true;
                    if (!Monitor.Wait(stateLock, timeout))
                        return false;
                    var now = Environment.TickCount;
                    timeout -= (now - start);
                    if (sw.Direction == direction)
                        return true;
                    if (timeout <= 0)
                        return false;
                    start = now;
                }
            }
        }

        /// <summary>
        /// Gets all loco-io units found on the network
        /// </summary>
        public IEnumerable<LocoIO> FoundLocoIOs
        {
            get
            {
                lock (stateLock)
                {
                    return locoIOs.Values.ToList();
                }
            }
        }

        /// <summary>
        /// Process a loconet message and update the state accordingly.
        /// </summary>
        private bool ProcessMessage(byte[] message, Message decoded)
        {
            var response = Response.Decode(message);
            lastPreviewMessageTime = DateTime.Now;

            SwitchState sw = null;
            lock (stateLock)
            {
                var inpRep = response as InputReport;
                var swRep = response as SwitchReport;
                var peerXferResponse = response as PeerXferResponse;

                if (inpRep != null)
                {
                    //var item = GetItem(inpRep.Address);
                }
                else if (swRep != null)
                {
                    sw = GetSwitch(swRep.Address);
                    if (sw.Direction != swRep.SensorLevel)
                    {
                        // State change
                        sw.Direction = swRep.SensorLevel;
                        Monitor.PulseAll(stateLock);
                    }
                    else
                    {
                        return false;
                    }
                }
                else if (peerXferResponse != null)
                {
                    if (peerXferResponse.SvAddress == 0)
                    {
                        if (peerXferResponse.IsSourcePC)
                        {
                            if (peerXferResponse.IsDestinationBroadcast)
                            {
                                // Query request
                                locoIOs.Clear();
                                LocoIOQuery.Fire(this);
                            }
                        }
                        else
                        {
                            var entry = new LocoIO(peerXferResponse.Source, peerXferResponse.LocoIOVersion);
                            if (!locoIOs.ContainsKey(entry.Address))
                            {
                                locoIOs[entry.Address] = entry;
                                LocoIOFound.Fire(this, new LocoIOEventArgs(entry));
                            }
                        }
                    }
                }
            }

            // Notify listeners
            if (sw != null)
                StateChanged.Fire(this, new StateMessage(sw));
            return false;
        }

        /// <summary>
        /// Get or create a switch state.
        /// </summary>
        private SwitchState GetSwitch(int address)
        {
            return switches.GetOrCreateItem(address, x => new SwitchState(x));
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <filterpriority>2</filterpriority>
        public void Dispose()
        {
            StopIdleDetection();
            StateChanged = null;
            Idle = null;
            LocoIOQuery = null;
            LocoIOFound = null;
            lock (stateLock)
            {
                Monitor.PulseAll(stateLock);
            }
            lb.PreviewMessage -= ProcessMessage;
        }
    }
}

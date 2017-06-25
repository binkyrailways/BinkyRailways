using BinkyRailways.Core.Model;
using BinkyRailways.Protocols.P50x;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinkyRailways.Core.State.Impl.P50x
{
    /// <summary>
    /// Command station which drives a P50x device
    /// </summary>
    public sealed partial class P50xCommandStationState : CommandStationState, IP50xCommandStationState
    {
        private readonly Client client = new Client();
        private DateTime lastStatusTime;
        private bool clientIdle = false;

        /// <summary>
        /// Default ctor
        /// </summary>
        public P50xCommandStationState(IP50xCommandStation entity, RailwayState railwayState, string[] addressSpaces)
            : base(entity, railwayState, addressSpaces)
        {
            client.PortName = entity.ComPortName;
            client.Opened += onClientOpened;
            client.Closed += onClientClosed;
        }

        /// <summary>
        /// Gets the strong typed entity.
        /// </summary>
        internal new IP50xCommandStation Entity
        {
            get { return (IP50xCommandStation)base.Entity; }
        }

        /// <summary>
        /// Request to enable/disable the power on the track.
        /// </summary>
        protected override void OnRequestedPowerChanged(bool value)
        {
            if (Power.Actual != value)
            {
                try
                {
                    if (value)
                    {
                        // Power on
                        Log.Info("Powering on");
                        client.PowerOn();
                    }
                    else
                    {
                        // Power off
                        Log.Info("Powering off");
                        client.PowerOff();
                    }
                    Power.Actual = value;
                }
                catch (Exception ex)
                {
                    Log.Warn("Power command failed: " + ex);
                }
            }
        }

        /// <summary>
        /// Send the speed and direction of the given loc towards the railway.
        /// </summary>
        protected override void OnSendLocSpeedAndDirection(ILocState loc)
        {
            Log.Trace("OnSendLocSpeedAndDirection: {0}", loc);
            try
            {
                var forward = (loc.Direction.Requested == LocDirection.Forward);
                client.LocCommand(loc.Address.ValueAsInt, loc.SpeedInSteps.Requested, forward, loc.F0.Requested, loc.F1.Requested, loc.F2.Requested, loc.F3.Requested, loc.F4.Requested);
                loc.Direction.Actual = loc.Direction.Requested;
                loc.Speed.Actual = loc.Speed.Requested;
                loc.F0.Actual = loc.F0.Requested;
                loc.F1.Actual = loc.F1.Requested;
                loc.F2.Actual = loc.F2.Requested;
                loc.F3.Actual = loc.F3.Requested;
                loc.F4.Actual = loc.F4.Requested;

                client.LocFunctions(loc.Address.ValueAsInt, loc.F1.Requested, loc.F2.Requested, loc.F3.Requested, loc.F4.Requested, loc.F5.Requested, loc.F6.Requested, loc.F7.Requested, loc.F8.Requested);
                loc.F1.Actual = loc.F1.Requested;
                loc.F2.Actual = loc.F2.Requested;
                loc.F3.Actual = loc.F3.Requested;
                loc.F4.Actual = loc.F4.Requested;
                loc.F5.Actual = loc.F5.Requested;
                loc.F6.Actual = loc.F6.Requested;
                loc.F7.Actual = loc.F7.Requested;
                loc.F8.Actual = loc.F8.Requested;
            }
            catch (Exception ex)
            {
                Log.Warn("Loc command failed: " + ex);
            }
        }

        /// <summary>
        /// Async worker will wait.
        /// </summary>
        protected override void OnWorkerWait(object sender, AsynchronousWorker.WaitEventArgs e)
        {
            base.OnWorkerWait(sender, e);
            Console.WriteLine("WorkerWait " + e.DelayedActionCount);
            if (lastStatusTime.AddSeconds(1) < DateTime.Now)
            {
                PostWorkerAction(() => UpdateStatus());
            }
            else
            {
                client.Idle = true;
                if (e.DelayedActionCount == 0)
                {
                    PostWorkerDelayedAction(TimeSpan.FromSeconds(1), () => UpdateStatus());
                }
                RefreshIdle();
            }
        }

        /// <summary>
        /// Request command station status
        /// </summary>
        private void UpdateStatus()
        {
            lastStatusTime = DateTime.Now;
            var st = client.Status();
            if (Power.Actual != st.Pwr)
            {
                Log.Info("Power status changed -> " + (st.Pwr ? "on" : "off"));
                if (st.Pwr)
                {
                    RailwayState.Power.Requested = true;
                }
                Power.Actual = st.Pwr;
            }
        }

        void onClientClosed()
        {
            Log.Info("Connection closed");
        }

        void onClientOpened()
        {
            Log.Info("Connection opened");
            PostWorkerAction(sendInitCommands);
        }

        void sendInitCommands()
        {
            client.SetCTime(255); // Disable CTS on non-PC power off.
            var version = client.Version();
            Log.Info("Found version: " + version.ToString());
        }

        /// <summary>
        /// Determine if this command station is idle.
        /// </summary>
        protected override bool IsIdle()
        {
            return base.IsIdle() && client.Idle;
        }

        /// <summary>
        /// Prepare this state for use in a live railway. 
        /// Make sure all relevant connections to other state objects are resolved.
        /// </summary>
        /// <returns>True if the entity is now ready for use in a live railway, false otherwise.</returns>
        protected override bool TryPrepareForUse(IStateUserInterface ui, IStatePersistence statePersistence)
        {
            if (!base.TryPrepareForUse(ui, statePersistence))
                return false;
            var portNames = SerialPort.GetPortNames();
            if (!portNames.Contains(Entity.ComPortName))
            {
                var portName = ui.ChooseComPortName(this);
                if (portNames.Contains(portName))
                {
                    // Now we have an available port name
                    client.PortName = portName;
                }
                else
                {
                    // No available port name provided, we cannot be used.
                    return false;
                }
            }
            // Request for status, this triggers the open process.
            PostWorkerAction(() => client.Status());
            return true;
        }

        /// <summary>
        /// Cleanup
        /// </summary>
        public override void Dispose()
        {
            ((IDisposable)client).Dispose();
            base.Dispose();
        }

        /// <summary>
        /// Gets advanced info for the given loc
        /// </summary>
        internal override string GetLocInfo(ILocState loc)
        {
            return "";
            //return client.GetLocInfo(loc);
        }
    }
}

using BinkyRailways.Core.Model;
using BinkyRailways.Protocols.P50x;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
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
                var f1 = loc.GetFunctionRequestedState(LocFunction.F1);
                var f2 = loc.GetFunctionRequestedState(LocFunction.F2);
                var f3 = loc.GetFunctionRequestedState(LocFunction.F3);
                var f4 = loc.GetFunctionRequestedState(LocFunction.F4);
                client.LocCommand(loc.Address.ValueAsInt, loc.SpeedInSteps.Requested, forward, loc.F0.Requested, f1, f2, f3, f4);
                loc.Direction.Actual = loc.Direction.Requested;
                loc.Speed.Actual = loc.Speed.Requested;
                loc.F0.Actual = loc.F0.Requested;
                loc.SetFunctionActualState(LocFunction.F1, f1);
                loc.SetFunctionActualState(LocFunction.F2, f2);
                loc.SetFunctionActualState(LocFunction.F3, f3);
                loc.SetFunctionActualState(LocFunction.F4, f4);

                var f5 = loc.GetFunctionRequestedState(LocFunction.F5);
                var f6 = loc.GetFunctionRequestedState(LocFunction.F6);
                var f7 = loc.GetFunctionRequestedState(LocFunction.F7);
                var f8 = loc.GetFunctionRequestedState(LocFunction.F8);
                client.LocFunctions(loc.Address.ValueAsInt, f1, f2, f3, f4, f5, f6, f7, f8);
                loc.SetFunctionActualState(LocFunction.F1, f1);
                loc.SetFunctionActualState(LocFunction.F2, f2);
                loc.SetFunctionActualState(LocFunction.F3, f3);
                loc.SetFunctionActualState(LocFunction.F4, f4);
                loc.SetFunctionActualState(LocFunction.F5, f5);
                loc.SetFunctionActualState(LocFunction.F6, f6);
                loc.SetFunctionActualState(LocFunction.F7, f7);
                loc.SetFunctionActualState(LocFunction.F8, f8);

                var f9 = loc.GetFunctionRequestedState(LocFunction.F9);
                var f10 = loc.GetFunctionRequestedState(LocFunction.F10);
                var f11 = loc.GetFunctionRequestedState(LocFunction.F11);
                var f12 = loc.GetFunctionRequestedState(LocFunction.F12);
                var f13 = loc.GetFunctionRequestedState(LocFunction.F13);
                var f14 = loc.GetFunctionRequestedState(LocFunction.F14);
                var f15 = loc.GetFunctionRequestedState(LocFunction.F15);
                var f16 = loc.GetFunctionRequestedState(LocFunction.F16);
                client.LocFunctions2(loc.Address.ValueAsInt, f9, f10, f11, f12, f13, f14, f15, f16);
                loc.SetFunctionActualState(LocFunction.F9, f9);
                loc.SetFunctionActualState(LocFunction.F10, f10);
                loc.SetFunctionActualState(LocFunction.F11, f11);
                loc.SetFunctionActualState(LocFunction.F12, f12);
                loc.SetFunctionActualState(LocFunction.F13, f13);
                loc.SetFunctionActualState(LocFunction.F14, f14);
                loc.SetFunctionActualState(LocFunction.F15, f15);
                loc.SetFunctionActualState(LocFunction.F16, f16);
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
            Thread.Sleep(500);
            Log.Info("Checking version");
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

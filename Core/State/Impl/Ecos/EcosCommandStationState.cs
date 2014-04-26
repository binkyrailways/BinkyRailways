using BinkyRailways.Core.Model;
using BinkyRailways.Protocols.Ecos;

namespace BinkyRailways.Core.State.Impl.Ecos
{
    /// <summary>
    /// Command station which drives an ECoS
    /// </summary>
    public sealed class EcosCommandStationState : CommandStationState, IEcosCommandStationState
    {
        private readonly EcosConnection connection;
        private bool networkIdle = false;

        /// <summary>
        /// Default ctor
        /// </summary>
        public EcosCommandStationState(IEcosCommandStation entity, RailwayState railwayState, string[] addressSpaces)
            : base(entity, railwayState, addressSpaces)
        {
            connection = new EcosConnection(entity.HostName, railwayState);
            // Request to be notified
            connection.SendCommand(new Command(Constants.CmdRequest, Constants.IdEcos, Constants.OptView));
            // Get status
            connection.SendCommand(new Command(Constants.CmdGet, Constants.IdEcos, Constants.OptStatus)).ContinueWith(t => OnStatusReply(t.Result));
        }

        /// <summary>
        /// Gets the strong typed entity.
        /// </summary>
        internal new IEcosCommandStation Entity
        {
            get { return (IEcosCommandStation) base.Entity;  }
        }

        /// <summary>
        /// Process a get(1, status) command reply.
        /// </summary>
        private void OnStatusReply(Reply reply)
        {
            if (!reply.IsSucceeded)
                return;
            var state = reply.Rows[0][Constants.OptStatus].Value;
            switch (state)
            {
                case "STOP":
                case "SHUTDOWN":
                    Power.Actual = false;
                    Power.Requested = false;
                    break;
                case "GO":
                    Power.Actual = true;
                    Power.Requested = true;
                    break;
            }
            // Initialization is complete
            networkIdle = true;
            RefreshIdle();
        }

        /// <summary>
        /// Request to enable/disable the power on the track.
        /// </summary>
        protected override void OnRequestedPowerChanged(bool value)
        {
            if (Power.Actual != value)
            {
                var option = value ? Constants.OptGo : Constants.OptStop;
                connection.SendCommand(new Command(Constants.CmdSet, Constants.IdEcos, option)).ContinueWith(t => {
                    if (t.Result.IsSucceeded) Power.Actual = value;
                });
            }
        }

        /// <summary>
        /// Send the speed and direction of the given loc towards the railway.
        /// </summary>
        protected override void OnSendLocSpeedAndDirection(ILocState loc)
        {
            Log.Trace("OnSendLocSpeedAndDirection: {0}", loc);
            Loc l;
            if (!connection.TryGetLoc(loc, true, out l))
                return;

            var direction = (loc.Direction.Requested == LocDirection.Forward);
            l.SetDirection(direction);
            loc.Direction.Actual = loc.Direction.Requested;
            if (!loc.F0.IsConsistent) l.SetFunction(0, loc.F0.Requested);
            if (!loc.F1.IsConsistent) l.SetFunction(1, loc.F1.Requested);
            if (!loc.F2.IsConsistent) l.SetFunction(2, loc.F2.Requested);
            if (!loc.F3.IsConsistent) l.SetFunction(3, loc.F3.Requested);
            if (!loc.F4.IsConsistent) l.SetFunction(4, loc.F4.Requested);
            if (!loc.F5.IsConsistent) l.SetFunction(5, loc.F5.Requested);
            if (!loc.F6.IsConsistent) l.SetFunction(6, loc.F6.Requested);
            if (!loc.F7.IsConsistent) l.SetFunction(7, loc.F7.Requested);
            if (!loc.F8.IsConsistent) l.SetFunction(8, loc.F8.Requested);

            l.SetSpeed((int)((127.0 / 100.0) * loc.Speed.Requested));
            loc.Speed.Actual = loc.Speed.Requested;
            loc.F0.Actual = loc.F0.Requested;
            loc.F1.Actual = loc.F1.Requested;
            loc.F2.Actual = loc.F2.Requested;
            loc.F3.Actual = loc.F3.Requested;
            loc.F4.Actual = loc.F4.Requested;
            loc.F5.Actual = loc.F5.Requested;
            loc.F6.Actual = loc.F6.Requested;
            loc.F7.Actual = loc.F7.Requested;
            loc.F8.Actual = loc.F8.Requested;
        }

        /// <summary>
        /// Determine if this command station is idle.
        /// </summary>
        protected override bool IsIdle()
        {
            return base.IsIdle() && networkIdle;
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
            /*var portNames = SerialPort.GetPortNames();
            if (!portNames.Contains(Entity.ComPortName))
            {
                var portName = ui.ChooseComPortName(this);
                if (portNames.Contains(portName))
                {
                    // Now we have an available port name
                    sender.PortName = portName;
                }
                else
                {
                    // No available port name provided, we cannot be used.
                    return false;
                }
            }*/
            return true;
        }

        /// <summary>
        /// Cleanup
        /// </summary>
        public override void Dispose()
        {
            connection.Dispose();
            base.Dispose();
        }

        /// <summary>
        /// Gets advanced info for the given loc
        /// </summary>
        internal override string GetLocInfo(ILocState loc)
        {
            Loc l;
            return connection.TryGetLoc(loc, false, out l) ? l.Id.ToString() : "";
        }
    }
}

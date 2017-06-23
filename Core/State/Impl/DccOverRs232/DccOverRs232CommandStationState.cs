using System;
using System.IO.Ports;
using System.Linq;
using BinkyRailways.Core.Model;
using BinkyRailways.Protocols.Dcc;

namespace BinkyRailways.Core.State.Impl.DccOverRs232
{
    /// <summary>
    /// Command station which drives a LocoBuffer
    /// </summary>
    public sealed partial class DccOverRs232CommandStationState : CommandStationState, IDccOverRs232CommandStationState
    {
        private readonly PacketSender sender;
        private bool networkIdle = false;

        /// <summary>
        /// Default ctor
        /// </summary>
        public DccOverRs232CommandStationState(IDccOverRs232CommandStation entity, RailwayState railwayState, string[] addressSpaces)
            : base(entity, railwayState, addressSpaces)
        {
            sender = new PacketSender(entity.ComPortName);
            sender.PriorityPacketsEmpty += (s, x) =>
            {
                networkIdle = true;
                RefreshIdle();
            };
        }

        /// <summary>
        /// Gets the strong typed entity.
        /// </summary>
        internal new IDccOverRs232CommandStation Entity
        {
            get { return (IDccOverRs232CommandStation) base.Entity;  }
        }

        /// <summary>
        /// Request to enable/disable the power on the track.
        /// </summary>
        protected override void OnRequestedPowerChanged(bool value)
        {
            if (Power.Actual != value)
            {
                if (value)
                {
                    // Power on
                    sender.Start();
                }
                else
                {
                    // Power off
                    sender.Stop();
                }
                Power.Actual = value;
            }
        }

        /// <summary>
        /// Send the speed and direction of the given loc towards the railway.
        /// </summary>
        protected override void OnSendLocSpeedAndDirection(ILocState loc)
        {
            Log.Trace("OnSendLocSpeedAndDirection: {0}", loc);
            var direction = (loc.Direction.Requested == LocDirection.Forward);
            var packet = Packets.CreateSpeedAndDirection(loc.Address.ValueAsInt, (byte)loc.SpeedInSteps.Requested, direction, loc.SpeedSteps);
            var data = PacketTranslater.Translate(packet);
            sender.SendSpeedAndDirection(loc.Address.ValueAsInt, data);
            loc.Direction.Actual = loc.Direction.Requested;
            loc.Speed.Actual = loc.Speed.Requested;
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
            var portNames = SerialPort.GetPortNames();
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
            }
            return true;
        }

        /// <summary>
        /// Cleanup
        /// </summary>
        public override void Dispose()
        {
            ((IDisposable)sender).Dispose();
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

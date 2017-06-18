using System;
using System.IO.Ports;
using System.Linq;
using BinkyRailways.Core.Model;
using BinkyRailways.Protocols.Dcc;

namespace BinkyRailways.Core.State.Impl.Mqtt
{
    /// <summary>
    /// Command station which sends/receives data from MQTT.
    /// </summary>
    public sealed partial class MqttCommandStationState : CommandStationState, IMqttCommandStationState
    {
        private readonly PacketSender sender;
        private bool networkIdle = false;

        /// <summary>
        /// Default ctor
        /// </summary>
        public MqttCommandStationState(IMqttCommandStation entity, RailwayState railwayState, string[] addressSpaces)
            : base(entity, railwayState, addressSpaces)
        {
        }

        /// <summary>
        /// Gets the strong typed entity.
        /// </summary>
        internal new IMqttCommandStation Entity
        {
            get { return (IMqttCommandStation) base.Entity;  }
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
            var packet = Packets.CreateSpeedAndDirection(loc.Address.Value, (byte)loc.SpeedInSteps.Requested, direction, loc.SpeedSteps);
            var data = PacketTranslater.Translate(packet);
            sender.SendSpeedAndDirection(loc.Address.Value, data);
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

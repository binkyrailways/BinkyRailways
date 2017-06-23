using System;
using System.IO.Ports;
using System.Linq;
using BinkyRailways.Core.Model;
using BinkyRailways.Protocols.Dcc;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;
using Newtonsoft.Json.Linq;
using System.Text;
using Newtonsoft.Json;

namespace BinkyRailways.Core.State.Impl.Mqtt
{
    /// <summary>
    /// Command station which sends/receives data from MQTT.
    /// </summary>
    public sealed partial class MqttCommandStationState : CommandStationState, IMqttCommandStationState
    {
        private bool networkIdle = false;
        private readonly MqttClient client;
        private readonly string clientID;
        private readonly string topicPrefix;

        /// <summary>
        /// Default ctor
        /// </summary>
        public MqttCommandStationState(IMqttCommandStation entity, RailwayState railwayState, string[] addressSpaces)
            : base(entity, railwayState, addressSpaces)
        {
            client = new MqttClient(entity.HostName);
            client.MqttMsgPublishReceived += client_MqttMsgPublishReceived;
            client.MqttMsgPublished += client_MqttMsgPublished;
            clientID = entity.Id;
            topicPrefix = entity.TopicPrefix;
        }

        void client_MqttMsgPublished(object sender, MqttMsgPublishedEventArgs e)
        {
            Log.Info("Mqtt message published: " + e.IsPublished);
        }

        void client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            Log.Info("Mqtt message received: " + e.Topic);
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
                    client.Connect(clientID);
                }
                else
                {
                    // Power off
                    client.Disconnect();
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

            var data = new JObject();
            data["speed"] = loc.SpeedInSteps.Requested;
            data["direction"] = loc.Direction.Requested.ToString();
            var payload = data.ToString(Formatting.None);
            client.Publish(topicPrefix + "/loc", Encoding.UTF8.GetBytes(payload), MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE, true);
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
        /// Gets advanced info for the given loc
        /// </summary>
        internal override string GetLocInfo(ILocState loc)
        {
            return "";
            //return client.GetLocInfo(loc);
        }
    }
}

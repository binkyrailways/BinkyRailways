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
            client.MqttMsgPublishReceived += onMqttMsgPublishReceived;
            client.MqttMsgPublished += onMqttMsgPublished;
            clientID = entity.Id;
            topicPrefix = entity.TopicPrefix;
        }

        /// <summary>
        /// Gets the strong typed entity.
        /// </summary>
        internal new IMqttCommandStation Entity
        {
            get { return (IMqttCommandStation)base.Entity; }
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

                    // Subscribe to topics 
                    client.Subscribe(new string[] { topicPrefix + "/binary-sensor" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE });
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
        /// MQTT message has been published.
        /// </summary>
        void onMqttMsgPublished(object sender, MqttMsgPublishedEventArgs e)
        {
            Log.Debug("Mqtt message published: " + e.IsPublished);
        }

        /// <summary>
        /// MQTT message has been received.
        /// </summary>
        void onMqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            if (!e.Topic.StartsWith(topicPrefix))
            {
                return;
            }
            var topic = e.Topic.Substring(topicPrefix.Length);
            var payload = Encoding.UTF8.GetString(e.Message);
            switch (topic)
            {
                case "/binary-sensor":
                    Log.Debug("Binary sensor message received: " + payload);
                    var msg = JsonConvert.DeserializeObject<BinarySensorMessage>(payload);
                    onBinarySensorMessageReceived(msg);
                    break;
                default:
                    Log.Info("Unhandled Mqtt message received: " + e.Topic);
                    break;
            }
        }

        /// <summary>
        /// Binary sensor message has been received.
        /// </summary>
        private void onBinarySensorMessageReceived(BinarySensorMessage msg)
        {
            var sensor = RailwayState.SensorStates.FirstOrDefault(x => (x.Address.Value == msg.Address) && (x.Address.Network.Type == AddressType.Mqtt));
            if (sensor != null)
            {
                sensor.Active.Actual = (msg.Value == 1);
            }
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

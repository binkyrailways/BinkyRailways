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
            if (!value && client.IsConnected)
            {
                // Send power off message
                var msg = new PowerMessage()
                {
                    Active = 0
                };
                publishMessage("/power", msg);
            }

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

            if (value && client.IsConnected)
            {
                // Send power on message
                var msg = new PowerMessage()
                {
                    Active = 1
                };
                publishMessage("/power", msg);
            }
        }

        /// <summary>
        /// Send the speed and direction of the given loc towards the railway.
        /// </summary>
        protected override void OnSendLocSpeedAndDirection(ILocState loc)
        {
            Log.Trace("OnSendLocSpeedAndDirection: {0}", loc);

            var msg = new LocMessage()
            {
                Address = loc.Address.Value,
                Direction = loc.Direction.Requested.ToString().ToLower(),
                Speed = loc.SpeedInSteps.Requested,
            };
            if (publishMessage("/loc", msg))
            {
                loc.Direction.Actual = loc.Direction.Requested;
                loc.Speed.Actual = loc.Speed.Requested;
            }
        }

        /// <summary>
        /// Send the on/off value of a binary output.
        /// </summary>
        protected override void OnSendBinaryOutput(IBinaryOutputState output)
        {
            Log.Trace("OnSendBinaryOutput: {0}", output);

            var msg = new BinaryOutputMessage()
            {
                Address = output.Address.Value,
                Value = output.Active.Requested ? 1 : 0
            };
            if (publishMessage("/binary-output", msg))
            {
                output.Active.Actual = output.Active.Actual;
            }
        }

        /// <summary>
        /// Send the pattern of a 4-stage clock output.
        /// </summary>
        protected override void OnSendClock4StageOutput(IClock4StageOutputState output)
        {
            Log.Trace("OnSendClock4StageOutput: {0}", output);

            var msg = new Clock4StageMessage()
            {
                Stage = output.Period.Requested.ToString().ToLower()
            };
            if (publishMessage("/clock-4-stage", msg))
            {
                output.Period.Actual = output.Period.Actual;
            }
        }

        /// <summary>
        /// Send the on/off value of a binary output.
        /// </summary>
        protected override void OnSendBinaryOutput(Address address, bool value)
        {
            Log.Trace("OnSendBinaryOutput: {0}, {1}", address, value);

            var msg = new BinaryOutputMessage()
            {
                Address = address.Value,
                Value = value ? 1 : 0
            };
            publishMessage("/binary-output", msg);
        }

        /// <summary>
        /// Convert the given message to json and publish it.
        /// </summary>
        private bool publishMessage(string topic, object msg)
        {
            if (client.IsConnected)
            {
                try
                {
                    var payload = JsonConvert.SerializeObject(msg, Formatting.None);
                    client.Publish(topicPrefix + topic, Encoding.UTF8.GetBytes(payload), MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE, true);
                    return true;
                }
                catch (Exception ex)
                {
                    Log.Error("MQTT publish failed: " + ex);
                    return false;
                }
            }
            else
            {
                return false;
            }
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

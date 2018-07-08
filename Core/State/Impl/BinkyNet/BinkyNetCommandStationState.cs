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
using System.Threading;
using System.Collections.Generic;

namespace BinkyRailways.Core.State.Impl.BinkyNet
{
    /// <summary>
    /// Command station which sends/receives data from MQTT.
    /// </summary>
    public sealed partial class BinkyNetCommandStationState : CommandStationState, IMqttCommandStationState
    {
        private delegate void CompletionCallback(MqttMsgPublishedEventArgs e);

        private bool networkIdle = false;
        private MqttClient client;
        private readonly string clientID;
        private readonly string sender;
        private readonly string topicPrefix;
        private readonly Dictionary<ushort, CompletionCallback> completionCallbacks;

        /// <summary>
        /// Default ctor
        /// </summary>
        public BinkyNetCommandStationState(IBinkyNetCommandStation entity, RailwayState railwayState, string[] addressSpaces)
            : base(entity, railwayState, addressSpaces)
        {
            completionCallbacks = new Dictionary<ushort, CompletionCallback>();
            clientID = entity.Id;
            sender = "binkyrailways";
            topicPrefix = entity.TopicPrefix;
        }

        /// <summary>
        /// Gets the strong typed entity.
        /// </summary>
        internal new IBinkyNetCommandStation Entity
        {
            get { return (IBinkyNetCommandStation)base.Entity; }
        }

        /// <summary>
        /// Request to enable/disable the power on the track.
        /// </summary>
        protected override void OnRequestedPowerChanged(bool value)
        {
            // Send power off message
            var msg = new PowerMessage()
            {
                Sender = sender,
                Mode = MessageBase.ModeRequest,
                Active = value
            };
            publishMessage(msg.Topic, msg, (e) =>
            {
                if (e.IsPublished)
                {
                    Power.Actual = value;
                }
            });
        }

        /// <summary>
        /// Connection to MQTT server has been closed.
        /// </summary>
        void onMqttConnectionClosed(object sender, EventArgs e)
        {
            Log.Error("Connection to MQTT server closed");
            Power.Actual = false;
        }

        /// <summary>
        /// Send the speed and direction of the given loc towards the railway.
        /// </summary>
        protected override void OnSendLocSpeedAndDirection(ILocState loc)
        {
            Log.Trace("OnSendLocSpeedAndDirection: {0}", loc);

            var msg = new LocMessage()
            {
                Sender = sender,
                Mode = MessageBase.ModeRequest,
                Address = loc.Address.Value,
                Direction = loc.Direction.Requested.ToString().ToLower(),
                Speed = loc.SpeedInSteps.Requested,
            };
            publishMessage(msg.Topic, msg, (e) =>
            {
                if (e.IsPublished)
                {
                    loc.Direction.Actual = loc.Direction.Requested;
                    loc.Speed.Actual = loc.Speed.Requested;
                }
            });
        }

        /// <summary>
        /// Send the on/off value of a binary output.
        /// </summary>
        protected override void OnSendBinaryOutput(IBinaryOutputState output)
        {
            Log.Trace("OnSendBinaryOutput: {0}", output);

            var msg = new BinaryMessage()
            {
                Sender = sender,
                Mode = MessageBase.ModeRequest,
                Address = output.Address.Value,
                Value = output.Active.Requested
            };
            publishMessage(msg.Topic, msg, (e) =>
            {
                if (e.IsPublished)
                {
                    output.Active.Actual = output.Active.Actual;
                }
            });
        }

        /// <summary>
        /// Send the pattern of a 4-stage clock output.
        /// </summary>
        protected override void OnSendClock4StageOutput(IClock4StageOutputState output)
        {
            Log.Trace("OnSendClock4StageOutput: {0}", output);

            var msg = new ClockMessage()
            {
                Sender = sender,
                Mode = MessageBase.ModeActual,
                Period = output.Period.Requested.ToString().ToLower()
            };
            publishMessage(msg.Topic, msg, (e) =>
            {
                if (e.IsPublished)
                {
                    output.Period.Actual = output.Period.Actual;
                }
            });
        }

        /// <summary>
        /// Send the on/off value of a binary output.
        /// </summary>
        protected override void OnSendBinaryOutput(Address address, bool value)
        {
            Log.Trace("OnSendBinaryOutput: {0}, {1}", address, value);

            var msg = new BinaryMessage()
            {
                Sender = sender,
                Mode = MessageBase.ModeRequest,
                Address = address.Value,
                Value = value
            };
            publishMessage(msg.Topic, msg, null);
        }

        /// <summary>
        /// Send the direction of the given switch towards the railway.
        /// This method is called on my worker thread.
        /// </summary>
        protected override void OnSendSwitchDirection(ISwitchState @switch)
        {
            Log.Trace("OnSendSwitchDirection: {0}", @switch);

            var direction = @switch.Direction.Requested;
            if (@switch.Invert)
            {
                direction = direction.Invert();
            }
            var msg = new SwitchMessage()
            {
                Sender = sender,
                Mode = MessageBase.ModeRequest,
                Address = @switch.Address.Value,
                Direction = direction.ToString().ToLower()
            };
            publishMessage(msg.Topic, msg, null);
        }


        /// <summary>
        /// Convert the given message to json and publish it.
        /// </summary>
        private bool publishMessage(string topic, object msg, CompletionCallback cb)
        {
            if ((client != null) && (client.IsConnected))
            {
                try
                {
                    lock (completionCallbacks)
                    {
                        var payload = JsonConvert.SerializeObject(msg, Formatting.None);
                        var msgID = client.Publish(topicPrefix + topic, Encoding.UTF8.GetBytes(payload), MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE, true);
                        if (cb != null)
                        {
                            completionCallbacks[msgID] = cb;
                        }
                        return true;
                    }
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
            CompletionCallback cb;
            lock (completionCallbacks)
            {
                if (completionCallbacks.TryGetValue(e.MessageId, out cb))
                {
                    completionCallbacks.Remove(e.MessageId);
                }
            }
            if (cb != null)
            {
                PostWorkerAction(() => cb(e));
            }
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
            var topicParts = topic.Split('/');
            var payload = Encoding.UTF8.GetString(e.Message);
            switch (topicParts.Last())
            {
                case "binary":
                    {
                        Log.Debug("Binary sensor message received: " + payload);
                        var msg = JsonConvert.DeserializeObject<BinaryMessage>(payload);
                        onBinaryMessageReceived(msg);
                    }
                    break;
                case "power":
                    {
                        Log.Debug("Power message received: " + payload);
                        var msg = JsonConvert.DeserializeObject<PowerMessage>(payload);
                        onPowerMessageReceived(msg);
                    }
                    break;
                case "switch":
                    {
                        Log.Debug("Switch message received: " + payload);
                        var msg = JsonConvert.DeserializeObject<SwitchMessage>(payload);
                        onSwitchMessageReceived(msg);
                    }
                    break;
                default:
                    Log.Info("Unhandled Mqtt message received: " + e.Topic);
                    break;
            }
        }

        /// <summary>
        /// Binary message has been received.
        /// </summary>
        private void onBinaryMessageReceived(BinaryMessage msg)
        {
            if (msg.IsActual && msg.Sender != sender)
            {
                var sensor = RailwayState.SensorStates.FirstOrDefault(x => (x.Address.Value == msg.Address) && (x.Address.Network.Type == AddressType.BinkyNet));
                if (sensor != null)
                {
                    sensor.Active.Actual = msg.Value;
                }
            }
        }

        /// <summary>
        /// Power message has been received.
        /// </summary>
        private void onPowerMessageReceived(PowerMessage msg)
        {
            if (msg.IsRequest && msg.Sender != sender)
            {
                Power.Requested = msg.Active;
            }
        }

        /// <summary>
        /// Switch message has been received.
        /// </summary>
        private void onSwitchMessageReceived(SwitchMessage msg)
        {
            if (msg.IsActual && msg.Sender != sender)
            {
                var @switch = RailwayState.JunctionStates.OfType<ISwitchState>().FirstOrDefault(x => (x.Address.Value == msg.Address) && (x.Address.Network.Type == AddressType.BinkyNet));
                if (@switch != null)
                {
                    var direction = (msg.Direction == "off") ? SwitchDirection.Off : SwitchDirection.Straight;
                    if (@switch.Invert)
                    {
                        direction = direction.Invert();
                    }
                    @switch.Direction.Actual = direction;
                }
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
            try
            {
                client = new MqttClient(Entity.HostName);
                client.MqttMsgPublishReceived += onMqttMsgPublishReceived;
                client.MqttMsgPublished += onMqttMsgPublished;
                client.ConnectionClosed += onMqttConnectionClosed;

                // Power on
                client.Connect(clientID);

                // Subscribe to topics 
                var topics = getSubscribeTopics();
                var qos = topics.Select(x => MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE).ToArray();
                client.Subscribe(topics, qos);
            }
            catch (Exception ex)
            {
                Log.Error("Failed to connect to MQTT server: " + ex);
                return false;
            }
            return true;
        }

        private string[] getSubscribeTopics()
        {
            var topics = new List<string>();
            var topicPrefix = this.topicPrefix;
            if (topicPrefix.Length > 0)
            {
                topicPrefix = topicPrefix + "/";
            }
            topics.Add(topicPrefix + "global/power");
            topics.AddRange(RailwayState.OutputStates.OfType<IBinaryOutputState>().Where(x => x.Address.Type == AddressType.BinkyNet).Select(x => topicPrefix + x.Address.Value.Split('/').First() + "/binary"));
            topics.AddRange(RailwayState.SensorStates.OfType<IBinarySensorState>().Where(x => x.Address.Type == AddressType.BinkyNet).Select(x => topicPrefix + x.Address.Value.Split('/').First() + "/binary"));
            topics.AddRange(RailwayState.JunctionStates.OfType<ISwitchState>().Where(x => x.Address.Type == AddressType.BinkyNet).Select(x => topicPrefix + x.Address.Value.Split('/').First() + "/switch"));
            topics.Sort();
            return topics.Distinct().ToArray();
        }

        public override void Dispose()
        {
            if (client != null)
            {
                client.MqttMsgPublishReceived -= onMqttMsgPublishReceived;
                client.MqttMsgPublished -= onMqttMsgPublished;
                client.ConnectionClosed -= onMqttConnectionClosed;
                client.Disconnect();
                client = null;
            }
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BinkyRailways.Core.Logging;
using BinkyRailways.Core.Model;
using BinkyRailways.Core.State;
using Newtonsoft.Json;
using NLog;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace BinkyRailways.Core.Server.Impl
{
    public class Server : IServer, IDisposable
    {
        private const string controlTopicPostfix = "/control";
        private const string dataTopicPostfix = "/data";
        IRailway railway;
        IRailwayState railwayState;
        private MqttClient client;
        private string controlTopic;
        private string dataTopic;
        private readonly Logger Log;
        private readonly string clientID;

        public Server()
        {
            Log = LogManager.GetLogger(LogNames.WebServer);
            clientID = Guid.NewGuid().ToString();
        }

        /// <summary>
        /// Currently active railway
        /// </summary>
        IRailway IServer.Railway
        {
            get => railway;
            set
            {
                if (railway != value)
                {
                    if (railway != null)
                    {
                        railway.PropertyChanged -= onRailwayPropertyChanged;
                    }
                    railway = value;
                    reconnectClient();
                    if (value != null)
                    {
                        value.PropertyChanged += onRailwayPropertyChanged;
                    }
                }
            }
        }

        /// <summary>
        /// A property of the railway has changed. Reconnect to MQTT broker.
        /// </summary>
        private void onRailwayPropertyChanged(object sender, EventArgs e)
        {
            reconnectClient();
        }

        /// <summary>
        /// Currently active railway state.
        /// </summary>
        IRailwayState IServer.RailwayState
        {
            get => railwayState;
            set
            {
                if (railwayState != value)
                {
                    if (railwayState != null)
                    {
                        railwayState.Power.ActualChanged -= onPowerChanged;
                        railwayState.Power.RequestedChanged -= onPowerChanged;
                        railwayState.AutomaticLocController.Enabled.ActualChanged -= onAutomaticLocControllerEnabledChanged;
                        railwayState.AutomaticLocController.Enabled.RequestedChanged -= onAutomaticLocControllerEnabledChanged;
                    }
                    railwayState = value;
                    if (value != null)
                    {
                        value.Power.ActualChanged += onPowerChanged;
                        value.Power.RequestedChanged += onPowerChanged;
                        value.AutomaticLocController.Enabled.ActualChanged += onAutomaticLocControllerEnabledChanged;
                        value.AutomaticLocController.Enabled.RequestedChanged += onAutomaticLocControllerEnabledChanged;
                    }
                    var msgType = (value != null) ? Messages.TypeRunning : Messages.TypeEditing;
                    publishMessage(new Messages.BaseServerMessage { Type = msgType });
                }
            }
        }

        /// <summary>
        /// Automatic loc controller enabled has changed.
        /// </summary>
        private void onAutomaticLocControllerEnabledChanged(object sender, EventArgs e)
        {
            if (railwayState != null)
            {
                publishMessage(new Messages.AutomaticLocControllerEnabledChangedMessage
                {
                    Actual = railwayState.AutomaticLocController.Enabled.Actual,
                    Requested = railwayState.AutomaticLocController.Enabled.Requested,
                });
            }
        }

        /// <summary>
        /// Power actual or request has changed.
        /// </summary>
        private void onPowerChanged(object sender, EventArgs e)
        {
            if (railwayState != null)
            {
                publishMessage(new Messages.PowerChangedMessage
                {
                    Actual = railwayState.Power.Actual,
                    Requested = railwayState.Power.Requested,
                });
            }
        }

        /// <summary>
        /// Convert the given message to json and publish it.
        /// </summary>
        private bool publishMessage(object msg)
        {
            if ((client != null) && (client.IsConnected))
            {
                try
                {
                    var payload = JsonConvert.SerializeObject(msg, Formatting.None);
                    var msgID = client.Publish(dataTopic, Encoding.UTF8.GetBytes(payload), MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE, true);
       
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
          /*  CompletionCallback cb;
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
            }*/
        }

        /// <summary>
        /// MQTT message has been received.
        /// </summary>
        void onMqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            if (e.Topic != controlTopic)
            {
                return;
            }          
            var payload = Encoding.UTF8.GetString(e.Message);
            var baseMsg = JsonConvert.DeserializeObject<Messages.BaseServerMessage>(payload);
            switch (baseMsg.Type)
            {
                case Messages.TypePowerOn:
                    {
                        Log.Debug("Power on message received");
                        if (railwayState != null)
                        {
                            railwayState.Power.Requested = true;
                        }
                    }
                    break;
                case Messages.TypePowerOff:
                    {
                        Log.Debug("Power off message received");
                        if (railwayState != null)
                        {
                            railwayState.Power.Requested = false;
                        }
                    }
                    break;
                case Messages.TypAutomaticLocControllerOn:
                    {
                        Log.Debug("Automatic loc control on message received");
                        if (railwayState != null)
                        {
                            railwayState.AutomaticLocController.Enabled.Requested = true;
                        }
                    }
                    break;
                case Messages.TypAutomaticLocControllerOff:
                    {
                        Log.Debug("Automatic loc control off message received");
                        if (railwayState != null)
                        {
                            railwayState.AutomaticLocController.Enabled.Requested = false;
                        }
                    }
                    break;
                default:
                    Log.Info("Unhandled Mqtt message received: " + baseMsg.Type);
                    break;
            }
        }


        /// <summary>
        /// Connection to MQTT server has been closed.
        /// </summary>
        void onMqttConnectionClosed(object sender, EventArgs e)
        {
            Log.Error("Connection to MQTT server closed");
        }

        bool reconnectClient()
        {
            try
            {
                Dispose();
                if (railway == null)
                {
                    return false;
                }
                var hostName = railway.MqttHostName;
                var brokerPort = railway.MqttPort;
                controlTopic = railway.MqttTopic.TrimEnd('/') + controlTopicPostfix;
                dataTopic = railway.MqttTopic.TrimEnd('/') + dataTopicPostfix;
                if (hostName == "")
                {
                    return false;
                }
                client = new MqttClient(hostName, brokerPort, false, null, null, MqttSslProtocols.None);
                client.MqttMsgPublishReceived += onMqttMsgPublishReceived;
                client.MqttMsgPublished += onMqttMsgPublished;
                client.ConnectionClosed += onMqttConnectionClosed;

                // Power on
                client.Connect(clientID);

                // Subscribe to topics 
                client.Subscribe(new string[] { controlTopic }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE });
            }
            catch (Exception ex)
            {
                Log.Error("Failed to connect to MQTT server: " + ex);
                return false;
            }
            return true;
        }

        public void Dispose()
        {
            if (client != null)
            {
                client.MqttMsgPublishReceived -= onMqttMsgPublishReceived;
                client.MqttMsgPublished -= onMqttMsgPublished;
                client.ConnectionClosed -= onMqttConnectionClosed;
                if (client.IsConnected)
                {
                    client.Disconnect();
                }
                client = null;
            }
        }
    }
}

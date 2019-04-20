using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BinkyRailways.Core.Logging;
using BinkyRailways.Core.Model;
using BinkyRailways.Core.State;
using BinkyRailways.Core.State.Impl;
using BinkyRailways.Core.Util;
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
        private readonly AsynchronousWorker worker;

        public Server()
        {
            Log = LogManager.GetLogger(LogNames.WebServer);
            clientID = "binky-"+ Guid.NewGuid().ToString();
            worker = new AsynchronousWorker("server");
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
                        railway.PropertyChanged -= syncOnRailwayPropertyChanged;
                    }
                    railway = value;
                    reconnectClient();
                    if (value != null)
                    {
                        value.PropertyChanged += syncOnRailwayPropertyChanged;
                    }
                    onRailwayChanged();
                }
            }
        }

        /// <summary>
        /// A property of the railway has changed. Reconnect to MQTT broker.
        /// </summary>
        private void syncOnRailwayPropertyChanged(object sender, EventArgs e)
        {
            worker.PostAction(() => reconnectClient());
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
                        railwayState.BlockStates.Foreach(x => x.ActualStateChanged -= onActualBlockStateChanged);
                        railwayState.LocStates.Foreach(x => x.ActualStateChanged -= onActualLocStateChanged);
                    }
                    railwayState = value;
                    if (value != null)
                    {
                        value.Power.ActualChanged += onPowerChanged;
                        value.Power.RequestedChanged += onPowerChanged;
                        value.AutomaticLocController.Enabled.ActualChanged += onAutomaticLocControllerEnabledChanged;
                        value.AutomaticLocController.Enabled.RequestedChanged += onAutomaticLocControllerEnabledChanged;
                        railwayState.BlockStates.Foreach(x => x.ActualStateChanged += onActualBlockStateChanged);
                        railwayState.LocStates.Foreach(x => x.ActualStateChanged += onActualLocStateChanged);
                    }
                    onModeChanged();
                }
            }
        }


        private void onRefresh()
        {
            onRailwayChanged();
            onModeChanged();
            onPowerChanged(this, EventArgs.Empty);
            onAutomaticLocControllerEnabledChanged(this, EventArgs.Empty);
            onTimeChanged();
        }

        private void onRailwayChanged()
        {
            if (railway != null)
            {
                publishAsyncMessage(new Messages.RailwayMessage(railway, railwayState));
            }
        }

        private void onTimeChanged()
        {
            if (railwayState != null)
            {
                publishAsyncMessage(new Messages.TimeChangedMessage(railwayState.ModelTime.Actual));
            }
        }

        private void onModeChanged()
        {
            var msgType = (railwayState != null) ? Messages.TypeRunning : Messages.TypeEditing;
            publishAsyncMessage(new Messages.BaseServerMessage { Type = msgType });
        }

        /// <summary>
        /// Automatic loc controller enabled has changed.
        /// </summary>
        private void onAutomaticLocControllerEnabledChanged(object sender, EventArgs e)
        {
            if (railwayState != null)
            {
                publishAsyncMessage(new Messages.AutomaticLocControllerEnabledChangedMessage
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
                publishAsyncMessage(new Messages.PowerChangedMessage
                {
                    Actual = railwayState.Power.Actual,
                    Requested = railwayState.Power.Requested,
                });
            }
        }

        private void onActualLocStateChanged(object sender, EventArgs e)
        {
            if (railwayState != null)
            {
                var locState = sender as ILocState;
                if (locState != null)
                {
                    ILoc locEntity = null;
                    foreach (var loc in railway.Locs)
                    {
                        if (loc.Id == locState.EntityId)
                        {
                            loc.TryResolve(out locEntity);
                            break;
                        }
                    }
                    if (locEntity != null)
                    {
                        publishAsyncMessage(new Messages.LocChangedMessage(locEntity, locState));
                    }
                }
            }
        }

        private void onActualBlockStateChanged(object sender, EventArgs e)
        {
            if (railwayState != null)
            {
                var blockState = sender as IBlockState;
                if (blockState != null)
                {
                    IBlock block;
                    IBlockState tmp;
                    if (tryGetBlock(blockState.EntityId, out block, out tmp))
                    {
                        publishAsyncMessage(new Messages.BlockChangedMessage(block, blockState));
                    }
                }
            }
        }
        
        /// <summary>
        /// Convert the given message to json and publish it on the worker thread.
        /// </summary>
        private void publishAsyncMessage(object msg)
        {
            worker.PostAction(() => publishSyncMessage(msg));
        }

        /// <summary>
        /// Convert the given message to json and publish it on the current thread.
        /// </summary>
        private bool publishSyncMessage(object msg)
        {
            if ((client != null) && (client.IsConnected))
            {
                try
                {
                    var payload = JsonConvert.SerializeObject(msg, Formatting.None);
                    var msgID = client.Publish(dataTopic, Encoding.UTF8.GetBytes(payload), MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE, false);
       
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
            worker.PostAction(() => onProcessMqttMessage(baseMsg, payload, e));
        }

        void onProcessMqttMessage(Messages.BaseServerMessage baseMsg, string payload, MqttMsgPublishEventArgs e)
        { 
            switch (baseMsg.Type)
            {
                case Messages.TypeRefresh:
                    {
                        Log.Debug("Refresh message received");
                        onRefresh();
                    }
                    break;
                case Messages.TypePowerOn:
                    {
                        Log.Debug("Power on message received");
                        if (railwayState != null)
                        {
                            railwayState.Power.Requested = true;
                            onPowerChanged(this, EventArgs.Empty);
                        }
                    }
                    break;
                case Messages.TypePowerOff:
                    {
                        Log.Debug("Power off message received");
                        if (railwayState != null)
                        {
                            railwayState.Power.Requested = false;
                            onPowerChanged(this, EventArgs.Empty);
                        }
                    }
                    break;
                case Messages.TypeAutomaticLocControllerOn:
                    {
                        Log.Debug("Automatic loc control on message received");
                        if (railwayState != null)
                        {
                            railwayState.AutomaticLocController.Enabled.Requested = true;
                            onAutomaticLocControllerEnabledChanged(this, EventArgs.Empty);
                        }
                    }
                    break;
                case Messages.TypeAutomaticLocControllerOff:
                    {
                        Log.Debug("Automatic loc control off message received");
                        if (railwayState != null)
                        {
                            railwayState.AutomaticLocController.Enabled.Requested = false;
                            onAutomaticLocControllerEnabledChanged(this, EventArgs.Empty);
                        }
                    }
                    break;
                case Messages.TypeControlAutomatically:
                    {
                        Log.Debug("Control loc automatically message received");
                        ILocState locState;
                        if (tryGetLocState(baseMsg.Id, out locState))
                        {
                            locState.ControlledAutomatically.Requested = true;
                        }
                    }
                    break;
                case Messages.TypeControlManually:
                    {
                        Log.Debug("Control loc manually message received");
                        ILocState locState;
                        if (tryGetLocState(baseMsg.Id, out locState))
                        {
                            locState.ControlledAutomatically.Requested = false;
                        }
                    }
                    break;
                case Messages.TypeDirectionForward:
                    {
                        Log.Debug("Control loc manually message received");
                        ILocState locState;
                        if (tryGetLocState(baseMsg.Id, out locState))
                        {
                            locState.Direction.Requested = LocDirection.Forward;
                        }
                    }
                    break;
                case Messages.TypeDirectionReverse:
                    {
                        Log.Debug("Control loc manually message received");
                        ILocState locState;
                        if (tryGetLocState(baseMsg.Id, out locState))
                        {
                            locState.Direction.Requested = LocDirection.Reverse;
                        }
                    }
                    break;
                case Messages.TypeRemoveFromTrack:
                    {
                        Log.Debug("Remove loc from track message received");
                        ILocState locState;
                        if (tryGetLocState(baseMsg.Id, out locState))
                        {
                            locState.Reset();
                        }
                    }
                    break;
                case Messages.TypeSpeed:
                    {
                        Log.Debug("Speed message received");
                        ILocState locState;
                        if (tryGetLocState(baseMsg.Id, out locState))
                        {
                            locState.Speed.Requested = baseMsg.Speed;
                        }
                    }
                    break;
                case Messages.TypeOpenBlock:
                    {
                        Log.Debug("Open-block message received");
                        IBlock block;
                        IBlockState blockState;
                        if (tryGetBlock(baseMsg.Id, out block, out blockState) && (blockState != null))
                        {
                            blockState.Closed.Requested = false;
                        }
                    }
                    break;
                case Messages.TypeCloseBlock:
                    {
                        Log.Debug("Close-block message received");
                        IBlock block;
                        IBlockState blockState;
                        if (tryGetBlock(baseMsg.Id, out block, out blockState) && (blockState != null))
                        {
                            blockState.Closed.Requested = true;
                        }
                    }
                    break;
                default:
                    Log.Info("Unhandled Mqtt message received: " + baseMsg.Type);
                    break;
            }
        }

        private bool tryGetBlock(string id, out IBlock block, out IBlockState blockState)
        {
            block = null;
            blockState = null;
            if (railwayState != null)
            {
                blockState = railwayState.BlockStates.FirstOrDefault(x => x.EntityId == id);
                if (blockState == null)
                {
                    return false;
                }
            }
            foreach (var module in railway.Modules)
            {
                IModule moduleEntity;
                if (module.TryResolve(out moduleEntity))
                {
                    block = moduleEntity.Blocks.FirstOrDefault(b => b.Id == id);
                }
                if (block != null)
                {
                    return true;
                }
            }
            return false;
        }

        private bool tryGetLocState(string id, out ILocState locState)
        {
            var railwayState = this.railwayState;
            if ((railwayState != null) && !string.IsNullOrEmpty(id))
            {
                locState = railwayState.LocStates.FirstOrDefault(x => x.EntityId == id);
                return (locState != null);
            }
            locState = null;
            return false;
        }

            /// <summary>
            /// Connection to MQTT server has been closed.
            /// </summary>
            void onMqttConnectionClosed(object sender, EventArgs e)
        {
            Log.Error("Connection to MQTT server closed");
            worker.PostAction(() => reconnectClient());

        }

        bool reconnectClient()
        {
            try
            {
                closeMqttClient();
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
                Log.Info("Connected to MQTT broker");
            }
            catch (Exception ex)
            {
                Log.Error("Failed to connect to MQTT server: " + ex);
                return false;
            }
            onTimer();
            return true;
        }

        /// <summary>
        /// Called on a timer to send interval based updates.
        /// </summary>
        private void onTimer()
        {
            var client = this.client;
            if (client == null)
            {
                return;
            }
            var railwayState = this.railwayState;
            if (railwayState != null)
            {
                foreach (var loc in railwayState.LocStates)
                {
                    if (loc.ControlledAutomatically.Actual)
                    {
                        onActualLocStateChanged(loc, EventArgs.Empty);
                    }
                }
            }
            onTimeChanged();
            worker.PostDelayedAction(TimeSpan.FromSeconds(2), () => {
                if (this.client != client)
                {
                    return;
                }
                onTimer();
            });
        }

        private void closeMqttClient()
        {
            if (client != null)
            {
                client.MqttMsgPublishReceived -= onMqttMsgPublishReceived;
                client.MqttMsgPublished -= onMqttMsgPublished;
                client.ConnectionClosed -= onMqttConnectionClosed;
                if (client.IsConnected)
                {
                    try
                    {
                        client.Disconnect();
                    } catch
                    {
                        // Ignore
                    }
                }
                client = null;
            }
        }

        public void Dispose()
        {
            closeMqttClient();
            worker.Dispose();
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using BinkyRailways.Core.Logging;
using BinkyRailways.Core.State;
using Fleck;
using NLog;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BinkyRailways.Core.Server
{
    /// <summary>
    /// Process websocket messages.
    /// </summary>
    internal class WebSocketProcessor : IDisposable
    {
        private static readonly Logger log = LogManager.GetLogger(LogNames.WebServer);
        private readonly List<IWebSocketConnection> connections = new List<IWebSocketConnection>();
        private readonly object connectionsLock = new object();
        private readonly IRailwayState railwayState;

        public WebSocketProcessor(IRailwayState railwayState)
        {
            this.railwayState = railwayState;
        }

        /// <summary>
        /// Process socket open events
        /// </summary>
        internal void OnOpen(IWebSocketConnection socket)
        {
            lock (connectionsLock)
            {
                connections.Add(socket);
            }
        }

        /// <summary>
        /// Process socket close events
        /// </summary>
        internal void OnClose(IWebSocketConnection socket)
        {
            lock (connectionsLock)
            {
                connections.Remove(socket);
            }            
        }

        /// <summary>
        /// Process string messages.
        /// </summary>
        internal void OnStringMessage(IWebSocketConnection socket, string message)
        {
            try
            {
                var json = JObject.Parse(message);
                var msg = json["msg"].Value<string>();
                switch (msg)
                {
                    case Messages.GetLocList:
                        SendLocList(socket);
                        break;         
                    case Messages.SetLocAuto:
                        var data = json["data"];
                        var id = data.Value<string>("id");
                        var value = data.Value<bool>("value");
                        var locState = railwayState.LocStates.FirstOrDefault(x => x.EntityId == id);
                        if (locState != null)
                            locState.ControlledAutomatically.Requested = value;
                        break;
                }
            }
            catch (Exception ex)
            {
                // Ignore invalid messages for now
                log.ErrorException("Invalid websocket message: " + message, ex);
            }
        }

        /// <summary>
        /// Process binary messages.
        /// </summary>
        internal void OnBinaryMessage(IWebSocketConnection socket, byte[] message)
        {
        }

        /// <summary>
        /// Send a list of all locs to the given socket.
        /// </summary>
        internal void SendLocList(IWebSocketConnection socket)
        {
            var list = railwayState.LocStates.Select(x => new Messages.LocState(x)).ToList();
            SendMessage(socket, Messages.LocList, list);
        }

        /// <summary>
        /// Send the given message to all sockets.
        /// </summary>
        internal void Send2All(string message, object data)
        {
            var json = CreateMessage(message, data);
            ForEach(s => s.Send(json));
        }

        /// <summary>
        /// Close all connections.
        /// </summary>
        internal void CloseAll()
        {
            ForEach(x => x.Close());
        }

        /// <summary>
        /// Call the given action for each socket connection.
        /// </summary>
        private void ForEach(Action<IWebSocketConnection> action)
        {
            List<IWebSocketConnection> all;
            lock (connectionsLock)
            {
                all = connections.ToList();
            }
            all.ForEach(action);
        }

        /// <summary>
        /// Send the given message to all sockets.
        /// </summary>
        private void SendMessage(IWebSocketConnection socket, string message, object data)
        {
            var json = CreateMessage(message, data);
            socket.Send(json);
        }

        /// <summary>
        /// Create a json object for the given message.
        /// </summary>
        private string CreateMessage(string message, object data)
        {
            object jData = (data is IList) ? (object)JArray.FromObject(data) : JObject.FromObject(data);
            var msg = new JObject(
                new JProperty("msg", message),
                new JProperty("data", jData));
            return msg.ToString(Formatting.None);            
        }

        /// <summary>
        /// Cleanup
        /// </summary>
        public void Dispose()
        {
            CloseAll();
        }
    }
}

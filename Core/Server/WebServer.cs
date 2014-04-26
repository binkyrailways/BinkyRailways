using System;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Threading;
using BinkyRailways.Core.Logging;
using BinkyRailways.Core.Server.Http;
using BinkyRailways.Core.State;
using BinkyRailways.Core.Util;
using Fleck;
using NLog;

namespace BinkyRailways.Core.Server
{
    /// <summary>
    /// State webserver.
    /// </summary>
    public sealed class WebServer : IDisposable
    {
        private static readonly Logger log = LogManager.GetLogger(LogNames.WebServer);
        private static WebServer current;

        private readonly int httpPort;
        private readonly int webSocketPort;
        private readonly IRailwayState state;
        private readonly HttpServer httpServer;
        private readonly WebSocketServer webSocketServer;
        private StateListener stateListener;
        private readonly WebSocketProcessor webSocketProcessor;
        private readonly HttpProcessor httpProcessor;

        /// <summary>
        /// Fired when a HTTP request is received.
        /// </summary>
        public event EventHandler HttpIndexRequest;

        /// <summary>
        /// Default ctor
        /// </summary>
        private WebServer(IRailwayState state, IWebServerPersistence persistence)
        {
            this.state = state;
            GetHttpPort(persistence, out httpPort, out webSocketPort);
            httpServer = new HttpServer(httpPort, IPAddress.Any);
            webSocketServer = new WebSocketServer(webSocketPort, WebSocketUrl) { SupportedSubProtocols = new[] { "binky" }};
            webSocketProcessor = new WebSocketProcessor(state);
            httpProcessor = new HttpProcessor(this);
        }

        /// <summary>
        /// Get or initialize a port number for the HTTP server.
        /// </summary>
        private static void GetHttpPort(IWebServerPersistence persistence, out int httpPort, out int webSocketPort)
        {
            int port;
            if (persistence.TryGetHttpPort(out port) && IsAvailablePort(port) && IsAvailablePort(port + 1))
            {
                httpPort = port;
                webSocketPort = port + 1;
                return;
            }
            while (true)
            {
                port = 52000 + (new Random(Environment.TickCount).Next(1024) * 2);
                if (!IsAvailablePort(port) || !IsAvailablePort(port + 1))
                    continue;
                persistence.SaveHttpPort(port);
                httpPort = port;
                webSocketPort = port + 1;
                return;
            }
        }

        /// <summary>
        /// Is the given TCP port available?
        /// </summary>
        private static bool IsAvailablePort(int port)
        {
            var ipGlobalProperties = IPGlobalProperties.GetIPGlobalProperties();
            return ipGlobalProperties.GetActiveTcpListeners().All(x => x.Port != port);
        }

        /// <summary>
        /// Gets the root URL of the HTTP server.
        /// </summary>
        public string HttpUrl
        {
            get { return string.Format("http://{0}:{1}/", IpUtils.GetIpAddress(), httpPort); }
        }

        /// <summary>
        /// Gets the URL of the websocket server.
        /// </summary>
        public string WebSocketUrl
        {
            get { return string.Format("ws://{0}:{1}/", IpUtils.GetIpAddress(), webSocketPort); }
        }

        /// <summary>
        /// Start the server.
        /// </summary>
        private void Start()
        {
            try
            {

                webSocketServer.Start(socket => {
                    socket.OnOpen = () => webSocketProcessor.OnOpen(socket);
                    socket.OnClose = () => webSocketProcessor.OnClose(socket);
                    socket.OnMessage = msg => webSocketProcessor.OnStringMessage(socket, msg);
                    socket.OnBinary = msg => webSocketProcessor.OnBinaryMessage(socket, msg);
                });

                httpServer.Start(conn => {
                    if (conn.Url == "/")
                    {
                        HttpIndexRequest.Fire(this);
                    }
                    httpProcessor.OnGet(conn);
                }, httpProcessor.OnPost);

                stateListener  = new StateListener(state, webSocketProcessor);
            }
            catch (Exception ex)
            {
                log.ErrorException("Failed to start HttpListener", ex);
            }
        }

        /// <summary>
        /// Start a new webserver for the given railway state.
        /// </summary>
        public static WebServer StartNew(IRailwayState state, IWebServerPersistence persistence)
        {
            var newServer = new WebServer(state, persistence);
            var old = Interlocked.Exchange(ref current, newServer);
            if (old != null)
            {
                old.Dispose();                
            }
            newServer.Start();
            return newServer;
        }

        /// <summary>
        /// Close
        /// </summary>
        public void Dispose()
        {
            IDisposable d;

            d = webSocketProcessor;
            if (d != null) d.Dispose();

            d = stateListener;
            stateListener = null;
            if (d != null) d.Dispose();            

            httpServer.Dispose();
            webSocketServer.Dispose();
        }
    }
}

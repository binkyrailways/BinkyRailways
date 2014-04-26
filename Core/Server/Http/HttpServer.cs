using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace BinkyRailways.Core.Server.Http
{
    public class HttpServer : IDisposable
    {
        private readonly int port;
        private readonly IPAddress localAddress;
        private TcpListener listener;
        private Action<HttpConnection> onGet;
        private Action<HttpConnection, StreamReader> onPost;
        private bool stop;

        /// <summary>
        /// Default ctor
        /// </summary>
        public HttpServer(int port, IPAddress localAddress)
        {
            this.port = port;
            this.localAddress = localAddress;
        }

        /// <summary>
        /// Start this server.
        /// </summary>
        public void Start(Action<HttpConnection> onGet, Action<HttpConnection, StreamReader> onPost)
        {
            this.onGet = onGet;
            this.onPost = onPost;
            stop = false;
            var thread = new Thread(Listen) { IsBackground = true };
            thread.Start();
        }

        /// <summary>
        /// Stop accepting requests.
        /// </summary>
        public void Stop()
        {
            stop = true;
        }

        /// <summary>
        /// Listen for connections and handle them.
        /// </summary>
        private void Listen()
        {
            try
            {
                listener = new TcpListener(localAddress, port);
                listener.Start();
                while (!stop)
                {
                    var l = listener;
                    if (l == null)
                        break;
                    try
                    {
                        var s = l.AcceptTcpClient();
                        var processor = new HttpConnection(s, this);
                        ThreadPool.QueueUserWorkItem(x => processor.Process());
                    }
                    catch (Exception ex)
                    {
                        if (!stop)
                        {
                            // Handle error
                        }
                    }
                }
            }
            finally
            {
                try
                {
                    var l = listener;
                    if (l != null)
                        l.Stop();
                }
                catch (Exception ex)
                {
                    // Ignore
                }
                listener = null;
            }
        }

        /// <summary>
        /// Handle GET requests.
        /// </summary>
        internal void HandleGetRequest(HttpConnection p)
        {
            if (onGet != null) onGet(p);
        }

        /// <summary>
        /// Handle POST requests.
        /// </summary>
        internal void HandlePostRequest(HttpConnection p, StreamReader inputData)
        {
            if (onPost != null) onPost(p, inputData);
        }

        /// <summary>
        /// Cleanup
        /// </summary>
        public void Dispose()
        {
            Stop();
            var current = listener;
            listener = null;

            if (current != null)
            {
                try
                {
                    current.Stop();
                }
                catch (Exception ex)
                {
                    // Ignore
                }
            }
        }
    }
}
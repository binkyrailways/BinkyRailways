using System;
using System.Diagnostics;
using System.IO;
using BinkyRailways.Core.Logging;
using BinkyRailways.Core.Server.Http;
using BinkyRailways.Core.Server.Site;
using NLog;

namespace BinkyRailways.Core.Server
{
    /// <summary>
    /// Handle all HTTP requests.
    /// </summary>
    internal class HttpProcessor
    {
        private readonly WebServer webServer;
        private static readonly Logger log = LogManager.GetLogger(LogNames.WebServer);

        /// <summary>
        /// Default ctor
        /// </summary>
        internal HttpProcessor(WebServer webServer)
        {
            this.webServer = webServer;
        }

        /// <summary>
        /// Handle GET requests.
        /// </summary>
        internal void OnGet(HttpConnection connection)
        {
            var sw = Stopwatch.StartNew();
            HttpResource<WebServer> resource;
            if (Resources.Files.TryGetValue(connection.Url.ToLowerInvariant(), out resource))
            {
                resource.Send(connection, webServer);
                Console.WriteLine("GET: " + connection.Url + " took " + sw.ElapsedMilliseconds + "ms");
            }
            else
            {
                connection.WriteFailure();
            }
        }

        /// <summary>
        /// Handle POST requests.
        /// </summary>
        internal void OnPost(HttpConnection connection, StreamReader input)
        {
            connection.WriteFailure();
        }
    }
}

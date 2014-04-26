using System.Collections.Generic;
using BinkyRailways.Core.Server.Http;

namespace BinkyRailways.Core.Server.Site
{
    internal static class Resources
    {
        private const string ContentTypeHtml = "text/html";

        internal static readonly Dictionary<string, HttpResource<WebServer>> Files = new Dictionary<string, HttpResource<WebServer>>() {
            // All URL's must be lower case.
            { "/", TemplateFile("Index.htm", ContentTypeHtml, CacheControl.Public) },
            { "/style.css", File("Style.css", "text/css", CacheControl.Public) },
            { "/bootstrap/css/bootstrap.min.css", File("bootstrap.css.bootstrap.min.css", "text/css", CacheControl.Public) },
            { "/bootstrap/js/bootstrap.min.js", File("bootstrap.js.bootstrap.min.js", "text/javascript", CacheControl.Public) },
            { "/jquery/jquery.min.js", File("jquery.jquery.min.js", "text/javascript", CacheControl.Public) },
        };

        /// <summary>
        /// Create a full resource name.
        /// </summary>
        private static string ResName(string relativeName)
        {
            return typeof (Resources).Namespace + "." + relativeName;
        }

        /// <summary>
        /// Create a file resource.
        /// </summary>
        private static HttpResource<WebServer> File(string relativeName, string contentType, CacheControl cacheControl)
        {
            return new HttpResource<WebServer>(ResName(relativeName), contentType, cacheControl);
        }

        /// <summary>
        /// Create a file resource.
        /// </summary>
        private static HttpTemplateResource<WebServer> TemplateFile(string relativeName, string contentType, CacheControl cacheControl)
        {
            return new HttpTemplateResource<WebServer>(ResName(relativeName), ProcessTemplate, contentType, cacheControl);
        }

        /// <summary>
        /// Replace variables in the given template.
        /// </summary>
        private static string ProcessTemplate(string template, WebServer webServer)
        {
            template = template.Replace("@@WSURL@@", webServer.WebSocketUrl);
            return template;
        }
    }
}

using System;
using System.IO;

namespace BinkyRailways.Core.Server.Http
{
    /// <summary>
    /// Resource that allowed for variable replacement.
    /// </summary>
    public class HttpTemplateResource<TData> : HttpResource<TData>
    {
        private readonly Func<string, TData, string> templateProcessor;

        /// <summary>
        /// Default ctor
        /// </summary>
        public HttpTemplateResource(string resourceName, Func<string, TData, string> templateProcessor, string contentType, CacheControl cacheControl)
            : base(resourceName, contentType, cacheControl)
        {
            this.templateProcessor = templateProcessor;
        }

        /// <summary>
        /// Send the content of this resource to the given connection.
        /// </summary>
        protected override void SendContent(HttpConnection connection, Stream resource, TData data)
        {
            var reader = new StreamReader(resource);
            var content = reader.ReadToEnd();
            var processedContent = templateProcessor(content, data);
            connection.Output.Write(processedContent);
        }
    }
}

using System;
using System.IO;
using System.Resources;

namespace BinkyRailways.Core.Server.Http
{
    /// <summary>
    /// Wrap a resource that can be served from a HTTP server.
    /// </summary>
    public class HttpResource<TData>
    {
        private readonly string resourceName;
        private readonly string contentType;
        private byte[] content;
        private DateTime lastModified;
        private readonly CacheControl cacheControl;

        /// <summary>
        /// Default ctor
        /// </summary>
        public HttpResource(string resourceName, string contentType, CacheControl cacheControl)
        {
            this.resourceName = resourceName;
            this.contentType = contentType;
            this.cacheControl = cacheControl;
        }

        /// <summary>
        /// MIME type.
        /// </summary>
        protected string ContentType { get { return contentType; } }

        /// <summary>
        /// Send this resource to the given connection.
        /// </summary>
        public void Send(HttpConnection connection, TData data)
        {
            Stream resource;
            try
            {
                resource = LoadContent();
            }
            catch (Exception ex)
            {
                connection.WriteFailure();
                return;
            }
            connection.WriteSuccess(contentType, GetLastModified(), cacheControl);
            connection.Flush();
            SendContent(connection, resource, data);
        }

        /// <summary>
        /// Load the content of the resource.
        /// </summary>
        private MemoryStream LoadContent()
        {
            if (content == null)
            {
                using (var src = GetType().Assembly.GetManifestResourceStream(resourceName))
                {
                    if (src == null)
                        throw new MissingManifestResourceException(resourceName);
                    var memStream = new MemoryStream((int) src.Length);
                    src.CopyTo(memStream);
                    memStream.Position = 0;
                    content = memStream.ToArray();
                }
            }
            return new MemoryStream(content);
        }

        /// <summary>
        /// Gets the last modification date.
        /// </summary>
        protected virtual DateTime GetLastModified()
        {
            if (lastModified == DateTime.MinValue)
            {
                lastModified = File.GetLastWriteTime(GetType().Assembly.Location);
            }
            return lastModified;
        }

        /// <summary>
        /// Send the content of this resource to the given connection.
        /// </summary>
        protected virtual void SendContent(HttpConnection connection, Stream resource, TData data)
        {
            var length = resource.Length;
            var bufferSize = (length > 64 * 1024) ? 64 * 1024 : 4096;
            resource.CopyTo(connection.Output.BaseStream, bufferSize);
        }
    }
}

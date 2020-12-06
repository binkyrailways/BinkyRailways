using System.IO;
using BinkyRailways.Core.Util;

namespace BinkyRailways.Core.Model.Impl
{
    internal sealed class StreamProperty
    {
        private IPackage package;
        private byte[] data;
        private string id;
        private IPersistentEntity entity;

        /// <summary>
        /// Gets the stream
        /// </summary>
        public Stream GetStream(string id)
        {
            if (data != null)
                return new MemoryStream(data);
            if ((package != null) && (entity != null))
                return package.GetGenericPart(entity, id);
            return null;
        }

        /// <summary>
        /// Set a stream value.
        /// </summary>
        public void SetStream(Stream stream, string id)
        {
            if ((package != null) && (entity != null))
            {
                // We're connected
                if (stream != null)
                {
                    package.SetGenericPart(entity, id, stream);
                }
                else
                {
                    package.RemoveGenericPart(entity, id);
                }
                data = null;
                this.id = null;
            }
            else
            {
                // We're not connected, cache it
                data = (stream != null) ? stream.ToArray() : null;
                this.id = (stream != null) ? id : null;
            }
        }

        /// <summary>
        /// Connect this property to its context
        /// </summary>
        public void SetContext(IPackage package, IPersistentEntity entity)
        {
            this.package = package;
            this.entity = entity;
            if ((package != null) && (entity != null))
            {
                if (data != null)
                {
                    // We have cached data, store it
                    package.SetGenericPart(entity, id, new MemoryStream(data));
                    data = null;
                    id = null;
                }
            }
        }
    }
}

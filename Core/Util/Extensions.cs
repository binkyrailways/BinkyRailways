using System.IO;

namespace BinkyRailways.Core.Util
{
    public static class Extensions
    {
        /// <summary>
        /// Load the data in the stream into a byte array.
        /// </summary>
        public static byte[] ToArray(this Stream stream)
        {
            if (stream == null)
                return null;
            var memStream = stream as MemoryStream;
            if (memStream != null)
                return memStream.ToArray();
            var bytes = new byte[stream.Length];
            stream.Read(bytes, 0, bytes.Length);
            return bytes;
        }
    }
}

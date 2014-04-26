using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace BinkyRailways.Core.Storage
{
    /// <summary>
    /// Single file containing zero or more persistent entities.
    /// </summary>
    partial class Package
    {
        private const string RootElementName = "BinkyRailwaysPackage";
        private const string EntityElementName = "Entity";
        private const string BlobElementName = "Blob";
        private const string KeyAttrName = "Key";

        /// <summary>
        /// Export this package to XML.
        /// </summary>
        public void Export(string path)
        {
            // Load everything first
            LoadAllEntities();

            var root = new XElement(RootElementName);

            // Add all loaded entities
            foreach (var entry in loadedEntities)
            {
                var container = new XElement(EntityElementName);
                container.SetAttributeValue(KeyAttrName, entry.Key);
                container.Add(entry.Value.Export());
                root.Add(container);
            }
            // Now all everything that was not de-serialized
            foreach (var entry in parts)
            {
                if (!loadedEntities.ContainsKey(entry.Key))
                {
                    var blob = new XElement(BlobElementName);
                    blob.SetAttributeValue(KeyAttrName, entry.Key);
                    blob.Add(Hex(entry.Value));
                    root.Add(blob);
                }
            }

            // Save the xml
            File.Delete(path);
            root.Save(path, SaveOptions.None);
        }

        /// <summary>
        /// Import a package from XML.
        /// </summary>
        public static Package Import(string path)
        {
            var root = XElement.Load(path);
            if (root.Name.LocalName != RootElementName)
                throw new ArgumentException("Invalid root element: " + root.Name);

            var parts = new Dictionary<Uri, byte[]>();

            // Load all entities
            foreach (var container in root.Elements(EntityElementName))
            {
                var uri = new Uri(container.Attribute(KeyAttrName).Value, UriKind.Relative);
                var entityContainer = container.Elements().First();
                var settings = new XmlWriterSettings();
                settings.Encoding = Encoding.Unicode;
                var stream = new MemoryStream();
                using (var writer = XmlWriter.Create(stream, settings))
                {
                    entityContainer.WriteTo(writer);
                    writer.Flush();
                    parts[uri] = stream.ToArray();
                }
            }
            // Now all blobs
            foreach (var container in root.Elements(BlobElementName))
            {
                var uri = new Uri(container.Attribute(KeyAttrName).Value, UriKind.Relative);
                parts[uri] = FromHex(container.Value);
            }

            var result = new Package(parts);
            result.LoadAllEntities();
            return result;
        }

        /// <summary>
        /// Create a HEX string from the given bytes.
        /// </summary>
        private static byte[] FromHex(string data)
        {
            var bytes = new List<byte>();
            for (int i = 0; i < data.Length; i += 2)
            {
                var hexStr = data.Substring(i, 2);
                bytes.Add(byte.Parse(hexStr, NumberStyles.HexNumber));
            }
            return bytes.ToArray();
        }
    }
}

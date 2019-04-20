using System;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace BinkyRailways.Core.Model.Impl
{
    /// <summary>
    /// Wrapper used to make serialization of all kinds of entities easier.
    /// </summary>
    [XmlRoot]
    public sealed class PersistentEntityContainer
    {
        private string loadedVersion;

        /// <summary>
        /// Wraps the entity
        /// </summary>
        public PersistentEntity Entity { get; set; }

        /// <summary>
        /// Version used to save this entity with.
        /// </summary>
        public string Version
        {
            get { return GetType().Assembly.GetName().Version.ToString(); }
            set { loadedVersion = value; }
        }

        /// <summary>
        /// Gets the version that was loaded from the package.
        /// </summary>
        [XmlIgnore]
        public string LoadedVersion
        {
            get { return loadedVersion; }
        }

        /// <summary>
        /// Gets the version element of a saved XML stream for this type.
        /// </summary>
        internal static bool TryGetVersion(string xml, out Version version)
        {
            version = new Version(0, 0, 0, 0);
            try
            {
                var element = XElement.Parse(xml);
                var versionElement = element.Element("Version");
                if (versionElement == null)
                    return false;
                version = new Version(versionElement.Value);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}

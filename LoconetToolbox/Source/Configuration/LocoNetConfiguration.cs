using System;
using System.IO;
using System.Xml.Linq;

namespace LocoNetToolBox.Configuration
{
    /// <summary>
    /// Entire configuration of a loconet with all it's devices.
    /// The configuration can be stored on disk.
    /// </summary>
    public class LocoNetConfiguration
    {
        private const string RootName = "LocoNetConfiguration";
        private const string VersionName = "version";

        private readonly LocoIOConfigMap locoIOs = new LocoIOConfigMap();
        private bool dirty;

        /// <summary>
        /// Default ctor
        /// </summary>
        public LocoNetConfiguration()
        {
            locoIOs.Modified += (_, x) => SetDirty();
        }

        /// <summary>
        /// XML ctor
        /// </summary>
        public LocoNetConfiguration(XElement element)
        {
            if (element.Name.LocalName != RootName)
                throw new ArgumentException("Invalid root element: " + element.Name);
            foreach (var x in element.Elements())
            {
                ConfigurationItemLoader.Load(x, this);
            }
        }

        /// <summary>
        /// Gets all configured loco IO's
        /// </summary>
        public LocoIOConfigMap LocoIOs
        {
            get { return locoIOs; }
        }

        /// <summary>
        /// Path of config on disk (can be null).
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// Is my state different then the state on disk?
        /// </summary>
        public bool Dirty
        {
            get { return dirty; }
        }

        /// <summary>
        /// Mark this configuration dirty (my state is different from the state on disk).
        /// </summary>
        public void SetDirty()
        {
            dirty = true;
        }

        /// <summary>
        /// Save myself to XML
        /// </summary>
        public XElement ToXml()
        {
            var element = new XElement(RootName);
            element.SetAttributeValue(VersionName, GetType().Assembly.GetName().Version);
            foreach (var config in locoIOs)
            {
                element.Add(config.ToXml());
            }
            return element;
        }

        /// <summary>
        /// Save to disk.
        /// </summary>
        public void Save(string path)
        {
            var folder = System.IO.Path.GetDirectoryName(path);
            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);
            var xml = ToXml();
            xml.Save(path, SaveOptions.None);
            Path = path;
            dirty = false;
        }

        /// <summary>
        /// Load a configuration from file.
        /// </summary>
        public static LocoNetConfiguration Load(string path)
        {
            var doc = XDocument.Load(path);
            return new LocoNetConfiguration(doc.Root) { Path = path };
        }
    }
}

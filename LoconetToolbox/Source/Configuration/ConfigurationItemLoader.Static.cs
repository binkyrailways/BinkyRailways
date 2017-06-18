using System.Xml.Linq;
using LocoNetToolBox.Devices.LocoIO;

namespace LocoNetToolBox.Configuration
{
    partial class ConfigurationItemLoader
    {
        /// <summary>
        /// All configuration item loaders.
        /// </summary>
        private static readonly ConfigurationItemLoader[] Loaders = new[] {
            new LocoIOConfigLoader(),
        };

        /// <summary>
        /// Load the given element using my loaders.
        /// </summary>
        internal static void Load(XElement element, LocoNetConfiguration configuration)
        {
            foreach (var loader in Loaders)
            {
                if (loader.TryLoad(element, configuration))
                    return;
            }
        }
    }
}

using System.Xml.Linq;
using LocoNetToolBox.Configuration;

namespace LocoNetToolBox.Devices.LocoIO
{
    /// <summary>
    /// Helper used to load elements from an XML configuration.
    /// </summary>
    public sealed class LocoIOConfigLoader : ConfigurationItemLoader
    {
        internal const string ElementName = "locoio";

        /// <summary>
        /// Try to load an item from the given element into the given configuration.
        /// </summary>
        /// <returns>True on success, false otherwise. When false, subsequent loaders are called.</returns>
        public override bool TryLoad(XElement element, LocoNetConfiguration configuration)
        {
            if (element.Name.LocalName != ElementName)
                return false;
            var config = new LocoIOConfig(element);

            return true;
        }
    }
}

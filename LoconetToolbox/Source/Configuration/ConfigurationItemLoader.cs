using System.Xml.Linq;

namespace LocoNetToolBox.Configuration
{
    /// <summary>
    /// Helper used to load elements from an XML configuration.
    /// </summary>
    public abstract partial class ConfigurationItemLoader
    {
        /// <summary>
        /// Try to load an item from the given element into the given configuration.
        /// </summary>
        /// <returns>True on success, false otherwise. When false, subsequent loaders are called.</returns>
        public abstract bool TryLoad(XElement element, LocoNetConfiguration configuration);
    }
}

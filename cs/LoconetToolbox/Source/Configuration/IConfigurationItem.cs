using System.Xml.Linq;

namespace LocoNetToolBox.Configuration
{
    /// <summary>
    /// Element of the loconet configuration.
    /// </summary>
    public interface IConfigurationItem
    {
        /// <summary>
        /// Save the configuration of this item in an XML element.
        /// </summary>
        XElement ToXml();
    }
}

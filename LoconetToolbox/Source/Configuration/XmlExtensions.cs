using System.Xml.Linq;

namespace LocoNetToolBox.Configuration
{
    /// <summary>
    /// XML extensions methods
    /// </summary>
    internal static class XmlExtensions
    {
        /// <summary>
        /// Try to get an integer attribute from the given element.
        /// </summary>
        internal static int GetIntAttribute(this XElement element, string attributeName, int defaultValue)
        {
            var attr = element.Attribute(attributeName);
            if (attr == null)
                return defaultValue;
            int result;
            if (!int.TryParse(attr.Value, out result))
                return defaultValue;
            return result;
        }
    }
}

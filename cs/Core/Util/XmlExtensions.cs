using System.Xml.Linq;

namespace BinkyRailways.Core.Util
{
    public static class XmlExtensions
    {
        /// <summary>
        /// Create a new XElement child element with the given name and add it to the given container.
        /// </summary>
        /// <returns>The added child element</returns>
        public static XElement AddElement(this XContainer container, XName name)
        {
            var child = new XElement(name);
            container.Add(child);
            return child;
        }

        /// <summary>
        /// Create a new XElement child element with the given name and content, and add it to the given container.
        /// </summary>
        /// <returns>The added child element</returns>
        public static XElement AddElement(this XContainer container, XName name, object content)
        {
            var child = new XElement(name, content);
            container.Add(child);
            return child;
        }

        /// <summary>
        /// Gets the value of an attribute.
        /// </summary>
        public static string GetAttributeValue(this XElement container, XName name, string defaultValue = null)
        {
            var attr = container.Attribute(name);
            if (attr == null)
                return defaultValue;
            return attr.Value;
        }

        /// <summary>
        /// Gets the value of an attribute.
        /// </summary>
        public static int GetIntAttributeValue(this XElement container, XName name, int defaultValue = -1)
        {
            int result;
            var s = GetAttributeValue(container, name, defaultValue.ToString());
            if (!int.TryParse(s, out result))
                return defaultValue;
            return result;
        }
    }
}

using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
#if !NOCONFIG
using LocoNetToolBox.Configuration;
#endif
using LocoNetToolBox.Protocol;

namespace LocoNetToolBox.Devices.LocoIO
{
    /// <summary>
    /// Configuration of an entire LocoIO.
    /// </summary>
    public class LocoIOConfig : IEnumerable<SVConfig>
#if !NOCONFIG
        , IConfigurationItem
#endif
    {
        private const string SvElementName = "sv";
        private const string SvIndexName = "index";
        private const string SvValueName = "value";

        private readonly SVConfig sv0;
        private readonly SVConfig sv1;
        private readonly SVConfig sv2;
        private PinConfigList pins;
        private ConnectorConfig[] connectors;

        /// <summary>
        /// Default ctor
        /// </summary>
        public LocoIOConfig()
        {
            var pins = new PinConfig[16];
            for (int i = 0; i < 16; i++)
            {
                pins[i] = new PinConfig(i + 1);
            }
            sv0 = new SVConfig(0);
            sv1 = new SVConfig(1);
            sv2 = new SVConfig(2);
            this.pins = new PinConfigList(pins);
            connectors = new ConnectorConfig[2];
            connectors[0] = new ConnectorConfig(pins.Take(8).ToArray());
            connectors[1] = new ConnectorConfig(pins.Skip(8).ToArray());
        }

#if !NOCONFIG
        /// <summary>
        /// XML ctor
        /// </summary>
        internal LocoIOConfig(XElement element) : this()
        {
            foreach (var svElement in element.Elements(SvElementName))
            {
                var index = svElement.GetIntAttribute(SvIndexName, -1);
                var value = svElement.GetIntAttribute(SvValueName, 0);
                var sv = this.FirstOrDefault(x => x.Index == index);
                if (sv != null)
                {
                    sv.Value = (byte)value;
                }
            }
        }

        /// <summary>
        /// Save the configuration of this item in an XML element.
        /// </summary>
        public XElement ToXml()
        {
            var element = new XElement(LocoIOConfigLoader.ElementName);
            foreach (var sv in this)
            {
                var svElement = new XElement(SvElementName);
                svElement.SetAttributeValue(SvIndexName, sv.Index.ToString());
                svElement.SetAttributeValue(SvValueName, sv.Value.ToString());
                element.Add(svElement);
            }
            return element;
        }
#endif

        /// <summary>
        /// Gets the address of this config.
        /// </summary>
        public LocoNetAddress Address
        {
            get { return new LocoNetAddress(sv1.Value, sv2.Value); }
            set 
            {
                sv1.Value = value.Address;
                sv2.Value = value.SubAddress;
            }
        }

        /// <summary>
        /// Gets all pins
        /// </summary>
        public PinConfigList Pins
        {
            get { return pins; }
        }

        /// <summary>
        /// Gets the first connector
        /// </summary>
        public ConnectorConfig ConnectorA
        {
            get { return connectors[0]; }
        }

        /// <summary>
        /// Gets the second connector
        /// </summary>
        public ConnectorConfig ConnectorB
        {
            get { return connectors[1]; }
        }

        /// <summary>
        /// Gets all SV's of this LocoIO
        /// </summary>
        public IEnumerator<SVConfig> GetEnumerator()
        {
            yield return sv0;
            yield return sv1;
            yield return sv2;
            foreach (var pin in pins)
            {
                foreach (var sv in pin)
                {
                    yield return sv;
                }
            }
        }

        /// <summary>
        /// Gets all SV's of this LocoIO
        /// </summary>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}

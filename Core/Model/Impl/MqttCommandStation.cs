using System.Collections.Generic;
using System.Xml.Serialization;

namespace BinkyRailways.Core.Model.Impl
{
    /// <summary>
    /// MQTT command station
    /// </summary>
    [XmlRoot]
    public sealed class MqttCommandStation : CommandStation, IMqttCommandStation
    {
        private readonly Property<string> hostName;
        private readonly Property<int> port;
        private readonly Property<string> topicPrefix;

        /// <summary>
        /// Default ctor
        /// </summary>
        public MqttCommandStation()
        {
            hostName = new Property<string>(this, "mqtt");
            port = new Property<int>(this, 1883);
            topicPrefix = new Property<string>(this, "");
        }

        /// <summary>
        /// What types of addresses does this command station support?
        /// </summary>
        public override IEnumerable<AddressType> GetSupportedAddressTypes(IAddressEntity entity)
        {
            if (entity is ILoc)
            {
                yield return AddressType.Mqtt;
                yield return AddressType.Dcc;
            }
            else
            {
                yield return AddressType.Mqtt;
            }
        }

        /// <summary>
        /// What types of addresses does this command station support?
        /// </summary>
        public override IEnumerable<AddressType> GetSupportedAddressTypes()
        {
            yield return AddressType.Mqtt;
        }

        /// <summary>
        /// Network hostname of the command station
        /// </summary>
        public string HostName
        {
            get { return hostName.Value; }
            set { hostName.Value = value ?? string.Empty; }
        }


        /// <summary>
        /// Network port of the command station
        /// </summary>
        public int Port
        {
            get { return port.Value; }
            set { port.Value = value; }
        }

        /// <summary>
        /// Prefix inserted before topics.
        /// </summary>
        public string TopicPrefix
        {
            get { return topicPrefix.Value; }
            set { topicPrefix.Value = value ?? string.Empty; }
        }

        /// <summary>
        /// Accept a visit by the given visitor
        /// </summary>
        public override TReturn Accept<TReturn, TData>(EntityVisitor<TReturn, TData> visitor, TData data)
        {
            return visitor.Visit(this, data);
        }

        /// <summary>
        /// Validate the integrity of this entity.
        /// </summary>
        public override void Validate(IEntity validationRoot, ValidationResults results)
        {
            base.Validate(validationRoot, results);
            if (string.IsNullOrEmpty(HostName))
            {
                results.Warn(this, Strings.WarningNoHostnameSpecified);
            }
        }

        /// <summary>
        /// Human readable name of this type of entity.
        /// </summary>
        public override string TypeName
        {
            get { return Strings.TypeNameMqttCommandStation; }
        }
    }
}

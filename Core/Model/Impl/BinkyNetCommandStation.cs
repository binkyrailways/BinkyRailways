using BinkyNet.Apis.V1;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace BinkyRailways.Core.Model.Impl
{
    /// <summary>
    /// MQTT command station
    /// </summary>
    [XmlRoot]
    public sealed class BinkyNetCommandStation : CommandStation, IBinkyNetCommandStation
    {
        private readonly Property<string> hostName;
        private readonly Property<int> port;
        private readonly Property<string> topicPrefix;
        private readonly Property<int> apiPort;
        private readonly Property<int> discoveryPort;

        /// <summary>
        /// Default ctor
        /// </summary>
        public BinkyNetCommandStation()
        {
            hostName = new Property<string>(this, "mqtt");
            port = new Property<int>(this, 1883);
            topicPrefix = new Property<string>(this, "");
            apiPort = new Property<int>(this, 9478);
            discoveryPort = new Property<int>(this, (int)Ports.Discovery);
        }

        /// <summary>
        /// What types of addresses does this command station support?
        /// </summary>
        public override IEnumerable<AddressType> GetSupportedAddressTypes(IAddressEntity entity)
        {
            if (entity is ILoc)
            {
                yield return AddressType.BinkyNet;
                yield return AddressType.Dcc;
            }
            else
            {
                yield return AddressType.BinkyNet;
            }
        }

        /// <summary>
        /// What types of addresses does this command station support?
        /// </summary>
        public override IEnumerable<AddressType> GetSupportedAddressTypes()
        {
            yield return AddressType.BinkyNet;
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
        /// TCP Port to run GRPC API on
        /// </summary>
        public int APIPort
        {
            get { return apiPort.Value; }
            set { apiPort.Value = value; }
        }

        /// <summary>
        /// UDP Port to run Discovery broadcasts on
        /// </summary>
        public int DiscoveryPort
        {
            get { return discoveryPort.Value; }
            set { discoveryPort.Value = value; }
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

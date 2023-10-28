using System.Collections.Generic;
using System.Xml.Serialization;

namespace BinkyRailways.Core.Model.Impl
{
    /// <summary>
    /// ESU Ecos command station
    /// </summary>
    [XmlRoot]
    public sealed class EcosCommandStation : CommandStation, IEcosCommandStation
    {
        private readonly Property<string> hostName;

        /// <summary>
        /// Default ctor
        /// </summary>
        public EcosCommandStation()
        {
            hostName = new Property<string>(this, "ecos");
        }

        /// <summary>
        /// What types of addresses does this command station support?
        /// </summary>
        public override IEnumerable<AddressType> GetSupportedAddressTypes(IAddressEntity entity)
        {
            if (entity is ILoc)
            {
                yield return AddressType.Dcc;
                yield return AddressType.Motorola;
                yield return AddressType.Mfx;
            }
            else
            {
                yield return AddressType.LocoNet;
            }
        }

        /// <summary>
        /// What types of addresses does this command station support?
        /// </summary>
        public override IEnumerable<AddressType> GetSupportedAddressTypes()
        {
            yield return AddressType.Dcc;
            yield return AddressType.LocoNet;
            yield return AddressType.Mfx;
            yield return AddressType.Motorola;
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
            get { return Strings.TypeNameEcosCommandStation; }
        }
    }
}

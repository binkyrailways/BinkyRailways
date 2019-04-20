using System.Collections.Generic;
using System.Xml.Serialization;

namespace BinkyRailways.Core.Model.Impl
{
    /// <summary>
    /// P50X type command station
    /// </summary>
    [XmlRoot]
    public sealed class P50xCommandStation : CommandStation, IP50xCommandStation
    {
        private readonly Property<string> comPortName;

        /// <summary>
        /// Default ctor
        /// </summary>
        public P50xCommandStation()
        {
            comPortName = new Property<string>(this, string.Empty);
        }

        /// <summary>
        /// What types of addresses does this command station support?
        /// </summary>
        public override IEnumerable<AddressType> GetSupportedAddressTypes(IAddressEntity entity)
        {
            if (entity is ILoc)
            {
                yield return AddressType.Dcc;
            }
        }

        /// <summary>
        /// What types of addresses does this command station support?
        /// </summary>
        public override IEnumerable<AddressType> GetSupportedAddressTypes()
        {
            yield return AddressType.Dcc;
        }

        /// <summary>
        /// Name of COM port used to communicate with the P50x device.
        /// </summary>
        public string ComPortName
        {
            get { return comPortName.Value; }
            set { comPortName.Value = value ?? string.Empty; }
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
            if (string.IsNullOrEmpty(ComPortName))
            {
                results.Warn(this, Strings.WarnNoComPortNameSpecified);
            }
        }

        /// <summary>
        /// Human readable name of this type of entity.
        /// </summary>
        public override string TypeName
        {
            get { return Strings.TypeNameP50xCommandStation; }
        }
    }
}

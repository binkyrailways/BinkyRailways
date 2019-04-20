using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;
using BinkyRailways.Core.Util;

namespace BinkyRailways.Core.Model.Impl
{
    /// <summary>
    /// Device that signals some event on the railway with a state of "on" or "off".
    /// </summary>
    public sealed class BinaryOutput : Output, IBinaryOutput
    {
        private readonly Property<Address> address;

        /// <summary>
        /// Default ctor
        /// </summary>
        public BinaryOutput()
        {
            address = new Property<Address>(this, null);
        }

        /// <summary>
        /// Address of the output
        /// </summary>
        [XmlIgnore]
        public Address Address
        {
            get { return address.Value; }
            set { address.Value = value; }
        }

        /// <summary>
        /// Gets all (non-null) addresses configured in this entity.
        /// </summary>
        [XmlIgnore]
        public IEnumerable<AddressUsage> AddressUsages
        {
            get
            {
                var addr = Address;
                if (addr != null) yield return new AddressUsage(addr, AddressDirection.Output);
            }
        }

        /// <summary>
        /// Address of the output.
        /// Used for serialization.
        /// </summary>
        [XmlElement("Address")]
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string AddressString
        {
            get { return Default<AddressTypeConverter>.Instance.ConvertToString(Address); }
            set { Address = (Address)Default<AddressTypeConverter>.Instance.ConvertFromString(value); }
        }


        /// <summary>
        /// Accept a visit by the given visitor
        /// </summary>
        public override TReturn Accept<TReturn, TData>(EntityVisitor<TReturn, TData> visitor, TData data)
        {
            return visitor.Visit(this, data);
        }

        /// <summary>
        /// Human readable name of this type of entity.
        /// </summary>
        public override string TypeName
        {
            get { return Strings.TypeNameBinaryOutput; }
        }

        /// <summary>
        /// Validate the integrity of this entity.
        /// </summary>
        public override void Validate(IEntity validationRoot, ValidationResults results)
        {
            base.Validate(validationRoot, results);
            if (Address == null)
            {
                results.Warn(this, Strings.WarnNoAddressSpecified);
            }
        }
    }
}

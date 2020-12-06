using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;
using BinkyRailways.Core.Util;

namespace BinkyRailways.Core.Model.Impl
{
    /// <summary>
    /// Send clock signal in 2 bits to the track.
    /// </summary>
    public sealed class Clock4StageOutput : Output, IClock4StageOutput
    {
        private readonly Property<Address> address1;
        private readonly Property<Address> address2;
        private readonly Property<int> morningPattern;
        private readonly Property<int> afternoonPattern;
        private readonly Property<int> eveningPattern;
        private readonly Property<int> nightPattern;

        /// <summary>
        /// Default ctor
        /// </summary>
        public Clock4StageOutput()
            : base(32, 32)
        {
            address1 = new Property<Address>(this, null);
            address2 = new Property<Address>(this, null);
            morningPattern = new Property<int>(this, DefaultValues.DefaultClock4StageOutputMorningPattern);
            afternoonPattern = new Property<int>(this, DefaultValues.DefaultClock4StageOutputAfternoonPattern);
            eveningPattern = new Property<int>(this, DefaultValues.DefaultClock4StageOutputEveningPattern);
            nightPattern = new Property<int>(this, DefaultValues.DefaultClock4StageOutputNightPattern);
        }

        /// <summary>
        /// Address of first clock bit.
        /// This is an output signal.
        /// </summary>
        [XmlIgnore]
        public Address Address1
        {
            get { return address1.Value; }
            set { address1.Value = value; }
        }

        /// <summary>
        /// Address of second clock bit.
        /// This is an output signal.
        /// </summary>
        [XmlIgnore]
        public Address Address2
        {
            get { return address2.Value; }
            set { address2.Value = value; }
        }

        /// <summary>
        /// Gets all (non-null) addresses configured in this entity.
        /// </summary>
        [XmlIgnore]
        public IEnumerable<AddressUsage> AddressUsages
        {
            get
            {
                var addr = Address1;
                if (addr != null) yield return new AddressUsage(addr, AddressDirection.Output);
                addr = Address2;
                if (addr != null) yield return new AddressUsage(addr, AddressDirection.Output);
            }
        }

        /// <summary>
        /// Bit pattern used for "morning".
        /// </summary>
        [DefaultValue(DefaultValues.DefaultClock4StageOutputMorningPattern)]
        public int MorningPattern
        {
            get { return morningPattern.Value; }
            set { morningPattern.Value = value; }
        }

        /// <summary>
        /// Bit pattern used for "afternoon".
        /// </summary>
        [DefaultValue(DefaultValues.DefaultClock4StageOutputAfternoonPattern)]
        public int AfternoonPattern
        {
            get { return afternoonPattern.Value; }
            set { afternoonPattern.Value = value; }
        }

        /// <summary>
        /// Bit pattern used for "evening".
        /// </summary>
        [DefaultValue(DefaultValues.DefaultClock4StageOutputEveningPattern)]
        public int EveningPattern
        {
            get { return eveningPattern.Value; }
            set { eveningPattern.Value = value; }
        }

        /// <summary>
        /// Bit pattern used for "night".
        /// </summary>
        [DefaultValue(DefaultValues.DefaultClock4StageOutputNightPattern)]
        public int NightPattern
        {
            get { return nightPattern.Value; }
            set { nightPattern.Value = value; }
        }

        /// <summary>
        /// Serialization of <see cref="Address1"/>
        /// </summary>
        [XmlElement("Address1"), Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public string Address1String
        {
            get { return Default<AddressTypeConverter>.Instance.ConvertToString(Address1); }
            set { Address1 = (Address)Default<AddressTypeConverter>.Instance.ConvertFromString(value); }
        }

        /// <summary>
        /// Serialization of <see cref="Address2"/>
        /// </summary>
        [XmlElement("Address2"), Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public string Address2String
        {
            get { return Default<AddressTypeConverter>.Instance.ConvertToString(Address2); }
            set { Address2 = (Address)Default<AddressTypeConverter>.Instance.ConvertFromString(value); }
        }

        /// <summary>
        /// Address of the entity.
        /// This maps to <see cref="Address1"/>.
        /// </summary>
        Address IAddressEntity.Address
        {
            get { return Address1; }
            set { throw new NotImplementedException(); }
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
            if (Address1 == null)
            {
                results.Warn(this, "No address 1 specified");
            }
            if (Address2 == null)
            {
                results.Warn(this, "No address 2 specified");
            }
        }

        /// <summary>
        /// Human readable name of this type of entity.
        /// </summary>
        public override string TypeName
        {
            get { return Strings.TypeNameClock4StageOutput; }
        }
    }
}

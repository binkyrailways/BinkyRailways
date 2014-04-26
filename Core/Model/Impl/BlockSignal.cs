using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;
using BinkyRailways.Core.Util;

namespace BinkyRailways.Core.Model.Impl
{
    /// <summary>
    /// Standard block signal with up to 4 aspects: red, green, yellow, white
    /// </summary>
    public sealed class BlockSignal : Signal, IBlockSignal
    {
        private readonly Property<Address> address1;
        private readonly Property<Address> address2;
        private readonly Property<Address> address3;
        private readonly Property<Address> address4;
        private readonly Property<int> redPattern;
        private readonly Property<int> greenPattern;
        private readonly Property<int> yellowPattern;
        private readonly Property<int> whitePattern;
        private readonly Property<EntityRef<Block>> block;
        private readonly Property<BlockSide> position;
        private readonly Property<BlockSignalType> type;

        /// <summary>
        /// Default ctor
        /// </summary>
        public BlockSignal()
            : base(16, 8)
        {
            address1 = new Property<Address>(this, null);
            address2 = new Property<Address>(this, null);
            address3 = new Property<Address>(this, null);
            address4 = new Property<Address>(this, null);
            redPattern = new Property<int>(this, DefaultValues.DefaultBlockSignalRedPattern);
            greenPattern = new Property<int>(this, DefaultValues.DefaultBlockSignalGreenPattern);
            yellowPattern = new Property<int>(this, DefaultValues.DefaultBlockSignalYellowPattern);
            whitePattern = new Property<int>(this, DefaultValues.DefaultBlockSignalWhitePattern);
            block = new Property<EntityRef<Block>>(this, null);
            position = new Property<BlockSide>(this, DefaultValues.DefaultBlockSignalPosition);
            type = new Property<BlockSignalType>(this, DefaultValues.DefaultBlockSignalType);
        }

        /// <summary>
        /// Address of first position bit.
        /// This is an output signal.
        /// </summary>
        [XmlIgnore]
        public Address Address1
        {
            get { return address1.Value; }
            set { address1.Value = value; }
        }

        /// <summary>
        /// Address of second position bit.
        /// This is an output signal.
        /// </summary>
        [XmlIgnore]
        public Address Address2
        {
            get { return address2.Value; }
            set { address2.Value = value; }
        }

        /// <summary>
        /// Address of third position bit.
        /// This is an output signal.
        /// </summary>
        [XmlIgnore]
        public Address Address3
        {
            get { return address3.Value; }
            set { address3.Value = value; }
        }

        /// <summary>
        /// Address of fourth position bit.
        /// This is an output signal.
        /// </summary>
        [XmlIgnore]
        public Address Address4
        {
            get { return address4.Value; }
            set { address4.Value = value; }
        }

        /// <summary>
        /// Gets all (non-null) addresses configured in this entity.
        /// </summary>
        [XmlIgnore]
        public override IEnumerable<AddressUsage> AddressUsages
        {
            get
            {
                var addr = Address1;
                if (addr != null) yield return new AddressUsage(addr, AddressDirection.Output);
                addr = Address2;
                if (addr != null) yield return new AddressUsage(addr, AddressDirection.Output);
                addr = Address3;
                if (addr != null) yield return new AddressUsage(addr, AddressDirection.Output);
                addr = Address4;
                if (addr != null) yield return new AddressUsage(addr, AddressDirection.Output);
            }
        }

        /// <summary>
        /// Is the Red color available?
        /// </summary>
        public bool IsRedAvailable { get { return (redPattern.Value != BlockSignalPatterns.Disabled); } }

        /// <summary>
        /// Bit pattern used for color Red.
        /// </summary>
        [DefaultValue(DefaultValues.DefaultBlockSignalRedPattern)]
        public int RedPattern
        {
            get { return redPattern.Value; }
            set { redPattern.Value = value; }
        }

        /// <summary>
        /// Is the Green color available?
        /// </summary>
        public bool IsGreenAvailable { get { return (greenPattern.Value != BlockSignalPatterns.Disabled); } }

        /// <summary>
        /// Bit pattern used for color Green.
        /// </summary>
        [DefaultValue(DefaultValues.DefaultBlockSignalGreenPattern)]
        public int GreenPattern
        {
            get { return greenPattern.Value; }
            set { greenPattern.Value = value; }
        }

        /// <summary>
        /// Is the Yellow color available?
        /// </summary>
        public bool IsYellowAvailable { get { return (yellowPattern.Value != BlockSignalPatterns.Disabled); } }

        /// <summary>
        /// Bit pattern used for color Yellow.
        /// Set to 0 when Yellow is not supported.
        /// </summary>
        [DefaultValue(DefaultValues.DefaultBlockSignalYellowPattern)]
        public int YellowPattern
        {
            get { return yellowPattern.Value; }
            set { yellowPattern.Value = value; }
        }

        /// <summary>
        /// Is the White color available?
        /// </summary>
        public bool IsWhiteAvailable { get { return (whitePattern.Value != BlockSignalPatterns.Disabled); } }

        /// <summary>
        /// Bit pattern used for color White.
        /// Set to 0 when White is not supported.
        /// </summary>
        [DefaultValue(DefaultValues.DefaultBlockSignalWhitePattern)]
        public int WhitePattern
        {
            get { return whitePattern.Value; }
            set { whitePattern.Value = value; }
        }

        /// <summary>
        /// The block this signal protects.
        /// </summary>
        [XmlIgnore]
        public IBlock Block
        {
            get
            {
                Block result;
                if ((block.Value != null) && (block.Value.TryGetItem(out result)))
                    return result;
                return null;
            }
            set
            {
                if (Block != value)
                {
                    block.Value = (value != null) ? new EntityRef<Block>(this, (Block)value) : null;
                }
            }
        }

        /// <summary>
        /// Side of the block where the signal is located.
        /// </summary>
        [DefaultValue(DefaultValues.DefaultBlockSignalPosition)]
        public BlockSide Position
        {
            get { return position.Value; }
            set { position.Value = value; }
        }

        /// <summary>
        /// Type of signal
        /// </summary>
        [DefaultValue(DefaultValues.DefaultBlockSignalType)]
        public BlockSignalType Type
        {
            get { return type.Value; }
            set { type.Value = value; }
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
        /// Serialization of <see cref="Address3"/>
        /// </summary>
        [XmlElement("Address3"), Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public string Address3String
        {
            get { return Default<AddressTypeConverter>.Instance.ConvertToString(Address3); }
            set { Address3 = (Address)Default<AddressTypeConverter>.Instance.ConvertFromString(value); }
        }

        /// <summary>
        /// Serialization of <see cref="Address4"/>
        /// </summary>
        [XmlElement("Address4"), Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public string Address4String
        {
            get { return Default<AddressTypeConverter>.Instance.ConvertToString(Address4); }
            set { Address4 = (Address)Default<AddressTypeConverter>.Instance.ConvertFromString(value); }
        }

        /// <summary>
        /// Gets the id of the to block.
        /// Used for serialization only.
        /// </summary>
        [XmlElement("Block"), Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public string BlockId
        {
            get { return block.GetId(); }
            set { block.Value = string.IsNullOrEmpty(value) ? null : new EntityRef<Block>(this, value, LookupBlock); }
        }

        /// <summary>
        /// Address of the entity.
        /// This maps to <see cref="Address1"/>.
        /// </summary>
        [XmlIgnore]
        public override Address Address
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
            if ((Address3 == null) && (Address4 != null))
            {
                results.Warn(this, "No address 3 specified");
            }
        }

        /// <summary>
        /// Does this entity generate it's own description?
        /// </summary>
        public override bool HasAutomaticDescription
        {
            get { return (Block != null); }
        }

        /// <summary>
        /// Human readable description
        /// </summary>
        public override string Description
        {
            get
            {
                var b = Block;
                if (b != null)
                {
                    var positionStr = (Position == BlockSide.Front) ? "F" : "B";
                    return string.Format(Type == BlockSignalType.Entry ? "> {0}.{1}" : "{0}.{1} >", b.Description, positionStr);
                }
                return base.Description;
            }
            set { base.Description = value; }
        }

        /// <summary>
        /// Lookup a block by id. 
        /// </summary>
        private Block LookupBlock(string id)
        {
            return Module.Blocks[id];
        }
    }
}

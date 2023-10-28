using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;
using BinkyRailways.Core.Util;

namespace BinkyRailways.Core.Model.Impl
{
    /// <summary>
    /// Turntable or fiddle yard
    /// </summary>
    public sealed class TurnTable : Junction, ITurnTable
    {
        private readonly Property<Address> positionAddress1;
        private readonly Property<Address> positionAddress2;
        private readonly Property<Address> positionAddress3;
        private readonly Property<Address> positionAddress4;
        private readonly Property<Address> positionAddress5;
        private readonly Property<Address> positionAddress6;
        private readonly Property<bool> invertPositions;
        private readonly Property<Address> writeAddress;
        private readonly Property<bool> invertWrite;
        private readonly Property<Address> busyAddress;
        private readonly Property<bool> invertBusy;
        private readonly Property<int> firstPosition;
        private readonly Property<int> lastPosition;
        private readonly Property<int> initialPosition;

        /// <summary>
        /// Default ctor
        /// </summary>
        public TurnTable()
            : base(32, 32)
        {
            positionAddress1 = new Property<Address>(this, null);
            positionAddress2 = new Property<Address>(this, null);
            positionAddress3 = new Property<Address>(this, null);
            positionAddress4 = new Property<Address>(this, null);
            positionAddress5 = new Property<Address>(this, null);
            positionAddress6 = new Property<Address>(this, null);
            invertPositions = new Property<bool>(this, DefaultValues.DefaultTurnTableInvertPositions);
            writeAddress = new Property<Address>(this, null);
            invertWrite = new Property<bool>(this, DefaultValues.DefaultTurnTableInvertWrite);
            busyAddress = new Property<Address>(this, null);
            invertBusy = new Property<bool>(this, DefaultValues.DefaultTurnTableInvertBusy);
            firstPosition = new Property<int>(this, DefaultValues.DefaultTurnTableFirstPosition);
            lastPosition = new Property<int>(this, DefaultValues.DefaultTurnTableLastPosition);
            initialPosition = new Property<int>(this, DefaultValues.DefaultTurnTableInitialPosition);
        }

        /// <summary>
        /// Address of first position bit.
        /// This is an output signal.
        /// </summary>
        [XmlIgnore]
        public Address PositionAddress1
        {
            get { return positionAddress1.Value; }
            set { positionAddress1.Value = value; }
        }

        /// <summary>
        /// Address of second position bit.
        /// This is an output signal.
        /// </summary>
        [XmlIgnore]
        public Address PositionAddress2
        {
            get { return positionAddress2.Value; }
            set { positionAddress2.Value = value; }
        }

        /// <summary>
        /// Address of third position bit.
        /// This is an output signal.
        /// </summary>
        [XmlIgnore]
        public Address PositionAddress3
        {
            get { return positionAddress3.Value; }
            set { positionAddress3.Value = value; }
        }

        /// <summary>
        /// Address of fourth position bit.
        /// This is an output signal.
        /// </summary>
        [XmlIgnore]
        public Address PositionAddress4
        {
            get { return positionAddress4.Value; }
            set { positionAddress4.Value = value; }
        }

        /// <summary>
        /// Address of fifth position bit.
        /// This is an output signal.
        /// </summary>
        [XmlIgnore]
        public Address PositionAddress5
        {
            get { return positionAddress5.Value; }
            set { positionAddress5.Value = value; }
        }

        /// <summary>
        /// Address of sixed position bit.
        /// This is an output signal.
        /// </summary>
        [XmlIgnore]
        public Address PositionAddress6
        {
            get { return positionAddress6.Value; }
            set { positionAddress6.Value = value; }
        }

        /// <summary>
        /// If set, the straight/off commands used for position addresses are inverted.
        /// </summary>
        [DefaultValue(DefaultValues.DefaultTurnTableInvertPositions)]
        public bool InvertPositions
        {
            get { return invertPositions.Value; }
            set { invertPositions.Value = value; }
        }

        /// <summary>
        /// Address of the line used to indicate a "write address".
        /// This is an output signal.
        /// </summary>
        [XmlIgnore]
        public Address WriteAddress
        {
            get { return writeAddress.Value; }
            set { writeAddress.Value = value; }
        }

        /// <summary>
        /// If set, the straight/off command used for "write address" line is inverted.
        /// </summary>
        [DefaultValue(DefaultValues.DefaultTurnTableInvertWrite)]
        public bool InvertWrite
        {
            get { return invertWrite.Value; }
            set { invertWrite.Value = value; }
        }

        /// <summary>
        /// Address of the line used to indicate a "change of position in progress".
        /// This is an input signal.
        /// </summary>
        [XmlIgnore]
        public Address BusyAddress
        {
            get { return busyAddress.Value; }
            set { busyAddress.Value = value; }
        }

        /// <summary>
        /// Gets all (non-null) addresses configured in this entity.
        /// </summary>
        [XmlIgnore]
        public IEnumerable<AddressUsage> AddressUsages
        {
            get
            {
                var addr = PositionAddress1;
                if (addr != null) yield return new AddressUsage(addr, AddressDirection.Output);
                addr = PositionAddress2;
                if (addr != null) yield return new AddressUsage(addr, AddressDirection.Output);
                addr = PositionAddress3;
                if (addr != null) yield return new AddressUsage(addr, AddressDirection.Output);
                addr = PositionAddress4;
                if (addr != null) yield return new AddressUsage(addr, AddressDirection.Output);
                addr = PositionAddress5;
                if (addr != null) yield return new AddressUsage(addr, AddressDirection.Output);
                addr = PositionAddress6;
                if (addr != null) yield return new AddressUsage(addr, AddressDirection.Output);
                addr = WriteAddress;
                if (addr != null) yield return new AddressUsage(addr, AddressDirection.Output);
                addr = BusyAddress;
                if (addr != null) yield return new AddressUsage(addr, AddressDirection.Input);
            }
        }

        /// <summary>
        /// If set, the input level used for "busy" line is inverted.
        /// </summary>
        [DefaultValue(DefaultValues.DefaultTurnTableInvertBusy)]
        public bool InvertBusy
        {
            get { return invertBusy.Value; }
            set { invertBusy.Value = value; }
        }

        /// <summary>
        /// First position number. Typically 1.
        /// </summary>
        [DefaultValue(DefaultValues.DefaultTurnTableFirstPosition)]
        public int FirstPosition
        {
            get { return firstPosition.Value; }
            set { firstPosition.Value = Math.Max(0, Math.Min(63, value)); }
        }

        /// <summary>
        /// Last position number. Typically 63.
        /// </summary>
        [DefaultValue(DefaultValues.DefaultTurnTableLastPosition)]
        public int LastPosition
        {
            get { return lastPosition.Value; }
            set { lastPosition.Value = Math.Max(0, Math.Min(63, value)); }
        }

        /// <summary>
        /// Position number used to initialize the turntable with?
        /// </summary>
        [DefaultValue(DefaultValues.DefaultTurnTableInitialPosition)]
        public int InitialPosition
        {
            get { return initialPosition.Value; }
            set { initialPosition.Value = Math.Max(0, Math.Min(63, value)); }
        }

        /// <summary>
        /// Serialization of <see cref="PositionAddress1"/>
        /// </summary>
        [XmlElement("PositionAddress1"), Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public string PositionAddress1String
        {
            get { return Default<AddressTypeConverter>.Instance.ConvertToString(PositionAddress1); }
            set { PositionAddress1 = (Address)Default<AddressTypeConverter>.Instance.ConvertFromString(value); }
        }

        /// <summary>
        /// Serialization of <see cref="PositionAddress2"/>
        /// </summary>
        [XmlElement("PositionAddress2"), Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public string PositionAddress2String
        {
            get { return Default<AddressTypeConverter>.Instance.ConvertToString(PositionAddress2); }
            set { PositionAddress2 = (Address)Default<AddressTypeConverter>.Instance.ConvertFromString(value); }
        }

        /// <summary>
        /// Serialization of <see cref="PositionAddress3"/>
        /// </summary>
        [XmlElement("PositionAddress3"), Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public string PositionAddress3String
        {
            get { return Default<AddressTypeConverter>.Instance.ConvertToString(PositionAddress3); }
            set { PositionAddress3 = (Address)Default<AddressTypeConverter>.Instance.ConvertFromString(value); }
        }

        /// <summary>
        /// Serialization of <see cref="PositionAddress4"/>
        /// </summary>
        [XmlElement("PositionAddress4"), Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public string PositionAddress4String
        {
            get { return Default<AddressTypeConverter>.Instance.ConvertToString(PositionAddress4); }
            set { PositionAddress4 = (Address)Default<AddressTypeConverter>.Instance.ConvertFromString(value); }
        }

        /// <summary>
        /// Serialization of <see cref="PositionAddress5"/>
        /// </summary>
        [XmlElement("PositionAddress5"), Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public string PositionAddress5String
        {
            get { return Default<AddressTypeConverter>.Instance.ConvertToString(PositionAddress5); }
            set { PositionAddress5 = (Address)Default<AddressTypeConverter>.Instance.ConvertFromString(value); }
        }

        /// <summary>
        /// Serialization of <see cref="PositionAddress6"/>
        /// </summary>
        [XmlElement("PositionAddress6"), Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public string PositionAddress6String
        {
            get { return Default<AddressTypeConverter>.Instance.ConvertToString(PositionAddress6); }
            set { PositionAddress6 = (Address)Default<AddressTypeConverter>.Instance.ConvertFromString(value); }
        }

        /// <summary>
        /// Serialization of <see cref="WriteAddress"/>
        /// </summary>
        [XmlElement("WriteAddress"), Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public string WriteAddressString
        {
            get { return Default<AddressTypeConverter>.Instance.ConvertToString(WriteAddress); }
            set { WriteAddress = (Address)Default<AddressTypeConverter>.Instance.ConvertFromString(value); }
        }

        /// <summary>
        /// Serialization of <see cref="BusyAddress"/>
        /// </summary>
        [XmlElement("BusyAddress"), Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public string BusyAddressString
        {
            get { return Default<AddressTypeConverter>.Instance.ConvertToString(BusyAddress); }
            set { BusyAddress = (Address)Default<AddressTypeConverter>.Instance.ConvertFromString(value); }
        }

        /// <summary>
        /// Address of the entity.
        /// This maps to <see cref="PositionAddress1"/>.
        /// </summary>
        Address IAddressEntity.Address
        {
            get { return PositionAddress1; }
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
            if (PositionAddress1 == null)
            {
                results.Warn(this, "No position address 1 specified");
            }
            if (PositionAddress2 == null)
            {
                results.Warn(this, "No position address 2 specified");
            }
            if (PositionAddress3 == null)
            {
                results.Warn(this, "No position address 3 specified");
            }
            if ((PositionAddress4 == null) && ((PositionAddress5 != null) || (PositionAddress6 != null)))
            {
                results.Warn(this, "No position address 4 specified");
            }
            if ((PositionAddress5 == null) && (PositionAddress6 != null))
            {
                results.Warn(this, "No position address 5 specified");
            }
            if (WriteAddress == null)
            {
                results.Warn(this, "No write address specified");
            }
            if (BusyAddress == null)
            {
                results.Warn(this, "No busy address specified");
            }
            if ((InitialPosition < FirstPosition) || (InitialPosition > LastPosition))
            {
                results.Warn(this, "Invalid initial position");
            }
        }

        /// <summary>
        /// Human readable name of this type of entity.
        /// </summary>
        public override string TypeName
        {
            get { return Strings.TypeNameTurnTable; }
        }
    }
}

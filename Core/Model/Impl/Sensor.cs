using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;
using BinkyRailways.Core.Util;

namespace BinkyRailways.Core.Model.Impl
{
    /// <summary>
    /// Device that signals some event on the railway.
    /// </summary>
    [XmlInclude(typeof(BinarySensor))]
    public abstract class Sensor : PositionedModuleEntity, ISensor
    {
        private readonly Property<Address> address;
        private readonly Property<EntityRef<Block>> block;
        private readonly Property<Shapes> shape;

        /// <summary>
        /// Default ctor
        /// </summary>
        protected Sensor()
            : this(12, 12)
        {
        }

        /// <summary>
        /// Default ctor
        /// </summary>
        protected Sensor(int defaultWidth, int defaultHeight)
            : base(defaultWidth, defaultHeight)
        {
            address = new Property<Address>(this, null);
            block = new Property<EntityRef<Block>>(this, null);
            shape = new Property<Shapes>(this, DefaultValues.DefaultSensorShape);
        }

        /// <summary>
        /// Address of the sensor
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
        public virtual IEnumerable<AddressUsage> AddressUsages
        {
            get
            {
                var addr = Address;
                if (addr != null) yield return new AddressUsage(addr, AddressDirection.Input);                
            }
        }

        /// <summary>
        /// The block that this sensor belongs to.
        /// When set, this connection is used in the loc-to-block assignment process.
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
        /// Shape used to visualize this sensor
        /// </summary>
        [DefaultValue(DefaultValues.DefaultSensorShape)]
        public Shapes Shape
        {
            get { return shape.Value; }
            set { shape.Value = value; }
        }

        /// <summary>
        /// Address of the sensor.
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
            if (Address == null)
            {
                results.Warn(this, Strings.WarnNoAddressSpecified);
            }
        }

        /// <summary>
        /// If this entity uses the given subject, add a <see cref="UsedByInfo"/> entry to 
        /// the given result.
        /// </summary>
        public override void CollectUsageInfo(IEntity subject, UsedByInfos results)
        {
            base.CollectUsageInfo(subject, results);
            if (subject == Block)
            {
                results.UsedBy(this, Strings.UsedBySensor);
            }
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

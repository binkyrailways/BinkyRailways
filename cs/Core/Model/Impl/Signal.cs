using System.Collections.Generic;
using System.Xml.Serialization;

namespace BinkyRailways.Core.Model.Impl
{
    /// <summary>
    /// Device that outputs some signal on the railway.
    /// </summary>
    [XmlInclude(typeof(BlockSignal))]
    public abstract class Signal : PositionedModuleEntity, ISignal
    {
        /// <summary>
        /// Default ctor
        /// </summary>
        protected Signal(int defaultWidth, int defaultHeight)
            : base(defaultWidth, defaultHeight)
        {
        }

        /// <summary>
        /// Accept a visit by the given visitor
        /// </summary>
        public override TReturn Accept<TReturn, TData>(EntityVisitor<TReturn, TData> visitor, TData data)
        {
            return visitor.Visit(this, data);
        }

        /// <summary>
        /// Address of the entity
        /// </summary>
        [XmlIgnore]
        public abstract Address Address { get; set; }

        /// <summary>
        /// Gets all (non-null) addresses configured in this entity.
        /// </summary>
        [XmlIgnore]
        public virtual IEnumerable<AddressUsage> AddressUsages
        {
            get
            {
                var addr = Address;
                if (addr != null) yield return new AddressUsage(addr, AddressDirection.Output);
            }
        }
    }
}

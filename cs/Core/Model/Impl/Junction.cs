using System.ComponentModel;
using System.Xml.Serialization;

namespace BinkyRailways.Core.Model.Impl
{
    /// <summary>
    /// Place where tracks split.
    /// </summary>
    [XmlInclude(typeof(PassiveJunction))]
    [XmlInclude(typeof(Switch))]
    [XmlInclude(typeof(TurnTable))]
    public abstract class Junction : PositionedModuleEntity, IJunction
    {
        private readonly Property<EntityRef<Block>> block;

        /// <summary>
        /// Default ctor
        /// </summary>
        protected Junction(int defaultWidth, int defaultHeight)
            : base(defaultWidth, defaultHeight)
        {
            block = new Property<EntityRef<Block>>(this, null);
        }

        /// <summary>
        /// The block that this junction belongs to.
        /// When set, this junction is considered lock if the block is locked.
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
        /// If this entity uses the given subject, add a <see cref="UsedByInfo"/> entry to 
        /// the given result.
        /// </summary>
        public override void CollectUsageInfo(IEntity subject, UsedByInfos results)
        {
            base.CollectUsageInfo(subject, results);
            if (subject == Block)
            {
                results.UsedBy(this, Strings.UsedByJunction);
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

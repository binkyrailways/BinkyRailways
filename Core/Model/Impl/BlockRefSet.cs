namespace BinkyRailways.Core.Model.Impl
{
    /// <summary>
    /// Set of entities.
    /// Each element may only occur once (if it occurs)
    /// Each element is stored by it's id in XML.
    /// </summary>
    public sealed class BlockRefSet : EntityRefSet<Block, IBlock> 
    {
        /// <summary>
        /// Default ctor
        /// </summary>
        internal BlockRefSet(ModuleEntity owner)
            : base(owner)
        {
        }

        /// <summary>
        /// Look for the item by it's id.
        /// </summary>
        protected override Block Lookup(Module module, string id)
        {
            return module.Blocks[id];
        }
    }
}

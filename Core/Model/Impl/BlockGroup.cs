namespace BinkyRailways.Core.Model.Impl
{
    /// <summary>
    /// Group of blocks that share a similar function.
    /// </summary>
    public sealed class BlockGroup : ModuleEntity, IBlockGroup
    {
        /// <summary>
        /// Default ctor
        /// </summary>
        public BlockGroup()
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
        /// Human readable name of this type of entity.
        /// </summary>
        public override string TypeName
        {
            get { return Strings.TypeNameBlockGroup; }
        }
    }
}

namespace BinkyRailways.Core.Model.Impl
{
    /// <summary>
    /// Set of entities.
    /// Each element may only occur once (if it occurs)
    /// Each element is stored by it's id in XML.
    /// </summary>
    public sealed class LocRefSet : EntityRefSet<Loc, ILoc>
    {
        private readonly RailwayEntity owner;

        /// <summary>
        /// Default ctor
        /// </summary>
        internal LocRefSet(RailwayEntity owner)
            : base(owner)
        {
            this.owner = owner;
        }

        /// <summary>
        /// Look for the item by it's id.
        /// </summary>
        protected override Loc Lookup(Module module, string id)
        {
            ILoc loc;
            if (owner.Railway.TryResolveLoc(id, out loc))
                return (Loc) loc;
            return null;
        }
    }
}

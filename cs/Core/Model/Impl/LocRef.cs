namespace BinkyRailways.Core.Model.Impl
{
    /// <summary>
    /// Single locomotive reference.
    /// </summary>
    public sealed class LocRef : PersistentEntityRef<ILoc>, ILocRef
    {
        /// <summary>
        /// Try to resolve the entity.
        /// </summary>
        /// <returns>True on success, false otherwise</returns>
        public override bool TryResolve(out ILoc entity)
        {
            entity = null;
            var railway = Railway;
            if (railway == null)
                return false;
            entity = railway.Package.GetLoc(Id);
            return (entity != null);
        }

        /// <summary>
        /// Accept a visit by the given visitor
        /// </summary>
        public override TReturn Accept<TReturn, TData>(EntityVisitor<TReturn, TData> visitor, TData data)
        {
            return visitor.Visit(this, data);
        }
    }
}

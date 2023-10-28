namespace BinkyRailways.Core.Model
{
    /// <summary>
    /// Set of persistent entity refs.
    /// Each element may only occur once (if it occurs)
    /// </summary>
    public interface IPersistentEntityRefSet<TRef, TEntity> : IEntitySet<TRef>
        where TRef : IPersistentEntityRef<TEntity>
        where TEntity : IEntity
    {
        /// <summary>
        /// Add a reference to the given entity
        /// </summary>
        TRef Add(TEntity entity);
    }
}

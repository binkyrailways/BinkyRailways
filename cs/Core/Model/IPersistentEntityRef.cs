namespace BinkyRailways.Core.Model
{
    /// <summary>
    /// Base class for references to persistent entities
    /// </summary>
    public interface IPersistentEntityRef<T> : IEntity
    {
        /// <summary>
        /// Try to resolve the entity.
        /// </summary>
        /// <returns>True on success, false otherwise</returns>
        bool TryResolve(out T entity);
    }
}

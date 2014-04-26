namespace BinkyRailways.Core.Model
{
    /// <summary>
    /// Interface implemented by all entities that are found in a package.
    /// </summary>
    internal interface IPackageListener
    {
        /// <summary>
        /// The given entity is removed from the package.
        /// </summary>
        void RemovedFromPackage(IPersistentEntity entity);
    }
}

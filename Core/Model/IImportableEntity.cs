namespace BinkyRailways.Core.Model
{
    /// <summary>
    /// Base class for objects that can be imported.
    /// </summary>
    public interface IImportableEntity : IEntity
    {
        /// <summary>
        /// Compare the last modification of this entity (from the import source) with the given entity found in
        /// the target package.
        /// </summary>
        /// <param name="target">The equal entity in the target package. Can be null.</param>
        ImportComparison CompareTo(IImportableEntity target);

        /// <summary>
        /// Import this entity into the given package.
        /// </summary>
        void Import(IPackage target);
    }
}

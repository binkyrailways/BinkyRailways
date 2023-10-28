namespace BinkyRailways.Core.Model
{
    public enum ImportComparison
    {
        /// <summary>
        /// The entity coming from the import source has no equal entity in the target package
        /// </summary>
        TargetDoesNotExists,

        /// <summary>
        /// The entity coming from the import source is newer
        /// </summary>
        SourceInNewer,

        /// <summary>
        /// The entity in the target package is newer
        /// </summary>
        TargetIsNewer,

        /// <summary>
        /// There is an entity in the target package equal to the entity from the import source,
        /// but it cannot be decided which is newest.
        /// </summary>
        TargetExists
    }
}

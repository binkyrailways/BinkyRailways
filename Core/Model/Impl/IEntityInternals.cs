namespace BinkyRailways.Core.Model.Impl
{
    public interface IEntityInternals
    {
        /// <summary>
        /// If this entity uses the given subject, add a <see cref="UsedByInfo"/> entry to 
        /// the given result.
        /// </summary>
        void CollectUsageInfo(IEntity subject, UsedByInfos result);

        /// <summary>
        /// Validate the integrity of this entity.
        /// </summary>
        void Validate(IEntity validationRoot, ValidationResults results);
    }
}

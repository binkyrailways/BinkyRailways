namespace BinkyRailways.Core.Model.Impl
{
    /// <summary>
    /// Base class for railway objects.
    /// </summary>
    public abstract class PersistentEntityRef<T> : RailwayEntity, IPersistentEntityRef<T>
        where T : IPersistentEntity
    {
        /// <summary>
        /// Human readable description
        /// </summary>
        public override string Description
        {
            get
            {
                T entity;
                return (TryResolve(out entity)) ? entity.Description : string.Empty;
            }
            set { /* Do nothing */ }
        }

        /// <summary>
        /// Does this entity generate it's own description?
        /// </summary>
        public override bool HasAutomaticDescription { get { return true; } }

        /// <summary>
        /// Try to resolve the entity.
        /// </summary>
        /// <returns>True on success, false otherwise</returns>
        public abstract bool TryResolve(out T entity);

        /// <summary>
        /// If this entity uses the given subject, add a <see cref="UsedByInfo"/> entry to 
        /// the given result.
        /// </summary>
        public override void CollectUsageInfo(IEntity subject, UsedByInfos results)
        {
            base.CollectUsageInfo(subject, results);
            T entity;
            if (TryResolve(out entity))
            {
                ((IEntityInternals)entity).CollectUsageInfo(subject, results);
            }
        }
    }
}

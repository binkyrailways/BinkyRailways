using System.Linq;

namespace BinkyRailways.Core.Model.Impl
{
    /// <summary>
    /// Group of locomotives.
    /// </summary>
    public sealed class LocGroup : RailwayEntity, ILocGroup
    {
        private readonly LocRefSet locs;

        /// <summary>
        /// Default ctor
        /// </summary>
        public LocGroup()
        {
            locs = new LocRefSet(this);
        }

        /// <summary>
        /// Set of locs which make up this group.
        /// </summary>
        public LocRefSet Locs
        {
            get { return locs; }
        }

        /// <summary>
        /// Set of locs which make up this group.
        /// </summary>
        IEntitySet3<ILoc> ILocGroup.Locs
        {
            get { return locs.Set; }
        }

        /// <summary>
        /// Accept a visit by the given visitor
        /// </summary>
        public override TReturn Accept<TReturn, TData>(EntityVisitor<TReturn, TData> visitor, TData data)
        {
            return visitor.Visit(this, data);
        }

        /// <summary>
        /// Validate the integrity of this entity.
        /// </summary>
        public override void Validate(IEntity validationRoot, ValidationResults results)
        {
            base.Validate(validationRoot, results);
            locs.Validate(validationRoot, results);
        }

        /// <summary>
        /// If this entity uses the given subject, add a <see cref="UsedByInfo"/> entry to 
        /// the given result.
        /// </summary>
        public override void CollectUsageInfo(IEntity subject, UsedByInfos results)
        {
            base.CollectUsageInfo(subject, results);
            if (locs.Contains(subject as Loc)) 
                results.UsedBy(this, Strings.UsedByMemberOfGroup);
        }

        /// <summary>
        /// The given entity is removed from the package.
        /// </summary>
        protected override void RemovedFromPackage(IPersistentEntity entity)
        {
            base.RemovedFromPackage(entity);
            if (locs.Contains(entity as Loc))
            {
                locs.Remove((Loc) entity);
            }
        }


        /// <summary>
        /// Human readable name of this type of entity.
        /// </summary>
        public override string TypeName
        {
            get { return Strings.TypeNameLocGroup; }
        }

        /// <summary>
        /// Compare the last modification of this entity (from the import source) with the given entity found in
        /// the target package.
        /// </summary>
        /// <param name="target">The equal entity in the target package. Can be null.</param>
        ImportComparison IImportableEntity.CompareTo(IImportableEntity target)
        {
            if (target == null)
                return ImportComparison.TargetDoesNotExists;
            return ImportComparison.TargetExists;
        }

        /// <summary>
        /// Import this entity into the given package.
        /// </summary>
        void IImportableEntity.Import(IPackage target)
        {
            var targetRailway = (Railway)target.Railway;
            var targetEntity = targetRailway.LocGroups.FirstOrDefault(x => x.Id == Id);
            if (targetEntity != null)
            {
                targetRailway.LocGroups.Remove(targetEntity);
            }

            var clone = new LocGroup();
            clone.Id = Id;
            clone.Description = Description;
            targetRailway.LocGroups.Add(clone);
            foreach (var loc in Locs.Set)
            {
                var targetLoc = target.GetLoc(loc.Id);
                if (targetLoc != null)
                {
                    clone.Locs.Add(loc);
                }
            }
        }
    }
}

using System.Xml.Serialization;

namespace BinkyRailways.Core.Model.Impl
{
    /// <summary>
    /// Base class for loc predicates that have N zero or more predicates.
    /// </summary>
    [XmlInclude(typeof(LocPredicateSet))]
    public abstract class LocPredicatesPredicate : LocPredicate, ILocPredicatesPredicate
    {
        private readonly LocPredicateSet predicates;

        /// <summary>
        /// Default ctor
        /// </summary>
        protected LocPredicatesPredicate()
        {
            predicates = new LocPredicateSet(this);
        }

        /// <summary>
        /// Gets the set of nested predicates.
        /// </summary>
        public LocPredicateSet Predicates
        {
            get { return predicates; }
        }

        /// <summary>
        /// Is the <see cref="ILocPredicatesPredicate.Predicates"/> set emtpy?
        /// </summary>
        public bool IsEmpty { get { return (predicates.Count == 0); } }

        /// <summary>
        /// Gets the set of nested predicates.
        /// </summary>
        ILocPredicateSet ILocPredicatesPredicate.Predicates { get { return Predicates.Set; } }

        /// <summary>
        /// Create a deep clone.
        /// </summary>
        protected internal override LocPredicate Clone()
        {
            var clone = CreateInstance();
            foreach (var p in predicates)
            {
                clone.Predicates.Add(p.Clone());
            }
            return clone;
        }

        /// <summary>
        /// Create an empty instance.
        /// </summary>
        protected abstract LocPredicatesPredicate CreateInstance();

        /// <summary>
        /// Module which contains this entity
        /// </summary>
        internal override Module Module
        {
            get { return base.Module; }
            set
            {
                base.Module = value;
                foreach (var p in predicates)
                {
                    p.Module = value;
                }
            }
        }

        /// <summary>
        /// Validate the integrity of this entity.
        /// </summary>
        public override void Validate(IEntity validationRoot, ValidationResults results)
        {
            base.Validate(validationRoot, results);
            foreach (var p in predicates)
            {
                p.Validate(validationRoot, results);
            }
        }

        /// <summary>
        /// If this entity uses the given subject, add a <see cref="UsedByInfo"/> entry to 
        /// the given result.
        /// </summary>
        public override void CollectUsageInfo(IEntity subject, UsedByInfos results)
        {
            base.CollectUsageInfo(subject, results);
            foreach (var p in predicates)
            {
                p.CollectUsageInfo(subject, results);
            }
        }

        /// <summary>
        /// The given entity is removed from the package.
        /// </summary>
        protected override void RemovedFromPackage(IPersistentEntity entity)
        {
            base.RemovedFromPackage(entity);
            predicates.RemovedFromPackage(entity);
        }
    }
}

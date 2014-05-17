using System.ComponentModel;
using System.Linq;
using System.Xml.Serialization;

namespace BinkyRailways.Core.Model.Impl
{
    /// <summary>
    /// Predicate that evaluates to true the given loc is equal to the specified loc.
    /// </summary>
    public sealed class LocEqualsPredicate : LocPredicate, ILocEqualsPredicate
    {
        private readonly Property<EntityRef<Loc>> loc;

        /// <summary>
        /// Default ctor
        /// </summary>
        public LocEqualsPredicate()
        {
            loc = new Property<EntityRef<Loc>>(this, null);
        }

        /// <summary>
        /// Gets/Sets the loc to compare to.
        /// </summary>
        [XmlIgnore]
        public Loc Loc
        {
            get
            {
                if (loc.Value == null)
                    return null;
                Loc result;
                return loc.Value.TryGetItem(out result) ? result : null;
            }
            set
            {
                if (Loc != value)
                {
                    loc.Value = (value != null) ? new EntityRef<Loc>(this, value) : null;
                }
            }
        }

        /// <summary>
        /// Gets the id of the loc.
        /// Used for serialization only.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string LocId
        {
            get { return loc.GetId(); }
            set { loc.Value = string.IsNullOrEmpty(value) ? null : new EntityRef<Loc>(this, value, LookupLoc); }
        }

        /// <summary>
        /// Gets/Sets the loc to compare to.
        /// </summary>
        ILoc ILocEqualsPredicate.Loc
        {
            get { return Loc; }
            set { Loc = (Loc) value; }
        }

        /// <summary>
        /// Accept a visit by the given visitor
        /// </summary>
        public override TReturn Accept<TReturn, TData>(EntityVisitor<TReturn, TData> visitor, TData data)
        {
            return visitor.Visit(this, data);
        }

        /// <summary>
        /// Evaluate this predicate for the given loc.
        /// </summary>
        public override bool Evaluate(ILoc loc)
        {
            return (Loc != null) && (loc != null) && (Loc.Id == loc.Id);
        }

        /// <summary>
        /// Create a deep clone.
        /// </summary>
        protected internal override LocPredicate Clone()
        {
            return new LocEqualsPredicate { LocId = LocId };
        }

        /// <summary>
        /// Validate the integrity of this entity.
        /// </summary>
        public override void Validate(IEntity validationRoot, ValidationResults results)
        {
            base.Validate(validationRoot, results);
            if (loc.Value == null) 
                results.Warn(this, "No loc specified");
            else if (Loc == null) 
                results.Error(this, "Loc with id {0} not found", loc.Value.Id);
        }

        /// <summary>
        /// If this entity uses the given subject, add a <see cref="UsedByInfo"/> entry to 
        /// the given result.
        /// </summary>
        public override void CollectUsageInfo(IEntity subject, UsedByInfos results)
        {
            base.CollectUsageInfo(subject, results);
            if (Loc == subject)
                results.UsedBy(this, Strings.UsedByPermission);
        }

        /// <summary>
        /// The given entity is removed from the package.
        /// </summary>
        protected override void RemovedFromPackage(IPersistentEntity entity)
        {
            base.RemovedFromPackage(entity);
            if (entity.Id == loc.Value.Id)
                loc.Value = null;
        }

        /// <summary>
        /// Human readable name of this type of entity.
        /// </summary>
        public override string TypeName
        {
            get { return Strings.LocEqualsPredicateTypeName; }
        }

        /// <summary>
        /// Convert to a string
        /// </summary>
        public override string ToString()
        {
            var l = Loc;
            return (l != null) ? l.ToString() : "?";
        }

        /// <summary>
        /// Lookup a loc by id.
        /// </summary>
        private Loc LookupLoc(string id)
        {
            ILoc loc;
            var railway = Root;
            if ((railway != null) && railway.TryResolveLoc(id, out loc))
                return (Loc) loc;
            return null;
        }
    }
}

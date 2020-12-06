using System.ComponentModel;
using System.Linq;
using System.Xml.Serialization;

namespace BinkyRailways.Core.Model.Impl
{
    /// <summary>
    /// Predicate that evaluates to true the given loc is part of the specified group.
    /// </summary>
    public sealed class LocGroupEqualsPredicate : LocPredicate, ILocGroupEqualsPredicate
    {
        private readonly Property<EntityRef<LocGroup>> group;

        /// <summary>
        /// Default ctor
        /// </summary>
        public LocGroupEqualsPredicate()
        {
            group = new Property<EntityRef<LocGroup>>(this, null);
        }

        /// <summary>
        /// Gets/Sets the group to look into.
        /// </summary>
        [XmlIgnore]
        public LocGroup Group
        {
            get
            {
                if (group.Value == null)
                    return null;
                LocGroup result;
                return group.Value.TryGetItem(out result) ? result : null;
            }
            set
            {
                if (Group != value)
                {
                    group.Value = (value != null) ? new EntityRef<LocGroup>(this, value) : null;
                }
            }
        }

        /// <summary>
        /// Gets the id of the loc group.
        /// Used for serialization only.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string GroupId
        {
            get { return group.GetId(); }
            set { group.Value = string.IsNullOrEmpty(value) ? null : new EntityRef<LocGroup>(this, value, LookupGroup); }
        }

        /// <summary>
        /// Gets/Sets the group to look into.
        /// </summary>
        ILocGroup ILocGroupEqualsPredicate.Group
        {
            get { return Group; }
            set { Group = (LocGroup) value; }
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
            var @group = Group;
            return (@group != null) && (loc != null) && @group.Locs.ContainsId(loc.Id);
        }

        /// <summary>
        /// Create a deep clone.
        /// </summary>
        protected internal override LocPredicate Clone()
        {
            return new LocGroupEqualsPredicate { GroupId = GroupId };
        }

        /// <summary>
        /// Validate the integrity of this entity.
        /// </summary>
        public override void Validate(IEntity validationRoot, ValidationResults results)
        {
            base.Validate(validationRoot, results);
            if (group.Value == null) 
                results.Warn(this, "No group specified");
            else if (Group == null) 
                results.Error(this, "Loc group with id {0} not found", group.Value.Id);
        }

        /// <summary>
        /// If this entity uses the given subject, add a <see cref="UsedByInfo"/> entry to 
        /// the given result.
        /// </summary>
        public override void CollectUsageInfo(IEntity subject, UsedByInfos results)
        {
            base.CollectUsageInfo(subject, results);
            if (Group == subject)
                results.UsedBy(this, Strings.UsedByPermission);
        }

        /// <summary>
        /// The given entity is removed from the package.
        /// </summary>
        protected override void RemovedFromPackage(IPersistentEntity entity)
        {
            base.RemovedFromPackage(entity);
            if (entity.Id == group.Value.Id)
                group.Value = null;
        }

        /// <summary>
        /// Human readable name of this type of entity.
        /// </summary>
        public override string TypeName
        {
            get { return Strings.LocGroupEqualsPredicateTypeName; }
        }

        /// <summary>
        /// Convert to a string
        /// </summary>
        public override string ToString()
        {
            var l = Group;
            return (l != null) ? l.ToString() : "?";
        }

        /// <summary>
        /// Lookup a group by id.
        /// </summary>
        private LocGroup LookupGroup(string id)
        {
            var railway = Root;
            return railway != null ? railway.LocGroups.FirstOrDefault(x => x.Id == id) : null;
        }
    }
}

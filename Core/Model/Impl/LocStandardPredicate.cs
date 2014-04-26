using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace BinkyRailways.Core.Model.Impl
{
    /// <summary>
    /// Predicate that evaluates to true based on includes and excludes predicates.
    /// The predicate evaluates to true if:
    /// - Includes is empty and the excludes predicate for the loc evaluates to false.
    /// - The Includes predicate evaluates to true and the excludes predicate for the loc evaluates to false
    /// </summary>    
    [XmlInclude(typeof(LocOrPredicate))]
    public sealed class LocStandardPredicate : LocPredicate, ILocStandardPredicate
    {
        private LocOrPredicate includes;
        private LocOrPredicate excludes;

        /// <summary>
        /// Default ctor
        /// </summary>
        public LocStandardPredicate()
        {
            includes = new LocOrPredicate();
            includes.EnsureId();
            excludes = new LocOrPredicate();
            excludes.EnsureId();
        }

        /// <summary>
        /// Default ctor
        /// </summary>
        private LocStandardPredicate(LocStandardPredicate source)
        {
            includes = (LocOrPredicate) source.includes.Clone();
            includes.EnsureId();
            excludes = (LocOrPredicate) source.excludes.Clone();
            excludes.EnsureId();
        }

        /// <summary>
        /// Create a deep clone.
        /// </summary>
        protected internal override LocPredicate Clone()
        {
            return new LocStandardPredicate(this);
        }

        /// <summary>
        /// Module which contains this entity
        /// </summary>
        internal override Module Module
        {
            get { return base.Module; }
            set
            {
                base.Module = value;
                includes.Module = value;
                excludes.Module = value;
            }
        }

        /// <summary>
        /// Including predicates.
        /// </summary>
        public LocOrPredicate Includes
        {
            get { return includes; }
            [EditorBrowsable(EditorBrowsableState.Never)]
            set
            {
                // Store is used only for serialization.
                if (value == null)
                    throw new ArgumentNullException("includes");
                value.Module = Module;
                value.EnsureId();
                includes = value;
            }
        }

        /// <summary>
        /// Should <see cref="Includes"/> be serialized?
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool ShouldSerializeIncludes()
        {
            return !includes.IsEmpty;
        }

        /// <summary>
        /// Excluding predicates.
        /// </summary>
        public LocOrPredicate Excludes
        {
            get { return excludes; }
            [EditorBrowsable(EditorBrowsableState.Never)]
            set
            {
                // Store is used only for serialization.
                if (value == null)
                    throw new ArgumentNullException("excludes");
                value.Module = Module;
                value.EnsureId();
                excludes = value;
            }
        }

        /// <summary>
        /// Should <see cref="Excludes"/> be serialized?
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool ShouldSerializeExcludes()
        {
            return !excludes.IsEmpty;
        }

        /// <summary>
        /// Are both the <see cref="ILocStandardPredicate.Includes"/> and <see cref="ILocStandardPredicate.Excludes"/> set empty?
        /// </summary>
        public bool IsEmpty
        {
            get { return includes.IsEmpty && excludes.IsEmpty; }
        }

        /// <summary>
        /// Including predicates.
        /// </summary>
        ILocOrPredicate ILocStandardPredicate.Includes { get { return includes; } }

        /// <summary>
        /// Excluding predicates.
        /// </summary>
        ILocOrPredicate ILocStandardPredicate.Excludes { get { return excludes; } }

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
            includes.Validate(validationRoot, results);
            excludes.Validate(validationRoot, results);
        }

        /// <summary>
        /// If this entity uses the given subject, add a <see cref="UsedByInfo"/> entry to 
        /// the given result.
        /// </summary>
        public override void CollectUsageInfo(IEntity subject, UsedByInfos results)
        {
            base.CollectUsageInfo(subject, results);
            includes.CollectUsageInfo(subject, results);
            excludes.CollectUsageInfo(subject, results);
        }

        /// <summary>
        /// The given entity is removed from the package.
        /// </summary>
        protected override void RemovedFromPackage(IPersistentEntity entity)
        {
            base.RemovedFromPackage(entity);
            ((IPackageListener)includes).RemovedFromPackage(entity);
            ((IPackageListener)excludes).RemovedFromPackage(entity);
        }

        /// <summary>
        /// Human readable name of this type of entity.
        /// </summary>
        public override string TypeName
        {
            get { return Strings.LocStandardPredicateTypeName; }
        }

        /// <summary>
        /// Convert to a string
        /// </summary>
        public override string ToString()
        {
            if (includes.IsEmpty && excludes.IsEmpty)
                return Strings.All;
            if (includes.IsEmpty)
                return string.Format(Strings.AllButX, excludes);
            if (excludes.IsEmpty)
                return string.Format(Strings.OnlyX, includes);
            return string.Format(Strings.XExcludingY, includes, excludes);
        }
    }
}

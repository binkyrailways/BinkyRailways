using System;
using BinkyRailways.Core.Util;
using Newtonsoft.Json;

namespace BinkyRailways.Core.Model.Impl
{
    /// <summary>
    /// Base class for railway objects.
    /// </summary>
    public abstract class Entity : IEntity, IModifiable, IEntityInternals, IPackageListener
    {
        /// <summary>
        /// A property of this entity has changed.
        /// </summary>
        public event EventHandler PropertyChanged;

        private readonly Property<string> id;
        private readonly Property<string> description;

        /// <summary>
        /// Default ctor
        /// </summary>
        protected Entity()
        {
            id = new Property<string>(this, string.Empty);
            description = new Property<string>(this, string.Empty);
        }

        /// <summary>
        /// Identification value. Must be unique within it's context.
        /// </summary>
        public string Id
        {
            get { return id.Value; }
            set { id.Value = value; }
        }

        /// <summary>
        /// Human readable description
        /// </summary>
        public virtual string Description
        {
            get { return description.Value; }
            set { description.Value = value; }
        }

        /// <summary>
        /// Does this entity generate it's own description?
        /// </summary>
        [JsonIgnore]
        public virtual bool HasAutomaticDescription { get { return false; } }

        /// <summary>
        /// Human readable name of this type of entity.
        /// </summary>
        [JsonIgnore]
        public virtual string TypeName { get { return "_" + GetType().Name; } }

        /// <summary>
        /// Accept a visit by the given visitor
        /// </summary>
        public abstract TReturn Accept<TReturn, TData>(EntityVisitor<TReturn, TData> visitor, TData data);

        /// <summary>
        /// Validate the integrity of this entity.
        /// </summary>
        void IValidationSubject.Validate(ValidationResults results)
        {
            Validate(this, results);
        }

        /// <summary>
        /// Validate the integrity of this entity.
        /// </summary>
        public virtual void Validate(IEntity validationRoot, ValidationResults results)
        {
            if (string.IsNullOrEmpty(Id))
            {
                results.Error(this, Strings.ErrIdMissing);
            }
            if (string.IsNullOrEmpty(Description))
            {
                results.Warn(this, Strings.WarnDescriptionMissing);
            }
        }

        /// <summary>
        /// Find all entities that use this entity.
        /// </summary>
        public UsedByInfos GetUsageInfo()
        {
            var result = new UsedByInfos();            
            Root.CollectUsageInfo(this, result);
            return result;
        }

        /// <summary>
        /// If this entity uses the given subject, add a <see cref="UsedByInfo"/> entry to 
        /// the given result.
        /// </summary>
        public virtual void CollectUsageInfo(IEntity subject, UsedByInfos results)
        {
            // Do nothing here, override me
        }

        /// <summary>
        /// Gets the railway.
        /// </summary>
        protected abstract Railway Root { get; }

        /// <summary>
        /// Called when a property of this entity has changed.
        /// </summary>
        internal virtual void OnModified()
        {
            // Override me
        }

        /// <summary>
        /// Called when a property of this entity has changed.
        /// </summary>
        void IModifiable.OnModified()
        {
            OnModified();
            OnPropertyChanged();
        }

        /// <summary>
        /// Fire the PropertyChanged event.
        /// </summary>
        protected void OnPropertyChanged()
        {
            PropertyChanged.Fire(this);            
        }

        /// <summary>
        /// The given entity is removed from the package.
        /// </summary>
        protected virtual void RemovedFromPackage(IPersistentEntity entity)
        {
            // Override me
        }

        /// <summary>
        /// The given entity is removed from the package.
        /// </summary>
        void IPackageListener.RemovedFromPackage(IPersistentEntity entity)
        {
            RemovedFromPackage(entity);
        }

        /// <summary>
        /// Generate a unique identifier.
        /// </summary>
        internal static string UniqueId()
        {
            return Guid.NewGuid().ToString("D");
        }

        /// <summary>
        /// Upgrade to the latest format
        /// </summary>
        internal virtual void Upgrade()
        {
            // Override when needed
        }

        /// <summary>
        /// Convert to a string
        /// </summary>
        public override string ToString()
        {
            return string.IsNullOrEmpty(Description) ? Id : Description;
        }
    }
}

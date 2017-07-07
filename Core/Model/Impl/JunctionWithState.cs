using System;
using System.ComponentModel;
using System.Xml.Serialization;
using BinkyRailways.Core.Util;
using Newtonsoft.Json;

namespace BinkyRailways.Core.Model.Impl
{
    /// <summary>
    /// Junction reference with intended state.
    /// </summary>
    [XmlInclude(typeof(PassiveJunctionWithState))]
    [XmlInclude(typeof(SwitchWithState))]
    [XmlInclude(typeof(TurnTableWithState))]
    public abstract class JunctionWithState : IJunctionWithState, IModifiable, IEntityInternals, IPackageListener
    {
        /// <summary>
        /// A property of this entity has changed.
        /// </summary>
        public event EventHandler PropertyChanged;

        private readonly Property<EntityRef<Junction>> junction;

        /// <summary>
        /// Default ctor
        /// </summary>
        protected JunctionWithState()
        {
            junction = new Property<EntityRef<Junction>>(this, null);
        }

        /// <summary>
        /// Gets/sets the containing route
        /// </summary>
        internal Route Route { get; set; }

        /// <summary>
        /// Module which contains this entity
        /// </summary>
        IModule IModuleEntity.Module { get { return (Route != null) ? Route.Module : null; } }

        /// <summary>
        /// The junction involved
        /// </summary>
        [XmlIgnore]
        public Junction Junction
        {
            get
            {
                Junction result;
                if (junction.Value == null)
                    return null;
                return junction.Value.TryGetItem(out result) ? result : null;
            }
            set { junction.Value = (value != null) ? new EntityRef<Junction>(this, value) : null; }
        }

        /// <summary>
        /// The id of the junction involved.
        /// Used for serialization
        /// </summary>
        [XmlElement("Junction")]
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string JunctionId
        {
            get
            {
                var entityRef = junction.Value;
                return (entityRef != null) ? entityRef.Id : null;
            }
            set { junction.Value = (!string.IsNullOrEmpty(value)) ? new EntityRef<Junction>(this, value, Lookup) : null; }
        }

        /// <summary>
        /// The junction involved
        /// </summary>
        IJunction IJunctionWithState.Junction
        {
            get { return Junction; }
        }

        /// <summary>
        /// Identification value. Must be unique within it's context.
        /// </summary>
        string IEntity.Id
        {
            get { return JunctionId; }
            set { throw new NotImplementedException(); }
        }

        /// <summary>
        /// Human readable description
        /// </summary>
        string IEntity.Description
        {
            get
            {
                var x = Junction;
                return (x != null) ? x.Description : string.Empty;
            }
            set { throw new NotImplementedException(); }
        }

        /// <summary>
        /// Does this entity generate it's own description?
        /// </summary>
        public bool HasAutomaticDescription { get { return true; } }

        /// <summary>
        /// Human readable name of this type of entity.
        /// </summary>
        public virtual string TypeName { get { return "_" + GetType().Name; } }

        /// <summary>
        /// Create a clone of this entity.
        /// Do not clone the junction.
        /// </summary>
        public abstract IJunctionWithState Clone();

        /// <summary>
        /// Accept a visit by the given visitor
        /// </summary>
        public virtual TReturn Accept<TReturn, TData>(EntityVisitor<TReturn, TData> visitor, TData data)
        {
            return visitor.Visit(this, data);
        }

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
            junction.Validate(validationRoot, results);
            if (Junction == null)
            {
                results.Error(this, Strings.ErrNoJunctionSpecified);
            }
        }

        /// <summary>
        /// Find all entities that use this entity.
        /// </summary>
        public UsedByInfos GetUsageInfo()
        {
            var result = new UsedByInfos();
            if (Junction != null)
            {
                var railway = (Railway) Junction.Module.Package.Railway;
                railway.CollectUsageInfo(this, result);
            }
            return result;
        }

        /// <summary>
        /// If this entity uses the given subject, add a <see cref="UsedByInfo"/> entry to 
        /// the given result.
        /// </summary>
        public virtual void CollectUsageInfo(IEntity subject, UsedByInfos result)
        {
            // Do nothing here
        }

        /// <summary>
        /// Settings in this object have been modified
        /// </summary>
        void IModifiable.OnModified()
        {
            if (Route != null)
            {
                Route.OnModified();
            }
            PropertyChanged.Fire(this);
        }

        /// <summary>
        /// Lookup a junction by it's id.
        /// </summary>
        private Junction Lookup(string id)
        {
            if (Route == null)    
                throw new ApplicationException("Lookup: Route is null");
            var module = Route.Module;
            if (module == null)
                throw new ApplicationException("Lookup: Route.Module is module");
            var item = module.Junctions[id];
            if (item == null)
                throw new ApplicationException("Lookup: id not found: " + id);
            return item;
        }

        /// <summary>
        /// The given entity is removed from the package.
        /// </summary>
        public virtual void RemovedFromPackage(IPersistentEntity entity)
        {
            // Never needed
        }

        /// <summary>
        /// Convert this entity to JSON.
        /// </summary>
        public virtual void ToJSON(JsonSerializer serializer, JsonWriter writer)
        {
            serializer.Serialize(writer, this);
        }
    }
}

using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace BinkyRailways.Core.Model.Impl
{
    /// <summary>
    /// Sensor event in a route.
    /// </summary>
    public sealed class RouteEvent : ModuleEntity, IRouteEvent
    {
        private readonly Property<EntityRef<Sensor>> sensor;
        private readonly RouteEventBehaviorList behaviors;
        private Route route;

        /// <summary>
        /// Default ctor
        /// </summary>
        public RouteEvent()
        {
            sensor = new Property<EntityRef<Sensor>>(this, null);
            behaviors = new RouteEventBehaviorList(this);
        }

        /// <summary>
        /// The containing route.
        /// </summary>
        internal Route Route
        {
            get { return route; }
            set
            {
                route = value;
                behaviors.LinkItems();
            }
        }

        /// <summary>
        /// Module which contains this entity
        /// </summary>
        internal override Module Module
        {
            get { return (Route != null) ? Route.Module : null; }
            set { /* Do nothing */ }
        }

        /// <summary>
        /// The sensor involved
        /// </summary>
        [XmlIgnore]
        public Sensor Sensor
        {
            get
            {
                Sensor result;
                if (sensor.Value == null)
                    return null;
                return sensor.Value.TryGetItem(out result) ? result : null;
            }
            set { sensor.Value = (value != null) ? new EntityRef<Sensor>(this, value) : null; }
        }

        /// <summary>
        /// The id of the sensor involved.
        /// Used for serialization
        /// </summary>
        [XmlElement("Junction")]
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string SensorId
        {
            get
            {
                var entityRef = sensor.Value;
                return (entityRef != null) ? entityRef.Id : null;
            }
            set { sensor.Value = (!string.IsNullOrEmpty(value)) ? new EntityRef<Sensor>(this, value, Lookup) : null; }
        }

        /// <summary>
        /// The junction involved
        /// </summary>
        ISensor IRouteEvent.Sensor
        {
            get { return Sensor; }
        }

        /// <summary>
        /// Gets the list of behaviors to choose from.
        /// The first matching behavior is used.
        /// </summary>
        public RouteEventBehaviorList Behaviors
        {
            get { return behaviors; }
        }

        /// <summary>
        /// Gets the list of behaviors to choose from.
        /// The first matching behavior is used.
        /// </summary>
        IRouteEventBehaviorList IRouteEvent.Behaviors
        {
            get { return behaviors.List; }
        }

        /// <summary>
        /// Does this entity generate it's own description?
        /// </summary>
        public override bool HasAutomaticDescription { get { return true; } }

        /// <summary>
        /// Human readable description
        /// </summary>
        public override string Description
        {
            get { return (Sensor != null) ? Sensor.Description : "-"; }
            set{ /* Ignore */ }
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
            //appliesTo.Validate(results);
        }

        /// <summary>
        /// If this entity uses the given subject, add a <see cref="UsedByInfo"/> entry to 
        /// the given result.
        /// </summary>
        public override void CollectUsageInfo(IEntity subject, UsedByInfos result)
        {
            // Do nothing here
        }

        /// <summary>
        /// The given entity is removed from the package.
        /// </summary>
        protected override void RemovedFromPackage(IPersistentEntity entity)
        {
            //appliesTo.
        }

        /// <summary>
        /// Lookup a sensor by it's id.
        /// </summary>
        private Sensor Lookup(string id)
        {
            var module = Module;
            if (module == null)
                throw new ApplicationException("Lookup: Module is not set");
            var item = module.Sensors[id];
            if (item == null)
                throw new ApplicationException("Lookup: id not found: " + id);
            return item;
        }
    }
}

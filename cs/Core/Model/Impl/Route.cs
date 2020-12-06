using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Xml.Serialization;

namespace BinkyRailways.Core.Model.Impl
{
    /// <summary>
    /// Route from one block to another.
    /// </summary>
    public sealed partial class Route : ModuleEntity, IRoute, IActionTriggerSourceInternals
    {
        private readonly Property<EntityRef<Block>> fromBlock;
        private readonly Property<BlockSide> fromBlockSide;
        private readonly Property<EntityRef<Block>> toBlock;
        private readonly Property<BlockSide> toBlockSide;
        private readonly Property<EntityRef<Edge>> fromEdge;
        private readonly Property<EntityRef<Edge>> toEdge;
        private readonly JunctionWithStateSet crossingJunctions;
        private readonly EventSet events;
        private readonly EnteringDestinationSensorSet _enteringSensors;
        private readonly ReachedDestinationSensorSet _reachedSensors;
        private readonly Property<int> speed;
        private readonly Property<int> chooseProbability;
        private LocStandardPredicate permissions;
        private readonly Property<bool> closed;
        private readonly Property<int> maxDuration;
        private readonly ActionTrigger enteringDestinationTrigger;
        private readonly ActionTrigger destinationReachedTrigger;

        /// <summary>
        /// Default ctor
        /// </summary>
        public Route()
        {
            fromBlock = new Property<EntityRef<Block>>(this, null);
            fromBlockSide = new Property<BlockSide>(this, BlockSide.Front);
            toBlock = new Property<EntityRef<Block>>(this, null);
            toBlockSide = new Property<BlockSide>(this, BlockSide.Back);
            fromEdge = new Property<EntityRef<Edge>>(this, null);
            toEdge = new Property<EntityRef<Edge>>(this, null);
            crossingJunctions = new JunctionWithStateSet(this);
            events = new EventSet(this);
            _enteringSensors = new EnteringDestinationSensorSet(this);
            _reachedSensors = new ReachedDestinationSensorSet(this);
            speed = new Property<int>(this, DefaultValues.DefaultRouteSpeed);
            chooseProbability = new Property<int>(this, DefaultValues.DefaultRouteChooseProbability);
            permissions = new LocStandardPredicate();
            permissions.EnsureId();
            closed = new Property<bool>(this, false, null);
            maxDuration = new Property<int>(this, DefaultValues.DefaultRouteMaxDuration);
            enteringDestinationTrigger = new ActionTrigger(this, Strings.TriggerNameEnteringDestination);
            destinationReachedTrigger = new ActionTrigger(this, Strings.TriggerNameDestinationReached);
        }

        /// <summary>
        /// Human readable description
        /// </summary>
        [XmlIgnore]
        public override string Description
        {
            get
            {
                var x = (From != null) ? From.Description : "?";
                var y = (To != null) ? To.Description : "?";
                return x + " -> " + y;
            }
            set { base.Description = value; }
        }

        /// <summary>
        /// Trigger fired when a loc has starts entering the destination of this route.
        /// </summary>
        public ActionTrigger EnteringDestinationTrigger { get { return enteringDestinationTrigger; } }

        /// <summary>
        /// Should <see cref="EnteringDestinationTrigger"/> be serialized?
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool ShouldSerializeEnteringDestinationTrigger()
        {
            return !enteringDestinationTrigger.IsEmpty;
        }

        /// <summary>
        /// Trigger fired when a loc has reached the destination of this route.
        /// </summary>
        public ActionTrigger DestinationReachedTrigger { get { return destinationReachedTrigger; } }

        /// <summary>
        /// Should <see cref="DestinationReachedTrigger"/> be serialized?
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool ShouldSerializeDestinationReachedTrigger()
        {
            return !destinationReachedTrigger.IsEmpty;
        }

        /// <summary>
        /// Trigger fired when a loc has starts entering the destination of this route.
        /// </summary>
        IActionTrigger IRoute.EnteringDestinationTrigger { get { return enteringDestinationTrigger; } }

        /// <summary>
        /// Trigger fired when a loc has reached the destination of this route.
        /// </summary>
        IActionTrigger IRoute.DestinationReachedTrigger { get { return destinationReachedTrigger; } }

        /// <summary>
        /// Gets all triggers of this entity.
        /// </summary>
        IEnumerable<IActionTrigger> IActionTriggerSource.Triggers
        {
            get
            {
                yield return enteringDestinationTrigger;
                yield return destinationReachedTrigger;
            }
        }

        /// <summary>
        /// Gets the containing railway.
        /// </summary>
        IRailway IActionTriggerSourceInternals.Railway { get { return Root; } }

        /// <summary>
        /// Gets the persistent entity that contains this action trigger.
        /// </summary>
        IPersistentEntity IActionTriggerSourceInternals.Container { get { return Module; } }

        /// <summary>
        /// Does this entity generate it's own description?
        /// </summary>
        public override bool HasAutomaticDescription { get { return true; } }

        /// <summary>
        /// Starting point of the route
        /// </summary>
        [XmlIgnore]
        public EndPoint From
        {
            get
            {
                Block block;
                if ((fromBlock.Value != null) && (fromBlock.Value.TryGetItem(out block)))
                    return block;
                Edge edge;
                if ((fromEdge.Value != null) && (fromEdge.Value.TryGetItem(out edge)))
                    return edge;
                return null;
            }
            set
            {
                if (From != value)
                {
                    var block = value as Block;
                    var edge = value as Edge;
                    fromBlock.Value = (block != null) ? new EntityRef<Block>(this, block) : null;
                    fromEdge.Value = (edge != null) ? new EntityRef<Edge>(this, edge) : null;
                }
            }
        }

        /// <summary>
        /// Side of the <see cref="From"/> block at which this route will leave that block.
        /// </summary>
        [DefaultValue(BlockSide.Front)]
        public BlockSide FromBlockSide
        {
            get { return fromBlockSide.Value; }
            set { fromBlockSide.Value = value; }
        }

        /// <summary>
        /// End point of the route
        /// </summary>
        [XmlIgnore]
        public EndPoint To
        {
            get
            {
                Block block;
                if ((toBlock.Value != null) && (toBlock.Value.TryGetItem(out block)))
                    return block;
                Edge edge;
                if ((toEdge.Value != null) && (toEdge.Value.TryGetItem(out edge)))
                    return edge;
                return null;
            }
            set
            {
                if (To != value)
                {
                    var block = value as Block;
                    var edge = value as Edge;
                    toBlock.Value = (block != null) ? new EntityRef<Block>(this, block) : null;
                    toEdge.Value = (edge != null) ? new EntityRef<Edge>(this, edge) : null;
                }
            }
        }

        /// <summary>
        /// Side of the <see cref="To"/> block at which this route will enter that block.
        /// </summary>
        [DefaultValue(BlockSide.Back)]
        public BlockSide ToBlockSide
        {
            get { return toBlockSide.Value; }
            set { toBlockSide.Value = value; }
        }

        /// <summary>
        /// Gets the id of the from block.
        /// Used for serialization only.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string FromBlock
        {
            get { return fromBlock.GetId(); }
            set { fromBlock.Value = string.IsNullOrEmpty(value) ? null : new EntityRef<Block>(this, value, LookupBlock); }
        }

        /// <summary>
        /// Gets the id of the to block.
        /// Used for serialization only.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string ToBlock
        {
            get { return toBlock.GetId(); }
            set { toBlock.Value = string.IsNullOrEmpty(value) ? null : new EntityRef<Block>(this, value, LookupBlock); }
        }

        /// <summary>
        /// Gets the id of the from edge.
        /// Used for serialization only.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string FromEdge
        {
            get { return fromEdge.GetId(); }
            set { fromEdge.Value = string.IsNullOrEmpty(value) ? null : new EntityRef<Edge>(this, value, LookupEdge); }
        }

        /// <summary>
        /// Gets the id of the to edge.
        /// Used for serialization only.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string ToEdge
        {
            get { return toEdge.GetId(); }
            set { toEdge.Value = string.IsNullOrEmpty(value) ? null : new EntityRef<Edge>(this, value, LookupEdge); }
        }

        /// <summary>
        /// Set of junctions with their states that are crossed when taking this route.
        /// </summary>
        public JunctionWithStateSet CrossingJunctions { get { return crossingJunctions; } }

        /// <summary>
        /// Control serialization of <see cref="CrossingJunctions"/>.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool ShouldSerializeCrossingJunctions() { return (crossingJunctions.Count > 0); }

        /// <summary>
        /// Events that control this route
        /// </summary>
        public EventSet Events { get { return events; } }

        /// <summary>
        /// Control serialization of <see cref="Events"/>.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool ShouldSerializeEvents() { return (events.Count > 0); }

        /// <summary>
        /// Set of sensors which (when active) indicate that a loc is has entered
        /// the To block. The loc will then slow down, but not stop.
        /// OBSOLETE
        /// </summary>        
        public SensorRefSet EnteringDestinationSensors { get { return _enteringSensors; } }

        /// <summary>
        /// Control serialization of <see cref="EnteringDestinationSensors"/>.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool ShouldSerializeEnteringDestinationSensors() { return (_enteringSensors.Count > 0); }

        /// <summary>
        /// Set of sensors which (when active) indicate that a loc is has completely 
        /// reached the To block. The loc will then stop.
        /// OBSOLETE
        /// </summary>
        public SensorRefSet ReachedDestinationSensors { get { return _reachedSensors; } }

        /// <summary>
        /// Control serialization of <see cref="ReachedDestinationSensors"/>.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool ShouldSerializeReachedDestinationSensors() { return (_reachedSensors.Count > 0); }

        /// <summary>
        /// Speed of locs when going this route.
        /// This value is a percentage of the maximum / medium speed of the loc.
        /// </summary>
        /// <value>0..100</value>
        [DefaultValue(DefaultValues.DefaultRouteSpeed)]
        public int Speed
        {
            get { return speed.Value; }
            set { speed.Value = Math.Max(0, Math.Min(100, value)); }
        }

        /// <summary>
        /// Probability (in percentage) that a loc will take this route.
        /// When multiple routes are available to choose from the route with the highest probability will have the highest
        /// chance or being chosen.
        /// </summary>
        /// <value>0..100</value>
        [DefaultValue(DefaultValues.DefaultRouteChooseProbability)]
        public int ChooseProbability
        {
            get { return chooseProbability.Value; }
            set { chooseProbability.Value = Math.Max(0, Math.Min(100, value)); }
        }

        /// <summary>
        /// Gets the predicate used to decide which locs are allowed to use this route.
        /// </summary>
        public LocStandardPredicate Permissions
        {
            get { return permissions; }
            [EditorBrowsable(EditorBrowsableState.Never)]
            set
            {
                // Store is used only for serialization.
                if (value == null)
                    throw new ArgumentNullException("permissions");
                value.Module = Module;
                value.EnsureId();
                permissions = value;
            }
        }

        /// <summary>
        /// Should Permissions be serialized?
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool ShouldSerializePermissions()
        {
            return !permissions.IsEmpty;
        }

        /// <summary>
        /// Is this route open for traffic or not?
        /// Setting to true, allows for maintance etc. on this route.
        /// </summary>
        [DefaultValue(false)]
        public bool Closed
        {
            get { return closed.Value; }
            set { closed.Value = value; }
        }

        /// <summary>
        /// Maximum time in seconds that this route should take.
        /// If a loc takes this route and exceeds this duration, a warning is given.
        /// </summary>
        [DefaultValue(DefaultValues.DefaultRouteMaxDuration)]
        public int MaxDuration
        {
            get { return maxDuration.Value; }
            set
            {
                if (value < 2)
                    throw new ArgumentOutOfRangeException("Maximum duration is too short");
                maxDuration.Value = value;
            }
        }

        /// <summary>
        /// Does this route references the given block in one of its properties?
        /// </summary>
        internal bool References(Block block)
        {
            return (From == block) || (To == block);
        }

        /// <summary>
        /// Does this route references the given edge in one of its properties?
        /// </summary>
        internal bool References(Edge edge)
        {
            return (From == edge) || (To == edge);
        }

        /// <summary>
        /// Does this route references the given junction in one of its properties?
        /// </summary>
        internal bool References(Junction junction)
        {
            return crossingJunctions.Contains(junction);
        }

        /// <summary>
        /// Starting point of the route
        /// </summary>
        IEndPoint IRoute.From
        {
            get { return From; }
            set { From = (EndPoint)value; }
        }

        /// <summary>
        /// End point of the route
        /// </summary>
        IEndPoint IRoute.To
        {
            get { return To; }
            set { To = (EndPoint) value; }
        }

        /// <summary>
        /// Set of junctions with their states that are crossed when taking this route.
        /// </summary>
        IJunctionWithStateSet IRoute.CrossingJunctions { get { return crossingJunctions.Set; } }

        /// <summary>
        /// Set of events that change the state of the route and it's running loc.
        /// </summary>
        IRouteEventSet IRoute.Events { get { return events.Set; } }

        /// <summary>
        /// Set of sensors which (when active) indicate that a loc is has entered
        /// the To block. The loc will then slow down, but not stop.
        /// </summary>
        //IRouteSensorSet IRoute.EnteringDestinationSensors { get { return enteringSensors.Set; } }

        /// <summary>
        /// Set of sensors which (when active) indicate that a loc is has completely 
        /// reached the To block. The loc will then stop.
        /// </summary>
        //IRouteSensorSet IRoute.ReachedDestinationSensors { get { return reachedSensors.Set; } }

        /// <summary>
        /// Gets the predicate used to decide which locs are allowed to use this route.
        /// </summary>
        ILocStandardPredicate IRoute.Permissions { get { return permissions; } }

        /// <summary>
        /// Module which contains this entity
        /// </summary>
        internal override Module Module
        {
            get { return base.Module; }
            set
            {
                base.Module = value;
                permissions.Module = value;
                events.LinkItems();
            }
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
            toBlock.Validate(validationRoot, results);
            toEdge.Validate(validationRoot, results);
            fromBlock.Validate(validationRoot, results);
            fromEdge.Validate(validationRoot, results);
            enteringDestinationTrigger.Validate(validationRoot, results);
            destinationReachedTrigger.Validate(validationRoot, results);

            if (From == null)
                results.Warn(this, Strings.WarnNoFromEndPointSpecified);
            if (To == null)
                results.Warn(this, Strings.WarnNoToEndPointSpecified);
            if ((To == From) && (To != null)) 
                results.Warn(this, Strings.WarnToEndPointEqualsFromEndPoint);

            crossingJunctions.Validate(validationRoot, results);
            events.Validate(validationRoot, results);
            _enteringSensors.Validate(validationRoot, results);
            _reachedSensors.Validate(validationRoot, results);

            var enterEventCount = events.Count(evt => evt.Behaviors.Any(x => x.StateBehavior == RouteStateBehavior.Enter));
            var reachedEventCount = events.Count(evt => evt.Behaviors.Any(x => x.StateBehavior == RouteStateBehavior.Reached));

            if ((enterEventCount == 0) && (reachedEventCount == 0) && this.IsInternal())
                results.Warn(this, Strings.WarnNoSensorsSpecified);
            else if ((reachedEventCount == 0) && this.IsToInternal())
                results.Warn(this, Strings.WarnNoReachedDestinationSensorsSpecified);
            else if ((enterEventCount == 0) && this.IsToInternal())
                results.Warn(this, Strings.WarnNoEnteringDestinationSensorsSpecified);
        }

        /// <summary>
        /// If this entity uses the given subject, add a <see cref="UsedByInfo"/> entry to 
        /// the given result.
        /// </summary>
        public override void CollectUsageInfo(IEntity subject, UsedByInfos results)
        {
            base.CollectUsageInfo(subject, results);
            if (subject == From)
                results.UsedBy(this, Strings.UsedByRouteAsFrom);
            if (subject == To)
                results.UsedBy(this, Strings.UsedByRouteAsTo);
            if (crossingJunctions.Contains(subject as IJunction)) 
                results.UsedBy(this, Strings.UsedByRouteAsCrossingJunction);
            var @event = events.FirstOrDefault(x => x.Sensor == subject);
            if (@event != null)
            {
                if (@event.Behaviors.Any(x => x.StateBehavior == RouteStateBehavior.Enter))
                    results.UsedBy(this, Strings.UsedByRouteAsEnteringSensor);
                if (@event.Behaviors.Any(x => x.StateBehavior == RouteStateBehavior.Reached))
                    results.UsedBy(this, Strings.UsedByRouteAsReachedSensor);
                if (@event.Behaviors.Any(x => (x.StateBehavior != RouteStateBehavior.Reached) && (x.StateBehavior != RouteStateBehavior.Enter)))
                    results.UsedBy(this, Strings.UsedByRouteAsOtherSensor);
            }
            permissions.CollectUsageInfo(subject, results);
            enteringDestinationTrigger.CollectUsageInfo(subject, results);
            destinationReachedTrigger.CollectUsageInfo(subject, results);
        }

        /// <summary>
        /// The given entity is removed from the package.
        /// </summary>
        protected override void RemovedFromPackage(IPersistentEntity entity)
        {
            base.RemovedFromPackage(entity);
            crossingJunctions.RemovedFromPackage(entity);
            ((IPackageListener)permissions).RemovedFromPackage(entity);
            enteringDestinationTrigger.RemovedFromPackage(entity);
            destinationReachedTrigger.RemovedFromPackage(entity);
        }

        /// <summary>
        /// Human readable name of this type of entity.
        /// </summary>
        public override string TypeName
        {
            get { return Strings.TypeNameRoute; }
        }

        /// <summary>
        /// Upgrade to the latest format
        /// </summary>
        internal override void Upgrade()
        {
            IRoute route = this;
            if (_enteringSensors.Any())
            {
                foreach (var s in _enteringSensors.Set)
                {
                    var @event = route.Events.Add(s);
                    var b = @event.Behaviors.Add();
                    b.StateBehavior = RouteStateBehavior.Enter;
                    b.SpeedBehavior = LocSpeedBehavior.Default;
                }
                _enteringSensors.RemoveAll(x => true);
            }
            if (_reachedSensors.Any())
            {
                foreach (var s in _reachedSensors.Set)
                {
                    var @event = route.Events.Add(s);
                    var b = @event.Behaviors.Add();
                    b.StateBehavior = RouteStateBehavior.Reached;
                    b.SpeedBehavior = LocSpeedBehavior.Default;
                }
                _reachedSensors.RemoveAll(x => true);
            }
        }

        /// <summary>
        /// Lookup a block by id. 
        /// </summary>
        private Block LookupBlock(string id)
        {
            return Module.Blocks[id];
        }

        /// <summary>
        /// Lookup an edge by id.
        /// </summary>
        private Edge LookupEdge(string id)
        {
            return Module.Edges[id];
        }
    }
}

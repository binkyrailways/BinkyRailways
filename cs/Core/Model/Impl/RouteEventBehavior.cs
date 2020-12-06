using System;
using System.ComponentModel;

namespace BinkyRailways.Core.Model.Impl
{
    /// <summary>
    /// Possible behavior of a route event.
    /// </summary>
    public sealed class RouteEventBehavior : ModuleEntity, IRouteEventBehavior
    {
        private LocStandardPredicate appliesTo;
        private readonly Property<RouteStateBehavior> stateBehavior;
        private readonly Property<LocSpeedBehavior> speedBehavior;
        private RouteEvent routeEvent;

        /// <summary>
        /// Default ctor
        /// </summary>
        public RouteEventBehavior()
        {
            appliesTo = new LocStandardPredicate();
            stateBehavior = new Property<RouteStateBehavior>(this, DefaultValues.DefaultRouteEventBehaviorStateBehavior);
            speedBehavior = new Property<LocSpeedBehavior>(this, DefaultValues.DefaultRouteEventBehaviorSpeedBehavior);
        }

        /// <summary>
        /// The containing route event.
        /// </summary>
        internal RouteEvent RouteEvent
        {
            get { return routeEvent; }
            set
            {
                routeEvent = value;
                if (appliesTo != null) appliesTo.Module = Module;
                LinkItem();
            }
        }

        /// <summary>
        /// Refresh the RouteEvent property of each item.
        /// </summary>
        internal void LinkItem()
        {
            LinkAppliesTo();
        }

        /// <summary>
        /// Module which contains this entity
        /// </summary>
        internal override Module Module
        {
            get { return (RouteEvent != null) ? RouteEvent.Module : null; }
            set { /* Do nothing */ }
        }

        /// <summary>
        /// Gets the predicate used to decide which locs are allowed to use this route.
        /// </summary>
        public LocStandardPredicate AppliesTo
        {
            get
            {
                LinkAppliesTo();
                return appliesTo;
            }
            [EditorBrowsable(EditorBrowsableState.Never)]
            set
            {
                // Store is used only for serialization.
                if (value == null)
                    throw new ArgumentNullException("value");
                value.Module = Module;
                value.EnsureId();
                appliesTo = value;
            }
        }

        private void LinkAppliesTo()
        {
            if ((appliesTo != null) && (appliesTo.Module == null))
                appliesTo.Module = Module;            
        }

        /// <summary>
        /// Predicate used to select the locs to which this event applies.
        /// </summary>
        ILocPredicate IRouteEventBehavior.AppliesTo
        {
            get { return appliesTo; }
        }

        /// <summary>
        /// How is the state of the route changed.
        /// </summary>
        public RouteStateBehavior StateBehavior
        {
            get { return stateBehavior.Value; }
            set
            {
                stateBehavior.Value = value;
                speedBehavior.Value = FixSpeedBehavior(speedBehavior.Value, value);
            }
        }

        /// <summary>
        /// How is the speed of the occupying loc changed.
        /// </summary>
        public LocSpeedBehavior SpeedBehavior
        {
            get { return FixSpeedBehavior(speedBehavior.Value, stateBehavior.Value); }
            set { speedBehavior.Value = FixSpeedBehavior(value, stateBehavior.Value); }
        }

        /// <summary>
        /// Does this entity generate it's own description?
        /// </summary>
        public override bool HasAutomaticDescription { get { return true; } }

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
            appliesTo.Validate(validationRoot, results);
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
        /// Make sure that the speed behavior is default in value the state is reached.
        /// </summary>
        private static LocSpeedBehavior FixSpeedBehavior(LocSpeedBehavior value, RouteStateBehavior stateBehavior)
        {
            switch (stateBehavior)
            {
                case RouteStateBehavior.Reached:
                    return LocSpeedBehavior.Default;
                default:
                    return value;
            }
        }
    }
}

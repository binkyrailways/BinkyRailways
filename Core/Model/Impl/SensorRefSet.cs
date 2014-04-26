using System;

namespace BinkyRailways.Core.Model.Impl
{
    /// <summary>
    /// Set of entities.
    /// Each element may only occur once (if it occurs)
    /// Each element is stored by it's id in XML.
    /// </summary>
    public class SensorRefSet : EntityRefSet<Sensor, ISensor>
    {
        private readonly RouteSensorSetType type;
        private readonly IRoute route;

        /// <summary>
        /// Default ctor
        /// </summary>
        internal SensorRefSet(ModuleEntity owner, RouteSensorSetType type, IRoute route)
            : base(owner)
        {
            this.type = type;
            this.route = route;
        }


        /// <summary>
        /// Implementation of IEntitySet
        /// </summary>
        public new IRouteSensorSet Set
        {
            get { return (IRouteSensorSet) base.Set; }
        }

        /// <summary>
        /// Look for the item by it's id.
        /// </summary>
        protected override Sensor Lookup(Module module, string id)
        {
            return module.Sensors[id];
        }

        /// <summary>
        /// Create an implementation of IEntitySet.
        /// </summary>
        protected override SetImpl CreateSetImpl()
        {
            return new SensorSetImpl(this, type, route);
        }

        /// <summary>
        /// IRouteSensorSet implementation
        /// </summary>
        private class SensorSetImpl : SetImpl, IRouteSensorSet
        {
            private readonly RouteSensorSetType type;
            private readonly IRoute route;

            /// <summary>
            /// Default ctor
            /// </summary>
            internal SensorSetImpl(SensorRefSet impl, RouteSensorSetType type, IRoute route)
                : base(impl)
            {
                this.type = type;
                this.route = route;
            }

            /// <summary>
            /// Gets the containing route.
            /// </summary>
            public IRoute Route
            {
                get { return route; }
            }

            /// <summary>
            /// Set type
            /// </summary>
            public RouteSensorSetType Type
            {
                get { return type; }
            }
        }
    }
}

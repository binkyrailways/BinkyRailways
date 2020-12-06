namespace BinkyRailways.Core.Model.Impl
{
    /// <summary>
    /// Unbreakable part of an entire railway.
    /// </summary>
    partial class Module 
    {
        /// <summary>
        /// Set of sensors contained in this module.
        /// </summary>
        public class SensorSet : ModuleEntitySet<Sensor, ISensor>
        {
            /// <summary>
            /// Default ctor
            /// </summary>
            internal SensorSet(Module module)
                : base(module, module)
            {
            }

            /// <summary>
            /// Implementation of ISensorSet
            /// </summary>
            public new ISensorSet Set
            {
                get { return (ISensorSet)base.Set; }
            }

            /// <summary>
            /// Create an implementation of IEntitySet.
            /// </summary>
            protected override SetImpl CreateSetImpl()
            {
                return new SensorSetImpl(this);
            }

            /// <summary>
            /// The given item has been removed from this set.
            /// </summary>
            protected override void OnRemoved(Sensor item)
            {
                foreach (var route in Module.Routes)
                {
                    route.EnteringDestinationSensors.Remove(item);
                    route.ReachedDestinationSensors.Remove(item);
                }
                base.OnRemoved(item);
            }

            private sealed class SensorSetImpl : SetImpl, ISensorSet
            {
                /// <summary>
                /// Default ctor
                /// </summary>
                internal SensorSetImpl(SensorSet impl)
                    : base(impl)
                {
                }

                /// <summary>
                /// Add a new binary sensor
                /// </summary>
                public IBinarySensor AddNewBinarySensor()
                {
                    var item = new BinarySensor();
                    Add(item);
                    return item;
                }
            }
        }
    }
}

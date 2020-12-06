namespace BinkyRailways.Core.Model.Impl
{
    /// <summary>
    /// Set of entities.
    /// Each element may only occur once (if it occurs)
    /// Each element is stored by it's id in XML.
    /// </summary>
    public class JunctionRefSet : EntityRefSet<Junction, IJunction>
    {
        /// <summary>
        /// Default ctor
        /// </summary>
        internal JunctionRefSet(ModuleEntity owner)
            : base(owner)
        {
        }


        /// <summary>
        /// Implementation of IEntitySet
        /// </summary>
        public new IJunctionRefSet Set
        {
            get { return (IJunctionRefSet) base.Set; }
        }

        /// <summary>
        /// Look for the item by it's id.
        /// </summary>
        protected override Junction Lookup(Module module, string id)
        {
            return module.Junctions[id];
        }

        /// <summary>
        /// Create an implementation of IEntitySet.
        /// </summary>
        protected override SetImpl CreateSetImpl()
        {
            return new JunctionSetImpl(this);
        }

        /// <summary>
        /// IRouteSensorSet implementation
        /// </summary>
        private class JunctionSetImpl : SetImpl, IJunctionRefSet
        {
            /// <summary>
            /// Default ctor
            /// </summary>
            internal JunctionSetImpl(JunctionRefSet impl)
                : base(impl)
            {
            }
        }
    }
}

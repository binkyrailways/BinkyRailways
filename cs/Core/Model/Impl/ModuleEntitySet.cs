namespace BinkyRailways.Core.Model.Impl
{
    /// <summary>
    /// Set of module entities.
    /// </summary>
    public class ModuleEntitySet<T, TIntf> : EntitySet<T, TIntf>
        where T : ModuleEntity, TIntf
        where TIntf : IEntity
    {
        private readonly Module module;

        /// <summary>
        /// Default ctor
        /// </summary>
        internal ModuleEntitySet(Entity owner, Module module)
            : base(owner)
        {
            this.module = module;
        }

        /// <summary>
        /// Gets the containing module.
        /// </summary>
        protected virtual Module Module { get { return module; } }

        /// <summary>
        /// The given item has been added to this set.
        /// </summary>
        protected override void OnAdded(T item)
        {
            item.Module = Module;
        }

        /// <summary>
        /// The given item has been removed from this set.
        /// </summary>
        protected override void OnRemoved(T item)
        {
            item.Module = null;
        }
    }

    /// <summary>
    /// Set of module entities.
    /// </summary>
    public class ModuleEntitySet2<T, TIntf> : EntitySet2<T, TIntf>
        where T : ModuleEntity, TIntf, new()
        where TIntf : IEntity
    {
        private readonly Module module;

        /// <summary>
        /// Default ctor
        /// </summary>
        internal ModuleEntitySet2(Module module)
            : base(module)
        {
            this.module = module;
        }

        /// <summary>
        /// Gets the containing module.
        /// </summary>
        protected Module Module { get { return module; } }

        /// <summary>
        /// The given item has been added to this set.
        /// </summary>
        protected override void OnAdded(T item)
        {
            item.Module = module;
        }

        /// <summary>
        /// The given item has been removed from this set.
        /// </summary>
        protected override void OnRemoved(T item)
        {
            item.Module = null;
        }
    }
}

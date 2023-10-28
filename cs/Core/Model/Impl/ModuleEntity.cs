namespace BinkyRailways.Core.Model.Impl
{
    /// <summary>
    /// Base class for entities contained in a module.
    /// </summary>
    public abstract class ModuleEntity : Entity, IModuleEntity
    {
        /// <summary>
        /// Module which contains this entity
        /// </summary>
        internal virtual Module Module { get; set; }

        /// <summary>
        /// Module which contains this entity
        /// </summary>
        IModule IModuleEntity.Module { get { return Module; } }

        /// <summary>
        /// Gets the railway.
        /// </summary>
        protected override Railway Root
        {
            get { return (Module != null) ? (Railway) Module.Package.Railway : null; }
        }

        /// <summary>
        /// Called when a property of this entity has changed.
        /// </summary>
        internal override void OnModified()
        {
            if (Module != null)
            {
                Module.OnModified();
                var root = Root;
                if (root != null)
                {
                    root.OnModified();
                }
            }
        }
    }
}

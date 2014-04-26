namespace BinkyRailways.Core.Model.Impl
{
    /// <summary>
    /// Base class for actions constrained to a module.
    /// </summary>
    public abstract class ModuleAction : Action, IModuleAction
    {
        /// <summary>
        /// Module which contains this entity.
        /// This can be null.
        /// </summary>
        internal Module Module { get { return (Owner != null) ? Container as Module : null; } }        

        /// <summary>
        /// Module which contains this entity
        /// </summary>
        IModule IModuleEntity.Module { get { return Module; } }

        /// <summary>
        /// Called when a property of this entity has changed.
        /// </summary>
        internal override void OnModified()
        {
            if (Module != null)
            {
                Module.OnModified();
            }
        }
    }
}

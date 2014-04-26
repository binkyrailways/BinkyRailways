namespace BinkyRailways.Core.Model
{
    /// <summary>
    /// Implemented by entities that are contained in a module.
    /// </summary>
    public interface IModuleEntity : IEntity
    {
        /// <summary>
        /// Gets the containing module
        /// </summary>
        IModule Module { get; }
    }
}

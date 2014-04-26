namespace BinkyRailways.Core.Model
{
    /// <summary>
    /// Connection between the edges of two modules.
    /// </summary>
    public interface IModuleConnection : IEntity
    {
        /// <summary>
        /// The first module in the connection
        /// </summary>
        IModule ModuleA { get; }

        /// <summary>
        /// Edge of module A
        /// </summary>
        IEdge EdgeA { get; set; }

        /// <summary>
        /// The second module in the connection
        /// </summary>
        IModule ModuleB { get; }

        /// <summary>
        /// Edge of module B
        /// </summary>
        IEdge EdgeB { get; set; }
    }
}

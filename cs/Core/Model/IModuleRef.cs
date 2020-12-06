namespace BinkyRailways.Core.Model
{
    /// <summary>
    /// Reference towards a single module.
    /// </summary>
    public interface IModuleRef : IPersistentEntityRef<IModule>, IPositionedEntity
    {
        /// <summary>
        /// Zoomfactor used in displaying the module (in percentage).
        /// </summary>
        /// <value>100 means 100%</value>
        int ZoomFactor { get; set; }

        /// <summary>
        /// Is this module a reference to the given module?
        /// </summary>
        bool IsReferenceTo(IModule module);
    }
}

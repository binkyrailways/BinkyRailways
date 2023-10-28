namespace BinkyRailways.Core.Model
{
    /// <summary>
    /// Place where tracks split.
    /// </summary>
    public interface IJunction : IPositionedEntity, IModuleEntity
    {
        /// <summary>
        /// The block that this junction belongs to.
        /// When set, this junction is considered lock if the block is locked.
        /// </summary>
        IBlock Block { get; set; }
    }
}

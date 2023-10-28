using BinkyRailways.Core.Model;

namespace BinkyRailways.WinApp.Items
{
    /// <summary>
    /// Item showing a positioned entity
    /// </summary>
    public interface IPositionedEntityItem : IEntityItem
    {
        /// <summary>
        /// Gets the represented entity
        /// </summary>
        new IPositionedEntity Entity { get; }
    }
}

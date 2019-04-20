namespace BinkyRailways.Core.Model
{
    /// <summary>
    /// Group of locs.
    /// </summary>
    public interface ILocGroup : IImportableEntity
    {
        /// <summary>
        /// Set of locs which make up this group.
        /// </summary>
        IEntitySet3<ILoc> Locs { get; }
    }
}

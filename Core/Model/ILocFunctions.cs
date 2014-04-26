namespace BinkyRailways.Core.Model
{
    /// <summary>
    /// List of loc functions.
    /// </summary>
    public interface ILocFunctions : IEntitySet<ILocFunction>
    {
        /// <summary>
        /// Add a function for the given function number.
        /// </summary>
        ILocFunction Add(LocFunction function);
    }
}

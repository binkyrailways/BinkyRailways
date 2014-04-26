namespace BinkyRailways.Core.Model
{
    /// <summary>
    /// Single function of a locomotive.
    /// </summary>
    public interface ILocFunction : IEntity
    {
        /// <summary>
        /// Function number.
        /// </summary>
        LocFunction Function { get; }
    }
}

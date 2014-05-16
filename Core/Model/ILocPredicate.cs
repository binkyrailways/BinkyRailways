namespace BinkyRailways.Core.Model
{
    /// <summary>
    /// Some predicate about locs.
    /// </summary>
    public interface ILocPredicate : IEntity
    {
        /// <summary>
        /// Gets a human readable description of this predicate
        /// </summary>
        new string Description { get; }

        /// <summary>
        /// Create a deep clone.
        /// </summary>
        ILocPredicate Clone(bool setModule);

        /// <summary>
        /// Evaluate this predicate for the given loc.
        /// </summary>
        bool Evaluate(ILoc loc);
    }
}

namespace BinkyRailways.Core.Model
{
    /// <summary>
    /// Predicate that evaluates to true the given loc is equal to the specified loc.
    /// </summary>
    public interface ILocEqualsPredicate : ILocPredicate
    {
        /// <summary>
        /// Gets/Sets the loc to compare to.
        /// </summary>
        ILoc Loc { get; set; }
    }
}

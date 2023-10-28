namespace BinkyRailways.Core.Model
{
    /// <summary>
    /// Predicate that evaluates to true the given loc is part of the specified group.
    /// </summary>
    public interface ILocGroupEqualsPredicate : ILocPredicate
    {
        /// <summary>
        /// Gets/Sets the group to look into.
        /// </summary>
        ILocGroup Group { get; set; }
    }
}

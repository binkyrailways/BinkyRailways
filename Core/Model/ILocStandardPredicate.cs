namespace BinkyRailways.Core.Model
{
    /// <summary>
    /// Predicate that evaluates to true based on includes and excludes predicates.
    /// The predicate evaluates to true if:
    /// - Includes is empty and the excludes predicate for the loc evaluates to false.
    /// - The Includes predicate evaluates to true and the excludes predicate for the loc evaluates to false
    /// </summary>
    public interface ILocStandardPredicate : ILocPredicate
    {
        /// <summary>
        /// Including predicates.
        /// </summary>
        ILocOrPredicate Includes { get; }

        /// <summary>
        /// Excluding predicates.
        /// </summary>
        ILocOrPredicate Excludes { get; }

        /// <summary>
        /// Are both the <see cref="Includes"/> and <see cref="Excludes"/> set empty?
        /// </summary>
        bool IsEmpty { get; }
    }
}

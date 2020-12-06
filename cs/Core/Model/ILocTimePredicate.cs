namespace BinkyRailways.Core.Model
{
    /// <summary>
    /// Predicate that evaluates to true the current (model) time is between <see cref="PeriodStart"/> and <see cref="PeriodEnd"/> (both inclusive).
    /// </summary>
    public interface ILocTimePredicate : ILocPredicate
    {
        /// <summary>
        /// Start of the (valid) period.
        /// </summary>
        Time PeriodStart { get; }

        /// <summary>
        /// End of the (valid) period.
        /// </summary>
        Time PeriodEnd { get; }
    }
}

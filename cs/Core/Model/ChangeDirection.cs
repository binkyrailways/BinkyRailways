namespace BinkyRailways.Core.Model
{
    /// <summary>
    /// Is it allowed / should it be avoided to change direction in a block,
    /// or is it allowed / should is be avoided that a loc changes direction?
    /// </summary>
    public enum ChangeDirection
    {
        /// <summary>
        /// Changing direction is allowed
        /// </summary>
        Allow,

        /// <summary>
        /// Changing direction should be avoided.
        /// </summary>
        Avoid
    }
}

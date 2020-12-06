namespace BinkyRailways.Core.Model
{
    /// <summary>
    /// Junction reference with intended state.
    /// </summary>
    public interface IJunctionWithState : IModuleEntity
    {
        /// <summary>
        /// The junction involved
        /// </summary>
        IJunction Junction { get; }

        /// <summary>
        /// Create a clone of this entity.
        /// Do not clone the junction.
        /// </summary>
        IJunctionWithState Clone();
    }
}

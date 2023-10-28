namespace BinkyRailways.Core.Model
{
    /// <summary>
    /// Action that sets a junction in it's initial state.
    /// </summary>
    public interface IInitializeJunctionAction : IModuleAction
    {
        /// <summary>
        /// Gets/sets the specific loc to use.
        /// When no loc is set, the loc is taken from the context.
        /// </summary>
        IJunction Junction { get; set; }
    }
}

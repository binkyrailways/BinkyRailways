namespace BinkyRailways.Core.Model
{
    /// <summary>
    /// Base class for actions that involve a loc.
    /// </summary>
    public interface ILocAction : IAction
    {
        /// <summary>
        /// Gets/sets the specific loc to use.
        /// When no loc is set, the loc is taken from the context.
        /// </summary>
        ILoc Loc { get; set; }
    }
}

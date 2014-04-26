namespace BinkyRailways.Core.State
{
    /// <summary>
    /// Context for executing actions.
    /// </summary>
    public interface IActionContext 
    {
        /// <summary>
        /// Gets the current loc.
        /// </summary>
        ILocState Loc { get; }
    }
}

namespace BinkyRailways.Core.Model
{
    /// <summary>
    /// Switch reference with intended state.
    /// </summary>
    public interface ISwitchWithState : IJunctionWithState
    {
        /// <summary>
        /// Desired direction
        /// </summary>
        SwitchDirection Direction { get; }
    }
}

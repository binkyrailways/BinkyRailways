namespace BinkyRailways.Core.Model
{
    /// <summary>
    /// Turntable reference with intended state.
    /// </summary>
    public interface ITurnTableWithState : IJunctionWithState
    {
        /// <summary>
        /// Desired position
        /// </summary>
        int Position { get; }
    }
}

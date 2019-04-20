namespace BinkyRailways.Core.Model
{
    /// <summary>
    /// Set of junctions.
    /// Each element may only occur once (if it occurs)
    /// </summary>
    public interface IJunctionSet : IEntitySet<IJunction>
    {
        /// <summary>
        /// Add a new passive junction
        /// </summary>
        IPassiveJunction AddPassiveJunction();

        /// <summary>
        /// Add a new standard switch
        /// </summary>
        ISwitch AddSwitch();

        /// <summary>
        /// Add a new turn table
        /// </summary>
        ITurnTable AddTurnTable();
    }
}

namespace BinkyRailways.Core.Model
{
    /// <summary>
    /// Set of signals.
    /// Each element may only occur once (if it occurs)
    /// </summary>
    public interface ISignalSet : IEntitySet<ISignal>
    {
        /// <summary>
        /// Add a new block signal
        /// </summary>
        IBlockSignal AddNewBlockSignal();
    }
}

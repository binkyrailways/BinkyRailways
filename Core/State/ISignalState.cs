using BinkyRailways.Core.Model;

namespace BinkyRailways.Core.State
{
    /// <summary>
    /// State of a single signal.
    /// </summary>
    public interface ISignalState  : IEntityState<ISignal>
    {
        /// <summary>
        /// Update the output of this signal
        /// </summary>
        void Update();
    }
}

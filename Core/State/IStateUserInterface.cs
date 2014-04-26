namespace BinkyRailways.Core.State
{
    /// <summary>
    /// User interface passed to the railway state
    /// </summary>
    public interface IStateUserInterface
    {
        /// <summary>
        /// The COM port for the given command station is invalid.
        /// Choose a new one.
        /// </summary>
        string ChooseComPortName(ICommandStationState cs);

        /// <summary>
        /// Gets a sound player implementation.
        /// </summary>
        ISoundPlayer SoundPlayer { get; }
    }
}

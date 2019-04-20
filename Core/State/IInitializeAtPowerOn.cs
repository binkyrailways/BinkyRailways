namespace BinkyRailways.Core.State
{
    /// <summary>
    /// Interface implemented by state objects that must be initialized on power on's.
    /// </summary>
    internal interface IInitializeAtPowerOn : IEntityState
    {
        /// <summary>
        /// Used for ordering initialization called.
        /// </summary>
        InitializationPriority Priority { get; }

        /// <summary>
        /// Perform initialization actions.
        /// This method is always called on the dispatcher thread.
        /// </summary>
        void Initialize();
    }
}

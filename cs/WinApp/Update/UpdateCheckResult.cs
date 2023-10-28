namespace BinkyRailways.WinApp.Update
{
    public enum UpdateCheckResult
    {
        /// <summary>
        /// The latest version is being used
        /// </summary>
        Up2Date,

        /// <summary>
        /// An update is available
        /// </summary>
        UpdateAvailable,

        /// <summary>
        /// Update check failed
        /// </summary>
        CheckFailed,
    }
}

namespace BinkyRailways.Import
{
    public enum ImportFilterPriority
    {
        /// <summary>
        /// File format is used by Binky itself.
        /// </summary>
        NativeFileFormat = 0,

        /// <summary>
        /// File format is used by other program
        /// </summary>
        ThirdPartyFileFormat = 1
    }
}

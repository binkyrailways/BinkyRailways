namespace BinkyRailways.Core.Model
{
    /// <summary>
    /// Reference towards a single command station
    /// </summary>
    public interface ICommandStationRef : IPersistentEntityRef<ICommandStation>
    {
        /// <summary>
        /// The names of address spaces served by this command station
        /// </summary>
        /// <remarks>Should never return null</remarks>
        string[] AddressSpaces { get; set; }
    }
}

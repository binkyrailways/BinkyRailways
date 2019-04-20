namespace BinkyRailways.Core.Model
{
    /// <summary>
    /// Ecos type command station
    /// </summary>
    public interface IEcosCommandStation : ICommandStation 
    {
        /// <summary>
        /// Network hostname of the command station
        /// </summary>
        string HostName { get; set; }
    }
}

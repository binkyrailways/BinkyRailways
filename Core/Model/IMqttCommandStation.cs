namespace BinkyRailways.Core.Model
{
    /// <summary>
    /// MQTT type command station
    /// </summary>
    public interface IMqttCommandStation : ICommandStation 
    {
        /// <summary>
        /// Network hostname of the command station
        /// </summary>
        string HostName { get; set; }
    }
}

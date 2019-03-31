namespace BinkyRailways.Core.Model
{
    /// <summary>
    /// MQTT type command station
    /// </summary>
    public interface IBinkyNetCommandStation : ICommandStation 
    {
        /// <summary>
        /// Network hostname of the command station
        /// </summary>
        string HostName { get; set; }
    
        /// <summary>
        /// Network port of the command station
        /// </summary>
        int Port { get; set; }

        /// <summary>
        /// Prefix inserted before topics.
        /// </summary>
        string TopicPrefix { get; set; }

        /// <summary>
        /// TCP Port to run GRPC API on
        /// </summary>
        int APIPort { get; set; }

        /// <summary>
        /// UDP Port to run Discovery broadcasts on
        /// </summary>
        int DiscoveryPort { get; set; }
    }
}

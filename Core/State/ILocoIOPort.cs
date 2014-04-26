namespace BinkyRailways.Core.State
{
    /// <summary>
    /// Single port of LocoIO device.
    /// </summary>
    public interface ILocoIOPort
    {
        /// <summary>
        /// Number of this port [1..16]
        /// </summary>
        int PortNr { get; }

        /// <summary>
        /// Configuration of the port.
        /// Human readable description.
        /// </summary>
        string Configuration { get; }

        /// <summary>
        /// Configured address 
        /// </summary>
        int Address { get; }
    }
}

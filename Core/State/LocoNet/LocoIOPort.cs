using LocoNetToolBox.Devices.LocoIO;

namespace BinkyRailways.Core.State.LocoNet
{
    /// <summary>
    /// Single port of LocoIO device.
    /// </summary>
    internal sealed class LocoIOPort : ILocoIOPort
    {
        /// <summary>
        /// Default ctor
        /// </summary>
        internal LocoIOPort(int portNr, PinConfig config)
        {
            PortNr = portNr;
            var mode = config.Mode;
            Configuration = (mode != null) ? mode.Name : "?";
            Address = (mode != null) ? config.Address : 0;
        }

        /// <summary>
        /// Number of this port [1..16]
        /// </summary>
        public int PortNr { get; private set; }

        /// <summary>
        /// Configuration of the port.
        /// Human readable description.
        /// </summary>
        public string Configuration { get; private set; }

        /// <summary>
        /// Configured address 
        /// </summary>
        public int Address { get; private set; }
    }
}

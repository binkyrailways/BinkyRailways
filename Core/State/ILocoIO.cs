using System.Collections.Generic;
using LocoNetToolBox.Protocol;

namespace BinkyRailways.Core.State
{
    /// <summary>
    /// Single LocoIO device.
    /// </summary>
    public interface ILocoIO
    {
        /// <summary>
        /// Address of the device.
        /// </summary>
        LocoNetAddress Address { get; }

        /// <summary>
        /// Firmware version
        /// </summary>
        string Version { get; }

        /// <summary>
        /// Read the port configurations.
        /// </summary>
        IEnumerable<ILocoIOPort> ReadPorts();
    }
}

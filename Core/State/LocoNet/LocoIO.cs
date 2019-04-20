using System.Collections.Generic;
using LocoNetToolBox.Devices.LocoIO;
using LocoNetToolBox.Protocol;

namespace BinkyRailways.Core.State.LocoNet
{
    /// <summary>
    /// Single LocoIO device.
    /// </summary>
    internal sealed class LocoIO : ILocoIO
    {
        private readonly LocoBuffer lb;

        /// <summary>
        /// Default ctor
        /// </summary>
        internal LocoIO(PeerXferResponse response, LocoBuffer lb)
        {
            this.lb = lb;
            Address = response.Source;
            var version = response.LocoIOVersion;
            Version = string.Format("{0}.{1}", version / 100, version % 100); 
        }

        /// <summary>
        /// Address of the device.
        /// </summary>
        public LocoNetAddress Address { get; private set; }

        /// <summary>
        /// Firmware version
        /// </summary>
        public string Version { get; private set; }

        /// <summary>
        /// Read the port configurations.
        /// </summary>
        public IEnumerable<ILocoIOPort> ReadPorts()
        {
            var programmer = new Programmer(Address);
            var config = new LocoIOConfig();
            programmer.Read(lb, config);
            for (int i = 0; i < 16; i++)
            {
                yield return new LocoIOPort(i+1, config.Pins[i]);
            }
        }
    }
}

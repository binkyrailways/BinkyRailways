using System.Collections.Generic;

namespace LocoNetToolBox.Devices.LocoIO
{
    /// <summary>
    /// Config setting for a specific connector
    /// </summary>
    public sealed class ConnectorConfig : IEnumerable<SVConfig>
    {
        private readonly PinConfigList pins;

        /// <summary>
        /// Default ctor
        /// </summary>
        internal ConnectorConfig(PinConfig[] pins)
        {
            this.pins = new PinConfigList(pins);
        }

        /// <summary>
        /// Gets all pins on this connector
        /// </summary>
        public PinConfigList Pins
        {
            get { return pins; }
        }

        /// <summary>
        /// Enumerate SV's of pins on this connector
        /// </summary>
        public IEnumerator<SVConfig> GetEnumerator()
        {
            foreach (var pin in pins)
            {
                foreach (var config in pin)
                {
                    yield return config;
                }
            }
        }

        /// <summary>
        /// Enumerate SV's of pins on this connector
        /// </summary>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}

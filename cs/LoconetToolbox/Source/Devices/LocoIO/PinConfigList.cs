using System.Collections.Generic;
using System.Linq;

namespace LocoNetToolBox.Devices.LocoIO
{
    /// <summary>
    /// List of pin configurations
    /// </summary>
    public class PinConfigList : IEnumerable<PinConfig>
    {
        private readonly PinConfig[] pins;

        /// <summary>
        /// Default ctor
        /// </summary>
        internal PinConfigList(PinConfig[] pins)
        {
            this.pins = pins;
        }

        /// <summary>
        /// Gets the number of pins
        /// </summary>
        public int Count
        {
            get { return pins.Length; }
        }

        /// <summary>
        /// Gets the pins by index (0..)
        /// </summary>
        public PinConfig this[int index] 
        {
            get { return pins[index]; }
        }

        /// <summary>
        /// Enumerate SV's of pins on this connector
        /// </summary>
        public IEnumerator<PinConfig> GetEnumerator()
        {
            return pins.Cast<PinConfig>().GetEnumerator();
        }

        /// <summary>
        /// Enumerate SV's of pins on this connector
        /// </summary>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// Enumerate SV's of pins on this connector
        /// </summary>
        public IEnumerable<SVConfig> GetSVConfigs()
        {
            return pins.SelectMany(pin => pin);
        }
    }
}

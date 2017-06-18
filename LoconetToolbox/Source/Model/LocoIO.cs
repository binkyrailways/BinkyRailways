using LocoNetToolBox.Protocol;

namespace LocoNetToolBox.Model
{
    /// <summary>
    /// Maintains address and version of 1 loco-IO unit found on the network.
    /// </summary>
    public class LocoIO
    {
        private readonly LocoNetAddress address;
        private readonly int version;

        /// <summary>
        /// Default ctor
        /// </summary>
        public LocoIO(LocoNetAddress address, int version)
        {
            this.address = address;
            this.version = version;
        }

        public string Version
        {
            get { return string.Format("{0}.{1}", version / 100, version % 100); }
        }

        public LocoNetAddress Address
        {
            get { return address; }
        }
    }
}

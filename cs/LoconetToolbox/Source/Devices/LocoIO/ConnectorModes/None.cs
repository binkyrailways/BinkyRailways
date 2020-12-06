namespace LocoNetToolBox.Devices.LocoIO.ConnectorModes
{
    /// <summary>
    /// Connector mode for an "none".
    /// </summary>
    public sealed class None : ConnectorMode
    {
        /// <summary>
        /// Default ctor
        /// </summary>
        public None()
            : base("None",
                0, new string[0])
        {
        }

        /// <summary>
        /// Use this mode to configure the given target.
        /// </summary>
        protected override void Configure(ConnectorConfig target, AddressList addresses, int subMode)
        {
        }
    }
}

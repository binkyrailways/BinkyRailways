namespace LocoNetToolBox.Devices.LocoIO.ConnectorModes
{
    /// <summary>
    /// Connector mode for an un-used connector.
    /// </summary>
    public sealed class NotUsed : ConnectorMode
    {
        /// <summary>
        /// Default ctor
        /// </summary>
        public NotUsed()
            : base("Not in use",
                0,
                new string[0],
                PinMode.BlockOccupied,
                PinMode.BlockOccupied,
                PinMode.BlockOccupied,
                PinMode.BlockOccupied,
                PinMode.BlockOccupied,
                PinMode.BlockOccupied,
                PinMode.BlockOccupied,
                PinMode.BlockOccupied)
        {
        }

        /// <summary>
        /// Use this mode to configure the given target.
        /// </summary>
        protected override void Configure(ConnectorConfig target, AddressList addresses, int subMode)
        {
            for (int i = 0; i < 8; i++)
            {
                var pin = target.Pins[i];
                pin.Mode = GetPin(i);
                pin.Address = 2048;
            }
        }
    }
}

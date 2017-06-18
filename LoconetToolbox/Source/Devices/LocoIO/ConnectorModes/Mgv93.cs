namespace LocoNetToolBox.Devices.LocoIO.ConnectorModes
{
    /// <summary>
    /// Connector mode for an MGV93.
    /// </summary>
    public sealed class Mgv93 : ConnectorMode
    {
        /// <summary>
        /// Default ctor
        /// </summary>
        public Mgv93()
            : base("MGV93",
                8,
                new[] {
                    "Feedback 1",
                    "Feedback 2",
                    "Feedback 3",
                    "Feedback 4",
                    "Feedback 5",
                    "Feedback 6",
                    "Feedback 7",
                    "Feedback 8",
                },
                PinMode.BlockActiveLowDelay,
                PinMode.BlockActiveLowDelay,
                PinMode.BlockActiveLowDelay,
                PinMode.BlockActiveLowDelay,
                PinMode.BlockActiveLowDelay,
                PinMode.BlockActiveLowDelay,
                PinMode.BlockActiveLowDelay,
                PinMode.BlockActiveLowDelay)
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
                pin.Address = addresses[i];
            }
        }
    }
}

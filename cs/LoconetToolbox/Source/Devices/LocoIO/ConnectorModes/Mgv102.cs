namespace LocoNetToolBox.Devices.LocoIO.ConnectorModes
{
    /// <summary>
    /// Connector mode for an MGV102.
    /// </summary>
    public sealed class Mgv102 : ConnectorMode
    {
        /// <summary>
        /// Default ctor
        /// </summary>
        public Mgv102()
            : base("MGV102",
                6,
                new[] {
                    "Feedback X",
                    "Feedback Y",
                    "Feedback A",
                    "Feedback B",
                    "Feedback C",
                    "Conflict",
                },
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
            for (int i = 0; i < 5; i++)
            {
                var pin = target.Pins[i];
                pin.Mode = GetPin(i);
                pin.Address = addresses[i];
            }
        }
    }
}

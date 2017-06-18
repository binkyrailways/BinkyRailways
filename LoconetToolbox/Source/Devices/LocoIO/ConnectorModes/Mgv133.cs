namespace LocoNetToolBox.Devices.LocoIO.ConnectorModes
{
    /// <summary>
    /// Connector mode for an MGV133 with sensors connected to pin 1-4 or 5-8.
    /// </summary>
    public sealed class Mgv133 : ConnectorMode
    {
        /// <summary>
        /// Default ctor
        /// </summary>
        public Mgv133()
            : base("MGV133",
                4,
                new[] {
                    "Sensor 1",
                    "Sensor 2",
                    "Sensor 3",
                    "Sensor 4",
                },
                new[] {
                    "Pins 1-4",
                    "Pins 5-8"
                    },
                PinMode.BlockActiveLowDelay,
                PinMode.BlockActiveLowDelay,
                PinMode.BlockActiveLowDelay,
                PinMode.BlockActiveLowDelay)
        {
        }

        /// <summary>
        /// Gets the offset added to each pin number for the given submode.
        /// </summary>
        public override int GetPinOffset(int subMode)
        {
            return (subMode == 0) ? 0 : 4;
        }

        /// <summary>
        /// Use this mode to configure the given target.
        /// </summary>
        protected override void Configure(ConnectorConfig target, AddressList addresses, int subMode)
        {
            for (int i = 0; i < 4; i++)
            {
                var pin = target.Pins[i];
                pin.Mode = GetPin(i);
                pin.Address = addresses[i];
            }
        }
    }
}

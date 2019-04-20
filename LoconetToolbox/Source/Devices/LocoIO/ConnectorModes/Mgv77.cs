namespace LocoNetToolBox.Devices.LocoIO.ConnectorModes
{
    /// <summary>
    /// Connector mode for an MGV77.
    /// </summary>
    public sealed class Mgv77 : ConnectorMode
    {
        /// <summary>
        /// Default ctor
        /// </summary>
        public Mgv77()
            : base("MGV77",
                4,
                new[] {
                    "Switch 1",
                    "Switch 2",
                    "Switch 3",
                    "Switch 4",
                },
                PinMode.PulseFirmwareSingle,
                PinMode.PulseFirmwarePaired,
                PinMode.PulseFirmwareSingle,
                PinMode.PulseFirmwarePaired,
                PinMode.PulseFirmwareSingle,
                PinMode.PulseFirmwarePaired,
                PinMode.PulseFirmwareSingle,
                PinMode.PulseFirmwarePaired)
        {
        }

        /// <summary>
        /// Use this mode to configure the given target.
        /// </summary>
        protected override void Configure(ConnectorConfig target, AddressList addresses, int subMode)
        {
            for (int i = 0; i < 4; i++)
            {
                var pin = target.Pins[i * 2];
                pin.Mode = GetPin(i * 2);
                pin.Address = addresses[i];

                pin = target.Pins[i * 2 + 1];
                pin.Mode = GetPin(i * 2 + 1);
                pin.Address = addresses[i];
            }
        }
    }
}

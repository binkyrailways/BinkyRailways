namespace LocoNetToolBox.Devices.LocoIO.ConnectorModes
{
    /// <summary>
    /// Connector mode for an MGV81 v1.
    /// </summary>
    public sealed class Mgv81V1 : ConnectorMode
    {
        /// <summary>
        /// Default ctor
        /// </summary>
        public Mgv81V1()
            : base("MGV81 v1",
                4,
                new[] {
                    "Servo 1",
                    "Servo 2",
                    "Servo 3",
                    "Servo 4",
                },
                PinMode.SteadyStatePairedOff,
                PinMode.SteadyStatePairedOff,
                PinMode.SteadyStatePairedOff,
                PinMode.SteadyStatePairedOff)
        {
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

namespace LocoNetToolBox.Devices.LocoIO.ConnectorModes
{
    /// <summary>
    /// Connector mode for an MGV145 Turntable/FY controller (J6).
    /// </summary>
    public sealed class Mgv145J6 : ConnectorMode
    {
        /// <summary>
        /// Default ctor
        /// </summary>
        public Mgv145J6()
            : base("MGV145 J6",
                6,
                new[] {
                    "Feedback 1st section",
                    "Feedback 2nd section",
                    "Feedback 3rd section",
                    "Feedback 4th section",
                    "Forward sign",
                    "Reverse sign",
                },
                PinMode.BlockActiveLowDelay,
                PinMode.BlockActiveLowDelay,
                PinMode.BlockActiveLowDelay,
                PinMode.BlockActiveLowDelay,
                PinMode.SteadyStateSingleOff,
                PinMode.SteadyStateSingleOff)
        {
        }

        /// <summary>
        /// Use this mode to configure the given target.
        /// </summary>
        protected override void Configure(ConnectorConfig target, AddressList addresses, int subMode)
        {
            for (int i = 0; i < 6; i++)
            {
                var pin = target.Pins[i];
                pin.Mode = GetPin(i);
                pin.Address = addresses[i];
            }
        }
    }
}

namespace LocoNetToolBox.Devices.LocoIO.ConnectorModes
{
    /// <summary>
    /// Connector mode for an MGV145 Turntable/FY controller (J5).
    /// </summary>
    public sealed class Mgv145J5 : ConnectorMode
    {
        /// <summary>
        /// Default ctor
        /// </summary>
        public Mgv145J5()
            : base("MGV145 J5",
                8,
                new[] {
                    "Position command bit 0",
                    "Position command bit 1",
                    "Position command bit 2",
                    "Position command bit 3",
                    "Position command bit 4",
                    "Position command bit 5",
                    "New position flag",
                    "Position match",
                },
                PinMode.SteadyStateSingleOff,
                PinMode.SteadyStateSingleOff,
                PinMode.SteadyStateSingleOff,
                PinMode.SteadyStateSingleOff,
                PinMode.SteadyStateSingleOff,
                PinMode.SteadyStateSingleOff,
                PinMode.SteadyStateSingleOff,
                PinMode.TurnoutFeedbackSingle)
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

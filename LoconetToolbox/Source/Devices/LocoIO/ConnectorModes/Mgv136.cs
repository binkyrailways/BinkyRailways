namespace LocoNetToolBox.Devices.LocoIO.ConnectorModes
{
    /// <summary>
    /// Connector mode for an MGV81 v1.
    /// </summary>
    public sealed class Mgv136 : ConnectorMode
    {
        /// <summary>
        /// Default ctor
        /// </summary>
        public Mgv136()
            : base("MGV136, MGV84, MGV81 v1.2",
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
                PinMode.SteadyStatePairedOff,
                PinMode.TurnoutFeedbackSingle,
                PinMode.TurnoutFeedbackSingle,
                PinMode.TurnoutFeedbackSingle,
                PinMode.TurnoutFeedbackSingle)
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

                pin = target.Pins[4 + i];
                pin.Mode = GetPin(4 + i);
                pin.Address = addresses[i];
            }
        }
    }
}

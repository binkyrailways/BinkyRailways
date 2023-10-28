namespace LocoNetToolBox.Devices.LocoIO.ConnectorModes
{
    /// <summary>
    /// Connector mode for an BS-1 used for led signals.
    /// </summary>
    public sealed class Bs1ForSignals : ConnectorMode
    {
        /// <summary>
        /// Default ctor
        /// </summary>
        public Bs1ForSignals()
            : base("BS-1 for signals",
                2,
                new[] {
                    "Signal 1",
                    "Signal 2",
                },
                new[] {
                    "Pin 1, 2",
                    "Pin 3, 4",
                    "Pin 5, 6",
                    "Pin 7, 8",
                },
                PinMode.SteadyStateSingleOff,
                PinMode.SteadyStateSingleOff)
        {
        }

        /// <summary>
        /// Gets the offset added to each pin number for the given submode.
        /// </summary>
        public override int GetPinOffset(int subMode)
        {
            return subMode * 2;
        }

        /// <summary>
        /// Use this mode to configure the given target.
        /// </summary>
        protected override void Configure(ConnectorConfig target, AddressList addresses, int subMode)
        {
            for (int i = 0; i < 2; i++)
            {
                var pin = target.Pins[i];
                pin.Mode = GetPin(i);
                pin.Address = addresses[i];
            }
        }
    }
}

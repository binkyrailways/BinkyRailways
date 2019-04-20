namespace BinkyRailways.Protocols.P50x
{
    public class XStatus
    {
        // set if voltage regulation (N scale) is enabled
        public bool VReg { get; internal set; }
        // set if an external I2C Central Unit is present
        public bool ExtCU { get; internal set; }
        // set if in Halt mode (Lok's stopped, Pwr On)
        public bool Halt { get; internal set; }
        // set if we are in Power On
        public bool Pwr { get; internal set; }
        // Overheating condition detected
        public bool Hot { get; internal set; }
        // set if a [Go] key on an external I2C device is currently pressed
        public bool Go { get; internal set; }
        // set if a [Stop] key on an external I2C device is currently pressed
        public bool Stop { get; internal set; }
    }
}
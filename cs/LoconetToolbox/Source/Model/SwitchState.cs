namespace LocoNetToolBox.Model
{
    /// <summary>
    /// Maintains the state of a single switch.
    /// </summary>
    public class SwitchState : AddressState
    {
        /// <summary>
        /// Default ctor
        /// </summary>
        public SwitchState(int address)
            : base(address)
        {
        }

        /// <summary>
        /// Current direction
        /// </summary>
        public bool Direction { get; set; }
    }
}

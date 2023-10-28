namespace LocoNetToolBox.Model
{
    /// <summary>
    /// Maintains the state of a single address.
    /// </summary>
    public abstract class AddressState
    {
        private readonly int address;

        /// <summary>
        /// Default ctor
        /// </summary>
        protected AddressState(int address)
        {
            this.address = address;
        }

        /// <summary>
        /// Gets the loconet address.
        /// </summary>
        public int Address
        {
            get { return address; }
        }
    }
}

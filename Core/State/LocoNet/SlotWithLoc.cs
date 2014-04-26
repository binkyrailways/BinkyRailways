namespace BinkyRailways.Core.State.LocoNet
{
    internal sealed class SlotWithLoc
    {
        private readonly Slot slot;
        private readonly ILocState loc;

        /// <summary>
        /// Default ctor
        /// </summary>
        public SlotWithLoc(Slot slot, ILocState loc)
        {
            this.slot = slot;
            this.loc = loc;
        }

        /// <summary>
        /// Gets the loc that is using the slot.
        /// </summary>
        public ILocState Loc
        {
            get { return loc; }
        }

        /// <summary>
        /// Gets the slot
        /// </summary>
        public Slot Slot
        {
            get { return slot; }
        }
    }
}

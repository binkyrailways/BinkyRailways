using System;
using System.Linq;

namespace BinkyRailways.Core.State.LocoNet
{
    internal sealed class LocSlotMap
    {
        private readonly SlotWithLoc[] slots = new SlotWithLoc[128];

        /// <summary>
        /// Gets/sets the slot for the given loc.
        /// </summary>
        /// <returns>Null if not found</returns>
        public Slot this[ILocState loc]
        {
            get
            {
                if (loc == null)
                    throw new ArgumentNullException("loc");
                var entry = slots.FirstOrDefault(x => (x != null) && (x.Loc == loc));
                return (entry != null) ? entry.Slot : null;
            }
            set
            {
                if (value == null)
                {
                    for (var i = 0; i < slots.Length; i++)
                    {
                        if ((slots[i] != null) && (slots[i].Loc == loc))
                        {
                            slots[i] = null;
                        }
                    }
                }
                else
                {
                    slots[value.SlotNumber] = new SlotWithLoc(value, loc);
                }
            }
        }

        /// <summary>
        /// Gets a slot by its slot number
        /// </summary>
        /// <returns>Null if not found</returns>
        public Slot this[int slotNumber]
        {
            get
            {
                var entry = slots[slotNumber];
                return (entry != null) ? entry.Slot : null;
            }
        }

        /// <summary>
        /// Gets the loc that belongs to the given slot number.
        /// </summary>
        /// <returns>Null if not found</returns>
        public ILocState GetLoc(int slotNumber)
        {
            var entry = slots[slotNumber];
            return (entry != null) ? entry.Loc : null;
        }

        /// <summary>
        /// Remove the given slot.
        /// </summary>
        public void Remove(Slot slot)
        {
            if (slot != null)
            {
                slots[slot.SlotNumber] = null;
            }
        }
    }
}

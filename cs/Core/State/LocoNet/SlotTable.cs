using System;
using System.Collections;
using System.Collections.Generic;

namespace BinkyRailways.Core.State.LocoNet
{
    /// <summary>
    /// Master table of slots
    /// </summary>
    public class SlotTable : IEnumerable<Slot>
    {
        private readonly Slot[] slots;

        /// <summary>
        /// Default ctor
        /// </summary>
        public SlotTable()
        {
            slots = new Slot[128];
        }

        /// <summary>
        /// Gets / sets a specific slot.
        /// </summary>
        protected Slot this[int slotNumber]
        {
            get { return slots[slotNumber]; }
            set
            {
                if (value != null)
                {
                    if (value.SlotNumber != slotNumber)
                    {
                        throw new ArgumentException("Invalid slot number");
                    }
                }
                slots[slotNumber] = value;
            }
        }

        /// <summary>
        /// Gets a slot by number
        /// </summary>
        public Slot FindBySlotNumber(int slotNumber, bool create, int address)
        {
            var slot = slots[slotNumber];
            if (slot != null)
                return slot;
            if ((slotNumber >= 1) && (slotNumber < 120) && create)
            {
                slot = CreateSlot(slotNumber);
                slot.Address = address;
                slots[slotNumber] = slot;
                return slot;
            }
            return null;
        }

        /// <summary>
        /// Look for a matching slot by address.
        /// If asked, a new slot will be created when not matching slot is found.
        /// </summary>
        /// <returns>Null if not found</returns>
        public Slot FindByAddress(int address, bool create)
        {
            var firstFree = -1;
            for (int i = 1; i < 120; i++)
            {
                var slot = slots[i];
                if ((slot != null) && (slot.Address == address))
                    return slot;
                if ((slot == null) && (firstFree < 0))
                {
                    firstFree = i;
                }
            }
            // Not found
            if ((create) && (firstFree > 0))
            {
                var slot = CreateSlot(firstFree);
                slot.Address = address;
                slots[firstFree] = slot;
                return slot;
            }
            return null;
        }

        /// <summary>
        /// Remove all slots that have not been used for the given delay.
        /// </summary>
        public void Purge(TimeSpan purgeDelay)
        {
            var now = DateTime.Now;
            for (int i = 1; i < 120; i++)
            {
                var slot = slots[i];
                if ((slot != null) && (now.Subtract(slot.LastUsed) > purgeDelay))
                {
                    // Not used recently, cleanup
                    slots[i] = null;
                }
            }
        }

        /// <summary>
        /// Create a new slot for the given address.
        /// </summary>
        protected virtual Slot CreateSlot(int slotNumber)
        {
            return new Slot(slotNumber);
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// All in-use slots are returned.
        /// </summary>
        public IEnumerator<Slot> GetEnumerator()
        {
            for (int i = 1; i < 120; i++)
            {
                var slot = slots[i];
                if (slot != null)
                    yield return slot;
            }
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}

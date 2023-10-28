using System;
using System.Collections.Generic;

namespace LocoNetToolBox.Model
{
    /// <summary>
    /// Maintains the state of all inputs and switches in the loconet.
    /// </summary>
    public class AddressStateMap<T>
    {
        private readonly Dictionary<int, T> items = new Dictionary<int, T>();
        private readonly object dataLock = new object();

        /// <summary>
        /// Get or create a state item.
        /// </summary>
        public T GetOrCreateItem(int address, Func<int, T> creator)
        {
            lock (dataLock)
            {
                T result;
                if (items.TryGetValue(address, out result))
                    return result;
                result = creator(address);
                items[address] = result;
                return result;
            }
        }
    }
}

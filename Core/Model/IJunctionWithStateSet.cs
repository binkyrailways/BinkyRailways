using System.Collections.Generic;

namespace BinkyRailways.Core.Model
{
    /// <summary>
    /// Set of junction with state elements.
    /// Each junction may only occur once (if it occurs)
    /// </summary>
    public interface IJunctionWithStateSet : IEnumerable<IJunctionWithState>
    {
        /// <summary>
        /// Gets the number of elements
        /// </summary>
        int Count { get; }

        /// <summary>
        /// Add the given item to this set
        /// </summary>
        void Add(IPassiveJunction item);

        /// <summary>
        /// Add the given item to this set
        /// </summary>
        void Add(ISwitch item, SwitchDirection direction);

        /// <summary>
        /// Add the given item to this set
        /// </summary>
        void Add(ITurnTable item, int position);

        /// <summary>
        /// Remove the given item from this set.
        /// </summary>
        /// <returns>True if it was removed, false otherwise</returns>
        bool Remove(IJunction item);

        /// <summary>
        /// Remove all
        /// </summary>
        void Clear();

        /// <summary>
        /// Does this set contain the given junction with some state?
        /// </summary>
        bool Contains(IJunction item);

        /// <summary>
        /// Copy (clone) all my entries into the given destination.
        /// </summary>
        void CopyTo(IJunctionWithStateSet destination);
    }
}

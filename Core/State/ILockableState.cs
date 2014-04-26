using System;

namespace BinkyRailways.Core.State
{
    /// <summary>
    /// State that can be locked by a locomotive
    /// </summary>
    public interface ILockableState : IEntityState
    {
        /// <summary>
        /// Gets the locomotive that has this state locked.
        /// Returns null if this state is not locked.
        /// </summary>
        ILocState LockedBy { get; }

        /// <summary>
        /// Can this state be locked by the intended owner?
        /// Return true is this entity and all underlying entities are not locked.
        /// </summary>
        bool CanLock(ILocState owner);

        /// <summary>
        /// Lock this state by the given owner.
        /// Also lock all underlying entities.
        /// </summary>
        void Lock(ILocState owner);

        /// <summary>
        /// Unlock this state from the given owner.
        /// Also unlock all underlying entities except those where the given predicate returns true.
        /// </summary>
        void Unlock(Predicate<ILockableState> exclusion);
    }
}

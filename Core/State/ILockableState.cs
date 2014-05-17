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
        bool CanLock(ILocState owner, out ILocState lockedBy);

        /// <summary>
        /// Lock this state by the given owner.
        /// Also lock all underlying entities.
        /// </summary>
        void Lock(ILocState owner);

        /// <summary>
        /// Unlock this state from the given owner.
        /// Also unlock all underlying entities except the given exclusion and the underlying entities of the given exclusion.
        /// </summary>
        void Unlock(ILockableState exclusion);
    }
}

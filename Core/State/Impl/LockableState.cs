using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using BinkyRailways.Core.Model;

namespace BinkyRailways.Core.State.Impl
{
    /// <summary>
    /// State of a single entity that can be locked by a locomotive.
    /// </summary>
    public abstract class LockableState<T> : EntityState<T>, ILockableState
        where T : class, IEntity
    {
        private LocState lockedBy;

        /// <summary>
        /// Default ctor
        /// </summary>
        protected LockableState(T entity, RailwayState railwayState)
            : base(entity, railwayState)
        {
        }

        /// <summary>
        /// Gets the locomotive that has this state locked.
        /// Returns null if this state is not locked.
        /// </summary>
        [DisplayName(@"Locked by")]
        public ILocState LockedBy { get { return lockedBy; } }

        /// <summary>
        /// Can this state be locked by the intended owner?
        /// Return true is this entity and all underlying entities are not locked.
        /// </summary>
        public bool CanLock(ILocState owner)
        {
            if ((!IsReadyForUse) || (lockedBy != null))
                return false;
            return UnderlyingLockableEntities.All(item => CanLockUnderlyingEntity(item, owner));
        }

        /// <summary>
        /// Can the given underlying entity be locked by the intended owner?
        /// </summary>
        protected virtual bool CanLockUnderlyingEntity(ILockableState entity, ILocState owner)
        {
            return entity.CanLock(owner);
        }

        /// <summary>
        /// Lock this state by the given owner.
        /// Also lock all underlying entities.
        /// </summary>
        public void Lock(ILocState owner)
        {
            if (owner == null)
                throw new ArgumentNullException("owner");

            // Lock if not already locked
            var lockedNow = new List<ILockableState>();
            if (owner != lockedBy)
            {
                var locState = (LocState) owner;
                if (lockedBy != null)
                    throw new LockException("Entity already locked");
                // Lock myself
                lockedBy = locState;
                locState.AddLockedEntity(this);
                lockedNow.Add(this);
            }

            // Lock underlying entities);
            foreach (var state in UnderlyingLockableEntities)
            {
                try
                {
                    lockedNow.Add(state);
                    state.Lock(owner);
                }
                catch 
                {
                    // Underlying lock failed, unlock everything
                    foreach (var x in lockedNow)
                    {
                        x.Unlock(null);
                    }
                    throw;
                }
            }
            OnActualStateChanged();
        }

        /// <summary>
        /// Unlock this state from the given owner.
        /// Also unlock all underlying entities except those where the given predicate returns true.
        /// </summary>
        public void Unlock(Predicate<ILockableState> exclusion)
        {
            if (lockedBy != null)
            {
                // Unlock underlying entities
                foreach (var item in UnderlyingLockableEntities)
                {
                    item.Unlock(exclusion);        
                }
                // Unlock me
                if ((exclusion == null) || !exclusion(this))
                {
                    lockedBy.RemoveLockedEntity(this);
                    lockedBy = null;
                }
                OnActualStateChanged();
                AfterUnlock();
            }
        }

        /// <summary>
        /// Called when this object is unlocked.
        /// </summary>
        protected virtual void AfterUnlock()
        {
            // Override me
        }

        /// <summary>
        /// Gets all entities that must be locked in order to lock me.
        /// </summary>
        protected abstract IEnumerable<ILockableState> UnderlyingLockableEntities { get; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using BinkyRailways.Core.Model;
using Newtonsoft.Json;

namespace BinkyRailways.Core.State.Impl
{
    /// <summary>
    /// State of a single entity that can be locked by a locomotive.
    /// </summary>
    public abstract class LockableState<T> : EntityState<T>, ILockableStateImpl
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
        [JsonIgnore]
        public ILocState LockedBy { get { return lockedBy; } }

        /// <summary>
        /// Can this state be locked by the intended owner?
        /// Return true is this entity and all underlying entities are not locked.
        /// </summary>
        public bool CanLock(ILocState owner, out ILocState lockedBy)
        {
            if ((!IsReadyForUse) || ((this.lockedBy != null) && (this.lockedBy != owner)))
            {
                lockedBy = this.lockedBy;
                return false;
            }
            foreach (var item in UnderlyingLockableEntities)
            {
                if (!CanLockUnderlyingEntity(item, owner, out lockedBy))
                {
                    return false;
                }
            }
            lockedBy = null;
            return true;
        }

        /// <summary>
        /// Can the given underlying entity be locked by the intended owner?
        /// </summary>
        protected virtual bool CanLockUnderlyingEntity(ILockableState entity, ILocState owner, out ILocState lockedBy)
        {
            return entity.CanLock(owner, out lockedBy);
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
        public void Unlock(ILockableState exclusion)
        {
            if (lockedBy != null)
            {
                // Unlock underlying entities
                foreach (var item in UnderlyingLockableEntities)
                {
                    item.Unlock(exclusion);        
                }
                // Unlock me
                if (!ExcludeMe(exclusion))
                {
                    lockedBy.RemoveLockedEntity(this);
                    lockedBy = null;
                }
                OnActualStateChanged();
                AfterUnlock();
            }
        }

        /// <summary>
        /// Should this state be excluded given the exclusion?
        /// </summary>
        private bool ExcludeMe(ILockableState exclusion)
        {
            if (exclusion == null) return false;
            if (exclusion == this) return true;
            var impl = exclusion as ILockableStateImpl;
            return (impl != null) && impl.UnderlyingLockableEntities.Contains(this);
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
        protected abstract IEnumerable<ILockableStateImpl> UnderlyingLockableEntities { get; }

        /// <summary>
        /// Gets all entities that must be locked in order to lock me.
        /// </summary>
        IEnumerable<ILockableStateImpl> ILockableStateImpl.UnderlyingLockableEntities
        {
            get { return this.UnderlyingLockableEntities; }
        }        
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace BinkyRailways.Core.Model.Impl
{
    /// <summary>
    /// Set of entities.
    /// Each element may only occur once (if it occurs)
    /// Each element is stored by it's content in XML.
    /// </summary>
    public abstract class EntitySet<T, TIntf> : IEntitySet<T>
        where T : class, TIntf, IEntityInternals
        where TIntf : IEntity
    {
        private readonly Dictionary<string, T> entries = new Dictionary<string, T>();
        private SetImpl setImpl;
        private readonly Entity owner;

        /// <summary>
        /// Default ctor
        /// </summary>
        internal EntitySet(Entity owner)
        {
            if (owner == null)
            {
                throw new ArgumentNullException("owner");
            }
            this.owner = owner;
        }

        /// <summary>
        /// Implementation of IEntitySet
        /// </summary>
        public IEntitySet<TIntf> Set
        {
            get { return setImpl ?? (setImpl = CreateSetImpl()); }
        }

        /// <summary>
        /// Gets the number of elements
        /// </summary>
        public int Count { get { return entries.Count; } }

        /// <summary>
        /// Is this set empty?
        /// </summary>
        public bool IsEmpty { get { return (entries.Count == 0); } }

        /// <summary>
        /// Gets the underlying entries.
        /// </summary>
        protected Dictionary<string, T> Entries { get { return entries; } } 

        /// <summary>
        /// Lookup by id.
        /// </summary>
        public T this[string id]
        {
            get
            {
                T item;
                return TryGetValue(id, out item) ? item : default(T);
            }
        }

        /// <summary>
        /// Lookup by id.
        /// </summary>
        protected virtual bool TryGetValue(string id, out T item)
        {
            return entries.TryGetValue(id, out item);
        }

        /// <summary>
        /// Does this set contain the given item?
        /// </summary>
        public bool Contains(T item)
        {
            return (item != null) && entries.ContainsKey(item.Id);
        }

        /// <summary>
        /// Does this set contain an item with the given id?
        /// </summary>
        public bool ContainsId(string id)
        {
            return entries.ContainsKey(id);
        }

        /// <summary>
        /// Serialization add.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void Add(object item)
        {
            Add((T)item);
        }

        /// <summary>
        /// Add the given item to this set
        /// </summary>
        public void Add(T item)
        {
            item.EnsureId();
            var key = item.Id;
            if (!entries.ContainsKey(key))
            {
                OnAdding(item);
                entries[key] = item;
                OnAdded(item);
                owner.OnModified();
            }
        }

        /// <summary>
        /// Remove the given item from this set.
        /// </summary>
        /// <returns>True if it was removed, false otherwise</returns>
        public bool Remove(T item)
        {
            if (!entries.Remove(item.Id))
                return false;
            OnRemoved(item);
            owner.OnModified();
            return true;
        }

        /// <summary>
        /// Remove all items for which the given predicates matches.
        /// </summary>
        public void RemoveAll(Func<T, bool> predicate)
        {
            var selection = entries.Values.Where(predicate).ToList();
            foreach (var item in selection)
            {
                Remove(item);
            }
        }

        /// <summary>
        /// Validate the integrity of this entity.
        /// </summary>
        public void Validate(IEntity validationRoot, ValidationResults results)
        {
            foreach (var entity in this)
            {
                entity.Validate(validationRoot, results);
            }
        }

        /// <summary>
        /// If this entity uses the given subject, add a <see cref="UsedByInfo"/> entry to 
        /// the given result.
        /// </summary>
        public void CollectUsageInfo(IEntity subject, UsedByInfos results)
        {
            foreach (IEntityInternals entity in this)
            {
                entity.CollectUsageInfo(subject, results);
            }
        }

        /// <summary>
        /// The given entity is removed from the package.
        /// </summary>
        public void RemovedFromPackage(IPersistentEntity entity)
        {
            var clonedListeners = this.Cast<IPackageListener>().ToList();
            foreach (var iterator in clonedListeners)
            {
                iterator.RemovedFromPackage(entity);
            }
        }

        /// <summary>
        /// If this entity uses the given subject, add a <see cref="UsedByInfo"/> entry to 
        /// the given result.
        /// </summary>
        internal void Upgrade()
        {
            foreach (var entity in this.OfType<Entity>())
            {
                entity.Upgrade();
            }
        }

        /// <summary>
        /// Enumerate over all entries
        /// </summary>
        public IEnumerator<T> GetEnumerator()
        {
            return entries.Values.GetEnumerator();
        }

        /// <summary>
        /// Enumerate over all entries
        /// </summary>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// The given item will been added to this set.
        /// </summary>
        protected virtual void OnAdding(T item)
        {
        }

        /// <summary>
        /// The given item has been added to this set.
        /// </summary>
        protected virtual void OnAdded(T item)
        {
        }

        /// <summary>
        /// The given item has been removed from this set.
        /// </summary>
        protected virtual void OnRemoved(T item)
        {
        }

        /// <summary>
        /// Create an implementation of IEntitySet.
        /// </summary>
        protected virtual SetImpl CreateSetImpl()
        {
            return new SetImpl(this);
        }

        /// <summary>
        /// Implementation of IEntitySet
        /// </summary>
        protected class SetImpl : IEntitySet<TIntf>
        {
            private readonly EntitySet<T, TIntf> impl;

            /// <summary>
            /// Default ctor
            /// </summary>
            internal SetImpl(EntitySet<T, TIntf> impl)
            {
                this.impl = impl;
            }

            /// <summary>
            /// Does this set contain an item with the given id?
            /// </summary>
            public bool ContainsId(string id)
            {
                return impl.ContainsId(id);
            }

            /// <summary>
            /// Returns an enumerator that iterates through the collection.
            /// </summary>
            public IEnumerator<TIntf> GetEnumerator()
            {
                return impl.Cast<TIntf>().GetEnumerator();
            }

            /// <summary>
            /// Returns an enumerator that iterates through a collection.
            /// </summary>
            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }

            /// <summary>
            /// Gets the number of elements
            /// </summary>
            public int Count
            {
                get { return impl.Count; }
            }

            /// <summary>
            /// Add the given item to this set
            /// </summary>
            public void Add(TIntf item)
            {
                impl.Add((T)item);
            }

            /// <summary>
            /// Remove the given item from this set.
            /// </summary>
            /// <returns>True if it was removed, false otherwise</returns>
            public bool Remove(TIntf item)
            {
                return impl.Remove((T) item);
            }

            /// <summary>
            /// Does this set contain the given item?
            /// </summary>
            public bool Contains(TIntf item)
            {
                return impl.Contains((T)item);
            }
        }
    }
}

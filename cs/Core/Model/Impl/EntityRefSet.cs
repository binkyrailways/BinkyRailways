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
    /// Each element is stored by it's id in XML.
    /// </summary>
    public abstract class EntityRefSet<T, TIntf> : IEnumerable<string>
        where T : Entity, TIntf
        where TIntf : IEntity
    {
        private readonly Dictionary<string, EntityRef<T>> entries = new Dictionary<string, EntityRef<T>>();
        private SetImpl setImpl;
        private readonly Entity owner;

        /// <summary>
        /// Default ctor
        /// </summary>
        internal EntityRefSet(Entity owner)
        {
            this.owner = owner;
        }

        /// <summary>
        /// Implementation of IEntitySet
        /// </summary>
        public IEntitySet3<TIntf> Set
        {
            get { return setImpl ?? (setImpl = CreateSetImpl()); }
        }

        /// <summary>
        /// Gets the number of elements
        /// </summary>
        public int Count { get { return entries.Count; } }

        /// <summary>
        /// Is the given item contained in this set?
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
            var id = item as string;
            if (id != null)
                Add(id);
            else
                Add((T)item);
        }

        /// <summary>
        /// Add an entry by it's id
        /// </summary>
        private void Add(string key)
        {
            if (!entries.ContainsKey(key))
            {
                entries[key] = new EntityRef<T>(owner, key, Lookup);
                owner.OnModified();
            }
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
                entries[key] = new EntityRef<T>(owner, item);
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
            var selection = entries.Values.Select(x => {
                T result;
                return x.TryGetItem(out result) ? result : null;
            }).Where(x => x != null).Where(predicate).ToList();
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
            foreach (var entity in entries.Values)
            {
                entity.Validate(validationRoot, results);
            }
        }

        /// <summary>
        /// Enumerate over all entries
        /// </summary>
        public IEnumerator<string> GetEnumerator()
        {
            return entries.Values.Select(x => x.Id).GetEnumerator();
        }

        /// <summary>
        /// Enumerate over all entries
        /// </summary>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
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
        /// Look for the item by it's id.
        /// </summary>
        private T Lookup(string id)
        {
            var ownerX = owner as ModuleEntity;
            var module = (ownerX != null) ? ownerX.Module : null;
            var item = Lookup(module, id);
            if (item != null)
            {
                OnAdded(item);
            }
            return item;
        }

        /// <summary>
        /// Look for the item by it's id.
        /// </summary>
        protected abstract T Lookup(Module module, string id);

        /// <summary>
        /// Implementation of IEntitySet
        /// </summary>
        protected class SetImpl : IEntitySet3<TIntf>
        {
            private readonly EntityRefSet<T, TIntf> impl;

            /// <summary>
            /// Default ctor
            /// </summary>
            internal SetImpl(EntityRefSet<T, TIntf> impl)
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
                return impl.entries.Values.Select(x =>
                {
                    T result;
                    return x.TryGetItem(out result) ? result : null;
                }).Where(x => x != null).Cast<TIntf>().GetEnumerator();
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
            /// Copy all entries into the given destination.
            /// </summary>
            public void CopyTo(IEntitySet3<TIntf> destination)
            {
                foreach (var x in this)
                {
                    destination.Add(x);
                }
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

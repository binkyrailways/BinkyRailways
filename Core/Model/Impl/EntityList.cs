using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace BinkyRailways.Core.Model.Impl
{
    /// <summary>
    /// List of entities.
    /// </summary>
    public class EntityList<T, TIntf> : IEnumerable<T>
        where T : Entity, TIntf
        where TIntf : IEntity
    {
        private readonly List<T> entries = new List<T>();
        private ListImpl impl;
        private readonly IModifiable owner;

        /// <summary>
        /// Default ctor
        /// </summary>
        internal EntityList(IModifiable owner)
        {
            this.owner = owner;
        }

        /// <summary>
        /// Gets my implementation of IEntityList
        /// </summary>
        public IEntityList<TIntf> List { get
        {
            if (impl == null)
                impl = CreateListImpl();
            return impl;
        } }

        /// <summary>
        /// Gets the underlying entries.
        /// </summary>
        protected List<T> Entries { get { return entries; } } 

        /// <summary>
        /// Gets the number of elements
        /// </summary>
        public int Count { get { return entries.Count; } }

        /// <summary>
        /// Gets an element at the given index.
        /// </summary>
        public T this[int index] { get { return entries[index]; } }

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
            entries.Add(item);
            OnAdded(item);
            owner.OnModified();
        }

        /// <summary>
        /// Insert the given item at the given index
        /// </summary>
        public void Insert(int index, T item)
        {
            item.EnsureId();
            entries.Insert(index, item);
            OnAdded(item);
            owner.OnModified();
        }

        /// <summary>
        /// Remove the given item from this set.
        /// </summary>
        /// <returns>True if it was removed, false otherwise</returns>
        public bool Remove(T item)
        {
            if (!entries.Remove(item))
                return false;
            OnRemoved(item);
            owner.OnModified();
            return true;
        }

        /// <summary>
        /// Move the given item to the given new index.
        /// </summary>
        public void MoveTo(T item, int index)
        {
            var oldIndex = entries.IndexOf(item);
            if (oldIndex < 0)
                throw new ArgumentException("Unknown item");
            if ((index < 0) || (index >= entries.Count))
                throw new ArgumentOutOfRangeException("index");
            if (oldIndex != index)
            {
                if (oldIndex < index)
                    index--;
                entries.RemoveAt(oldIndex);
                entries.Insert(index, item);
                owner.OnModified();
            }
        }

        /// <summary>
        /// Remove all items
        /// </summary>
        public void Clear()
        {
            while (Count > 0)
            {
                Remove(this[0]);
            }
        }

        /// <summary>
        /// Enumerate over all entries
        /// </summary>
        public IEnumerator<T> GetEnumerator()
        {
            return entries.GetEnumerator();
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
        /// Create an implementation of the actual list interface.
        /// </summary>
        protected virtual ListImpl CreateListImpl()
        {
            return new ListImpl(this);
        }

        /// <summary>
        /// Implementation of IEntityList
        /// </summary>
        protected class ListImpl : IEntityList<TIntf>
        {
            private readonly EntityList<T, TIntf> impl;

            /// <summary>
            /// Default ctor
            /// </summary>
            public ListImpl(EntityList<T, TIntf> impl)
            {
                this.impl = impl;
            }

            /// <summary>
            /// Gets the number of elements
            /// </summary>
            public int Count { get { return impl.Count; } }

            /// <summary>
            /// Gets an element at the given index.
            /// </summary>
            TIntf IEntityList<TIntf>.this[int index] { get { return impl[index]; } }

            /// <summary>
            /// Remove the given item from this set.
            /// </summary>
            /// <returns>True if it was removed, false otherwise</returns>
            bool IEntityList<TIntf>.Remove(TIntf item)
            {
                return impl.Remove((T)item);
            }

            /// <summary>
            /// Move the given item to the given new index.
            /// </summary>
            void IEntityList<TIntf>.MoveTo(TIntf item, int index)
            {
                impl.MoveTo((T)item, index);
            }

            /// <summary>
            /// Remove all items
            /// </summary>
            void IEntityList<TIntf>.Clear()
            {
                impl.Clear();
            }

            /// <summary>
            /// Enumerate over all entries
            /// </summary>
            public IEnumerator<TIntf> GetEnumerator()
            {
                return impl.Cast<TIntf>().GetEnumerator();
            }

            /// <summary>
            /// Enumerate over all entries
            /// </summary>
            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }
    }
}

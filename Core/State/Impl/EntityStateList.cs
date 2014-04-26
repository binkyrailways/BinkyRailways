using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using BinkyRailways.Core.ComponentModel;
using BinkyRailways.Core.Model;

namespace BinkyRailways.Core.State.Impl
{
    /// <summary>
    /// List of entities.
    /// </summary>
    [TypeDescriptionProvider(typeof(CollectionTypeDescriptionProvider))]
    internal class EntityStateList<TInternalState, TState, TEntity> : IEntityStateList<TState, TEntity>
        where TInternalState : TState, IInternalEntityState<TEntity>
        where TState : IEntityState<TEntity>
        where TEntity : class, IEntity
    {
        private readonly List<TInternalState> list;

        /// <summary>
        /// Default ctor
        /// </summary>
        public EntityStateList(IEnumerable<TInternalState> states)
        {
            list = states.OrderBy(x => x.Description).ToList();
        }

        /// <summary>
        /// Gets the number of elements
        /// </summary>
        public int Count { get { return list.Count; } }

        /// <summary>
        /// Enumerate over all entries
        /// </summary>
        public IEnumerator<TState> GetEnumerator()
        {
            return list.Cast<TState>().GetEnumerator();
        }

        /// <summary>
        /// Enumerate over all entries
        /// </summary>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// Cleanup
        /// </summary>
        internal void Dispose()
        {
            foreach (var state in list)
            {
                try
                {
                    state.Dispose();
                }
                catch
                {
                    // Ignore for now
                }
            }
        }

        /// <summary>
        /// Called when an entity in the railway model has changed.
        /// </summary>
        internal void OnModelChanged()
        {
            foreach (var state in list)
            {
                state.OnModelChanged();
            }
        }

        /// <summary>
        /// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </summary>
        public override string ToString()
        {
            return string.Format("Count {0}", Count);
        }
    }
}

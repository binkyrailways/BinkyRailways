using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using BinkyRailways.Core.ComponentModel;
using BinkyRailways.Core.Model;
using BinkyRailways.Core.Util;
using Newtonsoft.Json;

namespace BinkyRailways.Core.State.Impl
{
    /// <summary>
    /// Set of entities.
    /// Each element may only occur once (if it occurs)
    /// </summary>
    [TypeDescriptionProvider(typeof(CollectionTypeDescriptionProvider))]
    [JsonArray]
    internal class EntityStateSet<TInternalState, TState, TEntity> : IEntityStateSet<TState, TEntity>
        where TInternalState : TState, IInternalEntityState<TEntity>
        where TState : IEntityState<TEntity>
        where TEntity : class, IEntity
    {
        private readonly Dictionary<TEntity, TInternalState> entries = new Dictionary<TEntity, TInternalState>();
        private readonly List<TInternalState> list;

        /// <summary>
        /// Default ctor
        /// </summary>
        public EntityStateSet(IEnumerable<TEntity> entities, RailwayState railwayState)
        {
            foreach (var entity in entities)
            {
                var state = entity.Accept(Default<StateBuilder>.Instance, railwayState);
                entries.Add(entity, (TInternalState)state);
            }
            list = entries.Values.OrderBy(x => x.Description).ToList();
        }

        /// <summary>
        /// Default ctor
        /// </summary>
        public EntityStateSet(IEnumerable<TInternalState> states)
        {
            foreach (var state in states)
            {
                entries.Add(state.Entity, state);
            }
            list = entries.Values.OrderBy(x => x.Description).ToList();
        }

        /// <summary>
        /// Gets the number of elements
        /// </summary>
        public int Count { get { return list.Count; } }

        /// <summary>
        /// Gets the state for the given entity
        /// </summary>
        public TState this[TEntity entity]
        {
            get
            {
                if (entity == null)
                    throw new ArgumentNullException("entity");
                return entries[entity];
            }
        }

        /// <summary>
        /// Does this collection contain a state item for the given entity?
        /// </summary>
        public bool Contains(TEntity entity)
        {
            return entries.ContainsKey(entity);
        }

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

using System;
using System.ComponentModel;
using BinkyRailways.Core.Model;
using BinkyRailways.Core.Util;
using Newtonsoft.Json;

namespace BinkyRailways.Core.State.Impl
{
    /// <summary>
    /// State of a single entity.
    /// </summary>
    [TypeConverter(typeof(ExpandableObjectConverter))]
    [JsonObject]
    public abstract class EntityState 
    {
        /// <summary>
        /// A requested value of a property of this state object has changed.
        /// </summary>
        public event EventHandler RequestedStateChanged;

        /// <summary>
        /// An actual value of a property of this state object has changed.
        /// </summary>
        public event EventHandler ActualStateChanged;

        private readonly RailwayState railwayState;
        private bool readyForUse;

        /// <summary>
        /// Default ctor
        /// </summary>
        protected EntityState(RailwayState railwayState)
        {
            this.railwayState = railwayState;
        }

        /// <summary>
        /// Gets the containing railway state
        /// </summary>
        protected RailwayState RailwayState
        {
            get { return railwayState; }
        }

        /// <summary>
        /// Is this entity fully resolved such that is can be used in the live railway?
        /// </summary>
        [DisplayName(@"Is ready for use")]
        public bool IsReadyForUse { get { return readyForUse; } }

        /// <summary>
        /// Prepare this state for use in a live railway. 
        /// Make sure all relevant connections to other state objects are resolved.
        /// Check the IsReadyForUse property afterwards if it has succeeded.
        /// </summary>
        internal void PrepareForUse(IStateUserInterface ui, IStatePersistence statePersistence)
        {
            if (!readyForUse)
            {
                readyForUse = TryPrepareForUse(ui, statePersistence);
            }
        }

        /// <summary>
        /// Prepare this state for use in a live railway. 
        /// Make sure all relevant connections to other state objects are resolved.
        /// </summary>
        /// <returns>True if the entity is now ready for use in a live railway, false otherwise.</returns>
        protected abstract bool TryPrepareForUse(IStateUserInterface ui, IStatePersistence statePersistence);

        /// <summary>
        /// Fire the RequestedStateChanged event
        /// </summary>
        internal void OnRequestedStateChanged()
        {
            RequestedStateChanged.Fire(this);
        }

        /// <summary>
        /// Fire the ActualStateChanged event
        /// </summary>
        internal void OnActualStateChanged()
        {
            ActualStateChanged.Fire(this);
        }

        /// <summary>
        /// Cleanup
        /// </summary>
        public virtual void Dispose()
        {
            readyForUse = false;
            RequestedStateChanged = null;
            ActualStateChanged = null;
        }

        /// <summary>
        /// Called when an entity in the railway model has changed.
        /// </summary>
        protected virtual void OnModelChanged()
        {
            // Override me
        }
    }

    /// <summary>
    /// State of a single entity.
    /// </summary>
    public abstract class EntityState<T> : EntityState, IInternalEntityState<T>
        where T : class, IEntity
    {
        private readonly T entity;

        /// <summary>
        /// Default ctor
        /// </summary>
        protected EntityState(T entity, RailwayState railwayState)
            : base(railwayState)
        {
            this.entity = entity;
        }

        /// <summary>
        /// Gets the entity model object
        /// </summary>
        [Browsable(false)]
        [JsonProperty()]
        public T Entity
        {
            get { return entity; }
        }

        /// <summary>
        /// Gets the entity model object
        /// </summary>
        [Browsable(false)]
        IEntity IEntityState.Entity { get { return Entity; } }

        /// <summary>
        /// Called when an entity in the railway model has changed.
        /// </summary>
        void IInternalEntityState<T>.OnModelChanged()
        {
            OnModelChanged();
        }

        /// <summary>
        /// Unique ID of the underlying entity (if any)
        /// </summary>
        [DisplayName(@"ID")]
        [JsonProperty("Id")]
        public string EntityId { get { return (entity != null) ? entity.Id : "-"; } }

        /// <summary>
        /// Gets the description of the entity.
        /// </summary>
        [DisplayName(@"Description")]
        public virtual string Description { get { return (entity != null) ? entity.ToString() : "?"; } }

        /// <summary>
        /// Gets the railway state this object is a part of.
        /// </summary>
        IRailwayState IEntityState.RailwayState { get { return RailwayState; } }

        /// <summary>
        /// Convert to a string
        /// </summary>
        public override string ToString()
        {
            if (entity == null)
                return string.Empty;
            return Description ?? entity.Id;
        }
    }
}

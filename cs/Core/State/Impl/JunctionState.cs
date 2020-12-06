using System.Collections.Generic;
using BinkyRailways.Core.Model;

namespace BinkyRailways.Core.State.Impl
{
    /// <summary>
    /// State of a single junction.
    /// </summary>
    public abstract class JunctionState  : LockableState<IJunction>, IJunctionState
    {
        /// <summary>
        /// Default ctor
        /// </summary>
        protected JunctionState(IJunction junction, RailwayState railwayState)
            : base(junction, railwayState)
        {
        }

        /// <summary>
        /// Gets all entities that must be locked in order to lock me.
        /// </summary>
        protected override IEnumerable<ILockableStateImpl> UnderlyingLockableEntities
        {
            get { yield break; }
        }

        /// <summary>
        /// Prepare this state for use in a live railway. 
        /// Make sure all relevant connections to other state objects are resolved.
        /// </summary>
        /// <returns>True if the entity is now ready for use in a live railway, false otherwise.</returns>
        protected override bool TryPrepareForUse(IStateUserInterface ui, IStatePersistence statePersistence)
        {
            return true;
        }
    }

    /// <summary>
    /// State of a single junction.
    /// </summary>
    public abstract class JunctionState<T> : JunctionState
        where T : class, IJunction
    {
        /// <summary>
        /// Default ctor
        /// </summary>
        protected JunctionState(T junction, RailwayState railwayState)
            : base(junction, railwayState)
        {
        }

        /// <summary>
        /// Gets the entity model object
        /// </summary>
        public new T Entity { get { return (T)base.Entity; } }
    }
}

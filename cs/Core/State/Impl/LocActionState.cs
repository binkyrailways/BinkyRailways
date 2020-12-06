using BinkyRailways.Core.Model;

namespace BinkyRailways.Core.State.Impl
{
    /// <summary>
    /// State for a loc action.
    /// </summary>
    public abstract class LocActionState<T> : ActionState<T>
        where T : ILocAction
    {
        private ILocState loc;

        /// <summary>
        /// Default ctor
        /// </summary>
        protected LocActionState(T entity, RailwayState railwayState)
            : base(entity, railwayState)
        {
        }

        /// <summary>
        /// Execute this action in the given context.
        /// </summary>
        public sealed override void Execute(IActionContext context)
        {
            var currentLoc = loc ?? context.Loc;
            if (currentLoc != null)
            {
                Execute(currentLoc, context);
            }
        }

        /// <summary>
        /// Execute this action on the given loc.
        /// </summary>
        protected abstract void Execute(ILocState loc, IActionContext context);

        /// <summary>
        /// Prepare this state for use in a live railway. 
        /// Make sure all relevant connections to other state objects are resolved.
        /// </summary>
        /// <returns>True if the entity is now ready for use in a live railway, false otherwise.</returns>
        protected override bool TryPrepareForUse(IStateUserInterface ui, IStatePersistence statePersistence)
        {
            if (Entity.Loc != null)
            {
                loc = RailwayState.LocStates[Entity.Loc];
                if (loc == null)
                    return false;
            }
            return true;
        }
    }
}

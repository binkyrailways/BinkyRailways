using BinkyRailways.Core.Model;

namespace BinkyRailways.Core.State.Impl
{
    /// <summary>
    /// Set a junction to it's initial state.
    /// </summary>
    public sealed class InitializeJunctionActionState : ActionState<IInitializeJunctionAction>
    {
        private IInitializationJunctionState junction;

        /// <summary>
        /// Default ctor
        /// </summary>
        public InitializeJunctionActionState(IInitializeJunctionAction entity, RailwayState railwayState)
            : base(entity, railwayState)
        {
        }

        /// <summary>
        /// Execute this action on the given loc.
        /// </summary>
        public override void Execute(IActionContext context)
        {
            junction.Initialize();
        }

        /// <summary>
        /// Prepare this state for use in a live railway. 
        /// Make sure all relevant connections to other state objects are resolved.
        /// </summary>
        /// <returns>True if the entity is now ready for use in a live railway, false otherwise.</returns>
        protected override bool TryPrepareForUse(IStateUserInterface ui, IStatePersistence statePersistence)
        {
            junction = RailwayState.JunctionStates[Entity.Junction] as IInitializationJunctionState;
            return (junction != null);
        }
    }
}

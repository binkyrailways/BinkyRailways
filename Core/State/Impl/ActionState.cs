using BinkyRailways.Core.Model;
using BinkyRailways.Core.Util;

namespace BinkyRailways.Core.State.Impl
{
    /// <summary>
    /// State for a actions.
    /// </summary>
    public abstract class ActionState : EntityState<IAction>, IActionState
    {
        /// <summary>
        /// Default ctor
        /// </summary>
        protected ActionState(IAction entity, RailwayState railwayState)
            : base(entity, railwayState)
        {
        }

        /// <summary>
        /// Execute this action in the given context.
        /// </summary>
        public abstract void Execute(IActionContext context);

        /// <summary>
        /// Build a action state from the given action.
        /// </summary>
        internal static ActionState Build(IAction action, RailwayState railwayState)
        {
            return (ActionState) action.Accept(Default<StateBuilder>.Instance, railwayState);
        }
    }

    /// <summary>
    /// State for an action.
    /// </summary>
    public abstract class ActionState<T> : ActionState
        where T : IAction
    {
        /// <summary>
        /// Default ctor
        /// </summary>
        protected ActionState(T entity, RailwayState railwayState)
            : base(entity, railwayState)
        {
        }

        /// <summary>
        /// Gets the entity model object
        /// </summary>
        public new T Entity
        {
            get { return (T) base.Entity; }
        }
    }
}

using BinkyRailways.Core.Model;

namespace BinkyRailways.Core.State
{
    /// <summary>
    /// State for an action.
    /// </summary>
    public interface IActionState : IEntityState<IAction>
    {
        /// <summary>
        /// Execute this action in the given context.
        /// </summary>
        void Execute(IActionContext context);
    }
}

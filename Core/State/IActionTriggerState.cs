namespace BinkyRailways.Core.State
{
    /// <summary>
    /// State for an action trigger.
    /// </summary>
    public interface IActionTriggerState
    {
        /// <summary>
        /// Execute this action in the given context.
        /// </summary>
        void Execute(IActionContext context);

        /// <summary>
        /// Is this set empty?
        /// </summary>
        bool IsEmpty { get; }
    }
}

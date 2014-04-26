namespace BinkyRailways.Core.Model
{
    /// <summary>
    /// Event source to which actions can be added.
    /// </summary>
    public interface IActionTrigger : IEntityList<IAction>
    {
        /// <summary>
        /// Gets human readable (localizable) name of this trigger.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Add the given action to this list.
        /// </summary>
        void Add(IAction action);
    }
}

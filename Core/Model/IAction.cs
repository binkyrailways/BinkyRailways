namespace BinkyRailways.Core.Model
{
    /// <summary>
    /// Base class for actions that are invoked upon a changing sensor value.
    /// </summary>
    public interface IAction : IEntity
    {
        /// <summary>
        /// Create a clone of this action.
        /// </summary>
        IAction Clone();
    }
}

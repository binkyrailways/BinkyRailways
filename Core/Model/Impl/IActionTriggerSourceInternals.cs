namespace BinkyRailways.Core.Model.Impl
{
    /// <summary>
    /// Entity that has one of more actions triggers.
    /// </summary>
    internal interface IActionTriggerSourceInternals : IActionTriggerSource, IModifiable
    {
        /// <summary>
        /// Gets the persistent entity that contains this action trigger.
        /// </summary>
        IPersistentEntity Container { get; }

        /// <summary>
        /// Gets the containing railway.
        /// </summary>
        IRailway Railway { get; }
    }
}

namespace BinkyRailways.Core.State
{
    /// <summary>
    /// State of a single output.
    /// </summary>
    public interface IBinaryOutputState  : IOutputState, IAddressEntityState
    {
        /// <summary>
        /// Is this output active?
        /// </summary>
        IStateProperty<bool> Active { get; }
    }
}

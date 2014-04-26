using BinkyRailways.Core.Model;

namespace BinkyRailways.Core.State
{
    /// <summary>
    /// State of a single input.
    /// </summary>
    public interface IInputState  : IEntityState
    {
        /// <summary>
        /// Address of the entity
        /// </summary>
        Address Address { get; }

        /// <summary>
        /// Is this sensor in the 'active' state?
        /// </summary>
        IActualStateProperty<bool> Active { get; }
    }
}

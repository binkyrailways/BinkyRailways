using BinkyRailways.Core.Model;

namespace BinkyRailways.Core.State
{
    /// <summary>
    /// State of a single address entity.
    /// </summary>
    public interface IAddressEntityState : IEntityState<IAddressEntity>
    {
        /// <summary>
        /// Address of the entity
        /// </summary>
        Address Address { get; }
    }
}

using System.Collections.Generic;

namespace BinkyRailways.Core.Model
{
    /// <summary>
    /// Entity with address
    /// </summary>
    public interface IAddressEntity : IEntity
    {
        /// <summary>
        /// Address of the entity
        /// </summary>
        Address Address { get; set; }

        /// <summary>
        /// Gets all (non-null) addresses configured in this entity with the direction their being used.
        /// </summary>
        IEnumerable<AddressUsage> AddressUsages { get; }
    }
}

using System.Collections.Generic;

namespace BinkyRailways.Core.Model
{
    /// <summary>
    /// Device used to communicate between the computer and the railway.
    /// </summary>
    public interface ICommandStation : IPersistentEntity, IImportableEntity 
    {
        /// <summary>
        /// What types of addresses does this command station support?
        /// The result may vary depending on the type of the given entity
        /// </summary>
        IEnumerable<AddressType> GetSupportedAddressTypes(IAddressEntity entity);

        /// <summary>
        /// What types of addresses does this command station support?
        /// </summary>
        IEnumerable<AddressType> GetSupportedAddressTypes();
    }
}

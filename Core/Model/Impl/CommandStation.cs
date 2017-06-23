using System.Collections.Generic;
using System.Xml.Serialization;
using BinkyRailways.Core.Storage;

namespace BinkyRailways.Core.Model.Impl
{
    /// <summary>
    /// Device used to communicate between the computer and the railway.
    /// </summary>
    [XmlRoot]
    [XmlInclude(typeof(DccOverRs232CommandStation))]
    [XmlInclude(typeof(EcosCommandStation))]
    [XmlInclude(typeof(LocoBufferCommandStation))]
    [XmlInclude(typeof(MqttCommandStation))]
    public abstract class CommandStation : PersistentEntity, ICommandStation
    {
        /// <summary>
        /// What types of addresses does this command station support?
        /// The result may vary depending on the type of the given entity
        /// </summary>
        public abstract IEnumerable<AddressType> GetSupportedAddressTypes(IAddressEntity entity);

        /// <summary>
        /// What types of addresses does this command station support?
        /// </summary>
        public abstract IEnumerable<AddressType> GetSupportedAddressTypes();

        /// <summary>
        /// Gets package relative folder for this type of entity.
        /// </summary>
        internal override string PackageFolder
        {
            get { return PackageFolders.CommandStation; }
        }

        /// <summary>
        /// Compare the last modification of this entity (from the import source) with the given entity found in
        /// the target package.
        /// </summary>
        /// <param name="target">The equal entity in the target package. Can be null.</param>
        ImportComparison IImportableEntity.CompareTo(IImportableEntity target)
        {
            return CompareTo((IPersistentEntity)target);
        }

        /// <summary>
        /// Import this entity into the given package.
        /// </summary>
        void IImportableEntity.Import(IPackage target)
        {
            ((Package) target).Import(this);
            target.Railway.CommandStations.Add(target.GetCommandStation(Id));
        }
    }
}

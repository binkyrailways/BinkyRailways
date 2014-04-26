using System.Collections.Generic;
using System.IO;

namespace BinkyRailways.Core.Model
{
    /// <summary>
    /// Interface implemented by package class.
    /// </summary>
    public interface IPackage : IValidationSubject
    {
        /// <summary>
        /// Gets the default railway contained in this package.
        /// </summary>
        IRailway Railway { get; }

        /// <summary>
        /// Remove the given entity from this package
        /// </summary>
        void Remove(IPersistentEntity entity);

        /// <summary>
        /// Add a new LocoBuffer type command station.
        /// </summary>
        ILocoBufferCommandStation AddNewLocoBufferCommandStation();

        /// <summary>
        /// Add a new DCC over RS232 type command station.
        /// </summary>
        IDccOverRs232CommandStation AddNewDccOverRs232CommandStation();

        /// <summary>
        /// Add a new Ecos command station.
        /// </summary>
        IEcosCommandStation AddNewEcosCommandStation();

        /// <summary>
        /// Load a command station by it's id.
        /// </summary>
        /// <returns>Null if not found</returns>
        ICommandStation GetCommandStation(string id);

        /// <summary>
        /// Get all command stations
        /// </summary>
        IEnumerable<ICommandStation> GetCommandStations();

        /// <summary>
        /// Add a new loc.
        /// </summary>
        ILoc AddNewLoc();

        /// <summary>
        /// Load a loc by it's id.
        /// </summary>
        /// <returns>Null if not found</returns>
        ILoc GetLoc(string id);

        /// <summary>
        /// Get all locs
        /// </summary>
        IEnumerable<ILoc> GetLocs();

        /// <summary>
        /// Add a new module.
        /// </summary>
        IModule AddNewModule();

        /// <summary>
        /// Load a module by it's id.
        /// </summary>
        /// <returns>Null if not found</returns>
        IModule GetModule(string id);

        /// <summary>
        /// Get all modules
        /// </summary>
        IEnumerable<IModule> GetModules();

        /// <summary>
        /// Gets the ID's of all generic parts that belong to the given entity.
        /// </summary>
        IEnumerable<string> GetGenericPartIDs(IPersistentEntity entity);

        /// <summary>
        /// Load a generic file part that belongs to the given entity by it's id.
        /// </summary>
        /// <returns>Null if not found</returns>
        Stream GetGenericPart(IPersistentEntity entity, string id);

        /// <summary>
        /// Store a generic file part that belongs to the given entity by it's id.
        /// </summary>
        void SetGenericPart(IPersistentEntity entity, string id, Stream source);

        /// <summary>
        /// Remove a generic file part that belongs to the given entity by it's id.
        /// </summary>
        void RemoveGenericPart(IPersistentEntity entity, string id);

        /// <summary>
        /// Save to disk.
        /// </summary>
        void Save(string path);

        /// <summary>
        /// Has this package been changed since the last save?
        /// </summary>
        bool IsDirty { get; }
    }
}

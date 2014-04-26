using System.IO;

namespace BinkyRailways.Core.Model
{
    /// <summary>
    /// Combined description of entire railway.
    /// </summary>
    public interface IRailway : IPersistentEntity
    {
        /// <summary>
        /// Gets command stations used in this railway
        /// </summary>
        IPersistentEntityRefSet<ICommandStationRef, ICommandStation> CommandStations { get; }

        /// <summary>
        /// Gets locomotives used in this railway
        /// </summary>
        IPersistentEntityRefSet<ILocRef, ILoc> Locs { get; }

        /// <summary>
        /// Gets all groups of locs used in this railway.
        /// </summary>
        IEntitySet2<ILocGroup> LocGroups { get; }

        /// <summary>
        /// Gets modules used in this railway
        /// </summary>
        IPersistentEntityRefSet<IModuleRef, IModule> Modules { get; }

        /// <summary>
        /// Gets the connections between modules used in this railway
        /// </summary>
        IEntitySet2<IModuleConnection> ModuleConnections { get; }

        /// <summary>
        /// Gets/sets the background image of the this module.
        /// </summary>
        /// <value>Null if there is no image.</value>
        /// <remarks>Image must be png, bmp, gif, jpg, wmf or emf</remarks>
        Stream BackgroundImage { get; set; }

        /// <summary>
        /// The number of times human time is speed up to reach model time.
        /// </summary>
        int ClockSpeedFactor { get; set; }

        /// <summary>
        /// Gets the builder used to create predicates.
        /// </summary>
        ILocPredicateBuilder PredicateBuilder { get; }

        /// <summary>
        /// Preferred command station for DCC addresses.
        /// </summary>
        ICommandStation PreferredDccCommandStation { get; set; }

        /// <summary>
        /// Preferred command station for LocoNet addresses.
        /// </summary>
        ICommandStation PreferredLocoNetCommandStation { get; set; }

        /// <summary>
        /// Preferred command station for Motorola addresses.
        /// </summary>
        ICommandStation PreferredMotorolaCommandStation { get; set; }

        /// <summary>
        /// Preferred command station for MFX addresses.
        /// </summary>
        ICommandStation PreferredMfxCommandStation { get; set; }
    }
}

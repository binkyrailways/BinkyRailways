using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using BinkyRailways.Core.Util;

namespace BinkyRailways.Core.Model.Impl
{
    /// <summary>
    /// Combined description of entire railway.
    /// </summary>
    [XmlRoot]
    public sealed partial class Railway : PersistentEntity, IRailway
    {
        private const string BackgroundImageId = "BackgroundImage";

        private readonly CommandStationRefSet commandStationRefs;
        private readonly LocRefSet locRefs;
        private readonly ModuleRefSet moduleRefs;
        private readonly ModuleConnectionSet moduleConnections;
        private readonly LocGroupSet locGroups;
        private readonly Property<EntityRef<CommandStation>> preferredDccCommandStation;
        private readonly Property<EntityRef<CommandStation>> preferredLocoNetCommandStation;
        private readonly Property<EntityRef<CommandStation>> preferredMotorolaCommandStation;
        private readonly Property<EntityRef<CommandStation>> preferredMfxCommandStation;
        private readonly Property<EntityRef<CommandStation>> preferredMqttCommandStation;
        private readonly Property<int> clockSpeedFactor;
        private readonly Property<string> mqttHostName;
        private readonly Property<int> mqttPort;
        private readonly Property<string> mqttTopic;

        /// <summary>
        /// Default ctor
        /// </summary>
        public Railway()
        {
            clockSpeedFactor = new Property<int>(this, DefaultValues.DefaultRailwayClockSpeedFactor);
            commandStationRefs = new CommandStationRefSet(this);
            locRefs = new LocRefSet(this);
            moduleRefs = new ModuleRefSet(this);
            moduleConnections = new ModuleConnectionSet(this);
            locGroups = new LocGroupSet(this);
            preferredDccCommandStation = new Property<EntityRef<CommandStation>>(this, null);
            preferredLocoNetCommandStation = new Property<EntityRef<CommandStation>>(this, null);
            preferredMotorolaCommandStation = new Property<EntityRef<CommandStation>>(this, null);
            preferredMfxCommandStation = new Property<EntityRef<CommandStation>>(this, null);
            preferredMqttCommandStation = new Property<EntityRef<CommandStation>>(this, null);
            mqttHostName = new Property<string>(this, DefaultValues.DefaultRailwayMqttHostName);
            mqttPort = new Property<int>(this, DefaultValues.DefaultRailwayMqttPort);
            mqttTopic = new Property<string>(this, DefaultValues.DefaultRailwayMqttTopic);
        }

        /// <summary>
        /// The number of times human time is speed up to reach model time.
        /// </summary>
        [DefaultValue(DefaultValues.DefaultRailwayClockSpeedFactor)]
        public int ClockSpeedFactor
        {
            get { return clockSpeedFactor.Value; }
            set
            {
                if (value < 1)
                    throw new ArgumentException("Must > 0");
                clockSpeedFactor.Value = value;
            }
        }

        /// <summary>
        /// Accept a visit by the given visitor
        /// </summary>
        public override TReturn Accept<TReturn, TData>(EntityVisitor<TReturn, TData> visitor, TData data)
        {
            return visitor.Visit(this, data);
        }

        /// <summary>
        /// Validate the integrity of this entity.
        /// </summary>
        public override void Validate(IEntity validationRoot, ValidationResults results)
        {
            base.Validate(validationRoot, results);
            commandStationRefs.Validate(validationRoot, results);
            locRefs.Validate(validationRoot, results);
            moduleRefs.Validate(validationRoot, results);
            moduleConnections.Validate(validationRoot, results);
            locGroups.Validate(validationRoot, results);

            // Check for duplicate addresses
            var addressEntities = this.GetAddressEntities().ToList();
            addressEntities.WarnForDuplicateAddresses(results);
        }

        /// <summary>
        /// If this entity uses the given subject, add a <see cref="UsedByInfo"/> entry to 
        /// the given result.
        /// </summary>
        public override void CollectUsageInfo(IEntity subject, UsedByInfos results)
        {
            base.CollectUsageInfo(subject, results);
            commandStationRefs.CollectUsageInfo(subject, results);
            locRefs.CollectUsageInfo(subject, results);
            moduleRefs.CollectUsageInfo(subject, results);
            moduleConnections.CollectUsageInfo(subject, results);
            locGroups.CollectUsageInfo(subject, results);
        }

        /// <summary>
        /// The given entity is removed from the package.
        /// </summary>
        protected override void RemovedFromPackage(IPersistentEntity entity)
        {
            base.RemovedFromPackage(entity);
            commandStationRefs.RemovedFromPackage(entity);
            locRefs.RemovedFromPackage(entity);
            moduleRefs.RemovedFromPackage(entity);
            moduleConnections.RemovedFromPackage(entity);
            locGroups.RemovedFromPackage(entity);
        }

        /// <summary>
        /// Called when a property of this entity has changed.
        /// </summary>
        internal override void OnModified()
        {
            base.OnModified();
            OnPropertyChanged();
        }

        /// <summary>
        /// Human readable name of this type of entity.
        /// </summary>
        public override string TypeName
        {
            get { return Strings.TypeNameRailway; }
        }

        /// <summary>
        /// Gets command stations used in this railway
        /// </summary>
        public CommandStationRefSet CommandStations { get { return commandStationRefs; } }

        /// <summary>
        /// Serialization of CommandStations needed?
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool ShouldSerializeCommandStations() { return !commandStationRefs.IsEmpty; }

        /// <summary>
        /// Gets locomotives used in this railway
        /// </summary>
        public LocRefSet Locs { get { return locRefs; } }

        /// <summary>
        /// Serialization of Locs needed?
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool ShouldSerializeLocs() { return !locRefs.IsEmpty; }

        /// <summary>
        /// Gets modules used in this railway
        /// </summary>
        public ModuleRefSet Modules { get { return moduleRefs; } }

        /// <summary>
        /// Serialization of Modules needed?
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool ShouldSerializeModules() { return !moduleRefs.IsEmpty; }

        /// <summary>
        /// Gets the connections between modules used in this railway
        /// </summary>
        public ModuleConnectionSet ModuleConnections { get { return moduleConnections; } }

        /// <summary>
        /// Serialization of ModuleConnections needed?
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool ShouldSerializeModuleConnections() { return !moduleConnections.IsEmpty; }

        /// <summary>
        /// Gets all groups of locs
        /// </summary>
        public LocGroupSet LocGroups { get { return locGroups; } }

        /// <summary>
        /// Serialization of LocGroups needed?
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool ShouldSerializeLocGroups() { return !locGroups.IsEmpty; }

        /// <summary>
        /// Gets/sets the background image of this module.
        /// </summary>
        /// <value>Null if there is no image.</value>
        /// <remarks>Image must be png, bmp, gif, jpg, wmf or emf</remarks>
        [XmlIgnore]
        public Stream BackgroundImage
        {
            get
            {
                var pkg = Package;
                return (pkg != null) ? pkg.GetGenericPart(this, BackgroundImageId) : null;
            }
            set
            {
                var pkg = Package;
                if (pkg == null)
                {
                    throw new ArgumentException("Cannot set background-image when not part of a package");
                }
                if (value == null)
                {
                    pkg.RemoveGenericPart(this, BackgroundImageId);
                }
                else
                {
                    pkg.SetGenericPart(this, BackgroundImageId, value);
                }
            }
        }

        /// <summary>
        /// Preferred command station for DCC addresses.
        /// </summary>
        [XmlIgnore]
        public CommandStation PreferredDccCommandStation
        {
            get
            {
                if (preferredDccCommandStation.Value == null)
                    return null;
                CommandStation result;
                return preferredDccCommandStation.Value.TryGetItem(out result) ? result : null;
            }
            set
            {
                if (PreferredDccCommandStation != value)
                {
                    preferredDccCommandStation.Value = (value != null) ? new EntityRef<CommandStation>(this, value) : null;
                }
            }
        }

        /// <summary>
        /// Gets the id of the preferred DCC command station.
        /// Used for serialization only.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string PreferredDccCommandStationId
        {
            get { return preferredDccCommandStation.GetId(); }
            set { preferredDccCommandStation.Value = string.IsNullOrEmpty(value) ? null : new EntityRef<CommandStation>(this, value, LookupCommandStation); }
        }

        /// <summary>
        /// Preferred command station for LocoNet addresses.
        /// </summary>
        [XmlIgnore]
        public CommandStation PreferredLocoNetCommandStation
        {
            get
            {
                if (preferredLocoNetCommandStation.Value == null)
                    return null;
                CommandStation result;
                return preferredLocoNetCommandStation.Value.TryGetItem(out result) ? result : null;
            }
            set
            {
                if (PreferredLocoNetCommandStation != value)
                {
                    preferredLocoNetCommandStation.Value = (value != null) ? new EntityRef<CommandStation>(this, value) : null;
                }
            }
        }

        /// <summary>
        /// Gets the id of the preferred LocoNet command station.
        /// Used for serialization only.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string PreferredLocoNetCommandStationId
        {
            get { return preferredLocoNetCommandStation.GetId(); }
            set { preferredLocoNetCommandStation.Value = string.IsNullOrEmpty(value) ? null : new EntityRef<CommandStation>(this, value, LookupCommandStation); }
        }

        /// <summary>
        /// Preferred command station for Motorola addresses.
        /// </summary>
        [XmlIgnore]
        public CommandStation PreferredMotorolaCommandStation
        {
            get
            {
                if (preferredMotorolaCommandStation.Value == null)
                    return null;
                CommandStation result;
                return preferredMotorolaCommandStation.Value.TryGetItem(out result) ? result : null;
            }
            set
            {
                if (PreferredMotorolaCommandStation != value)
                {
                    preferredMotorolaCommandStation.Value = (value != null) ? new EntityRef<CommandStation>(this, value) : null;
                }
            }
        }

        /// <summary>
        /// Gets the id of the preferred LocoNet command station.
        /// Used for serialization only.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string PreferredMotorolaCommandStationId
        {
            get { return preferredMotorolaCommandStation.GetId(); }
            set { preferredMotorolaCommandStation.Value = string.IsNullOrEmpty(value) ? null : new EntityRef<CommandStation>(this, value, LookupCommandStation); }
        }

        /// <summary>
        /// Preferred command station for MFX addresses.
        /// </summary>
        [XmlIgnore]
        public CommandStation PreferredMfxCommandStation
        {
            get
            {
                if (preferredMfxCommandStation.Value == null)
                    return null;
                CommandStation result;
                return preferredMfxCommandStation.Value.TryGetItem(out result) ? result : null;
            }
            set
            {
                if (PreferredMfxCommandStation != value)
                {
                    preferredMfxCommandStation.Value = (value != null) ? new EntityRef<CommandStation>(this, value) : null;
                }
            }
        }

        /// <summary>
        /// Gets the id of the preferred MFX command station.
        /// Used for serialization only.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string PreferredMfxCommandStationId
        {
            get { return preferredMfxCommandStation.GetId(); }
            set { preferredMfxCommandStation.Value = string.IsNullOrEmpty(value) ? null : new EntityRef<CommandStation>(this, value, LookupCommandStation); }
        }

        /// <summary>
        /// Preferred command station for Mqtt addresses.
        /// </summary>
        [XmlIgnore]
        public CommandStation PreferredMqttCommandStation
        {
            get
            {
                if (preferredMqttCommandStation.Value == null)
                    return null;
                CommandStation result;
                return preferredMqttCommandStation.Value.TryGetItem(out result) ? result : null;
            }
            set
            {
                if (PreferredMqttCommandStation != value)
                {
                    preferredMqttCommandStation.Value = (value != null) ? new EntityRef<CommandStation>(this, value) : null;
                }
            }
        }

        /// <summary>
        /// Gets the id of the preferred Mqtt command station.
        /// Used for serialization only.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string PreferredMqttCommandStationId
        {
            get { return preferredMqttCommandStation.GetId(); }
            set { preferredMqttCommandStation.Value = string.IsNullOrEmpty(value) ? null : new EntityRef<CommandStation>(this, value, LookupCommandStation); }
        }

        /// <summary>
        /// Gets package relative folder for this type of entity.
        /// </summary>
        internal override string PackageFolder
        {
            get { return PackageFolders.Railway; }
        }

        /// <summary>
        /// Gets command stations used in this railway
        /// </summary>
        IPersistentEntityRefSet<ICommandStationRef, ICommandStation> IRailway.CommandStations { get { return commandStationRefs.Set; } }

        /// <summary>
        /// Gets locomotives used in this railway
        /// </summary>
        IPersistentEntityRefSet<ILocRef, ILoc> IRailway.Locs { get { return locRefs.Set; } }

        /// <summary>
        /// Gets modules used in this railway
        /// </summary>
        IPersistentEntityRefSet<IModuleRef, IModule> IRailway.Modules { get { return moduleRefs.Set; } }

        /// <summary>
        /// Gets the connections between modules used in this railway
        /// </summary>
        IEntitySet2<IModuleConnection> IRailway.ModuleConnections { get { return moduleConnections.Set; } }

        /// <summary>
        /// Gets all groups of locs used in this railway.
        /// </summary>
        IEntitySet2<ILocGroup> IRailway.LocGroups { get { return locGroups.Set; } }

        /// <summary>
        /// Gets the builder used to create predicates.
        /// </summary>
        ILocPredicateBuilder IRailway.PredicateBuilder { get { return Default<LocPredicateBuilder>.Instance; } }

        /// <summary>
        /// Preferred command station for DCC addresses.
        /// </summary>
        ICommandStation IRailway.PreferredDccCommandStation
        {
            get { return PreferredDccCommandStation; }
            set { PreferredDccCommandStation = (CommandStation)value; }
        }

        /// <summary>
        /// Preferred command station for LocoNet addresses.
        /// </summary>
        ICommandStation IRailway.PreferredLocoNetCommandStation
        {
            get { return PreferredLocoNetCommandStation; }
            set { PreferredLocoNetCommandStation = (CommandStation)value; }
        }

        /// <summary>
        /// Preferred command station for Motorola addresses.
        /// </summary>
        ICommandStation IRailway.PreferredMotorolaCommandStation
        {
            get { return PreferredMotorolaCommandStation; }
            set { PreferredMotorolaCommandStation = (CommandStation)value; }
        }

        /// <summary>
        /// Preferred command station for MFX addresses.
        /// </summary>
        ICommandStation IRailway.PreferredMfxCommandStation
        {
            get { return PreferredMfxCommandStation; }
            set { PreferredMfxCommandStation = (CommandStation)value; }
        }

        /// <summary>
        /// Preferred command station for Mqtt addresses.
        /// </summary>
        ICommandStation IRailway.PreferredMqttCommandStation
        {
            get { return PreferredMqttCommandStation; }
            set { PreferredMqttCommandStation = (CommandStation)value; }
        }


        /// <summary>
        /// Network hostname of the MQTT server to post server messages to.
        /// </summary>
        [DefaultValue(DefaultValues.DefaultRailwayMqttHostName)]
        string IRailway.MqttHostName
        {
            get { return mqttHostName.Value; }
            set { mqttHostName.Value = value; }
        }

        /// <summary>
        /// Network port of the MQTT server to post server messages to.
        /// </summary>
        [DefaultValue(DefaultValues.DefaultRailwayMqttPort)]
        int IRailway.MqttPort
        {
            get { return mqttPort.Value; }
            set
            {
                if (value < 1)
                {
                    throw new ArgumentException("Value must be larger than 0");
                }
                mqttPort.Value = value;
            }
        }

        /// <summary>
        /// Topic on the MQTT server to post server messages to.
        /// </summary>
        [DefaultValue(DefaultValues.DefaultRailwayMqttTopic)]
        string IRailway.MqttTopic
        {
            get { return mqttTopic.Value; }
            set { mqttTopic.Value = value; }
        }


        /// <summary>
        /// Try to resolve a loc by the given id.
        /// The loc is searched in the <see cref="Locs"/> set and the archived locs.
        /// </summary>
        internal bool TryResolveLoc(string id, out ILoc loc)
        {
            var locRef = Locs[id];
            if (locRef != null)
            {
                if (locRef.TryResolve(out loc))
                    return true;
            }
            var package = Package;
            if (package != null)
            {
                loc = package.GetLoc(id);
                return (loc != null);
            }
            loc = null;
            return false;
        }

        /// <summary>
        /// Lookup a command station by id.
        /// </summary>
        private CommandStation LookupCommandStation(string id)
        {
            var csRef = CommandStations.FirstOrDefault(x => x.Id == id);
            if (csRef == null)
                return null;
            ICommandStation commandStation;
            return csRef.TryResolve(out commandStation) ? (CommandStation)commandStation : null;
        }
    }
}

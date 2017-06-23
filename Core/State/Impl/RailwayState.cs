using System;
using System.ComponentModel;
using System.Linq;
using BinkyRailways.Core.Model;
using BinkyRailways.Core.State.Automatic;
using BinkyRailways.Core.State.Impl.Virtual;
using BinkyRailways.Core.Util;

namespace BinkyRailways.Core.State.Impl
{
    /// <summary>
    /// State of an entire railway.
    /// </summary>
    public sealed partial class RailwayState : EntityState<IRailway>, IRailwayState
    {
        /// <summary>
        /// Unknown sensor has been detected.
        /// </summary>
        public event EventHandler<PropertyEventArgs<Address>> UnknownSensor;

        /// <summary>
        /// Unknown standard switch has been detected.
        /// </summary>
        public event EventHandler<PropertyEventArgs<Address>> UnknownSwitch;

        private readonly EventHandler onPropertyChanged;
        private readonly PowerProperty power;
        private readonly EntityStateSet<BlockState, IBlockState, IBlock> blockStates;
        private readonly EntityStateSet<BlockGroupState, IBlockGroupState, IBlockGroup> blockGroupStates;
        private readonly EntityStateSet<CommandStationState, ICommandStationState, ICommandStation> commandStationStates;
        private readonly EntityStateSet<JunctionState, IJunctionState, IJunction> junctionStates;
        private readonly EntityStateSet<LocState, ILocState, ILoc> locStates;
        private readonly EntityStateList<RouteState, IRouteState, IRoute> routeStates;
        private readonly EntityStateSet<SensorState, ISensorState, ISensor> sensorStates;
        private readonly EntityStateSet<SignalState, ISignalState, ISignal> signalStates;
        private readonly EntityStateSet<OutputState, IOutputState, IOutput> outputStates;
        private readonly IStateDispatcher dispatcher;
        private readonly AutomaticLocController automaticLocController;
        private readonly VirtualMode virtualMode;
        private readonly Clock clock;

        /// <summary>
        /// Default ctor
        /// </summary>
        public RailwayState(IRailway railway, bool runInVirtualMode)
            : base(railway, null)
        {
            // Setup fields
            dispatcher = new StateDispatcher();
            virtualMode = new VirtualMode(runInVirtualMode, this);
            clock = new Clock(this);

            // Resolve single entities
            commandStationStates = new EntityStateSet<CommandStationState, ICommandStationState, ICommandStation>(ResolveCommandStations(railway, runInVirtualMode), this);
            locStates = new EntityStateSet<LocState, ILocState, ILoc>(ResolveLocs(railway), this);

            // Resolve modules
            var modules = ResolveModules(railway).ToList();
            blockStates = new EntityStateSet<BlockState, IBlockState, IBlock>(modules.SelectMany(x => x.Blocks), this);
            blockGroupStates = new EntityStateSet<BlockGroupState, IBlockGroupState, IBlockGroup>(modules.SelectMany(x => x.BlockGroups), this);
            junctionStates = new EntityStateSet<JunctionState, IJunctionState, IJunction>(modules.SelectMany(x => x.Junctions), this);
            sensorStates = new EntityStateSet<SensorState, ISensorState, ISensor>(modules.SelectMany(x => x.Sensors), this);
            signalStates = new EntityStateSet<SignalState, ISignalState, ISignal>(modules.SelectMany(x => x.Signals), this);
            outputStates = new EntityStateSet<OutputState, IOutputState, IOutput>(modules.SelectMany(x => x.Outputs), this);

            // Build routes
            var internalRouteStates = modules.SelectMany(x => x.Routes).Where(x => x.IsInternal()).Select(x => new RouteState(x, this)).ToList();
            var interModuleRouteStates = ResolveInterModuleRoutes(railway, modules).ToList();
            routeStates = new EntityStateList<RouteState, IRouteState, IRoute>(internalRouteStates.Concat(interModuleRouteStates.Cast<RouteState>()));

            // Setup properties
            power = new PowerProperty(this);
            automaticLocController = new AutomaticLocController(this);

            // Catch unhandled exception, so we can stop when they occur
            AppDomain.CurrentDomain.UnhandledException += OnCurrentDomainUnhandledException;

            // Connect to property changes
            onPropertyChanged = (s, x) => dispatcher.PostAction(OnModelChanged);
            railway.PropertyChanged += onPropertyChanged;
        }

        /// <summary>
        /// Gets the railway entity (model)
        /// </summary>
        IRailway IRailwayState.Model { get { return Entity; } }

        /// <summary>
        /// Gets the state of the automatic loc controller?
        /// </summary>
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [DisplayName(@"Automatic controller")]
        public IAutomaticLocController AutomaticLocController { get { return automaticLocController; } }

        /// <summary>
        /// Gets the state dispatcher
        /// </summary>
        [Browsable(false)]
        public IStateDispatcher Dispatcher { get { return dispatcher; } }

        /// <summary>
        /// Gets the virtual mode.
        /// This will never return null.
        /// </summary>
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [DisplayName(@"Virtual mode")]
        public IVirtualMode VirtualMode { get { return virtualMode; } }

        /// <summary>
        /// Get the model time
        /// </summary>
        [DisplayName(@"Time")]
        public IActualStateProperty<Time> ModelTime { get { return clock; }}

            /// <summary>
        /// Enable/disable power on all of the command stations of this railway
        /// </summary>
        [DisplayName(@"Power")]
        public IStateProperty<bool> Power { get { return power; } }

        /// <summary>
        /// Gets the states of all blocks in this railway
        /// </summary>
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [DisplayName(@"Blocks")]
        public IEntityStateSet<IBlockState, IBlock> BlockStates { get { return blockStates; } }

        /// <summary>
        /// Gets the states of all block groups in this railway
        /// </summary>
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [DisplayName(@"BlockGroups")]
        public IEntityStateSet<IBlockGroupState, IBlockGroup> BlockGroupStates { get { return blockGroupStates; } }

        /// <summary>
        /// Gets the states of all command stations in this railway
        /// </summary>
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [DisplayName(@"Command stations")]
        public IEntityStateSet<ICommandStationState, ICommandStation> CommandStationStates { get { return commandStationStates; } }

        /// <summary>
        /// Gets the states of all junctions in this railway
        /// </summary>
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [DisplayName(@"Junctions")]
        public IEntityStateSet<IJunctionState, IJunction> JunctionStates { get { return junctionStates; } }

        /// <summary>
        /// Gets the states of all locomotives in this railway
        /// </summary>
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [DisplayName(@"Locs")]
        public IEntityStateSet<ILocState, ILoc> LocStates { get { return locStates; } }

        /// <summary>
        /// Gets the states of all routes in this railway
        /// </summary>
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [DisplayName(@"Routes")]
        public IEntityStateList<IRouteState, IRoute> RouteStates { get { return routeStates; } }

        /// <summary>
        /// Gets the states of all sensors in this railway
        /// </summary>
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [DisplayName(@"Sensors")]
        public IEntityStateSet<ISensorState, ISensor> SensorStates { get { return sensorStates; } }

        /// <summary>
        /// Gets the states of all signals in this railway
        /// </summary>
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [DisplayName(@"Signals")]
        public IEntityStateSet<ISignalState, ISignal> SignalStates { get { return signalStates; } }

        /// <summary>
        /// Gets the states of all outputs in this railway
        /// </summary>
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [DisplayName(@"Outputs")]
        public IEntityStateSet<IOutputState, IOutput> OutputStates { get { return outputStates; } }

        /// <summary>
        /// Prepare this state for use in a live railway. 
        /// Make sure all relevant connections to other state objects are resolved.
        /// Check the IsReadyForUse property afterwards if it has succeeded.
        /// </summary>
        void IRailwayState.PrepareForUse(IStateUserInterface ui, IStatePersistence statePersistence)
        {
            PrepareForUse(ui, statePersistence);
        }

        /// <summary>
        /// Prepare this state for use in a live railway. 
        /// Make sure all relevant connections to other state objects are resolved.
        /// </summary>
        /// <returns>True if the entity is now ready for use in a live railway, false otherwise.</returns>
        protected override bool TryPrepareForUse(IStateUserInterface ui, IStatePersistence statePersistence)
        {
            // Do not change the order of initialization
            commandStationStates.Cast<EntityState>().Foreach(x => x.PrepareForUse(ui, statePersistence));
            locStates.Cast<EntityState>().Foreach(x => x.PrepareForUse(ui, statePersistence));
            junctionStates.Cast<EntityState>().Foreach(x => x.PrepareForUse(ui, statePersistence));
            signalStates.Cast<EntityState>().Foreach(x => x.PrepareForUse(ui, statePersistence));
            blockStates.Cast<EntityState>().Foreach(x => x.PrepareForUse(ui, statePersistence));
            blockGroupStates.Cast<EntityState>().Foreach(x => x.PrepareForUse(ui, statePersistence));
            routeStates.Cast<EntityState>().Foreach(x => x.PrepareForUse(ui, statePersistence));
            sensorStates.Cast<EntityState>().Foreach(x => x.PrepareForUse(ui, statePersistence));
            outputStates.Cast<EntityState>().Foreach(x => x.PrepareForUse(ui, statePersistence));

            // Wrap up
            blockStates.Cast<BlockState>().Foreach(x => x.FinalizePrepare());
            routeStates.Cast<RouteState>().Foreach(x => x.FinalizePrepare());

            // Attach power event
            foreach (var csState in commandStationStates)
            {
                csState.Power.ActualChanged += (s, _) => power.OnActualChanged();
            }

            return true;
        }

        /// <summary>
        /// Select the command station used to drive the given entity
        /// </summary>
        /// <returns>The selected command station or null if there is no match</returns>
        internal CommandStationState SelectCommandStation(IAddressEntity entity)
        {
            // Is an address configured?
            var address = entity.Address;
            if (address == null)
                return null;
            // Lookup a preferred command station
            var network = address.Network;
            var preferred = GetPreferredCommandStationState(address.Type);
            if (preferred != null)
            {
                if (preferred.Supports(entity, network))
                {
                    return (CommandStationState) preferred;
                }                
            }
            // Look for a command station to control me
            var csSelection = CommandStationStates.Where(x => x.Supports(entity, network)).ToList();
            if (csSelection.Count == 0)
                return null;
            // Look for the best matching command station
            // Select exact match
            var commandStation = csSelection.FirstOrDefault(x => x.SupportsExact(entity, network));
            if (commandStation == null)
            {
                // No exact match
                if (csSelection.Count > 1)
                {
                    // We cannot make a good choice
                    return null;
                }
                return (CommandStationState) csSelection[0];
            }
            return (CommandStationState) commandStation;
        }

        /// <summary>
        /// Gets the preferred command station (state) for the given address type.
        /// </summary>
        private ICommandStationState GetPreferredCommandStationState(AddressType addressType)
        {
            ICommandStation entity = null;
            switch (addressType)
            {
                case AddressType.Dcc:
                    entity = Entity.PreferredDccCommandStation;
                    break;
                case AddressType.LocoNet:
                    entity = Entity.PreferredLocoNetCommandStation;
                    break;
                case AddressType.Motorola:
                    entity = Entity.PreferredMotorolaCommandStation;
                    break;
                case AddressType.Mfx:
                    entity = Entity.PreferredMfxCommandStation;
                    break;
                case AddressType.Mqtt:
                    entity = Entity.PreferredMqttCommandStation;
                    break;
                default:
                    throw new ArgumentException("Unknown address type: " + addressType);
            }
            if ((entity == null) || (!commandStationStates.Contains(entity)))
                return null;
            return CommandStationStates[entity];
        }

        /// <summary>
        /// Fire the UnknownSensor event
        /// </summary>
        internal void OnUnknownSensor(Address address)
        {
            if (UnknownSensor != null)
            {
                UnknownSensor(this, new PropertyEventArgs<Address>(address));
            }
        }

        /// <summary>
        /// Fire the UnknownSwitch event
        /// </summary>
        internal void OnUnknownSwitch(Address address)
        {
            if (UnknownSwitch != null)
            {
                UnknownSwitch(this, new PropertyEventArgs<Address>(address));
            }
        }

        /// <summary>
        /// An unhandled exception has occurred.
        /// Stop everything.
        /// </summary>
        private void OnCurrentDomainUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            try
            {
                Power.Requested = false;
            }
            catch
            {
                // Ignore
            }
        }

        /// <summary>
        /// Cleanup
        /// </summary>
        public override void Dispose()
        {
            Entity.PropertyChanged -= onPropertyChanged;
            base.Dispose();
            if (automaticLocController != null)
            {
                automaticLocController.Dispose();
            }
            clock.Dispose();
            dispatcher.Dispose();
            locStates.Dispose();
            routeStates.Dispose();
            junctionStates.Dispose();
            sensorStates.Dispose();
            signalStates.Dispose();
            outputStates.Dispose();
            blockGroupStates.Dispose();
            blockStates.Dispose();
            commandStationStates.Dispose();

            // Cleanup exception handler
            AppDomain.CurrentDomain.UnhandledException -= OnCurrentDomainUnhandledException;
        }

        /// <summary>
        /// Called when an entity in the railway model has changed.
        /// </summary>
        protected override void OnModelChanged()
        {
            base.OnModelChanged();
            blockStates.OnModelChanged();
            blockGroupStates.OnModelChanged();
            commandStationStates.OnModelChanged();
            junctionStates.OnModelChanged();
            locStates.OnModelChanged();
            routeStates.OnModelChanged();
            sensorStates.OnModelChanged();
            signalStates.OnModelChanged();
            outputStates.OnModelChanged();
        }

        /// <summary>
        /// Implementation of Power property
        /// </summary>
        private sealed class PowerProperty : IStateProperty<bool>
        {
            /// <summary>
            /// Fired when the requested value has changed.
            /// </summary>
            public event EventHandler RequestedChanged;

            /// <summary>
            /// Fired when the actual value has changed.
            /// </summary>
            public event EventHandler ActualChanged;

            private readonly RailwayState owner;
            private bool requested;

            /// <summary>
            /// Default ctor
            /// </summary>
            public PowerProperty(RailwayState owner)
            {
                this.owner = owner;
            }

            /// <summary>
            /// Gets / sets the requested value
            /// </summary>
            public bool Requested
            {
                get { return requested; }
                set
                {
                    requested = value;
                    var changed = false;
                    foreach (var cs in owner.commandStationStates)
                    {
                        changed |= (cs.Power.Requested != value);
                        cs.Power.Requested = value;
                    }
                    if (changed)
                    {
                        RequestedChanged.Fire(this);
                    }
                }
            }

            /// <summary>
            /// Gets / sets the actual value
            /// </summary>
            [ReadOnly(true)]
            public bool Actual
            {
                get { return owner.CommandStationStates.All(x => x.Power.Actual); }
                set { /* not implemented */ }
            }

            /// <summary>
            /// Is the request value equal to the actual value?
            /// </summary>
            public bool IsConsistent { get { return (Requested == Actual); } }

            /// <summary>
            /// Fire ActualChanged event
            /// </summary>
            internal void OnActualChanged()
            {
                ActualChanged.Fire(this);
            }

            /// <summary>
            /// Convert to string
            /// </summary>
            public override string ToString()
            {
                return string.Format("{0}/{1}", Requested, Actual);
            }
        }
    }
}

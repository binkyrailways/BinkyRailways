using System.ComponentModel;
using BinkyRailways.Core.Logging;
using BinkyRailways.Core.Model;
using NLog;

namespace BinkyRailways.Core.State.Impl
{
    /// <summary>
    /// State of a single output.
    /// </summary>
    public abstract class OutputState : EntityState<IOutput>, IOutputState
    {
        protected static readonly Logger Log = LogManager.GetLogger(LogNames.Sensors);

        /// <summary>
        /// Default ctor
        /// </summary>
        protected OutputState(IOutput entity, RailwayState railwayState)
            : base(entity, railwayState)
        {
        }
    }

    /// <summary>
    /// State of a single output.
    /// </summary>
    public abstract class OutputState<T> : OutputState, IAddressEntityState
        where T : class, IAddressEntity, IOutput
    {
        private readonly Address address;
        private CommandStationState commandStation;

        /// <summary>
        /// Default ctor
        /// </summary>
        protected OutputState(T entity, RailwayState railwayState)
            : base(entity, railwayState)
        {
            address = entity.Address;
        }

        /// <summary>
        /// Gets the entity model object
        /// </summary>
        [Browsable(false)]
        public new T Entity
        {
            get { return (T)base.Entity; }
        }

        /// <summary>
        /// Address of the output
        /// </summary>
        [DisplayName(@"Address")]
        public Address Address { get { return address; } }

        /// <summary>
        /// Prepare this state for use in a live railway. 
        /// Make sure all relevant connections to other state objects are resolved.
        /// </summary>
        /// <returns>True if the entity is now ready for use in a live railway, false otherwise.</returns>
        protected override bool TryPrepareForUse(IStateUserInterface ui, IStatePersistence statePersistence)
        {
            // Resolve command station
            commandStation = RailwayState.SelectCommandStation((IAddressEntity) Entity);
            if (commandStation == null)
                return false;
            return true;
        }

        /// <summary>
        /// Gets the command statin connecting this output to the railway.
        /// </summary>
        protected CommandStationState CommandStation { get { return commandStation; } }
    }
}

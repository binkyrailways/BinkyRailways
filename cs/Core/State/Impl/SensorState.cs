using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using BinkyRailways.Core.Logging;
using BinkyRailways.Core.Model;
using BinkyRailways.Core.Util;
using NLog;

namespace BinkyRailways.Core.State.Impl
{
    /// <summary>
    /// State of a single sensor.
    /// </summary>
    public abstract class SensorState : EntityState<ISensor>, ISensorState
    {
        protected static readonly Logger Log = LogManager.GetLogger(LogNames.Sensors);
        private readonly Address address;
        private readonly ActualStateProperty<bool> active;
        private List<IBlockState> destinationBlocks;

        /// <summary>
        /// Default ctor
        /// </summary>
        protected SensorState(ISensor entity, RailwayState railwayState)
            : base(entity, railwayState)
        {
            address = entity.Address;
            active = new ActualStateProperty<bool>(this, false, null, OnActiveChanged);
        }

        /// <summary>
        /// Address of the entity
        /// </summary>
        [DisplayName(@"Address")]
        public Address Address { get { return address; } }

        /// <summary>
        /// Is this sensor in the 'active' state?
        /// </summary>
        [DisplayName(@"Active")]
        public IActualStateProperty<bool> Active
        {
            get { return active; }
        }

        /// <summary>
        /// Gets all blocks for which this sensor is either an "entering" or a "reached"
        /// sensor.
        /// </summary>
        [DisplayName(@"Destination blocks")]
        public IEnumerable<IBlockState> DestinationBlocks 
        {
            get { return destinationBlocks ?? Enumerable.Empty<IBlockState>(); }
        }

        /// <summary>
        /// Shape used to visualize this sensor
        /// </summary>
        [DisplayName(@"Shape")]
        public Shapes Shape
        {
            get { return Entity.Shape; }
        }

        /// <summary>
        /// Active property has changed.
        /// </summary>
        protected virtual void OnActiveChanged(bool value)
        {
            Log.Info(Strings.SensorXStateToY, Entity, value.OnOff());
        }

        /// <summary>
        /// Prepare this state for use in a live railway. 
        /// Make sure all relevant connections to other state objects are resolved.
        /// </summary>
        /// <returns>True if the entity is now ready for use in a live railway, false otherwise.</returns>
        protected override bool TryPrepareForUse(IStateUserInterface ui, IStatePersistence statePersistence)
        {
            // Resolve command station
            var cs = RailwayState.SelectCommandStation(Entity);
            if (cs == null)
                return false;
            cs.AddInput(this);

            // Resolve destination blocks
            var routes = RailwayState.RouteStates.Where(x => x.Contains(this));
            destinationBlocks = routes.Select(x => x.To).ToList();
            if (Entity.Block != null)
            {
                var blockState = RailwayState.BlockStates[Entity.Block];
                if ((blockState != null) && (!destinationBlocks.Contains(blockState)))
                    destinationBlocks.Add(blockState);
            }

            return true;
        }
    }
    /// <summary>
    /// State of a single sensor.
    /// </summary>
    public abstract class SensorState<T> : SensorState
        where T : class, ISensor
    {
        /// <summary>
        /// Default ctor
        /// </summary>
        protected SensorState(T entity, RailwayState railwayState)
            : base(entity, railwayState)
        {
        }
    }
}

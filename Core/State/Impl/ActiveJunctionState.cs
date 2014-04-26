using System;
using BinkyRailways.Core.Model;

namespace BinkyRailways.Core.State.Impl
{
    /// <summary>
    /// State of a single addressable junction.
    /// </summary>
    public abstract class ActiveJunctionState<T> : JunctionState<T>
        where T : class, IJunction, IAddressEntity
    {
        private CommandStationState commandStation;

        /// <summary>
        /// Default ctor
        /// </summary>
        protected ActiveJunctionState(T junction, RailwayState railwayState)
            : base(junction, railwayState)
        {
        }

        /// <summary>
        /// Gets the command station used to drive this junction
        /// </summary>
        protected CommandStationState CommandStation
        {
            get { return commandStation; }
        }

        /// <summary>
        /// Prepare this state for use in a live railway. 
        /// Make sure all relevant connections to other state objects are resolved.
        /// </summary>
        /// <returns>True if the entity is now ready for use in a live railway, false otherwise.</returns>
        protected override bool TryPrepareForUse(IStateUserInterface ui, IStatePersistence statePersistence)
        {
            if (!base.TryPrepareForUse(ui, statePersistence))
                return false;
            var cs = SelectCommandStation();
            if (cs == null)
                return false;
            cs.AddJunction(this);
            commandStation = cs;
            RailwayState.Power.ActualChanged += OnPowerActualChanged;
            return true;
        }

        /// <summary>
        /// Select the command station to use for this state.
        /// </summary>
        protected virtual CommandStationState SelectCommandStation()
        {
            var aEntity = Entity as IAddressEntity;
            if (aEntity == null)
                throw new NotSupportedException("Entity must have an address");
            return RailwayState.SelectCommandStation(aEntity);            
        }

        /// <summary>
        /// Called when a Power down is received.
        /// </summary>
        protected virtual void OnPowerDown()
        {
            // Override me
        }

        /// <summary>
        /// Called when a Power up is received.
        /// </summary>
        protected virtual void OnPowerUp()
        {
            // Override me
        }

        /// <summary>
        /// Actual state of Power has changed.
        /// </summary>
        private void OnPowerActualChanged(object sender, System.EventArgs e)
        {
            if (!RailwayState.Power.Actual)
            {
                OnPowerDown();
            }
            else
            {
                OnPowerUp();
            }
        }
    }
}

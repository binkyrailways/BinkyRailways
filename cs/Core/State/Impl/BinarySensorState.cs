using BinkyRailways.Core.Model;

namespace BinkyRailways.Core.State.Impl
{
    /// <summary>
    /// State of a single sensor.
    /// </summary>
    public sealed class BinarySensorState  : SensorState<IBinarySensor>, IBinarySensorState
    {
        private readonly ActionTriggerState activateTrigger;
        private readonly ActionTriggerState deActivateTrigger;

        /// <summary>
        /// Default ctor
        /// </summary>
        public BinarySensorState(IBinarySensor sensor, RailwayState railwayState)
            : base(sensor, railwayState)
        {
            activateTrigger = new ActionTriggerState(sensor.ActivateTrigger, railwayState);
            deActivateTrigger = new ActionTriggerState(sensor.DeActivateTrigger, railwayState);
        }

        /// <summary>
        /// Active property has changed.
        /// </summary>
        protected override void OnActiveChanged(bool value)
        {
            base.OnActiveChanged(value);
            var trigger = value ? activateTrigger : deActivateTrigger;
            if (!trigger.IsEmpty)
            {
                RailwayState.Dispatcher.PostAction(() => trigger.Execute(null));
            }
        }

        /// <summary>
        /// Prepare this state for use in a live railway. 
        /// Make sure all relevant connections to other state objects are resolved.
        /// </summary>
        /// <returns>True if the entity is now ready for use in a live railway, false otherwise.</returns>
        protected override bool TryPrepareForUse(IStateUserInterface ui, IStatePersistence statePersistence)
        {
            activateTrigger.PrepareForUse(ui, statePersistence);
            deActivateTrigger.PrepareForUse(ui, statePersistence);
            return base.TryPrepareForUse(ui, statePersistence);
        }
    }
}

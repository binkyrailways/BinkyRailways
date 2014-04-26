using System.ComponentModel;
using BinkyRailways.Core.Model;

namespace BinkyRailways.Core.State.Impl
{
    /// <summary>
    /// State of a single standard switch.
    /// </summary>
    public sealed class SwitchState : ActiveJunctionState<ISwitch>, ISwitchState, IInitializeAtPowerOn, IInitializationJunctionState
    {
        private readonly StateProperty<SwitchDirection> direction;
        private readonly Address address;
        private readonly Address feedbackAddress;
        private bool initializeDirectionNeeded;

        /// <summary>
        /// Default ctor
        /// </summary>
        public SwitchState(ISwitch entity, RailwayState railwayState)
            : base(entity, railwayState)
        {
            address = entity.Address;
            feedbackAddress = entity.HasFeedback ? (entity.FeedbackAddress ?? entity.Address) : null;
            direction = new StateProperty<SwitchDirection>(this, SwitchDirection.Straight, null, OnRequestedDirectionChanged, null);
        }

        /// <summary>
        /// Address of the entity
        /// </summary>
        [DisplayName(@"Address")]
        public Address Address { get { return address; } }

        /// <summary>
        /// Address of the feedback unit of entity
        /// </summary>
        [DisplayName(@"Feedback Address")]
        public Address FeedbackAddress { get { return feedbackAddress; } }

        /// <summary>
        /// Does this switch send a feedback when switched?
        /// </summary>
        [DisplayName(@"Has feedback")]
        public bool HasFeedback { get { return Entity.HasFeedback; } }

        /// <summary>
        /// Time (in ms) it takes for the switch to move from one direction to the other?
        /// This property is only used when <see cref="HasFeedback"/> is false.
        /// </summary>
        [DisplayName(@"Switch duration")]
        public int SwitchDuration { get { return Entity.SwitchDuration; } }

        /// <summary>
        /// If set, the straight/off commands are inverted.
        /// </summary>
        [DisplayName(@"Invert")]
        public bool Invert { get { return Entity.Invert; } }

        /// <summary>
        /// If set, the straight/off feedback states are inverted.
        /// </summary>
        [DisplayName(@"Invert feedback")]
        public bool InvertFeedback { get { return (Entity.FeedbackAddress != null) ? Entity.InvertFeedback : Entity.Invert; } }

        /// <summary>
        /// Direction of the switch.
        /// </summary>
        [DisplayName(@"Direction")]
        public IStateProperty<SwitchDirection> Direction { get { return direction; } }

        /// <summary>
        /// Forward direction request to command station
        /// </summary>
        private void OnRequestedDirectionChanged(SwitchDirection value)
        {
            var cs = CommandStation;
            if (cs != null)
            {
                cs.SendSwitchDirection(this);
            }
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
            return true;
        }

        /// <summary>
        /// Consider the next request a First request after power down.
        /// This ensures that the switch is always initialized properly.
        /// </summary>
        protected override void OnPowerDown()
        {
            base.OnPowerDown();
            direction.FirstRequest = true;
        }

        /// <summary>
        /// Called when a Power up is received.
        /// </summary>
        protected override void OnPowerUp()
        {
            base.OnPowerUp();
            initializeDirectionNeeded = true;
        }

        /// <summary>
        /// Used for ordering initialization called.
        /// </summary>
        InitializationPriority IInitializeAtPowerOn.Priority
        {
            get { return InitializationPriority.Junction; }
        }

        /// <summary>
        /// Perform initialization actions.
        /// This method is always called on the dispatcher thread.
        /// </summary>
        void IInitializeAtPowerOn.Initialize()
        {
            if (initializeDirectionNeeded)
            {
                initializeDirectionNeeded = false;
                if (LockedBy == null)
                {
                    direction.Requested = Entity.InitialDirection;
                }
            }
        }

        /// <summary>
        /// Set initial position
        /// </summary>
        void IInitializationJunctionState.Initialize()
        {
            direction.Requested = Entity.InitialDirection;
        }
    }
}

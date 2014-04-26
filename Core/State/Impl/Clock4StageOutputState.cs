using System;
using System.Collections.Generic;
using BinkyRailways.Core.Model;

namespace BinkyRailways.Core.State.Impl
{
    /// <summary>
    /// State of a single 4-stage clock output.
    /// </summary>
    public sealed class Clock4StageOutputState : OutputState<IClock4StageOutput>, IClock4StageOutputState
    {
        private readonly Address address1;
        private readonly Address address2;
        private readonly StateProperty<Clock4Stage> period;
        private readonly StateProperty<int> pattern;

        /// <summary>
        /// Default ctor
        /// </summary>
        public Clock4StageOutputState(IClock4StageOutput output, RailwayState railwayState)
            : base(output, railwayState)
        {
            address1 = output.Address1;
            address2 = output.Address2;
            period = new StateProperty<Clock4Stage>(this, Clock4Stage.Morning, null, OnPeriodRequestedValueChanged, null);
            pattern = new StateProperty<int>(this, DefaultValues.DefaultClock4StageOutputMorningPattern, null, OnPatternRequestedChanged, OnPatternActualChanged);
        }

        /// <summary>
        /// Addresses of first clock bits.
        /// Lowest bit comes first.
        /// This is an output signal.
        /// </summary>
        public IEnumerable<Address> Addresses
        {
            get
            {
                yield return address1;
                yield return address2;
            }
        }

        /// <summary>
        /// Gets the current clock period
        /// </summary>
        public IStateProperty<Clock4Stage> Period { get { return period; } }

        /// <summary>
        /// Gets the current pattern
        /// </summary>
        public IStateProperty<int> Pattern { get { return pattern; } }

        /// <summary>
        /// Period changed, set pattern
        /// </summary>
        private void OnPeriodRequestedValueChanged(Clock4Stage value)
        {
            switch (value)
            {
                case Clock4Stage.Morning:
                    Pattern.Requested = Entity.MorningPattern;
                    break;
                case Clock4Stage.Afternoon:
                    Pattern.Requested = Entity.AfternoonPattern;
                    break;
                case Clock4Stage.Evening:
                    Pattern.Requested = Entity.EveningPattern;
                    break;
                case Clock4Stage.Night:
                    Pattern.Requested = Entity.NightPattern;
                    break;
            }
        }

        /// <summary>
        /// Requested pattern state has changed.
        /// </summary>
        private void OnPatternRequestedChanged(int value)
        {
            CommandStation.SendClock4StageOutput(this);
        }

        /// <summary>
        /// Actual pattern state has changed.
        /// </summary>
        private void OnPatternActualChanged(int value)
        {
            Log.Info(Strings.ClockXY, Entity, string.Format("0x{0:x2}", value));
        }

        /// <summary>
        /// Model time has changed, update period.
        /// </summary>
        private void OnTimeChanged()
        {
            var time = RailwayState.ModelTime.Actual;
            var hour = time.Hour;
            if (hour < 7) Period.Requested = Clock4Stage.Night;
            else if (hour < 12) Period.Requested = Clock4Stage.Morning;
            else if (hour < 18) Period.Requested = Clock4Stage.Afternoon;
            else if (hour < 23) Period.Requested = Clock4Stage.Evening;
            else Period.Requested = Clock4Stage.Night;
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
            RailwayState.ModelTime.ActualChanged += (s, _) => OnTimeChanged();
            return true;
        }
    }
}

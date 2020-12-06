using System.Linq;

namespace BinkyRailways.Core.State.Impl.Virtual
{
    /// <summary>
    /// Auto run state for 1 loc.
    /// </summary>
    internal class AutoRunLocState
    {
        private enum States
        {
            Initial,
            Enter
        }

        private readonly ILocState loc;
        private ISensorState lastSensor;
        private States state;

        /// <summary>
        /// Default ctor
        /// </summary>
        public AutoRunLocState(ILocState loc)
        {
            this.loc = loc;
        }

        /// <summary>
        /// Go to the next state
        /// </summary>
        internal void Tick()
        {
            if (!loc.ControlledAutomatically.Actual)
                return;
            var route = loc.CurrentRoute.Actual;
            if (route == null)
                return;
            switch (loc.AutomaticState.Actual)
            {
                case AutoLocState.Running:
                case AutoLocState.EnterSensorActivated:
                case AutoLocState.EnteringDestination:
                    SelectNextState(route.Route);
                    break;
            }
        }

        private void SelectNextState(IRouteState route)
        {
            if (lastSensor != null)
            {
                lastSensor.Active.Actual = false;
            }
            if (state == States.Initial)
            {
                var sensor = route.Sensors.FirstOrDefault(x => route.IsEnteringDestinationSensor(x, loc));
                if (sensor != null)
                {
                    sensor.Active.Actual = true;
                    lastSensor = sensor;
                    state = States.Enter;
                    return;
                }
            }
            // Activate reached sensor
            {
                var sensor = route.Sensors.FirstOrDefault(x => route.IsReachedDestinationSensor(x, loc));
                if (sensor != null)
                {
                    sensor.Active.Actual = true;
                    lastSensor = sensor;
                    state = States.Initial;
                }
            }            
        }
    }
}

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using BinkyRailways.Core.Model;
using BinkyRailways.Protocols.Ecos;

namespace BinkyRailways.Core.State.Impl.Ecos
{
    /// <summary>
    /// ECoS feedback object
    /// </summary>
    internal class FeedbackModule : Protocols.Ecos.Objects.FeedbackModule
    {
        private readonly RailwayState railwayState;
        private readonly int firstPortNumber;
        private readonly ISensorState[] sensorStates;

        /// <summary>
        /// Default ctor
        /// </summary>
        public FeedbackModule(Client client, int id, RailwayState railwayState, int firstPortNumber)
            : base(client, id)
        {
            this.railwayState = railwayState;
            this.firstPortNumber = firstPortNumber;

            var portCount = GetPortCount();
            this.sensorStates = new ISensorState[portCount];
            for (var i = 0; i < portCount; i++)
            {
                var portNumber = firstPortNumber + i;
                sensorStates[i] = railwayState.SensorStates.FirstOrDefault(x => (x.Address.Value == portNumber) && (x.Address.Type == AddressType.LocoNet));
            }
        }

        /// <summary>
        /// Gets the number of the first port on this module.
        /// </summary>
        public int FirstPortNumber { get { return firstPortNumber; } }

        /// <summary>
        /// Gets the number of the last port on this module.
        /// </summary>
        public int LastPortNumber { get { return firstPortNumber + sensorStates.Length; } }

        /// <summary>
        /// Gets the sensors attached to this module.
        /// </summary>
        public IEnumerable<ISensorState> Sensors
        {
            get { return sensorStates.Where(x => x != null); }
        }

        /// <summary>
        /// Process events
        /// </summary>
        protected override void OnEvent(Event @event)
        {
            base.OnEvent(@event);
            var provider = new CultureInfo("en-US");
            foreach (var row in @event.Rows)
            {
                Option option;
                if (!row.TryGetValue(OptState, out option))
                    continue;

                var state = ParseState(option.Value);

                // Process the state
                for (var i = 0; i < sensorStates.Length; i++)
                {
                    var mask = (1 << i);
                    var portActive = (state & mask) != 0;

                    var sensor = sensorStates[i];
                    if (sensor != null)
                    {
                        // Update sensor state
                        sensor.Active.Actual = portActive;
                    }
                    else
                    {
                        // Unknown sensor
                        railwayState.OnUnknownSensor(new Address(AddressType.LocoNet, string.Empty, firstPortNumber + i));
                    }
                }
            }
        }
    }
}

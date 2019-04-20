using System.Collections.Concurrent;
using BinkyRailways.Protocols.Ecos;

namespace BinkyRailways.Core.State.Impl.Ecos
{
    internal class FeedbackManager : Protocols.Ecos.Objects.FeedbackManager
    {
        private readonly RailwayState railwayState;
        private readonly ConcurrentDictionary<ISensorState, FeedbackModule> sensorIds = new ConcurrentDictionary<ISensorState, FeedbackModule>();

        /// <summary>
        /// Default ctor
        /// </summary>
        public FeedbackManager(Client client, RailwayState railwayState)
            : base(client)
        {
            this.railwayState = railwayState;
        }

        /// <summary>
        /// Gets advanced info for the given sensor
        /// </summary>
        internal bool TryGetLoc(ISensorState sensorState, out FeedbackModule feedbackModule)
        {
            return sensorIds.TryGetValue(sensorState, out feedbackModule);
        }

        /// <summary>
        /// QueryObjects reply.
        /// </summary>
        protected override void OnQueryObjects(Reply reply)
        {
            var firstPortNumber = 1;
            foreach (var row in reply.Rows)
            {
                var module = new FeedbackModule(Client, row.Id, railwayState, firstPortNumber);
                module.RequestView();
                firstPortNumber = module.LastPortNumber + 1;
                foreach (var sensor in module.Sensors)
                {
                    sensorIds[sensor] = module;
                }
            }
        }
    }
}

using System;
using BinkyRailways.Core.State;
using Newtonsoft.Json.Linq;

namespace BinkyRailways.Core.Server
{
    /// <summary>
    /// Listen to the railway state and fire messages when needed.
    /// </summary>
    internal class StateListener : IDisposable
    {
        private readonly IRailwayState railwayState;
        private readonly WebSocketProcessor webSocketProcessor;

        /// <summary>
        /// Default ctor
        /// </summary>
        public StateListener(IRailwayState railwayState, WebSocketProcessor webSocketProcessor)
        {
            this.railwayState = railwayState;
            this.webSocketProcessor = webSocketProcessor;

            foreach (var loc in railwayState.LocStates)
            {
                loc.ActualStateChanged += OnLocActualStateChanged;
            }
        }

        /// <summary>
        /// Cleanup
        /// </summary>
        public void Dispose()
        {
            foreach (var loc in railwayState.LocStates)
            {
                loc.ActualStateChanged -= OnLocActualStateChanged;
            }            
        }

        /// <summary>
        /// State of a loc has changed.
        /// </summary>
        private void OnLocActualStateChanged(object sender, EventArgs eventArgs)
        {
            var loc = (ILocState) sender;
            webSocketProcessor.Send2All(Messages.LocStateChanged, new Messages.LocState(loc));
        }
    }
}

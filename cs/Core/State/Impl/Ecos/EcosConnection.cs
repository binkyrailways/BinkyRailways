using System;
using System.Threading.Tasks;
using BinkyRailways.Protocols.Ecos;

namespace BinkyRailways.Core.State.Impl.Ecos
{
    /// <summary>
    /// Network connection to the ecos.
    /// </summary>
    internal class EcosConnection : IDisposable
    {
        private readonly IRailwayState railwayState;
        private readonly Client socket;
        private LocManager locManager;
        private FeedbackManager feedbackManager;
        private SwitchManager switchManager;
        private bool initialized;

        /// <summary>
        /// Default ctor
        /// </summary>
        internal EcosConnection(string hostName, RailwayState railwayState)
        {
            this.railwayState = railwayState;
            // Open connection
            socket = new Client(hostName);

            locManager = new LocManager(socket, railwayState);
            var locTask = locManager.QueryObjects().ContinueWith(t => locManager.RequestView());

            feedbackManager = new FeedbackManager(socket, railwayState);
            var fbTask = feedbackManager.QueryObjects();

            switchManager = new SwitchManager(socket, railwayState);
            var swTask = switchManager.QueryObjects().ContinueWith(t => switchManager.RequestView());

            // Wait for all initialization to complete
            Task.Factory.StartNew(() => {
                Task.WaitAll(locTask, fbTask, swTask);
                initialized = true;
            });
        }

        /// <summary>
        /// Have all objects been initialized?
        /// </summary>
        internal bool IsInitialized
        {
            get { return initialized; }
        }
        
        /// <summary>
        /// Send a command to the ECoS
        /// </summary>
        internal Task<Reply> SendCommand(Command command)
        {
            return socket.SendCommand(command);
        }

        /// <summary>
        /// Gets advanced info for the given loc
        /// </summary>
        internal bool TryGetLoc(ILocState locState, bool requestControl, out Loc loc)
        {
            if (!locManager.TryGetLoc(locState, out loc))
                return false;
            if (requestControl && !loc.HasControl)
            {
                loc.RequestControl();
            }
            return true;
        }

        /// <summary>
        /// Gets advanced info for the given junction
        /// </summary>
        internal bool TryGetJunction(IJunctionState junctionState, bool requestControl, out Switch @switch)
        {
            if (!switchManager.TryGetJunction(junctionState, out @switch))
                return false;
            if (requestControl && !@switch.HasControl)
            {
                @switch.RequestControl();
            }
            return true;
        }

        /// <summary>
        /// Cleanup
        /// </summary>
        public void Dispose()
        {
            socket.Dispose();
        }
    }
}

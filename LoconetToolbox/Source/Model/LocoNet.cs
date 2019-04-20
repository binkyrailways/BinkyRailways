using System;
using System.Collections.Generic;
using LocoNetToolBox.Configuration;
using LocoNetToolBox.Protocol;
using LocoNetToolBox.Services;

namespace LocoNetToolBox.Model
{
    /// <summary>
    /// The entire loconet.
    /// That is a combi of loco buffer, configuration and state.
    /// </summary>
    /// <remarks>This class and it's members are multi-threaded.</remarks>
    public sealed class LocoNet : IDisposable
    {
        private readonly LocoBuffer lb;
        private readonly LocoNetConfiguration configuration;
        private readonly LocoNetState state;
        private readonly UnknownLocoIODetector unknownLocoIoDetector;

        /// <summary>
        /// Default ctor
        /// </summary>
        public LocoNet(LocoBuffer lb, LocoNetConfiguration configuration)
        {
            this.lb = lb;
            this.configuration = configuration;
            state = new LocoNetState(lb);
            unknownLocoIoDetector = new UnknownLocoIODetector(this);
        }

        /// <summary>
        /// Gets the stored configuration
        /// </summary>
        public LocoNetConfiguration Configuration { get { return configuration; } }

        /// <summary>
        /// Gets the loco buffer
        /// </summary>
        public LocoBuffer LocoBuffer { get { return lb; } }

        /// <summary>
        /// Gets the current state
        /// </summary>
        public LocoNetState State { get { return state; } }

        /// <summary>
        /// Have new loco IO units been found?
        /// </summary>
        public bool HasNewLocoIOs
        {
            get { return unknownLocoIoDetector.HasNewLocoIOs; }
        }

        /// <summary>
        /// Get all new loco IO units?
        /// </summary>
        public IEnumerable<LocoIO> NewLocoIOs
        {
            get { return unknownLocoIoDetector.NewLocoIOs; }
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            state.Dispose();
            lb.Close();
        }
    }
}

namespace BinkyRailways.Core.State.Impl.Virtual
{
    /// <summary>
    /// Virtual mode implementation
    /// </summary>
    internal class VirtualMode : IVirtualMode
    {
        private readonly bool enabled;
        private readonly RailwayState railwayState;
        private readonly AutoRunState autoRunState;

        /// <summary>
        /// Default ctor
        /// </summary>
        public VirtualMode(bool enabled, RailwayState railwayState)
        {
            this.enabled = enabled;
            this.railwayState = railwayState;
            autoRunState = new AutoRunState(railwayState);
        }

        /// <summary>
        /// Is virtual mode enabled?
        /// </summary>
        public bool Enabled { get { return enabled; } }

        /// <summary>
        /// Automatically run locs?
        /// </summary>
        public bool AutoRun
        {
            get { return autoRunState.Enabled; }
            set { autoRunState.Enabled = value && enabled; }
        }

        /// <summary>
        /// Entity is being clicked on.
        /// </summary>
        public void EntityClick(IEntityState entity)
        {
            if (enabled)
            {
                var sensor = entity as ISensorState;
                if (sensor != null)
                {
                    railwayState.Dispatcher.PostAction(() => sensor.Active.Actual = !sensor.Active.Actual);
                }
            }
        }

        public override string ToString()
        {
            return string.Format("Enabled {0}", Enabled);
        }
    }
}

using BinkyRailways.Core.Util;

namespace BinkyRailways.Core.State
{
    /// <summary>
    /// Event argument for UnexpectedSensorActivated event.
    /// </summary>
    public class UnexpectedSensorActivatedEventArgs : PropertyEventArgs<ISensorState>
    {
        /// <summary>
        /// Default ctor
        /// </summary>
        public UnexpectedSensorActivatedEventArgs(ISensorState value) : base(value)
        {
            Handled = false;
        }

        /// <summary>
        /// Has this event been handled?
        /// If not, a ghost train event will cause a global power off.
        /// </summary>
        public bool Handled { get; set; }
    }
}

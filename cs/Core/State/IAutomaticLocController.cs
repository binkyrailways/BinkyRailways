using System;

namespace BinkyRailways.Core.State
{
    /// <summary>
    /// State of the automatic loc controller.
    /// </summary>
    public interface IAutomaticLocController 
    {
        /// <summary>
        /// A sensor was activated which was not expected.
        /// </summary>
        event EventHandler<UnexpectedSensorActivatedEventArgs> UnexpectedSensorActivated;

        /// <summary>
        /// Is automatic loc control active?
        /// </summary>
        IStateProperty<bool> Enabled { get; }
    }
}

using System;
using BinkyRailways.Core.Util;

namespace BinkyRailways.Core.State
{
    /// <summary>
    /// State of a command station that support LocoNet.
    /// </summary>
    public interface ILocoNetCommandStationState : ICommandStationState
    {
        /// <summary>
        /// LocoIO device has been found.
        /// </summary>
        event EventHandler<PropertyEventArgs<ILocoIO>> LocoIOFound;

        /// <summary>
        /// Search for all LocoIO devices.
        /// </summary>
        void QueryLocoIOs();
    }
}

using System;

namespace LocoNetToolBox.Model
{
    /// <summary>
    /// Maintains the state of all inputs and switches in the loconet.
    /// </summary>
    public interface ILocoNetState : IDisposable
    {
        /// <summary>
        /// Fired when the loconet is idle for more then 1 second.
        /// </summary>
        event EventHandler Idle;

        /// <summary>
        /// Event is fired when an input or switch state has changed.
        /// </summary>
        event EventHandler<StateMessage> StateChanged;

        /// <summary>
        /// Event is fired when a query for loco-io units is detected.
        /// </summary>
        event EventHandler LocoIOQuery;

        /// <summary>
        /// Event is fired when a response from a loco-io unit on a query for such units is detected.
        /// </summary>
        event EventHandler<LocoIOEventArgs> LocoIOFound;

        /// <summary>
        /// Wait for the switch with the given address to have the given direction.
        /// </summary>
        /// <param name="address">Loconet address of the switch</param>
        /// <param name="direction">Intended direction</param>
        /// <param name="timeout">Timeout in ms</param>
        /// <returns>True if state is correct, false on timeout</returns>
        bool WaitForSwitchDirection(int address, bool direction, int timeout);
    }
}

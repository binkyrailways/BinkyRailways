using System;
using System.Collections.Generic;
using BinkyRailways.Core.Model;

namespace BinkyRailways.Core.State
{
    /// <summary>
    /// State of a single command station.
    /// </summary>
    public interface ICommandStationState : IEntityState<ICommandStation>
    {
        /// <summary>
        /// Fired when the value of <see cref="Idle"/> has changed.
        /// </summary>
        event EventHandler IdleChanged;

        /// <summary>
        /// Gets the entity
        /// </summary>
        ICommandStation Model { get; }

        /// <summary>
        /// Junctions driven by this command station
        /// </summary>
        IEnumerable<IJunctionState> Junctions { get; }

        /// <summary>
        /// Locomotives driven by this command station
        /// </summary>
        IEnumerable<ILocState> Locs { get; }

        /// <summary>
        /// Input signals driven by this command station (overlaps with <see cref="Sensors"/>).
        /// </summary>
        IEnumerable<IInputState> Inputs { get; }

        /// <summary>
        /// Sensors driven by this command station
        /// </summary>
        IEnumerable<ISensorState> Sensors { get; }

        /// <summary>
        /// Signals driven by this command station
        /// </summary>
        IEnumerable<ISignalState> Signals { get; }

        /// <summary>
        /// Can this command station be used to serve the given network?
        /// </summary>
        /// <param name="entity">The entity being search for.</param>
        /// <param name="network">The network in question</param>
        /// <param name="exactMatch">Set to true when there is an exact match in address type and address space, false otherwise.</param>
        bool Supports(IAddressEntity entity, Network network, out bool exactMatch);

        /// <summary>
        /// Enable/disable power on the railway
        /// </summary>
        IStateProperty<bool> Power { get; }

        /// <summary>
        /// Has the command station not send or received anything for a while.
        /// </summary>
        bool Idle { get; }

        /// <summary>
        /// Send the speed and direction of the given loc towards the railway.
        /// </summary>
        void SendLocSpeedAndDirection(ILocState loc);

        /// <summary>
        /// Send the direction of the given switch towards the railway.
        /// </summary>
        void SendSwitchDirection(ISwitchState @switch);

        /// <summary>
        /// Send the position of the given turntable towards the railway.
        /// </summary>
        void SendTurnTablePosition(ITurnTableState turnTable);
    }
}

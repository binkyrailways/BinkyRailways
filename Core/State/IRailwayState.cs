using System;
using BinkyRailways.Core.Model;
using BinkyRailways.Core.Util;

namespace BinkyRailways.Core.State
{
    /// <summary>
    /// State of an entire railway.
    /// </summary>
    public interface IRailwayState : IEntityState<IRailway>
    {
        /// <summary>
        /// Unknown sensor has been detected.
        /// </summary>
        event EventHandler<PropertyEventArgs<Address>> UnknownSensor;

        /// <summary>
        /// Unknown standard switch has been detected.
        /// </summary>
        event EventHandler<PropertyEventArgs<Address>> UnknownSwitch;

        /// <summary>
        /// Gets the railway entity (model)
        /// </summary>
        IRailway Model { get; }

        /// <summary>
        /// Prepare this state for use in a live railway. 
        /// Make sure all relevant connections to other state objects are resolved.
        /// Check the IsReadyForUse property afterwards if it has succeeded.
        /// </summary>
        void PrepareForUse(IStateUserInterface ui, IStatePersistence statePersistence);

        /// <summary>
        /// Gets the state of the automatic loc controller?
        /// </summary>
        IAutomaticLocController AutomaticLocController { get; }

        /// <summary>
        /// Gets the state dispatcher
        /// </summary>
        IStateDispatcher Dispatcher { get; }

        /// <summary>
        /// Gets the virtual mode.
        /// This will never return null.
        /// </summary>
        IVirtualMode VirtualMode { get; }

        /// <summary>
        /// Get the model time
        /// </summary>
        IActualStateProperty<Time> ModelTime { get; }
        
        /// <summary>
        /// Enable/disable power on all of the command stations of this railway
        /// </summary>
        IStateProperty<bool> Power { get; }

        /// <summary>
        /// Gets the states of all blocks in this railway
        /// </summary>
        IEntityStateSet<IBlockState, IBlock> BlockStates { get; }

        /// <summary>
        /// Gets the states of all command stations in this railway
        /// </summary>
        IEntityStateSet<ICommandStationState, ICommandStation> CommandStationStates { get; }

        /// <summary>
        /// Gets the states of all junctions in this railway
        /// </summary>
        IEntityStateSet<IJunctionState, IJunction> JunctionStates { get; }

        /// <summary>
        /// Gets the states of all locomotives in this railway
        /// </summary>
        IEntityStateSet<ILocState, ILoc> LocStates { get; }

        /// <summary>
        /// Gets the states of all routes in this railway
        /// </summary>
        IEntityStateList<IRouteState, IRoute> RouteStates { get; }

        /// <summary>
        /// Gets the states of all sensors in this railway
        /// </summary>
        IEntityStateSet<ISensorState, ISensor> SensorStates { get; }

        /// <summary>
        /// Gets the states of all signals in this railway
        /// </summary>
        IEntityStateSet<ISignalState, ISignal> SignalStates { get; }

        /// <summary>
        /// Gets the states of all outputs in this railway
        /// </summary>
        IEntityStateSet<IOutputState, IOutput> OutputStates { get; }
    }
}

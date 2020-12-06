using System.Reflection;

namespace BinkyRailways.Core.State
{
    /// <summary>
    /// State of a loc in the automatic control mode.
    /// </summary>
    public enum AutoLocState
    {
        /// <summary>
        /// Loc has not been assigned a route, but is ready to be assigned and run that route.
        /// If the loc is no longer is automatic mode, it will be removed from the automatic loc controller.
        /// If no suitable route can be found, the loc will stay in this state.
        /// When the loc has been assigned a route, the route will be prepared and the state will change
        /// to <see cref="WaitingForAssignedRouteReady"/>.
        /// </summary>
        [Obfuscation]
        AssignRoute,

        /// <summary>
        /// The loc that was reversing is changing direction back to normal.
        /// Once the direction is consistent, the state will change to <see cref="AssignRoute"/>.
        /// </summary>
        [Obfuscation]
        ReversingWaitingForDirectionChange,

        /// <summary>
        /// The loc has been assigned a route and it waiting for this route to become ready.
        /// Typically all junctions in the route will be set in the correct position now.
        /// When the route is ready, the state will change to <see cref="Running"/>.
        /// </summary>
        [Obfuscation]
        WaitingForAssignedRouteReady,

        /// <summary>
        /// The loc is running the assigned route.
        /// The state of the loc will not change until a sensor trigger is received.
        /// </summary>
        [Obfuscation]
        Running,

        /// <summary>
        /// The loc has triggered one of the 'entering destination' sensors of the assigned route.
        /// No changes are made to the loc state when switching to this state.
        /// </summary>
        [Obfuscation]
        EnterSensorActivated,

        /// <summary>
        /// The loc has triggered one of the 'entering destination' sensors of the assigned route.
        /// The state of the loc will not change until a 'reached destination' sensor trigger is received.
        /// </summary>
        [Obfuscation]
        EnteringDestination,

        /// <summary>
        /// The loc has triggered one of the 'reached destination' sensors of the assigned route.
        /// No changes are made to the loc state when switching to this state.
        /// </summary>
        [Obfuscation]
        ReachedSensorActivated,

        /// <summary>
        /// The loc has triggered one of the 'reached destination' sensors of the assigned route.
        /// If the destination let's the loc wait, a timeout is started and the state is changed to 
        /// <see cref="WaitingForDestinationTimeout"/>. 
        /// Otherwise the state will change to <see cref="AssignRoute"/>.
        /// If the loc is no longer is automatic mode, it will be removed from the automatic loc controller.
        /// </summary>
        [Obfuscation]
        ReachedDestination,

        /// <summary>
        /// The loc has stopped at the destination and is waiting for a timeout until it can be assigned
        /// a new route.
        /// </summary>
        [Obfuscation]
        WaitingForDestinationTimeout,

        /// <summary>
        /// The loc has stopped at the destination and is waiting for a requirement on the group that contains the destination block.
        /// </summary>
        [Obfuscation]
        WaitingForDestinationGroupMinimum
    }
}

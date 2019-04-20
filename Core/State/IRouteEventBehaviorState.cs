using BinkyRailways.Core.Model;

namespace BinkyRailways.Core.State
{
    /// <summary>
    /// State of a single route event behavior.
    /// </summary>
    public interface IRouteEventBehaviorState : IEntityState<IRouteEventBehavior>
    {
        /// <summary>
        /// Does this behavior apply to the given loc?
        /// </summary>
        bool AppliesTo(ILocState loc);

        /// <summary>
        /// How is the state of the route changed.
        /// </summary>
        RouteStateBehavior StateBehavior { get; }

        /// <summary>
        /// How is the speed of the occupying loc changed.
        /// </summary>
        LocSpeedBehavior SpeedBehavior { get; }
    }
}

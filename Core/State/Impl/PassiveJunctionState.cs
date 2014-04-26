using BinkyRailways.Core.Model;

namespace BinkyRailways.Core.State.Impl
{
    /// <summary>
    /// State of a single standard switch.
    /// </summary>
    public sealed class PassiveJunctionState : JunctionState<IPassiveJunction>, IPassiveJunctionState
    {
        /// <summary>
        /// Default ctor
        /// </summary>
        public PassiveJunctionState(IPassiveJunction entity, RailwayState railwayState)
            : base(entity, railwayState)
        {
        }
    }
}

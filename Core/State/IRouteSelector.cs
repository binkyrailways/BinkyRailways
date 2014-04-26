using System.Collections.Generic;
using BinkyRailways.Core.Model;

namespace BinkyRailways.Core.State
{
    /// <summary>
    /// Choose a next route for a loc.
    /// </summary>
    public interface IRouteSelector
    {
        /// <summary>
        /// Select one of the given possible routes.
        /// Returns null if no route should be taken.
        /// </summary>
        /// <param name="possibleRoutes">A list of routes to choose from</param>
        /// <param name="loc">The loc to choose for</param>
        /// <param name="fromBlock">The block from which the next route will leave</param>
        /// <param name="locDirection">The direction the loc is facing in the <see cref="fromBlock"/>.</param>
        IRouteState SelectRoute(IList<IRouteState> possibleRoutes, ILocState loc, IBlockState fromBlock, BlockSide locDirection);

        /// <summary>
        /// Called when the loc has entered the given to-block of the current route.
        /// </summary>
        void BlockEntered(ILocState loc, IBlockState block);

        /// <summary>
        /// Called when the loc has reached the given to-block of the current route.
        /// </summary>
        void BlockReached(ILocState loc, IBlockState block);
    }
}

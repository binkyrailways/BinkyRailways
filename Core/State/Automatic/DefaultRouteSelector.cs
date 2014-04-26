using System;
using System.Collections.Generic;
using System.Linq;
using BinkyRailways.Core.Model;
using BinkyRailways.Core.Util;

namespace BinkyRailways.Core.State.Automatic
{
    /// <summary>
    /// Route selector that chooses a next route based on random behavior influenced by recently visited routes.
    /// </summary>
    internal class DefaultRouteSelector : IRouteSelector
    {
        /// <summary>
        /// Select one of the given possible routes.
        /// Returns null if no route should be taken.
        /// </summary>
        public IRouteState SelectRoute(IList<IRouteState> possibleRoutes, ILocState loc, IBlockState fromBlock, BlockSide locDirection)
        {
            // If there is only 1 choice, take that
            if (possibleRoutes.Count == 1)
                return possibleRoutes[0];

            // Get all options with their choose probability
            var options = possibleRoutes.Select(x => Tuple.Create(x, x.ChooseProbability)).ToArray();

            // Lower the probability for routes towards blocks that we've just visited.
            var offset = 2;
            foreach (var blockState in loc.RecentlyVisitedBlocks)
            {
                var multiplier = 1.0 - (1.0 / offset);
                for (var i = 0; i < options.Length; i++)
                {
                    var option = options[i];
                    if (option.Item1.To == blockState)
                    {
                        // Lower probability of this route
                        options[i] = Tuple.Create(option.Item1, (int)(option.Item2 * multiplier));
                    }
                }
                offset++;
            }

            // If there are options with probability 0, remove them, unless all options are like that.
            if (options.Any(x => x.Item2 == 0) && options.Any(x => x.Item2 > 0))
            {
                // Remove all options with probability 0.
                options = options.Where(x => x.Item2 > 0).ToArray();
            }

            // Take a random route
            return ThreadStatics.Random.Gamble(options);
        }

        /// <summary>
        /// Called when the loc has entered the given to-block of the current route.
        /// </summary>
        public void BlockEntered(ILocState loc, IBlockState block)
        {
            // Do nothing
        }

        /// <summary>
        /// Called when the loc has reached the given to-block of the current route.
        /// </summary>
        public void BlockReached(ILocState loc, IBlockState block)
        {
            // Do nothing
        }
    }
}

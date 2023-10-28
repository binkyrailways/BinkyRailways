using System.Collections.Generic;
using System.Linq;
using BinkyRailways.Core.Model;
using BinkyRailways.Core.State.Impl;

namespace BinkyRailways.Core.State.Automatic
{
    /// <summary>
    /// Route selector that guides a loc towards a specific block.
    /// </summary>
    public class TargetBlockRouteSelector : IRouteSelector
    {
        private readonly IBlockState targetBlock;

        /// <summary>
        /// Default ctor
        /// </summary>
        public TargetBlockRouteSelector(IBlockState targetBlock)
        {
            this.targetBlock = targetBlock;
        }

        public IBlockState TargetBlock { get { return targetBlock; } }

        /// <summary>
        /// Select one of the given possible routes.
        /// Returns null if no route should be taken.
        /// </summary>
        /// <param name="possibleRoutes">A list of routes to choose from</param>
        /// <param name="loc">The loc to choose for</param>
        /// <param name="fromBlock">The block from which the next route will leave</param>
        /// <param name="locDirection">The direction the loc is facing in the <see cref="fromBlock"/>.</param>
        public IRouteState SelectRoute(IList<IRouteState> possibleRoutes, ILocState loc, IBlockState fromBlock, BlockSide locDirection)
        {
            // Look for a direct match
            var directRoutes = possibleRoutes.Where(x => x.To == targetBlock).ToList();
            if (directRoutes.Any())
                return new DefaultRouteSelector().SelectRoute(directRoutes, loc, fromBlock, locDirection);

            // Look for routes that get us closer to the target block.
            var sequences = FindRouteSequences(fromBlock, locDirection.Invert(), targetBlock).ToList();

            // Filter sequences to start with possible routes
            var possibleSequences = sequences.Where(x => possibleRoutes.Contains(x.First)).OrderBy(x => x.Length).ToList();

            // Any sequences left?
            if (possibleSequences.Count == 0)
                return null;

            // Take the first possible sequence.
            return possibleSequences[0].First;
        }

        /// <summary>
        /// Called when the loc has entered the given to-block of the current route.
        /// </summary>
        public void BlockEntered(ILocState loc, IBlockState block)
        {
        }

        /// <summary>
        /// Called when the loc has reached the given to-block of the current route.
        /// </summary>
        public void BlockReached(ILocState loc, IBlockState block)
        {
            if (block == targetBlock)
            {
                TargetReached(loc);
            }
        }

        /// <summary>
        /// The target block has been reached.
        /// </summary>
        protected virtual void TargetReached(ILocState loc)
        {
            loc.RouteSelector = null;
            loc.ControlledAutomatically.Requested = false;
        }

        /// <summary>
        /// Find all route sequences that lead from the given side of the given "from" block to the given "to" block.
        /// </summary>
        public static IEnumerable<IRouteSequence> FindRouteSequences(IBlockState fromBlock, BlockSide fromBlockEnterSide, IBlockState toBlock)
        {
            var railway = fromBlock.RailwayState;
            var fromBlockLeaveSide = fromBlockEnterSide.Invert();
            var startRoutes = railway.RouteStates.Where(x => (x.From == fromBlock) && (x.FromBlockSide == fromBlockLeaveSide)).ToList();
            var initialSequences = startRoutes.Select(x => new RouteSequence(x));
            return FindRouteSequences(initialSequences, railway, toBlock, railway.RouteStates.Count);
        }

        /// <summary>
        /// Find all sequences to the given toBlock that start with the given sequences.
        /// </summary>
        private static IEnumerable<RouteSequence> FindRouteSequences(IEnumerable<RouteSequence> sequences, IRailwayState railway, IBlockState toBlock, int maxLength)
        {
            foreach (var seq in sequences)
            {
                if (seq.Last.To == toBlock)
                {
                    yield return seq;
                }
                else if (seq.Length < maxLength)
                {
                    var lastRoute = seq.Last;
                    foreach (var route in railway.RouteStates)
                    {
                        if ((route.From == lastRoute.To) && (lastRoute.ToBlockSide == route.FromBlockSide.Invert()))
                        {
                            var newSeq = new RouteSequence(seq) { route };
                            foreach (var s in FindRouteSequences(new[] { newSeq }, railway, toBlock, maxLength))
                            {
                                yield return s;
                            }
                        }                        
                    }
                }
            }
        }
    }
}

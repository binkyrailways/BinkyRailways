using System;
using System.Collections.Generic;
using System.Linq;
using BinkyRailways.Core.Model;

namespace BinkyRailways.Core.State.Impl
{
    partial class RouteState
    {
        /// <summary>
        /// Helper class used to build the critical section routes for a specific route.
        /// </summary>
        private class CriticalSectionBuilder
        {
            private readonly IRouteState route;
            private readonly IRailwayState railway;

            /// <summary>
            /// Default ctor
            /// </summary>
            public CriticalSectionBuilder(IRouteState route, IRailwayState railway)
            {
                this.route = route;
                this.railway = railway;
            }

            /// <summary>
            /// Build the critical section routes list.
            /// </summary>
            public CriticalSectionRoutes Build()
            {
                // Build a list of all possible target blocks
                var blocks = new List<BlockAndSide>();
                var iterator = new BlockAndSide(route);
                while (true)
                {
                    // Are there any routes to the opposite side of the iterator block?
                    if (!AnyRoutesTo(iterator.Block, iterator.EnterSide.Invert()))
                    {
                        // No routes leading into the opposite side of the to block.
                        // No further critical section
                        break;
                    }
                    if (blocks.Count > 0)
                    {
                        // Are there more then 'exits' from the iterator
                        var iteratorExitCount = GetNextBlocks(iterator.Block, iterator.EnterSide).Count;
                        if (iteratorExitCount > 1)
                        {
                            // Multiple routes in the opposite direction possible
                            break;
                        }
                    }
                    // Take routes to the iterator into the critical section
                    blocks.Add(iterator);

                    // Find the blocks where we can go to from the iterator.
                    var nextBlocks = GetNextBlocks(iterator.Block, iterator.EnterSide.Invert());
                    if (nextBlocks.Count != 1)
                    {
                        // We've found multiple or no routes, stop now
                        break;
                    }
                    // Only 1 route found, continue
                    iterator = nextBlocks[0];
                    if (blocks.Contains(iterator))
                    {
                        // Circle found, stop now
                        break;
                    }
                }

                // Now build a list of all routes targeting the blocks (and sides) in the block list.
                var routes = new List<IRouteState>();
                foreach (var b in blocks)
                {
                    var targetBlock = b.Block;
                    var targetSide = b.EnterSide.Invert();
                    var routesTo = railway.RouteStates.Where(x => (x.To == targetBlock) && (x.ToBlockSide == targetSide));
                    routes.AddRange(routesTo);                    
                }

                // Look for the reverse of this route (if any)
                var reverse = route.GetReverse();
                if (reverse != null)
                {
                    routes.Add(reverse);
                }

                return new CriticalSectionRoutes(routes);
            }

            /// <summary>
            /// Create a list of blocks reachable from the given block.
            /// </summary>
            private List<BlockAndSide> GetNextBlocks(IBlockState fromBlock, BlockSide fromSide)
            {
                return railway.RouteStates.Where(x => (x.From == fromBlock) && (x.FromBlockSide == fromSide)).Select(x => new BlockAndSide(x)).ToList();
            }

            /// <summary>
            /// Are there any routes leading to the given block that enter the to block
            /// at the given to side?
            /// </summary>
            private bool AnyRoutesTo(IBlockState toBlock, BlockSide toSide)
            {
                return railway.RouteStates.Any(x => (x.To == toBlock) && (x.ToBlockSide == toSide));
            }
        }

        /// <summary>
        /// Target block and block side at which the block is entered.
        /// </summary>
        private sealed class BlockAndSide : IEquatable<BlockAndSide>
        {
            private readonly IBlockState block;
            private readonly BlockSide enterSide;

            /// <summary>
            /// Default ctor
            /// </summary>
            public BlockAndSide(IRouteState route)
            {
                block = route.To;                
                enterSide = route.ToBlockSide;
            }

            public BlockSide EnterSide
            {
                get { return enterSide; }
            }

            public IBlockState Block
            {
                get { return block; }
            }

            /// <summary>
            /// Indicates whether the current object is equal to another object of the same type.
            /// </summary>
            public bool Equals(BlockAndSide other)
            {
                return (other != null) && (other.block == block) && (other.enterSide == enterSide);
            }

            /// <summary>
            /// Indicates whether the current object is equal to another object of the same type.
            /// </summary>
            public override bool Equals(object obj)
            {
                return Equals(obj as BlockAndSide);
            }

            /// <summary>
            /// Serves as a hash function for a particular type. 
            /// </summary>
            public override int GetHashCode()
            {
                return block.GetHashCode() ^ ((int)enterSide << 16);
            }
        }

    }
}

using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using BinkyRailways.Core.Model;

namespace BinkyRailways.Core.State.Automatic
{
    /// <summary>
    /// Route availability tester for a future state (stored in this object) of the railway.
    /// </summary>
    internal sealed class FutureRouteAvailabilityTester : RouteAvailabilityTester
    {
        private readonly Dictionary<ILocState, LocState> locStates = new Dictionary<ILocState, LocState>();

        /// <summary>
        /// Initialize from current state
        /// </summary>
        internal FutureRouteAvailabilityTester(IRailwayState railwayState)
            : base(railwayState)
        {
            foreach (var loc in railwayState.LocStates)
            {
                locStates[loc] = new LocState(loc.CurrentBlock.Actual, loc.CurrentBlockEnterSide.Actual);
            }
        }

        /// <summary>
        /// Initialize from given state
        /// </summary>
        internal FutureRouteAvailabilityTester(FutureRouteAvailabilityTester source)
            : base(source.railwayState)
        {
            foreach (var loc in railwayState.LocStates)
            {
                locStates[loc] = new LocState(source.locStates[loc]);
            }
        }

        /// <summary>
        /// Expose the railway state
        /// </summary>
        public IRailwayState RailwayState
        {
            get { return railwayState; }
        }

        /// <summary>
        /// Let the given loc take the given route.
        /// </summary>
        public void TakeRoute(IRouteState route, ILocState loc)
        {
            locStates[loc].ChangeTo(route.To, route.ToBlockSide);
        }

        /// <summary>
        /// Gets the current block of the given lock.
        /// </summary>
        public IBlockState GetCurrentBlock(ILocState loc)
        {
            return locStates[loc].CurrentBlock;
        }

        /// <summary>
        /// Gets the current block enter side of the given lock.
        /// </summary>
        public Model.BlockSide GetCurrentBlockEnterSide(ILocState loc)
        {
            return locStates[loc].CurrentBlockEnterSide;
        }

        /// <summary>
        /// Gets the loc that has locked the given block.
        /// </summary>
        public ILocState GetLockedBy(IBlockState block)
        {
            return locStates.Where(x => x.Value.CurrentBlock == block).Select(x => x.Key).FirstOrDefault();
        }

        /// <summary>
        /// Can the given route be locked for the given loc?
        /// </summary>
        protected override bool CanLock(IRouteState route, ILocState loc, out ILocState lockedBy)
        {
            lockedBy = GetLockedBy(route.To);
            return (lockedBy == null) || (lockedBy == loc);
        }
        
        /// <summary>
        /// Is there are traffic in the opposite direction of the given to-block of a route?
        /// </summary>
        protected override bool HasTrafficInOppositeDirection(IBlockState toBlock, BlockSide toBlockSide, ILocState currentLoc)
        {
            var loc = GetLockedBy(toBlock);
            if ((loc != null) && (loc != currentLoc))
            {
                // Check direction
                var locEnterSide = GetCurrentBlockEnterSide(loc);
                if (locEnterSide != toBlockSide)
                {
                    // loc is in opposing direction
                    if (!loc.CanChangeDirectionIn(toBlock))
                    {
                        // The loc cannot change direction in to block, so there is absolutely opposite traffic.
                        return true;
                    }
                }
                else
                {
                    // Loc is in same direction, we're ok
                    return false;
                }
            }
            return false;
        }

        /// <summary>
        /// Is the critical section for the given route free for the given loc?
        /// </summary>
        protected override bool IsCriticalSectionFree(IRouteState route, ILocState loc)
        {
            return true;
        }

        /// <summary>
        /// Is any of the sensors of the given route active?
        /// Sensors that are also in the current route of the given loc are ignored.
        /// </summary>
        protected override bool IsAnySensorActive(IRouteState route, ILocState loc)
        {
            return false;
        }

        public override string ToString()
        {
            return string.Join(", ", locStates.Where(x => x.Value.CurrentBlock != null).Select(x => string.Format("{0}[{1}]", x.Key.Description, x.Value)));
        }

        /// <summary>
        /// Maintain the (future) state of a loc.
        /// </summary>
        [DebuggerDisplay("{CurrentBlock} {CurrentBlockEnterSide}")]
        private class LocState
        {
            private IBlockState currentBlock;
            private BlockSide currentBlockEnterSide;

            /// <summary>
            /// Default ctor
            /// </summary>
            public LocState(IBlockState currentBlock, BlockSide currentBlockEnterSide)
            {
                this.currentBlock = currentBlock;
                this.currentBlockEnterSide = currentBlockEnterSide;
            }

            /// <summary>
            /// Copy ctor
            /// </summary>
            public LocState(LocState source)
            {
                currentBlock = source.currentBlock;
                currentBlockEnterSide = source.currentBlockEnterSide;
            }

            public IBlockState CurrentBlock { get { return currentBlock; } }
            public BlockSide CurrentBlockEnterSide { get { return currentBlockEnterSide; } }

            public void ChangeTo(IBlockState currentBlock, BlockSide currentBlockEnterSide)
            {
                this.currentBlock = currentBlock;
                this.currentBlockEnterSide = currentBlockEnterSide;                
            }

            public override string ToString()
            {
                if (currentBlock == null) return "-";
                return string.Format("{0} {1}", currentBlock.Description, currentBlockEnterSide);
            }
        }
    }
}

using System.Collections.Generic;
using System.Linq;
using BinkyRailways.Core.Model;

namespace BinkyRailways.Core.State.Impl
{
    /// <summary>
    /// State of a single block group.
    /// </summary>
    public sealed class BlockGroupState : EntityState<IBlockGroup>, IBlockGroupState
    {
        private readonly List<IBlockState> blocks = new List<IBlockState>();

        /// <summary>
        /// Default ctor
        /// </summary>
        public BlockGroupState(IBlockGroup blockGroup, RailwayState railwayState)
            : base(blockGroup, railwayState)
        {
        }

        /// <summary>
        /// Gets all blocks in this group.
        /// </summary>
        public IEnumerable<IBlockState> Blocks { get { return blocks; } }

        /// <summary>
        /// The minimum number of locs that must be present in this group.
        /// Locs cannot leave if that results in a lower number of locs in this group.
        /// </summary>
        public int MinimumLocsInGroup { get { return Entity.MinimumLocsInGroup; } }

        /// <summary>
        /// Is the condition met to require the minimum number of locs in this group?
        /// </summary>
        public bool MinimumLocsInGroupEnabled
        {
            get
            {
                var assignedLocs = RailwayState.LocStates.Count(x => x.CanSetAutomaticControl);
                return (assignedLocs >= Entity.MinimumLocsOnTrackForMinimumLocsInGroupStart);
            }
        }

        /// <summary>
        /// Are there enough locs in this group so that one lock can leave?
        /// </summary>
        public bool FirstLocCanLeave
        {
            get
            {
                // Do we have enough locs on the track?
                if (!MinimumLocsInGroupEnabled) return true;

                // Ys we have, now enforce the minimum
                var waitingLocs = blocks.Count(x => x.HasWaitingLoc);
                return (waitingLocs > MinimumLocsInGroup);
            }
        }

        /// <summary>
        /// Prepare this state for use in a live railway. 
        /// Make sure all relevant connections to other state objects are resolved.
        /// </summary>
        /// <returns>True if the entity is now ready for use in a live railway, false otherwise.</returns>
        protected override bool TryPrepareForUse(IStateUserInterface ui, IStatePersistence statePersistence)
        {
            blocks.Clear();
            var myBlockEntities = Entity.Module.Blocks.Where(x => x.BlockGroup == Entity);
            blocks.AddRange(myBlockEntities.Select(x => RailwayState.BlockStates[x]));

            return true;
        }
    }
}

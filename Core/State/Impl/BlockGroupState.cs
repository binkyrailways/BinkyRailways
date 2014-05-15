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

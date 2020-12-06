using System;
using System.ComponentModel;

namespace BinkyRailways.Core.Model.Impl
{
    /// <summary>
    /// Group of blocks that share a similar function.
    /// </summary>
    public sealed class BlockGroup : ModuleEntity, IBlockGroup
    {
        private readonly Property<int> minimumLocsInGroup;
        private readonly Property<int> minimumLocsOnTrackForMinimumLocsInGroupStart;

        /// <summary>
        /// Default ctor
        /// </summary>
        public BlockGroup()
        {
            minimumLocsInGroup = new Property<int>(this, DefaultValues.DefaultBlockGroupMinimumLocsInGroup);
            minimumLocsOnTrackForMinimumLocsInGroupStart = new Property<int>(this, DefaultValues.DefaultBlockGroupMinimumLocsOnTrackForMinimumLocsInGroupStart);
        }

        /// <summary>
        /// The minimum number of locs that must be present in this group.
        /// Locs cannot leave if that results in a lower number of locs in this group.
        /// </summary>
        [DefaultValue(DefaultValues.DefaultBlockGroupMinimumLocsInGroup)]
        public int MinimumLocsInGroup
        {
            get { return minimumLocsInGroup.Value; }
            set { minimumLocsInGroup.Value = Math.Max(0, value); }
        }

        /// <summary>
        /// The minimum number of locs that must be on the track before the <see cref="MinimumLocsInGroup"/> becomes active.
        /// </summary>
        [DefaultValue(DefaultValues.DefaultBlockGroupMinimumLocsOnTrackForMinimumLocsInGroupStart)]
        public int MinimumLocsOnTrackForMinimumLocsInGroupStart
        {
            get { return minimumLocsOnTrackForMinimumLocsInGroupStart.Value; }
            set { minimumLocsOnTrackForMinimumLocsInGroupStart.Value = Math.Max(0, value); }
        }

        /// <summary>
        /// Accept a visit by the given visitor
        /// </summary>
        public override TReturn Accept<TReturn, TData>(EntityVisitor<TReturn, TData> visitor, TData data)
        {
            return visitor.Visit(this, data);
        }

        /// <summary>
        /// Human readable name of this type of entity.
        /// </summary>
        public override string TypeName
        {
            get { return Strings.TypeNameBlockGroup; }
        }
    }
}

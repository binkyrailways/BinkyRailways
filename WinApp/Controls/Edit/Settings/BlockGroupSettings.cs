using System.ComponentModel;
using System.Reflection;
using BinkyRailways.Core.Model;

namespace BinkyRailways.WinApp.Controls.Edit.Settings
{
    /// <summary>
    /// Settings wrapper for a block group.
    /// </summary>
    [Obfuscation(Feature = "@NodeSettings")]
    internal sealed class BlockGroupSettings : EntitySettings<IBlockGroup>
    {
        /// <summary>
        /// Default ctor
        /// </summary>
        internal BlockGroupSettings(IBlockGroup entity, GridContext context)
            : base(entity, context)
        {
        }

        /// <summary>
        /// Add all visible properties of this settings object to the given property collection.
        /// </summary>
        public override void GatherProperties(ExPropertyDescriptorCollection properties)
        {
            base.GatherProperties(properties);
            properties.Add(() => MinimumLocsInGroup, Strings.TabBehavior, Strings.MinimumLocsInGroupName, Strings.MinimumLocsInGroupHelp);
            properties.Add(() => MinimumLocsOnTrackForMinimumLocsInGroupStart, Strings.TabBehavior, Strings.MinimumLocsOnTrackForMinimumLocsInGroupStartName, Strings.MinimumLocsOnTrackForMinimumLocsInGroupStartHelp);
        }

        /// <summary>
        /// The minimum number of locs that must be present in this group.
        /// Locs cannot leave if that results in a lower number of locs in this group.
        /// </summary>
        [DefaultValue(DefaultValues.DefaultBlockGroupMinimumLocsInGroup)]
        [EditableInRunningState]
        public int MinimumLocsInGroup
        {
            get { return Entity.MinimumLocsInGroup; }
            set { Entity.MinimumLocsInGroup = value; }
        }

        /// <summary>
        /// The minimum number of locs that must be on the track before the <see cref="MinimumLocsInGroup"/> becomes active.
        /// </summary>
        [DefaultValue(DefaultValues.DefaultBlockGroupMinimumLocsOnTrackForMinimumLocsInGroupStart)]
        [EditableInRunningState]
        public int MinimumLocsOnTrackForMinimumLocsInGroupStart
        {
            get { return Entity.MinimumLocsOnTrackForMinimumLocsInGroupStart; }
            set { Entity.MinimumLocsOnTrackForMinimumLocsInGroupStart = value; }
        }
    }
}

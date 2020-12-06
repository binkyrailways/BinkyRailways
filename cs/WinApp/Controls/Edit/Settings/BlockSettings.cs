using System.ComponentModel;
using System.Drawing.Design;
using System.Reflection;
using BinkyRailways.Core.Model;
using BinkyRailways.WinApp.TypeConverters;
using BinkyRailways.WinApp.UIEditors;

namespace BinkyRailways.WinApp.Controls.Edit.Settings
{
    /// <summary>
    /// Settings wrapper for a block.
    /// </summary>
    [Obfuscation(Feature = "@NodeSettings")]
    internal sealed class BlockSettings : PositionedEntitySettings<IBlock>
    {
        /// <summary>
        /// Default ctor
        /// </summary>
        internal BlockSettings(IBlock entity, GridContext context)
            : base(entity, context)
        {
        }

        /// <summary>
        /// Add all visible properties of this settings object to the given property collection.
        /// </summary>
        public override void GatherProperties(ExPropertyDescriptorCollection properties)
        {
            base.GatherProperties(properties);
            properties.Add(() => WaitProbability, Strings.TabBehavior, Strings.WaitProbabilityName, Strings.WaitProbabilityHelp);
            properties.Add(() => WaitPermissions, Strings.TabAdvBehavior, Strings.WaitPermissionsName, Strings.WaitPermissionsHelp);
            if ((WaitProbability > 0) || ChangeDirectionReversingLocs)
            {
                properties.Add(() => MinimumWaitTime, Strings.TabBehavior, Strings.MinimumWaitTimeName, Strings.MinimumWaitTimeHelp);
                properties.Add(() => MaximumWaitTime, Strings.TabBehavior, Strings.MaximumWaitTimeName, Strings.MaximumWaitTimeHelp);
            }
            properties.Add(() => ReverseSides, Strings.TabDesign, Strings.ReverseSidesName, Strings.ReverseSidesHelp);
            properties.Add(() => ChangeDirection, Strings.TabBehavior, Strings.ChangeDirectionName, Strings.ChangeDirectionHelp);
            properties.Add(() => ChangeDirectionReversingLocs, Strings.TabAdvBehavior, Strings.ChangeDirectionReversingLocsName, Strings.ChangeDirectionReversingLocsHelp);
            properties.Add(() => StationMode, Strings.TabBehavior, Strings.StationModeName, Strings.StationModeHelp);
            properties.Add(() => BlockGroup, Strings.TabBehavior, Strings.BlockGroupName, Strings.BlockGroupHelp);
        }

        /// <summary>
        /// Probability (in percentage) that a loc that is allowed to wait in this block
        /// will actually wait.
        /// When set to 0, no locs will wait (unless there is no route available).
        /// When set to 100, all locs (that are allowed) will wait.
        /// </summary>
        [TypeConverter(typeof(PercentageTypeConverter))]
        [DefaultValue(DefaultValues.DefaultBlockWaitProbability)]
        [EditableInRunningState]
        public int WaitProbability
        {
            get { return Entity.WaitProbability; }
            set { Entity.WaitProbability = value; }
        }

        /// <summary>
        /// Minimum amount of time to wait (if <see cref="WaitProbability"/> is set) in seconds.
        /// </summary>
        [DefaultValue(DefaultValues.DefaultBlockMinimumWaitTime)]
        [EditableInRunningState]
        public int MinimumWaitTime
        {
            get { return Entity.MinimumWaitTime; }
            set { Entity.MinimumWaitTime = value; }
        }

        /// <summary>
        /// Maximum amount of time to wait (if <see cref="WaitProbability"/> is set) in seconds.
        /// </summary>
        [DefaultValue(DefaultValues.DefaultBlockMaximumWaitTime)]
        [EditableInRunningState]
        public int MaximumWaitTime
        {
            get { return Entity.MaximumWaitTime; }
            set { Entity.MaximumWaitTime = value; }
        }

        /// <summary>
        /// Wait in this block permission
        /// </summary>
        [TypeConverter(typeof(EntityTypeConverter))]
        [Editor(typeof(LocPredicateEditor), typeof(UITypeEditor))]
        [MergableProperty(false)]
        public ILocPredicate WaitPermissions
        {
            get { return Entity.WaitPermissions; }
        }

        /// <summary>
        /// By default the front of the block is on the right of the block.
        /// When this property is set, that is reversed to the left of the block.
        /// Setting this property will only alter the display behavior of the block.
        /// </summary>
        [TypeConverter(typeof(BoolTypeConverter))]
        [DefaultValue(DefaultValues.DefaultBlockReverseSides)]
        public bool ReverseSides
        {
            get { return Entity.ReverseSides; }
            set { Entity.ReverseSides = value; }
        }

        /// <summary>
        /// Is it allowed for locs to change direction in this block?
        /// </summary>
        [TypeConverter(typeof(ChangeDirectionTypeConverter))]
        [DefaultValue(DefaultValues.DefaultBlockChangeDirection)]
        [EditableInRunningState]
        public ChangeDirection ChangeDirection
        {
            get { return Entity.ChangeDirection; }
            set { Entity.ChangeDirection = value; }
        }

        /// <summary>
        /// Must reversing locs change direction (back to normal) in this block?
        /// </summary>
        [TypeConverter(typeof(BoolTypeConverter))]
        [DefaultValue(DefaultValues.DefaultBlockChangeDirectionReversingLocs)]
        [EditableInRunningState]
        public bool ChangeDirectionReversingLocs
        {
            get { return Entity.ChangeDirectionReversingLocs; }
            set { Entity.ChangeDirectionReversingLocs = value; }
        }

        /// <summary>
        /// Determines how the system decides if this block is part of a station
        /// </summary>
        [TypeConverter(typeof(StationModeTypeConverter))]
        [DefaultValue(DefaultValues.DefaultBlockStationMode)]
        [EditableInRunningState]
        public StationMode StationMode
        {
            get { return Entity.StationMode; }
            set { Entity.StationMode = value; }
        }

        /// <summary>
        /// The block group this block belongs to.
        /// </summary>
        [TypeConverter(typeof(EntityTypeConverter))]
        [Editor(typeof(OptionalBlockGroupEditor), typeof(UITypeEditor))]
        [MergableProperty(true)]
        [DefaultValue(null)]
        public IBlockGroup BlockGroup
        {
            get { return Entity.BlockGroup; }
            set { Entity.BlockGroup = value; }
        }
    }
}

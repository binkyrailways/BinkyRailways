using System.ComponentModel;
using System.Drawing.Design;
using System.Reflection;
using BinkyRailways.Core.Model;
using BinkyRailways.WinApp.TypeConverters;
using BinkyRailways.WinApp.UIEditors;

namespace BinkyRailways.WinApp.Controls.Edit.Settings
{
    [Obfuscation(Feature = "@NodeSettings")]
    internal sealed class BlockSignalSettings : SignalSettings<IBlockSignal>
    {
        /// <summary>
        /// Default ctor
        /// </summary>
        internal BlockSignalSettings(IBlockSignal entity, GridContext context)
            : base(entity, context)
        {
        }

        /// <summary>
        /// Gets the entity being edited.
        /// </summary>
        internal new IBlockSignal Entity
        {
            get { return base.Entity; }
        }

        /// <summary>
        /// Add all visible properties of this settings object to the given property collection.
        /// </summary>
        public override void GatherProperties(ExPropertyDescriptorCollection properties)
        {
            base.GatherProperties(properties);
            properties.Add(() => Address1, Strings.TabGeneral, "Address 1", "Address of bit 1.");
            properties.Add(() => Address2, Strings.TabGeneral, "Address 2", "Address of bit 2.");
            properties.Add(() => Address3, Strings.TabGeneral, "Address 3", "Address of bit 3.");
            properties.Add(() => Address4, Strings.TabGeneral, "Address 4", "Address of bit 4.");
            properties.Add(() => RedPattern, Strings.TabBehavior, "Red bit pattern", "Bit pattern needed to set the Red color.");
            properties.Add(() => GreenPattern, Strings.TabBehavior, "Green bit pattern", "Bit pattern needed to set the Green color.");
            properties.Add(() => YellowPattern, Strings.TabBehavior, "Yellow bit pattern", "Bit pattern needed to set the Yellow color.");
            properties.Add(() => WhitePattern, Strings.TabBehavior, "White bit pattern", "Bit pattern needed to set the White color.");
            properties.Add(() => Block, Strings.TabGeneral, "Block", "Block this signal protects.");
            properties.Add(() => Position, Strings.TabGeneral, "Position", "Side the block this signal is facing.");
            properties.Add(() => Type, Strings.TabGeneral, "Type", "Type of signal.");
        }

        [Editor(typeof(AddressEditor), typeof(UITypeEditor))]
        [MergableProperty(false)]
        public Address Address1
        {
            get { return Entity.Address1; }
            set { Entity.Address1 = value; }
        }

        [Editor(typeof(AddressEditor), typeof(UITypeEditor))]
        [MergableProperty(false)]
        public Address Address2
        {
            get { return Entity.Address2; }
            set { Entity.Address2 = value; }
        }

        [Editor(typeof(AddressEditor), typeof(UITypeEditor))]
        [MergableProperty(false)]
        public Address Address3
        {
            get { return Entity.Address3; }
            set { Entity.Address3 = value; }
        }

        [Editor(typeof(AddressEditor), typeof(UITypeEditor))]
        [MergableProperty(false)]
        public Address Address4
        {
            get { return Entity.Address4; }
            set { Entity.Address4 = value; }
        }

        /// <summary>
        /// Bit pattern used for color Red.
        /// </summary>
        [Editor(typeof(BlockSignalRedPatternEditor), typeof(UITypeEditor))]
        [TypeConverter(typeof(BlockSignalPatternTypeConverter))]
        [MergableProperty(false)]
        [EditableInRunningState]
        public int RedPattern
        {
            get { return Entity.RedPattern; }
            set { Entity.RedPattern = value; }
        }

        /// <summary>
        /// Bit pattern used for color Green.
        /// </summary>
        [Editor(typeof(BlockSignalGreenPatternEditor), typeof(UITypeEditor))]
        [TypeConverter(typeof(BlockSignalPatternTypeConverter))]
        [MergableProperty(false)]
        [EditableInRunningState]
        public int GreenPattern
        {
            get { return Entity.GreenPattern; }
            set { Entity.GreenPattern = value; }
        }

        /// <summary>
        /// Bit pattern used for color Yellow.
        /// Set to 0 when Yellow is not supported.
        /// </summary>
        [Editor(typeof(BlockSignalYellowPatternEditor), typeof(UITypeEditor))]
        [TypeConverter(typeof(BlockSignalPatternTypeConverter))]
        [MergableProperty(false)]
        [EditableInRunningState]
        public int YellowPattern
        {
            get { return Entity.YellowPattern; }
            set { Entity.YellowPattern = value; }
        }

        /// <summary>
        /// Bit pattern used for color White.
        /// Set to 0 when White is not supported.
        /// </summary>
        [Editor(typeof(BlockSignalWhitePatternEditor), typeof(UITypeEditor))]
        [TypeConverter(typeof(BlockSignalPatternTypeConverter))]
        [MergableProperty(false)]
        [EditableInRunningState]
        public int WhitePattern
        {
            get { return Entity.WhitePattern; }
            set { Entity.WhitePattern = value; }
        }

        /// <summary>
        /// The block this signal protects.
        /// </summary>
        [TypeConverter(typeof(EntityTypeConverter))]
        [Editor(typeof(BlockEditor), typeof(UITypeEditor))]
        public IBlock Block 
        {
            get { return Entity.Block; }
            set { Entity.Block = value; }
        }

        /// <summary>
        /// Side of the block where the signal is located.
        /// </summary>
        [TypeConverter(typeof(BlockSideTypeConverter))]
        public BlockSide Position
        {
            get { return Entity.Position; }
            set { Entity.Position = value; }
        }

        /// <summary>
        /// Type of signal
        /// </summary>
        [TypeConverter(typeof(BlockSignalTypeTypeConverter))]
        public BlockSignalType Type
        {
            get { return Entity.Type; }
            set { Entity.Type = value; }
        }
    }
}

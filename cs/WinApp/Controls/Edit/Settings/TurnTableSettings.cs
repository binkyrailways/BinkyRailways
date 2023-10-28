using System.ComponentModel;
using System.Drawing.Design;
using System.Reflection;
using BinkyRailways.Core.Model;
using BinkyRailways.WinApp.TypeConverters;
using BinkyRailways.WinApp.UIEditors;

namespace BinkyRailways.WinApp.Controls.Edit.Settings
{
    [Obfuscation(Feature = "@NodeSettings")]
    internal sealed class TurnTableSettings : JunctionSettings<ITurnTable>
    {
        /// <summary>
        /// Default ctor
        /// </summary>
        internal TurnTableSettings(ITurnTable entity, GridContext context)
            : base(entity, context)
        {
        }

        /// <summary>
        /// Add all visible properties of this settings object to the given property collection.
        /// </summary>
        public override void GatherProperties(ExPropertyDescriptorCollection properties)
        {
            base.GatherProperties(properties);
            properties.Add(() => PositionAddress1, Strings.TabGeneral, "Position address 1", "Address of position bit 1.");
            properties.Add(() => PositionAddress2, Strings.TabGeneral, "Position address 2", "Address of position bit 2.");
            properties.Add(() => PositionAddress3, Strings.TabGeneral, "Position address 3", "Address of position bit 3.");
            properties.Add(() => PositionAddress4, Strings.TabGeneral, "Position address 4", "Address of position bit 4.");
            properties.Add(() => PositionAddress5, Strings.TabGeneral, "Position address 5", "Address of position bit 5.");
            properties.Add(() => PositionAddress6, Strings.TabGeneral, "Position address 6", "Address of position bit 6.");
            properties.Add(() => InvertPositions, Strings.TabGeneral, "Invert position addresses", "When set, position bits are sent inverted to the turntable.");
            properties.Add(() => WriteAddress, Strings.TabGeneral, "Write signal address", "Address of 'write address in progress bit'.");
            properties.Add(() => InvertWrite, Strings.TabGeneral, "Invert write signal", "When set, write signal address bits is inverted.");
            properties.Add(() => BusyAddress, Strings.TabGeneral, "Busy signal address", "Address of 'turntable is busy bit'.");
            properties.Add(() => InvertBusy, Strings.TabGeneral, "Invert busy signal", "When set, busy signal address bits is inverted.");
            properties.Add(() => FirstPosition, Strings.TabGeneral, "First position", "Lowest position number allowed by the turntable.");
            properties.Add(() => LastPosition, Strings.TabGeneral, "Last position", "Highest position number allowed by the turntable.");
            properties.Add(() => InitialPosition, Strings.TabBehavior, "Initial position", "Position number used to initialize the turntable with.");
        }

        [Editor(typeof(AddressEditor), typeof(UITypeEditor))]
        [MergableProperty(false)]
        public Address PositionAddress1
        {
            get { return Entity.PositionAddress1; }
            set { Entity.PositionAddress1 = value; }
        }

        [Editor(typeof(AddressEditor), typeof(UITypeEditor))]
        [MergableProperty(false)]
        public Address PositionAddress2
        {
            get { return Entity.PositionAddress2; }
            set { Entity.PositionAddress2 = value; }
        }

        [Editor(typeof(AddressEditor), typeof(UITypeEditor))]
        [MergableProperty(false)]
        public Address PositionAddress3
        {
            get { return Entity.PositionAddress3; }
            set { Entity.PositionAddress3 = value; }
        }

        [Editor(typeof(AddressEditor), typeof(UITypeEditor))]
        [MergableProperty(false)]
        public Address PositionAddress4
        {
            get { return Entity.PositionAddress4; }
            set { Entity.PositionAddress4 = value; }
        }

        [Editor(typeof(AddressEditor), typeof(UITypeEditor))]
        [MergableProperty(false)]
        public Address PositionAddress5
        {
            get { return Entity.PositionAddress5; }
            set { Entity.PositionAddress5 = value; }
        }

        [Editor(typeof(AddressEditor), typeof(UITypeEditor))]
        [MergableProperty(false)]
        public Address PositionAddress6
        {
            get { return Entity.PositionAddress6; }
            set { Entity.PositionAddress6 = value; }
        }

        [TypeConverter(typeof(BoolTypeConverter))]
        public bool InvertPositions
        {
            get { return Entity.InvertPositions; }
            set { Entity.InvertPositions = value; }
        }

        [Editor(typeof(AddressEditor), typeof(UITypeEditor))]
        [MergableProperty(false)]
        public Address WriteAddress
        {
            get { return Entity.WriteAddress; }
            set { Entity.WriteAddress  = value; }
        }

        [TypeConverter(typeof(BoolTypeConverter))]
        public bool InvertWrite
        {
            get { return Entity.InvertWrite; }
            set { Entity.InvertWrite = value; }
        }

        [Editor(typeof(AddressEditor), typeof(UITypeEditor))]
        [MergableProperty(false)]
        public Address BusyAddress
        {
            get { return Entity.BusyAddress; }
            set { Entity.BusyAddress = value; }
        }

        [TypeConverter(typeof(BoolTypeConverter))]
        public bool InvertBusy
        {
            get { return Entity.InvertBusy; }
            set { Entity.InvertBusy = value; }
        }

        public int InitialPosition
        {
            get { return Entity.InitialPosition; }
            set { Entity.InitialPosition = value; }
        }

        public int FirstPosition
        {
            get { return Entity.FirstPosition; }
            set { Entity.FirstPosition = value; }
        }

        public int LastPosition
        {
            get { return Entity.LastPosition; }
            set { Entity.LastPosition = value; }
        }
    }
}

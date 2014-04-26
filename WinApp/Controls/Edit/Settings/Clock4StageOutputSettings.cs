using System.ComponentModel;
using System.Drawing.Design;
using System.Reflection;
using BinkyRailways.Core.Model;
using BinkyRailways.WinApp.TypeConverters;
using BinkyRailways.WinApp.UIEditors;

namespace BinkyRailways.WinApp.Controls.Edit.Settings
{
    /// <summary>
    /// Settings wrapper for a 4-stage clock output.
    /// </summary>
    [Obfuscation(Feature = "@NodeSettings")]
    internal sealed class Clock4StageOutputSettings : OutputSettings<IClock4StageOutput>
    {
        /// <summary>
        /// Default ctor
        /// </summary>
        internal Clock4StageOutputSettings(IClock4StageOutput entity, GridContext context)
            : base(entity, context)
        {
        }

        /// <summary>
        /// Gets the entity being edited.
        /// </summary>
        internal new IClock4StageOutput Entity
        {
            get { return base.Entity; }
        }

        /// <summary>
        /// Add all visible properties of this settings object to the given property collection.
        /// </summary>
        public override void GatherProperties(ExPropertyDescriptorCollection properties)
        {
            base.GatherProperties(properties);
            properties.Add(() => Address1, Strings.TabGeneral, "Address 1", "Address of bit 1");
            properties.Add(() => Address2, Strings.TabGeneral, "Address 2", "Address of bit 2");

            properties.Add(() => MorningPattern, Strings.TabBehavior, "Morning pattern", "Bit pattern used to indicate 'morning'");
            properties.Add(() => AfternoonPattern, Strings.TabBehavior, "Afternoot pattern", "Bit pattern used to indicate 'afternoon'");
            properties.Add(() => EveningPattern, Strings.TabBehavior, "Evening pattern", "Bit pattern used to indicate 'evening'");
            properties.Add(() => NightPattern, Strings.TabBehavior, "Night pattern", "Bit pattern used to indicate 'night'");
        }

        /// <summary>
        /// Clock bit 0 address
        /// </summary>
        [Editor(typeof(AddressEditor), typeof(UITypeEditor))]
        [MergableProperty(false)]
        public Address Address1
        {
            get { return Entity.Address1; }
            set { Entity.Address1 = value; }
        }

        /// <summary>
        /// Clock bit 1 address
        /// </summary>
        [Editor(typeof(AddressEditor), typeof(UITypeEditor))]
        [MergableProperty(false)]
        public Address Address2
        {
            get { return Entity.Address2; }
            set { Entity.Address2 = value; }
        }

        /// <summary>
        /// Bit pattern used for "morning".
        /// </summary>
        [Editor(typeof(Clock4StageOutputMorningPatternEditor), typeof(UITypeEditor))]
        [TypeConverter(typeof(Clock4StageOutputPatternTypeConverter))]
        [MergableProperty(false)]
        [EditableInRunningState]
        public int MorningPattern
        {
            get { return Entity.MorningPattern; }
            set { Entity.MorningPattern = value; }
        }

        /// <summary>
        /// Bit pattern used for "afternoon".
        /// </summary>
        [Editor(typeof(Clock4StageOutputAfternoonPatternEditor), typeof(UITypeEditor))]
        [TypeConverter(typeof(Clock4StageOutputPatternTypeConverter))]
        [MergableProperty(false)]
        [EditableInRunningState]
        public int AfternoonPattern
        {
            get { return Entity.AfternoonPattern; }
            set { Entity.AfternoonPattern = value; }
        }

        /// <summary>
        /// Bit pattern used for "evening".
        /// </summary>
        [Editor(typeof(Clock4StageOutputEveningPatternEditor), typeof(UITypeEditor))]
        [TypeConverter(typeof(Clock4StageOutputPatternTypeConverter))]
        [MergableProperty(false)]
        [EditableInRunningState]
        public int EveningPattern
        {
            get { return Entity.EveningPattern; }
            set { Entity.EveningPattern = value; }
        }

        /// <summary>
        /// Bit pattern used for "night".
        /// </summary>
        [Editor(typeof(Clock4StageOutputNightPatternEditor), typeof(UITypeEditor))]
        [TypeConverter(typeof(Clock4StageOutputPatternTypeConverter))]
        [MergableProperty(false)]
        [EditableInRunningState]
        public int NightPattern
        {
            get { return Entity.NightPattern; }
            set { Entity.NightPattern = value; }
        }
    }
}

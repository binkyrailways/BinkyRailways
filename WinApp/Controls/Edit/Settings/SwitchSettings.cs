using System.ComponentModel;
using System.Drawing.Design;
using System.Reflection;
using BinkyRailways.Core.Model;
using BinkyRailways.WinApp.TypeConverters;
using BinkyRailways.WinApp.UIEditors;

namespace BinkyRailways.WinApp.Controls.Edit.Settings
{
    [Obfuscation(Feature = "@NodeSettings")]
    internal sealed class SwitchSettings : JunctionSettings<ISwitch>
    {
        /// <summary>
        /// Default ctor
        /// </summary>
        internal SwitchSettings(ISwitch entity, GridContext context)
            : base(entity, context)
        {
        }

        /// <summary>
        /// Add all visible properties of this settings object to the given property collection.
        /// </summary>
        public override void GatherProperties(ExPropertyDescriptorCollection properties)
        {
            base.GatherProperties(properties);
            properties.Add(() => Address, Strings.TabGeneral, Strings.AddressName, Strings.AddressHelp);
            if (HasFeedback)
            {
                properties.Add(() => FeedbackAddress, Strings.TabGeneral, Strings.FeedbackAddressName, Strings.FeedbackAddressHelp);
            }
            properties.Add(() => HasFeedback, Strings.TabBehavior, Strings.SwitchHasFeedbackName, Strings.SwitchHasFeedbackHelp);
            if (!HasFeedback)
            {
                properties.Add(() => SwitchDuration, Strings.TabBehavior, Strings.SwitchDurationName, Strings.SwitchDurationHelp);
            }
            properties.Add(() => Invert, Strings.TabBehavior, Strings.SwitchInvertedName, Strings.SwitchInvertedHelp);
            if (HasFeedback && (FeedbackAddress != null))
            {
                properties.Add(() => InvertFeedback, Strings.TabBehavior, Strings.SwitchFeedbackInvertedName, Strings.SwitchFeedbackInvertedHelp);
            }
            properties.Add(() => InitialDirection, Strings.TabBehavior, Strings.SwitchInitialDirectionName, Strings.SwitchInitialDirectionHelp);
        }

        [Editor(typeof(AddressEditor), typeof(UITypeEditor))]
        [MergableProperty(false)]
        public Address Address
        {
            get { return Entity.Address; }
            set { Entity.Address = value; }
        }

        [Editor(typeof(AddressEditor), typeof(UITypeEditor))]
        [MergableProperty(false)]
        public Address FeedbackAddress
        {
            get { return Entity.FeedbackAddress; }
            set { Entity.FeedbackAddress = value; }
        }

        /// <summary>
        /// Does this switch send feedback when switched?
        /// </summary>
        [TypeConverter(typeof(BoolTypeConverter))]
        public bool HasFeedback
        {
            get { return Entity.HasFeedback; }
            set { Entity.HasFeedback = value; }
        }

        /// <summary>
        /// Time (in ms) it takes for the switch to move from one direction to the other?
        /// This property is only used when <see cref="HasFeedback"/> is false.
        /// </summary>
        public int SwitchDuration
        {
            get { return Entity.SwitchDuration; }
            set { Entity.SwitchDuration = value; }
        }

        /// <summary>
        /// Are the Straight/Off commands inverted?
        /// </summary>
        [TypeConverter(typeof(BoolTypeConverter))]
        public bool Invert
        {
            get { return Entity.Invert; }
            set { Entity.Invert = value; }
        }

        /// <summary>
        /// Are the Straight/Off feedback events inverted?
        /// </summary>
        [TypeConverter(typeof(BoolTypeConverter))]
        public bool InvertFeedback
        {
            get { return Entity.InvertFeedback; }
            set { Entity.InvertFeedback = value; }
        }

        /// <summary>
        /// Direction set at power on
        /// </summary>
        [TypeConverter(typeof(SwitchDirectionTypeConverter))]
        public SwitchDirection InitialDirection
        {
            get { return Entity.InitialDirection; }
            set { Entity.InitialDirection = value; }
        }
    }
}

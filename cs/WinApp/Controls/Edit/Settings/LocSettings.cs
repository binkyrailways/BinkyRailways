using System.ComponentModel;
using System.Drawing.Design;
using System.IO;
using System.Reflection;
using BinkyRailways.Core.Model;
using BinkyRailways.WinApp.TypeConverters;
using BinkyRailways.WinApp.UIEditors;

namespace BinkyRailways.WinApp.Controls.Edit.Settings
{
    [Obfuscation(Feature = "@NodeSettings")]
    internal sealed class LocSettings : PersistentEntitySettings<ILoc>
    {
        /// <summary>
        /// Default ctor
        /// </summary>
        internal LocSettings(ILoc entity, GridContext context)
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
            properties.Add(() => Image, Strings.TabDesign, Strings.LocImageName, Strings.LocImageHelp);
            properties.Add(() => MaximumSpeed, Strings.TabBehavior, Strings.MaximumSpeedName, Strings.LocMaximumSpeedHelp);
            properties.Add(() => MediumSpeed, Strings.TabBehavior, Strings.MediumSpeedName,
                           "Speed of this loc used to enter a block where this loc should stop (as percentage of the maximum #speed steps).");
            properties.Add(() => SlowSpeed, Strings.TabBehavior, Strings.MinimumSpeedName,
                           "Speed of this loc used to enter a block where this loc should stop (as percentage of the maximum #speed steps).");
            properties.Add(() => ChangeDirection, Strings.TabBehavior, Strings.ChangeDirectionName, Strings.LocChangeDirectionHelp);
            properties.Add(() => Functions, Strings.TabGeneral, Strings.LocFunctionsName, Strings.LocFunctionsHelp);
            properties.Add(() => Owner, Strings.TabGeneral, Strings.LocOwnerName, Strings.LocOwnerHelp);
            properties.Add(() => Remarks, Strings.TabGeneral, Strings.LocRemarksName, Strings.LocRemarksHelp);
        }

        [Editor(typeof(AddressEditor), typeof(UITypeEditor))]
        [MergableProperty(false)]
        public Address Address
        {
            get { return Entity.Address; }
            set { Entity.Address = value; }
        }

        [Editor(typeof(ImageEditor), typeof(UITypeEditor))]
        [TypeConverter(typeof(ImageTypeConverter))]
        [MergableProperty(false)]
        [EditableInRunningState]
        public Stream Image
        {
            get { return Entity.Image; }
            set { Entity.Image = value; }
        }

        [TypeConverter(typeof(SpeedTypeConverter))]
        [DefaultValue(DefaultValues.DefaultLocMaximumSpeed)]
        [EditableInRunningState]
        public int MaximumSpeed
        {
            get { return Entity.MaximumSpeed; }
            set { Entity.MaximumSpeed = value; }
        }

        [TypeConverter(typeof(SpeedTypeConverter))]
        [DefaultValue(DefaultValues.DefaultLocMediumSpeed)]
        [EditableInRunningState]
        public int MediumSpeed
        {
            get { return Entity.MediumSpeed; }
            set { Entity.MediumSpeed = value; }
        }

        [TypeConverter(typeof(SpeedTypeConverter))]
        [DefaultValue(DefaultValues.DefaultLocSlowSpeed)]
        [EditableInRunningState]
        public int SlowSpeed
        {
            get { return Entity.SlowSpeed; }
            set { Entity.SlowSpeed = value; }
        }

        /// <summary>
        /// Is it allowed for this loc to change direction?
        /// </summary>
        [TypeConverter(typeof(ChangeDirectionTypeConverter))]
        [DefaultValue(DefaultValues.DefaultLocChangeDirection)]
        [EditableInRunningState]
        public ChangeDirection ChangeDirection
        {
            get { return Entity.ChangeDirection; }
            set { Entity.ChangeDirection = value; }
        }

        /// <summary>
        /// Functions supported by this loc
        /// </summary>
        [TypeConverter(typeof(LocFunctionsTypeConverter))]
        [Editor(typeof(LocFunctionsEditor), typeof(UITypeEditor))]
        [MergableProperty(false)]
        public ILocFunctions Functions
        {
            get { return Entity.Functions; }
        }

        /// <summary>
        /// Gets/sets the name of the person that owns this loc.
        /// </summary>
        [DefaultValue(DefaultValues.DefaultLocOwner)]
        [EditableInRunningState]
        public string Owner
        {
            get { return Entity.Owner; }
            set { Entity.Owner = value; }
        }

        /// <summary>
        /// Gets/sets remarks (free text) about this loc.
        /// </summary>
        [Editor(typeof(FreeTextEditor), typeof(UITypeEditor))]
        [DefaultValue(DefaultValues.DefaultLocRemarks)]
        [EditableInRunningState]
        public string Remarks
        {
            get { return Entity.Remarks; }
            set { Entity.Remarks = value; }
        }
    }
}

using System.ComponentModel;
using System.Drawing.Design;
using System.Reflection;
using BinkyRailways.Core.Model;
using BinkyRailways.WinApp.TypeConverters;
using BinkyRailways.WinApp.UIEditors;

namespace BinkyRailways.WinApp.Controls.Edit.Settings
{
    /// <summary>
    /// Settings wrapper for a binary sensor.
    /// </summary>
    [Obfuscation(Feature = "@NodeSettings")]
    internal sealed class BinarySensorSettings : SensorSettings<IBinarySensor>
    {
        /// <summary>
        /// Default ctor
        /// </summary>
        internal BinarySensorSettings(IBinarySensor entity, GridContext context)
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
            properties.Add(() => ActionTriggerSource, Strings.TabBehavior, Strings.ActionsName, Strings.ActionsHelp);
        }

        /// <summary>
        /// Sensor address
        /// </summary>
        [Editor(typeof(AddressEditor), typeof(UITypeEditor))]
        [MergableProperty(false)]
        public Address Address
        {
            get { return Entity.Address; }
            set { Entity.Address = value; }
        }

        /// <summary>
        /// Actions
        /// </summary>
        [TypeConverter(typeof(ActionTriggerSourceTypeConverter))]
        [Editor(typeof(ActionTriggerSourceEditor), typeof(UITypeEditor))]
        [MergableProperty(false)]
        public IActionTriggerSource ActionTriggerSource
        {
            get { return Entity; }
        }
    }
}

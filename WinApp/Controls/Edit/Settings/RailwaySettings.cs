using System.ComponentModel;
using System.Drawing.Design;
using System.Reflection;
using BinkyRailways.Core.Model;
using BinkyRailways.WinApp.TypeConverters;
using BinkyRailways.WinApp.UIEditors;

namespace BinkyRailways.WinApp.Controls.Edit.Settings
{
    [Obfuscation(Feature = "@NodeSettings")]
    internal sealed class RailwaySettings : PersistentEntitySettings<IRailway>
    {
        /// <summary>
        /// Default ctor
        /// </summary>
        internal RailwaySettings(IRailway entity, GridContext context)
            : base(entity, context)
        {
        }

        /// <summary>
        /// Add all visible properties of this settings object to the given property collection.
        /// </summary>
        public override void GatherProperties(ExPropertyDescriptorCollection properties)
        {
            base.GatherProperties(properties);
            properties.Add(() => PreferredDccCommandStation, Strings.TabBehavior, Strings.PreferredDccCommandStationName, Strings.PreferredDccCommandStationHelp);
            properties.Add(() => PreferredLocoNetCommandStation, Strings.TabBehavior, Strings.PreferredLocoNetCommandStationName, Strings.PreferredLocoNetCommandStationHelp);
            properties.Add(() => PreferredMotorolaCommandStation, Strings.TabBehavior, Strings.PreferredMotorolaCommandStationName, Strings.PreferredMotorolaCommandStationHelp);
            properties.Add(() => PreferredMfxCommandStation, Strings.TabBehavior, Strings.PreferredMfxCommandStationName, Strings.PreferredMfxCommandStationHelp);
            properties.Add(() => PreferredMqttCommandStation, Strings.TabBehavior, Strings.PreferredMqttCommandStationName, Strings.PreferredMqttCommandStationHelp);
            properties.Add(() => ClockSpeedFactor, Strings.TabBehavior, Strings.ClockSpeedFactorName, Strings.ClockSpeedFactorHelp);
        }

        [DefaultValue(null)]
        [Editor(typeof(PreferredDccCommandStationEditor), typeof(UITypeEditor))]
        [TypeConverter(typeof(EntityTypeConverter))]
        public ICommandStation PreferredDccCommandStation
        {
            get { return Entity.PreferredDccCommandStation; }
            set { Entity.PreferredDccCommandStation = value; }
        }

        [DefaultValue(null)]
        [Editor(typeof(PreferredLocoNetCommandStationEditor), typeof(UITypeEditor))]
        [TypeConverter(typeof(EntityTypeConverter))]
        public ICommandStation PreferredLocoNetCommandStation
        {
            get { return Entity.PreferredLocoNetCommandStation; }
            set { Entity.PreferredLocoNetCommandStation = value; }
        }

        [DefaultValue(null)]
        [Editor(typeof(PreferredMotorolaCommandStationEditor), typeof(UITypeEditor))]
        [TypeConverter(typeof(EntityTypeConverter))]
        public ICommandStation PreferredMotorolaCommandStation
        {
            get { return Entity.PreferredMotorolaCommandStation; }
            set { Entity.PreferredMotorolaCommandStation = value; }
        }

        [DefaultValue(null)]
        [Editor(typeof(PreferredMfxCommandStationEditor), typeof(UITypeEditor))]
        [TypeConverter(typeof(EntityTypeConverter))]
        public ICommandStation PreferredMfxCommandStation
        {
            get { return Entity.PreferredMfxCommandStation; }
            set { Entity.PreferredMfxCommandStation = value; }
        }

        [DefaultValue(null)]
        [Editor(typeof(PreferredMqttCommandStationEditor), typeof(UITypeEditor))]
        [TypeConverter(typeof(EntityTypeConverter))]
        public ICommandStation PreferredMqttCommandStation
        {
            get { return Entity.PreferredMqttCommandStation; }
            set { Entity.PreferredMqttCommandStation = value; }
        }

        /// <summary>
        /// The number of times human time is speed up to reach model time.
        /// </summary>
        [DefaultValue(DefaultValues.DefaultRailwayClockSpeedFactor)]
        [TypeConverter(typeof(ClockSpeedFactorTypeConverter))]
        public int ClockSpeedFactor
        {
            get { return Entity.ClockSpeedFactor; }
            set { Entity.ClockSpeedFactor = value; }
        }
    }
}

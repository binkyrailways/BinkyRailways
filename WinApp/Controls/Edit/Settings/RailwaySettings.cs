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
            properties.Add(() => MqttHostName, Strings.TabServer, Strings.MqttHostNameName, Strings.MqttHostNameHelp);
            properties.Add(() => MqttPort, Strings.TabServer, Strings.MqttPortName, Strings.MqttPortHelp);
            properties.Add(() => MqttTopic, Strings.TabServer, Strings.MqttTopicName, Strings.MqttTopicHelp);
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
     
        [TypeConverter(typeof(ClockSpeedFactorTypeConverter))]
        [EditableInRunningState]
        public int ClockSpeedFactor
        {
            get { return Entity.ClockSpeedFactor; }
            set { Entity.ClockSpeedFactor = value; }
        }

        /// <summary>
        /// Network hostname of the MQTT server to post server messages to.
        /// </summary>
        [DefaultValue(DefaultValues.DefaultRailwayMqttHostName)]
        [MergableProperty(false)]
        public string MqttHostName
        {
            get { return Entity.MqttHostName; }
            set { Entity.MqttHostName = value; }
        }

        /// <summary>
        /// Network port of the MQTT server to post server messages to.
        /// </summary>
        [DefaultValue(DefaultValues.DefaultRailwayMqttPort)]
        [MergableProperty(false)]
        public int MqttPort
        {
            get { return Entity.MqttPort; }
            set { Entity.MqttPort = value; }
        }

        /// <summary>
        /// Topic on the MQTT server to post server messages to.
        /// </summary>
        [DefaultValue(DefaultValues.DefaultRailwayMqttTopic)]
        [MergableProperty(false)]
        public string MqttTopic
        {
            get { return Entity.MqttTopic; }
            set { Entity.MqttTopic = value; }
        }
    }
}

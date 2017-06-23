using System.ComponentModel;
using System.Reflection;
using BinkyRailways.Core.Model;
using BinkyRailways.WinApp.TypeConverters;

namespace BinkyRailways.WinApp.Controls.Edit.Settings
{
    [Obfuscation(Feature = "@NodeSettings")]
    internal sealed class MqttCommandStationSettings : CommandStationSettings<IMqttCommandStation> 
    {
        /// <summary>
        /// Default ctor
        /// </summary>
        internal MqttCommandStationSettings(IMqttCommandStation entity, GridContext context)
            : base(entity, context)
        {
        }

        /// <summary>
        /// Add all visible properties of this settings object to the given property collection.
        /// </summary>
        public override void GatherProperties(ExPropertyDescriptorCollection properties)
        {
            base.GatherProperties(properties);
            properties.Add(() => HostName, Strings.TabConnection, Strings.HostNameName, Strings.HostNameHelp);
            properties.Add(() => Port, Strings.TabConnection, Strings.PortName, Strings.PortHelp);
            properties.Add(() => TopicPrefix, Strings.TabConnection, Strings.TopicPrefixName, Strings.TopicPrefixHelp);
        }

        [MergableProperty(false)]
        public string HostName
        {
            get { return Entity.HostName; }
            set { Entity.HostName = value; }
        }

        [MergableProperty(false)]
        public int Port
        {
            get { return Entity.Port; }
            set { Entity.Port = value; }
        }

        [MergableProperty(false)]
        public string TopicPrefix
        {
            get { return Entity.TopicPrefix; }
            set { Entity.TopicPrefix = value; }
        }
    }
}

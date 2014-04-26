using System.ComponentModel;
using System.Reflection;
using BinkyRailways.Core.Model;
using BinkyRailways.WinApp.TypeConverters;

namespace BinkyRailways.WinApp.Controls.Edit.Settings
{
    [Obfuscation(Feature = "@NodeSettings")]
    internal sealed class EcosCommandStationSettings : CommandStationSettings<IEcosCommandStation> 
    {
        /// <summary>
        /// Default ctor
        /// </summary>
        internal EcosCommandStationSettings(IEcosCommandStation entity, GridContext context)
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
        }

        [MergableProperty(false)]
        public string HostName
        {
            get { return Entity.HostName; }
            set { Entity.HostName = value; }
        }
    }
}

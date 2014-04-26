using System.ComponentModel;
using System.Reflection;
using BinkyRailways.Core.Model;
using BinkyRailways.WinApp.TypeConverters;

namespace BinkyRailways.WinApp.Controls.Edit.Settings
{
    [Obfuscation(Feature = "@NodeSettings")]
    internal sealed class LocoBufferCommandStationSettings : CommandStationSettings<ILocoBufferCommandStation> 
    {
        /// <summary>
        /// Default ctor
        /// </summary>
        internal LocoBufferCommandStationSettings(ILocoBufferCommandStation entity, GridContext context)
            : base(entity, context)
        {
        }

        /// <summary>
        /// Add all visible properties of this settings object to the given property collection.
        /// </summary>
        public override void GatherProperties(ExPropertyDescriptorCollection properties)
        {
            base.GatherProperties(properties);
            properties.Add(() => ComPortName, Strings.TabConnection, Strings.ComPortNameName, Strings.ComPortNameHelp);
        }

        [TypeConverter(typeof(ComPortNameTypeConverter))]
        [MergableProperty(false)]
        public string ComPortName
        {
            get { return Entity.ComPortName; }
            set { Entity.ComPortName = value; }
        }
    }
}

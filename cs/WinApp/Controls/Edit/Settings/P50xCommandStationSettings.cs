using BinkyRailways.Core.Model;
using BinkyRailways.WinApp.TypeConverters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BinkyRailways.WinApp.Controls.Edit.Settings
{
    [Obfuscation(Feature = "@NodeSettings")]
    internal sealed class P50xCommandStationSettings : CommandStationSettings<IP50xCommandStation>
    {
        /// <summary>
        /// Default ctor
        /// </summary>
        internal P50xCommandStationSettings(IP50xCommandStation entity, GridContext context)
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

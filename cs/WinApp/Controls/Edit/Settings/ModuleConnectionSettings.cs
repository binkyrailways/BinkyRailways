using System.ComponentModel;
using System.Drawing.Design;
using System.Reflection;
using BinkyRailways.Core.Model;
using BinkyRailways.WinApp.TypeConverters;
using BinkyRailways.WinApp.UIEditors;

namespace BinkyRailways.WinApp.Controls.Edit.Settings
{
    [Obfuscation(Feature = "@NodeSettings")]
    internal sealed class ModuleConnectionSettings : EntitySettings<IModuleConnection>
    {
        /// <summary>
        /// Default ctor
        /// </summary>
        internal ModuleConnectionSettings(IModuleConnection entity, GridContext context)
            : base(entity, context)
        {
        }

        /// <summary>
        /// Add all visible properties of this settings object to the given property collection.
        /// </summary>
        public override void GatherProperties(ExPropertyDescriptorCollection properties)
        {
            base.GatherProperties(properties);
            properties.Add(() => EdgeA, Strings.TabGeneral, "Edge A", "The edge on the first module of this connection.");
            properties.Add(() => EdgeB, Strings.TabGeneral, "Edge B", "The edge on the second module of this connection.");
        }

        [TypeConverter(typeof(GlobalEntityTypeConverter))]
        [Editor(typeof(EdgeEditor), typeof(UITypeEditor))]
        public IEdge EdgeA
        {
            get { return Entity.EdgeA; }
            set { Entity.EdgeA = value; }
        }

        [TypeConverter(typeof(GlobalEntityTypeConverter))]
        [Editor(typeof(EdgeEditor), typeof(UITypeEditor))]
        public IEdge EdgeB
        {
            get { return Entity.EdgeB; }
            set { Entity.EdgeB = value; }
        }

        /// <summary>
        /// Should the description property be shown?
        /// </summary>
        protected override bool ShowDescription
        {
            get { return false; }
        }
    }
}

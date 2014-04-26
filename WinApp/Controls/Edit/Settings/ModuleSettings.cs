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
    internal sealed class ModuleSettings : PersistentEntitySettings<IModule>
    {
        /// <summary>
        /// Default ctor
        /// </summary>
        internal ModuleSettings(IModule entity, GridContext context)
            : base(entity, context)
        {
        }

        /// <summary>
        /// Add all visible properties of this settings object to the given property collection.
        /// </summary>
        public override void GatherProperties(ExPropertyDescriptorCollection properties)
        {
            base.GatherProperties(properties);
            properties.Add(() => Image, Strings.TabDesign, Strings.ModuleBackgroundImageName, Strings.ModuleBackgroundImageHelp);
        }

        [Editor(typeof(ImageEditor), typeof(UITypeEditor))]
        [TypeConverter(typeof(ImageTypeConverter))]
        [MergableProperty(false)]
        public Stream Image
        {
            get { return Entity.BackgroundImage; }
            set { Entity.BackgroundImage = value; Context.ReloadView(); }
        }
    }
}

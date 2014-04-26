using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using BinkyRailways.Core.Model;
using BinkyRailways.WinApp.TypeConverters;

namespace BinkyRailways.WinApp.Controls.Edit.Settings
{
    [Obfuscation(Feature = "@NodeSettings")]
    internal sealed class ModuleRefSettings : PositionedEntitySettings<IModuleRef>
    {
        /// <summary>
        /// Default ctor
        /// </summary>
        internal ModuleRefSettings(IModuleRef entity, GridContext context)
            : base(entity, context)
        {
        }

        /// <summary>
        /// Add all visible properties of this settings object to the given property collection.
        /// </summary>
        public override void GatherProperties(ExPropertyDescriptorCollection properties)
        {
            base.GatherProperties(properties);
            properties.Add(() => ZoomFactor, Strings.TabDesign, "Zoom factor", "Use this property to scale the module");
            properties.Add(() => Size, Strings.TabPosition, Strings.SizeName, Strings.ModuleSizeHelp);
            properties.Add(() => Description, Strings.TabGeneral, Strings.DescriptionName, Strings.DescriptionHelp);
        }

        /// <summary>
        /// Should the description property be shown?
        /// </summary>
        protected override bool ShowDescription
        {
            get { return false; }
        }

        /// <summary>
        /// Should width and height be shown?
        /// </summary>
        protected override bool ShowSize
        {
            get { return false; }
        }

        /// <summary>
        /// Zoomfactor used in displaying the module (in percentage).
        /// </summary>
        /// <value>100 means 100%</value>
        [TypeConverter(typeof(PercentageTypeConverter))]
        [DefaultValue(DefaultValues.DefaultModuleRefZoomFactor)]
        public int ZoomFactor
        {
            get { return Entity.ZoomFactor; }
            set { Entity.ZoomFactor = value; }
        }

        /// <summary>
        /// Size (in pixels) of this entity.
        /// </summary>
        [Category("Position")]
        [MergableProperty(false)]
        public Size Size
        {
            get { return new Size(Entity.Width, Entity.Height); }
        }

        /// <summary>
        /// Human readable description
        /// </summary>
        [Category("General")]
        public new string Description
        {
            get { return Entity.Description; }
        }
    }
}

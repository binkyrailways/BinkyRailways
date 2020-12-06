using System.ComponentModel;
using System.Drawing.Design;
using System.Reflection;
using BinkyRailways.Core.Model;
using BinkyRailways.WinApp.TypeConverters;
using BinkyRailways.WinApp.UIEditors;

namespace BinkyRailways.WinApp.Controls.Edit.Settings
{
    [Obfuscation(Feature = "@NodeSettings")]
    internal abstract class SensorSettings<T> : PositionedEntitySettings<T>
        where T : class, ISensor
    {
        /// <summary>
        /// Default ctor
        /// </summary>
        internal SensorSettings(T entity, GridContext context)
            : base(entity, context)
        {
        }

        /// <summary>
        /// Add all visible properties of this settings object to the given property collection.
        /// </summary>
        public override void GatherProperties(ExPropertyDescriptorCollection properties)
        {
            properties.Add(() => Block, Strings.TabGeneral, Strings.SensorBlockName, Strings.SensorBlockHelp);
            properties.Add(() => Shape, Strings.TabDesign, Strings.SensorShapeName, Strings.SensorShapeHelp);
            base.GatherProperties(properties);
        }

        /// <summary>
        /// The block this signal protects.
        /// </summary>
        [TypeConverter(typeof(EntityTypeConverter))]
        [Editor(typeof(OptionalBlockEditor), typeof(UITypeEditor))]
        [MergableProperty(true)]
        [DefaultValue(null)]
        public IBlock Block
        {
            get { return Entity.Block; }
            set { Entity.Block = value; }
        }

        /// <summary>
        /// Shape used to visualize this sensor
        /// </summary>
        [TypeConverter(typeof(ShapesTypeConverter))]
        [MergableProperty(true)]
        [DefaultValue(DefaultValues.DefaultSensorShape)]
        public Shapes Shape
        {
            get { return Entity.Shape; }
            set { Entity.Shape = value; }
        }
    }
}

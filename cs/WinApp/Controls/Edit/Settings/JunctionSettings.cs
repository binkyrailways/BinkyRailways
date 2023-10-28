using System.ComponentModel;
using System.Drawing.Design;
using BinkyRailways.Core.Model;
using BinkyRailways.WinApp.TypeConverters;
using BinkyRailways.WinApp.UIEditors;

namespace BinkyRailways.WinApp.Controls.Edit.Settings
{
    internal abstract class JunctionSettings<T> : PositionedEntitySettings<T>
        where T : class, IJunction
    {
        /// <summary>
        /// Default ctor
        /// </summary>
        internal JunctionSettings(T entity, GridContext context)
            : base(entity, context)
        {
        }

        /// <summary>
        /// Add all visible properties of this settings object to the given property collection.
        /// </summary>
        public override void GatherProperties(ExPropertyDescriptorCollection properties)
        {
            properties.Add(() => Block, Strings.TabGeneral, Strings.JunctionBlockName, Strings.JunctionBlockHelp);
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
    }
}

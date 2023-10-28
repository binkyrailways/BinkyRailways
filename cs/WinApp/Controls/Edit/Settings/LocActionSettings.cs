using System.ComponentModel;
using System.Drawing.Design;
using System.Reflection;
using BinkyRailways.Core.Model;
using BinkyRailways.WinApp.TypeConverters;
using BinkyRailways.WinApp.UIEditors;

namespace BinkyRailways.WinApp.Controls.Edit.Settings
{
    /// <summary>
    /// Settings wrapper for an action.
    /// </summary>
    [Obfuscation(Feature = "@NodeSettings")]
    internal abstract class LocActionSettings<T> : ActionSettings<T>
        where T : class, ILocAction
    {
        /// <summary>
        /// Default ctor
        /// </summary>
        internal LocActionSettings(T entity, GridContext context)
            : base(entity, context)
        {
        }

        /// <summary>
        /// Add all visible properties of this settings object to the given property collection.
        /// </summary>
        public override void GatherProperties(ExPropertyDescriptorCollection properties)
        {
            base.GatherProperties(properties);
            properties.Add(() => Loc, Strings.TabGeneral, "Loc", "Loc...");
        }

        [Editor(typeof(LocEditor), typeof(UITypeEditor))]
        [TypeConverter(typeof(EntityTypeConverter))]
        [MergableProperty(false)]
        public ILoc Loc
        {
            get { return Entity.Loc; }
            set { Entity.Loc = value; }
        }
    }
}

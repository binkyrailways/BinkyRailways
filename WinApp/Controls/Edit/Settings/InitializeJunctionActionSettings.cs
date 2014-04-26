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
    internal sealed class InitializeJunctionActionSettings : ActionSettings<IInitializeJunctionAction>
    {
        /// <summary>
        /// Default ctor
        /// </summary>
        internal InitializeJunctionActionSettings(IInitializeJunctionAction entity, GridContext context)
            : base(entity, context)
        {
        }

        /// <summary>
        /// Add all visible properties of this settings object to the given property collection.
        /// </summary>
        public override void GatherProperties(ExPropertyDescriptorCollection properties)
        {
            base.GatherProperties(properties);
            properties.Add(() => Junction, Strings.TabGeneral, "Junction", "The junction to set to it's initial position");
        }

        [TypeConverter(typeof(EntityTypeConverter))]
        [Editor(typeof(JunctionEditor), typeof(UITypeEditor))]
        public IJunction Junction
        {
            get { return Entity.Junction; }
            set { Entity.Junction = value; }
        }
    }
}

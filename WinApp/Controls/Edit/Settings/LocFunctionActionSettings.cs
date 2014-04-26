using System.ComponentModel;
using System.Reflection;
using BinkyRailways.Core.Model;

namespace BinkyRailways.WinApp.Controls.Edit.Settings
{
    /// <summary>
    /// Settings wrapper for an action.
    /// </summary>
    [Obfuscation(Feature = "@NodeSettings")]
    internal sealed class LocFunctionActionSettings : LocActionSettings<ILocFunctionAction>
    {
        /// <summary>
        /// Default ctor
        /// </summary>
        internal LocFunctionActionSettings(ILocFunctionAction entity, GridContext context)
            : base(entity, context)
        {
        }

        /// <summary>
        /// Add all visible properties of this settings object to the given property collection.
        /// </summary>
        public override void GatherProperties(ExPropertyDescriptorCollection properties)
        {
            base.GatherProperties(properties);
            properties.Add(() => Function, Strings.TabGeneral, "Function", "function...");
            properties.Add(() => Command, Strings.TabGeneral, "Command", "command...");
        }

        public LocFunction Function
        {
            get { return Entity.Function; }
            set { Entity.Function = value; }
        }

        public LocFunctionCommand Command
        {
            get { return Entity.Command; }
            set { Entity.Command = value; }
        }
    }
}

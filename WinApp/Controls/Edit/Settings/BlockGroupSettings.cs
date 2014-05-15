using System.Reflection;
using BinkyRailways.Core.Model;

namespace BinkyRailways.WinApp.Controls.Edit.Settings
{
    /// <summary>
    /// Settings wrapper for a block group.
    /// </summary>
    [Obfuscation(Feature = "@NodeSettings")]
    internal sealed class BlockGroupSettings : EntitySettings<IBlockGroup>
    {
        /// <summary>
        /// Default ctor
        /// </summary>
        internal BlockGroupSettings(IBlockGroup entity, GridContext context)
            : base(entity, context)
        {
        }

        /// <summary>
        /// Add all visible properties of this settings object to the given property collection.
        /// </summary>
        public override void GatherProperties(ExPropertyDescriptorCollection properties)
        {
            base.GatherProperties(properties);
            //properties.Add(() => StationMode, Strings.TabBehavior, Strings.StationModeName, Strings.StationModeHelp);
        }
    }
}

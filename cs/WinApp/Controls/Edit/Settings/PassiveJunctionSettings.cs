using System.Reflection;
using BinkyRailways.Core.Model;

namespace BinkyRailways.WinApp.Controls.Edit.Settings
{
    [Obfuscation(Feature = "@NodeSettings")]
    internal sealed class PassiveJunctionSettings : JunctionSettings<IPassiveJunction>
    {
        /// <summary>
        /// Default ctor
        /// </summary>
        internal PassiveJunctionSettings(IPassiveJunction entity, GridContext context)
            : base(entity, context)
        {
        }
    }
}

using System.Reflection;
using BinkyRailways.Core.Model;

namespace BinkyRailways.WinApp.Controls.Edit.Settings
{
    [Obfuscation(Feature = "@NodeSettings")]
    internal sealed class EdgeSettings : PositionedEntitySettings<IEdge>
    {
        /// <summary>
        /// Default ctor
        /// </summary>
        internal EdgeSettings(IEdge entity, GridContext context)
            : base(entity, context)
        {
        }
    }
}

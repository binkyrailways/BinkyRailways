using System.Reflection;
using BinkyRailways.Core.Model;

namespace BinkyRailways.WinApp.Controls.Edit.Settings
{
    [Obfuscation(Feature = "@NodeSettings")]
    internal abstract class OutputSettings<T> : PositionedEntitySettings<T>
        where T : class, IOutput
    {
        /// <summary>
        /// Default ctor
        /// </summary>
        internal OutputSettings(T entity, GridContext context)
            : base(entity, context)
        {
        }
    }
}

using System.Reflection;
using BinkyRailways.Core.Model;

namespace BinkyRailways.WinApp.Controls.Edit.Settings
{
    [Obfuscation(Feature = "@NodeSettings")]
    internal abstract class SignalSettings<T> : PositionedEntitySettings<T>
        where T : class, ISignal
    {
        /// <summary>
        /// Default ctor
        /// </summary>
        internal SignalSettings(T entity, GridContext context)
            : base(entity, context)
        {
        }
    }
}

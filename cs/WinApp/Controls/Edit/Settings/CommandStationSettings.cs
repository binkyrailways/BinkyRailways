using System.Reflection;
using BinkyRailways.Core.Model;

namespace BinkyRailways.WinApp.Controls.Edit.Settings
{
    [Obfuscation(Feature = "@NodeSettings")]
    internal abstract class CommandStationSettings<T> : PersistentEntitySettings<T>
        where T : class, ICommandStation
    {
        /// <summary>
        /// Default ctor
        /// </summary>
        internal CommandStationSettings(T entity, GridContext context)
            : base(entity, context)
        {
        }
    }
}

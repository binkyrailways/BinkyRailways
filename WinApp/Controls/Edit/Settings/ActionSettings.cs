using System.Reflection;
using BinkyRailways.Core.Model;

namespace BinkyRailways.WinApp.Controls.Edit.Settings
{
    /// <summary>
    /// Settings wrapper for an action.
    /// </summary>
    [Obfuscation(Feature="@NodeSettings")]
    internal abstract class ActionSettings<T> : EntitySettings<T>
        where T : class, IAction
    {
        /// <summary>
        /// Default ctor
        /// </summary>
        internal ActionSettings(T entity, GridContext context)
            : base(entity, context)
        {
        }

        /// <summary>
        /// Should the description property be shown?
        /// </summary>
        protected override bool ShowDescription { get { return false; } }

        /// <summary>
        /// Should the UsedBy property be shown?
        /// </summary>
        protected override bool ShowUsedBy { get { return false; } }
    }
}

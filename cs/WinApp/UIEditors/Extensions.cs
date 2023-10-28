using System.ComponentModel;
using System.Linq;
using BinkyRailways.WinApp.Controls.Edit.Settings;

namespace BinkyRailways.WinApp.UIEditors
{
    internal static class Extensions
    {
        /// <summary>
        /// Gets the first entity settings instance found in the given context.
        /// </summary>
        internal static T GetFirstEntitySettings<T>(this ITypeDescriptorContext context)
            where T : class, IGatherProperties
        {
            var result = context.Instance as T;
            if (result != null)
                return result;
            var array = context.Instance as object[];
            if ((array == null) || (array.Length == 0))
                return null;
            return array.OfType<T>().FirstOrDefault();
        }
    }
}

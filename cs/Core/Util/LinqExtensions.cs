using System;
using System.Collections.Generic;

namespace BinkyRailways.Core.Util
{
    public static class LinqExtensions
    {
        /// <summary>
        /// Perform the given action for each item in the given enumerable.
        /// </summary>
        public static void Foreach<T>(this IEnumerable<T> items, Action<T> action)
        {
            foreach (var item in items)
            {
                action(item);
            }
        }
    }
}

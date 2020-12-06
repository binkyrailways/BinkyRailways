using System.Reflection;

namespace BinkyRailways.Core.Util
{
    /// <summary>
    /// Helper for empty array's
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public static class Empty<T>
    {
        [Obfuscation]
        public static readonly T[] Array = new T[0];
    }
}

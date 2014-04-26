namespace BinkyRailways.Core.Util
{
    public static class Default<T>
        where T : new()
    {
        public static readonly T Instance = new T();
    }
}

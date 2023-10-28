namespace BinkyRailways.Core.Util
{
    /// <summary>
    /// Conversion to localized text.
    /// </summary>
    public static class TextExtensions
    {
        /// <summary>
        /// Convert boolean value to "on" or "off"
        /// </summary>
        public static string OnOff(this bool value)
        {
            return value ? Strings.On : Strings.Off;
        }
    }
}

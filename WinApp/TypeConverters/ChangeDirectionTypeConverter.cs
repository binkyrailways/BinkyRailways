using BinkyRailways.Core.Model;

namespace BinkyRailways.WinApp.TypeConverters
{
    /// <summary>
    /// TypeConverter for ChangeDirection enum.
    /// </summary>
    public class ChangeDirectionTypeConverter : EnumTypeConverter<ChangeDirection>
    {
        /// <summary>
        /// Default ctor
        /// </summary>
        public ChangeDirectionTypeConverter()
        {
            AddItem(Strings.ChangeDirectionAllow, ChangeDirection.Allow);
            AddItem(Strings.ChangeDirectionAvoid, ChangeDirection.Avoid);
        }
    }
}

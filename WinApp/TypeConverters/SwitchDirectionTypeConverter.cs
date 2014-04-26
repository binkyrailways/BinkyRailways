using BinkyRailways.Core.Model;

namespace BinkyRailways.WinApp.TypeConverters
{
    /// <summary>
    /// TypeConverter for SwitchDirection enum.
    /// </summary>
    public class SwitchDirectionTypeConverter : EnumTypeConverter<SwitchDirection>
    {
        /// <summary>
        /// Default ctor
        /// </summary>
        public SwitchDirectionTypeConverter()
        {
            AddItem(Strings.SwitchDirectionStraight, SwitchDirection.Straight);
            AddItem(Strings.SwitchDirectionOff, SwitchDirection.Off);
        }
    }
}

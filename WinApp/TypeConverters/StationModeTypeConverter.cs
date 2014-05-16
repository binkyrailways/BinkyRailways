using BinkyRailways.Core.Model;

namespace BinkyRailways.WinApp.TypeConverters
{
    /// <summary>
    /// TypeConverter for StationMode enum.
    /// </summary>
    public class StationModeTypeConverter : EnumTypeConverter<StationMode>
    {
        /// <summary>
        /// Default ctor
        /// </summary>
        public StationModeTypeConverter()
        {
            AddItem(Strings.StationModeAuto, StationMode.Auto);
            AddItem(Strings.StationModeAlways, StationMode.Always);
            AddItem(Strings.StationModeNever, StationMode.Never);
        }
    }
}

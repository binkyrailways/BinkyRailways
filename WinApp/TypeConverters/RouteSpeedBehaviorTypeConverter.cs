using BinkyRailways.Core.Model;

namespace BinkyRailways.WinApp.TypeConverters
{
    /// <summary>
    /// TypeConverter for LocSpeedBehavior enum.
    /// </summary>
    public class LocSpeedBehaviorTypeConverter: EnumTypeConverter<LocSpeedBehavior>
    {
        /// <summary>
        /// Default ctor
        /// </summary>
        public LocSpeedBehaviorTypeConverter()
        {
            AddItem(Strings.LocSpeedBehaviorDefault, LocSpeedBehavior.Default);
            AddItem(Strings.LocSpeedBehaviorNoChange, LocSpeedBehavior.NoChange);
            AddItem(Strings.LocSpeedBehaviorMaximum, LocSpeedBehavior.Maximum);
            AddItem(Strings.LocSpeedBehaviorMedium, LocSpeedBehavior.Medium);
            AddItem(Strings.LocSpeedBehaviorMinimum, LocSpeedBehavior.Minimum);
        }
    }
}

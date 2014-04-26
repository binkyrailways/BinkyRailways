using BinkyRailways.Core.Model;

namespace BinkyRailways.WinApp.TypeConverters
{
    /// <summary>
    /// TypeConverter for RouteStateBehavior enum.
    /// </summary>
    public class RouteStateBehaviorTypeConverter: EnumTypeConverter<RouteStateBehavior>
    {
        /// <summary>
        /// Default ctor
        /// </summary>
        public RouteStateBehaviorTypeConverter()
        {            
            AddItem(Strings.RouteStateBehaviorNoChange, RouteStateBehavior.NoChange);
            AddItem(Strings.RouteStateBehaviorEnter, RouteStateBehavior.Enter);
            AddItem(Strings.RouteStateBehaviorReached, RouteStateBehavior.Reached);
        }
    }
}

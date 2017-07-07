using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
namespace BinkyRailways.Core.Model
{
    /// <summary>
    /// How does a route event change the state of the route
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum RouteStateBehavior
    {
        /// <summary>
        /// State does not change
        /// </summary>
        NoChange,

        /// <summary>
        /// Loc has entered the To block.
        /// </summary>
        Enter,

        /// <summary>
        /// Loc has reached the To block.
        /// </summary>
        Reached
    }
}

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
namespace BinkyRailways.Core.Model
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum RouteSensorSetType
    {
        /// <summary>
        /// Set contains entering sensors
        /// </summary>
        Entering,

        /// <summary>
        /// Set contains reached sensors
        /// </summary>
        Reached
    }
}

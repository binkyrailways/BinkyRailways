using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
namespace BinkyRailways.Core.Model
{
    /// <summary>
    /// How does a route event change the speed of the occupying loc.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum LocSpeedBehavior
    {
        /// <summary>
        /// Speed change is controlled by state behavior
        /// </summary>
        Default,

        /// <summary>
        /// Do not change speed
        /// </summary>
        NoChange,

        /// <summary>
        /// Set speed to medium speed
        /// </summary>
        Medium,

        /// <summary>
        /// Set speed to minimum speed
        /// </summary>
        Minimum,

        /// <summary>
        /// Set speed to maximum speed
        /// </summary>
        Maximum
    }
}

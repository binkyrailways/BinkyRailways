using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
namespace BinkyRailways.Core.Model
{
    /// <summary>
    /// Is it allowed / should it be avoided to change direction in a block,
    /// or is it allowed / should is be avoided that a loc changes direction?
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum ChangeDirection
    {
        /// <summary>
        /// Changing direction is allowed
        /// </summary>
        Allow,

        /// <summary>
        /// Changing direction should be avoided.
        /// </summary>
        Avoid
    }
}

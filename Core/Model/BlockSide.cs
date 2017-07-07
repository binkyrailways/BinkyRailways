using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
namespace BinkyRailways.Core.Model
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum BlockSide
    {
        /// <summary>
        /// End of normal driving direction
        /// </summary>
        Front,

        /// <summary>
        /// Begining of normal driving direction
        /// </summary>
        Back
    }
}

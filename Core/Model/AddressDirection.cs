using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
namespace BinkyRailways.Core.Model
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum AddressDirection
    {
        /// <summary>
        /// From railway to computer
        /// </summary>
        Input,

        /// <summary>
        /// From computer to railway
        /// </summary>
        Output
    }
}

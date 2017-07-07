using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
namespace BinkyRailways.Core.Model
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum BlockSignalType
    {
        /// <summary>
        /// Signal shows if a block can be entered.
        /// </summary>
        Entry,

        /// <summary>
        /// Signal shows if a block can be left.
        /// </summary>
        Exit
    }
}

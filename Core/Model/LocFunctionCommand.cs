using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
namespace BinkyRailways.Core.Model
{
    /// <summary>
    /// Command for <see cref="ILocFunctionAction"/>.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum LocFunctionCommand
    {
        On,
        Off,
        Toggle
    }
}

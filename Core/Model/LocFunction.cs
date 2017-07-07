using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Reflection;
namespace BinkyRailways.Core.Model
{
    /// <summary>
    /// Function numbers.
    /// </summary>
    [Obfuscation]
    [JsonConverter(typeof(StringEnumConverter))]
    public enum LocFunction
    {
        Light = 0,
        F1,
        F2,
        F3,
        F4,
        F5,
        F6,
        F7,
        F8,
        F9,
        F10,
        F11,
        F12,
        F13,
        F14,
        F15,
        F16
    }
}

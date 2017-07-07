using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
namespace BinkyRailways.Core.Model
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum AddressType
    {
        Dcc,
        LocoNet,
        Motorola,
        Mfx,
        Mqtt
    }
}

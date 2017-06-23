using Newtonsoft.Json;
namespace BinkyRailways.Core.State.Impl.Mqtt
{
    [JsonObject]
    internal class BinaryOutputMessage
    {
        [JsonProperty("address")]
        public string Address { get; set; }
        [JsonProperty("value")]
        public int Value { get; set; }
    }

    [JsonObject]
    internal class BinarySensorMessage
    {
        [JsonProperty("address")]
        public string Address { get; set; }
        [JsonProperty("value")]
        public int Value { get; set; }
    }

    [JsonObject]
    internal class Clock4StageMessage
    {
        [JsonProperty("stage")]
        public string Stage { get; set; }
    }

    [JsonObject]
    internal class LocMessage
    {
        [JsonProperty("address")]
        public string Address { get; set; }
        [JsonProperty("speed")]
        public int Speed { get; set; }
        [JsonProperty("direction")]
        public string Direction { get; set; }
    }

    [JsonObject]
    internal class PowerMessage
    {
        [JsonProperty("active")]
        public int Active { get; set; }
    }

}
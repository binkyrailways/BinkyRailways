using Newtonsoft.Json;
using System.Linq;

namespace BinkyRailways.Core.State.Impl.BinkyNet
{
    [JsonObject]
    internal abstract class MessageBase
    {
        public const string ModeRequest = "request";
        public const string ModeActual = "actual";

        [JsonProperty("sender")]
        public string Sender { get; set; }
        [JsonProperty("mode")]
        public string Mode { get; set; }

        public bool IsRequest => (Mode == ModeRequest);
        public bool IsActual => (Mode == ModeActual);

        public abstract string Topic { get; }
    }

    [JsonObject]
    internal abstract class ObjectMessageBase : MessageBase
    {
        [JsonProperty("address")]
        public string Address { get; set; }

        public abstract string TopicSuffix { get; }

        public string ModuleID => Address.Split('/').First();

        public override string Topic => ModuleID + "/" + TopicSuffix;
    }

    [JsonObject]
    internal abstract class GlobalMessageBase : MessageBase
    {
        public abstract string TopicSuffix { get; }

        public override string Topic => "global/" + TopicSuffix;
    }

    [JsonObject]
    internal class BinaryMessage : ObjectMessageBase
    {
        [JsonProperty("value")]
        public bool Value { get; set; }

        public override string TopicSuffix => "binary";
    }

    [JsonObject]
    internal class ClockMessage : GlobalMessageBase
    {
        [JsonProperty("period")]
        public string Period { get; set; }

        public override string TopicSuffix => "clock";
    }

    [JsonObject]
    internal class LocMessage : ObjectMessageBase
    {
        [JsonProperty("speed")]
        public int Speed { get; set; }
        [JsonProperty("direction")]
        public string Direction { get; set; }

        public override string TopicSuffix => "loc";
    }

    [JsonObject]
    internal class PowerMessage : GlobalMessageBase
    {
        [JsonProperty("active")]
        public bool Active { get; set; }

        public override string TopicSuffix => "power";
    }

    [JsonObject]
    internal class SwitchMessage : ObjectMessageBase
    {
        [JsonProperty("direction")]
        public string Direction { get; set; }

        public override string TopicSuffix => "switch";
    }

}
using Newtonsoft.Json;

namespace BinkyRailways.Core.Server.Impl
{
    internal class Messages
    {
        // Data message types
        public const string TypeRunning = "running";
        public const string TypeEditing = "editing";

        public const string TypePowerChanged = "power-changed";
        public const string TypAutomaticLocControllerEnabledChanged = "automatic-loccontroller-changed";

        // Control message types
        public const string TypeRefresh = "refresh";
        public const string TypePowerOn = "power-on";
        public const string TypePowerOff = "power-off";
        public const string TypAutomaticLocControllerOn= "automatic-loccontroller-on";
        public const string TypAutomaticLocControllerOff = "automatic-loccontroller-off";

        [JsonObject]
        internal class BaseServerMessage
        {
            [JsonProperty("type")]
            public string Type { get; set; }
        }

        [JsonObject]
        internal class PowerChangedMessage : BaseServerMessage
        {
            public PowerChangedMessage()
            {
               Type = TypePowerChanged;
            }

            [JsonProperty("actual")]
            public bool Actual { get; set; }

            [JsonProperty("requested")]
            public bool Requested { get; set; }
        }

        [JsonObject]
        internal class AutomaticLocControllerEnabledChangedMessage : BaseServerMessage
        {
            public AutomaticLocControllerEnabledChangedMessage()
            {
                Type = TypAutomaticLocControllerEnabledChanged;
            }

            [JsonProperty("actual")]
            public bool Actual { get; set; }

            [JsonProperty("requested")]
            public bool Requested { get; set; }
        }
    }
}

using BinkyRailways.Core.State;
using Newtonsoft.Json;

namespace BinkyRailways.Core.Server
{
    public class Messages
    {
        /// <summary>
        /// Request a <see cref="LocList"/>.
        /// </summary>
        public const string GetLocList = "get-loc-list";

        /// <summary>
        /// Request a change of automatically controlled state of a loc.
        /// </summary>
        public const string SetLocAuto = "set-loc-auto";

        /// <summary>
        /// List of <see cref="LocState"/> items.
        /// </summary>
        public const string LocList = "loc-list";

        /// <summary>
        /// Single loc state change.
        /// Data is single <see cref="LocState"/> item.
        /// </summary>
        public const string LocStateChanged = "loc-state-changed";

        /// <summary>
        /// Single state info of a loc.
        /// </summary>
        public class LocState
        {
            /// <summary>
            /// Create from the given state
            /// </summary>
            public LocState(ILocState state)
            {
                LocId = state.EntityId;
                Name = state.Description;
                State = state.GetStateText();
                Speed = state.GetSpeedText();
                Owner = state.Owner;
                Auto = state.ControlledAutomatically.Actual;
                CanControlAutomatically = state.CanSetAutomaticControl;
                IsCurrentRouteDurationExceeded = state.IsCurrentRouteDurationExceeded;
            }

            [JsonProperty("id")]
            public string LocId { get; set; }

            [JsonProperty("name")]
            public string Name { get; set; }

            [JsonProperty("state")]
            public string State { get; set; }

            [JsonProperty("speed")]
            public string Speed { get; set; }

            [JsonProperty("owner")]
            public string Owner { get; set; }

            [JsonProperty("auto")]
            public bool Auto { get; set; }

            [JsonProperty("canSetAuto")]
            public bool CanControlAutomatically { get; set; }

            [JsonProperty("routeDurationExceeded")]
            public bool IsCurrentRouteDurationExceeded { get; set; }
        }
    }
}

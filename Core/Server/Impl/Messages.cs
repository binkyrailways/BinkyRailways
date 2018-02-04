using BinkyRailways.Core.Model;
using BinkyRailways.Core.State;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace BinkyRailways.Core.Server.Impl
{
    internal class Messages
    {
        // Data message types
        public const string TypeRailway = "railway";
        public const string TypeRunning = "running";
        public const string TypeEditing = "editing";

        public const string TypePowerChanged = "power-changed";
        public const string TypeAutomaticLocControllerEnabledChanged = "automatic-loccontroller-changed";
        public const string TypLocChanged = "loc-changed";
        public const string TypeTimeChanged = "time-changed";

        // Control message types
        public const string TypeRefresh = "refresh";
        public const string TypePowerOn = "power-on";
        public const string TypePowerOff = "power-off";
        public const string TypeAutomaticLocControllerOn = "automatic-loccontroller-on";
        public const string TypeAutomaticLocControllerOff = "automatic-loccontroller-off";
        public const string TypeControlAutomatically = "control-automatically";
        public const string TypeControlManually = "control-manually";
        public const string TypeDirectionForward = "direction-forward";
        public const string TypeDirectionReverse = "direction-reverse";
        public const string TypeRemoveFromTrack = "remove-from-track";
        public const string TypeSpeed = "speed";

        [JsonObject]
        internal class BaseServerMessage
        {
            [JsonProperty("type")]
            public string Type { get; set; }

            [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
            public string Id { get; set; }

            [JsonProperty("speed", NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
            public int Speed { get; set; }
        }

        [JsonObject]
        internal class RailwayMessage : BaseServerMessage
        {
            public RailwayMessage(IRailway railway, IRailwayState state)
            {
                Type = TypeRailway;
                Description = railway.Description;
                var locs = new List<Loc>();
                foreach (var loc in railway.Locs)
                {
                    ILoc locEntity;
                    if (loc.TryResolve(out locEntity))
                    {
                        ILocState locState = null;
                        if (state != null)
                        {
                            locState = state.LocStates[locEntity];
                        }
                        locs.Add(new Loc(locEntity, locState));
                    }
                }
                Locs = locs.ToArray();

                var blocks = new List<Block>();
                foreach (var module in railway.Modules)
                {
                    IModule moduleEntity;
                    if (module.TryResolve(out moduleEntity))
                    {
                        foreach (var block in moduleEntity.Blocks)
                        {
                            IBlockState blockState = null;
                            if (state != null)
                            {
                                blockState = state.BlockStates[block];
                            }
                            blocks.Add(new Block(block, blockState));
                        }
                    }
                }
                Blocks = blocks.ToArray();
            }

            [JsonProperty("description")]
            public string Description { get; set; }

            [JsonProperty("locs")]
            public Loc[] Locs { get; set; }

            [JsonProperty("blocks")]
            public Block[] Blocks { get; set; }
        }

        [JsonObject]
        internal class Loc
        {
            public Loc(ILoc loc, ILocState state)
            {
                Id = loc.Id;
                Description = loc.Description;
                Owner = loc.Owner;
                if (state != null)
                {
                    Speed = state.Speed.Actual;
                    SpeedText = state.GetSpeedText();
                    StateText = state.GetStateText();
                    IsAssigned = state.CurrentBlock.Actual != null;
                    IsCurrentRouteDurationExceeded = state.IsCurrentRouteDurationExceeded;
                    IsControlledAutomatically = state.ControlledAutomatically.Actual;
                    Direction = state.Direction.Actual.ToString().ToLower();
                    HasPossibleDeadlock = state.PossibleDeadlock.Actual;
                }
            }

            [JsonProperty("id")]
            public string Id { get; set; }

            [JsonProperty("description", NullValueHandling = NullValueHandling.Ignore)]
            public string Description { get; set; }

            [JsonProperty("owner", NullValueHandling = NullValueHandling.Ignore)]
            public string Owner { get; set; }

            [JsonProperty("speed", NullValueHandling = NullValueHandling.Ignore)]
            public int Speed { get; set; }

            [JsonProperty("speedText", NullValueHandling = NullValueHandling.Ignore)]
            public string SpeedText { get; set; }

            [JsonProperty("stateText", NullValueHandling = NullValueHandling.Ignore)]
            public string StateText { get; set; }

            [JsonProperty("is-assigned", NullValueHandling = NullValueHandling.Ignore)]
            public bool IsAssigned { get; set; }

            [JsonProperty("is-current-route-duration-exceeded", NullValueHandling = NullValueHandling.Ignore)]
            public bool IsCurrentRouteDurationExceeded { get; set; }

            [JsonProperty("is-controlled-automatically", NullValueHandling = NullValueHandling.Ignore)]
            public bool IsControlledAutomatically { get; set; }

            [JsonProperty("direction", NullValueHandling = NullValueHandling.Ignore)]
            public string Direction { get; set; }

            [JsonProperty("has-possible-deadlock", NullValueHandling = NullValueHandling.Ignore)]
            public bool HasPossibleDeadlock { get; set; }
        }

        [JsonObject]
        internal class Block
        {
            public Block(IBlock block, IBlockState state)
            {
                Id = block.Id;
                Description = block.Description;
                if (state != null)
                {
                }
            }

            [JsonProperty("id")]
            public string Id { get; set; }

            [JsonProperty("description", NullValueHandling = NullValueHandling.Ignore)]
            public string Description { get; set; }
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
        internal class TimeChangedMessage : BaseServerMessage
        {
            public TimeChangedMessage(Time t)
            {
                Type = TypeTimeChanged;
                Hour = t.Hour;
                Minute = t.Minute;
            }

            [JsonProperty("hour")]
            public int Hour { get; set; }

            [JsonProperty("minute")]
            public int Minute { get; set; }
        }

        [JsonObject]
        internal class AutomaticLocControllerEnabledChangedMessage : BaseServerMessage
        {
            public AutomaticLocControllerEnabledChangedMessage()
            {
                Type = TypeAutomaticLocControllerEnabledChanged;
            }

            [JsonProperty("actual")]
            public bool Actual { get; set; }

            [JsonProperty("requested")]
            public bool Requested { get; set; }
        }

        [JsonObject]
        internal class LocChangedMessage : BaseServerMessage
        {
            public LocChangedMessage(ILoc loc, ILocState locState)
            {
                Type = TypLocChanged;
                Loc = new Loc(loc, locState);
            }

            [JsonProperty("loc")]
            public Loc Loc{ get; set; }
        }
    }
}

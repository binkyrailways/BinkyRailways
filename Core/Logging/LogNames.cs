namespace BinkyRailways.Core.Logging
{
    /// <summary>
    /// Common logger names
    /// </summary>
    public static class LogNames
    {
        /// <summary>
        /// Log for automatic loc control.
        /// </summary>
        public const string AutoLocController = "autoloccontroller";

        /// <summary>
        /// Prefix for command station logger names.
        /// The description is added to this prefix.
        /// </summary>
        public const string CommandStationPrefix = "cs-";

        /// <summary>
        /// Log for loconet traffic
        /// </summary>
        public const string LocoNet = "loconet";

        /// <summary>
        /// Log for sensor state activity.
        /// </summary>
        public const string Sensors = "sensors";

        /// <summary>
        /// Log for actionas.
        /// </summary>
        public const string Actions = "actions";

        /// <summary>
        /// Log for sounds
        /// </summary>
        public const string Sound = "sound";

        /// <summary>
        /// Log for webserver
        /// </summary>
        public const string WebServer = "webserver";
    }
}

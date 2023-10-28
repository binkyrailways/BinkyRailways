using System;

namespace BinkyRailways.Protocols.Ecos
{
    public class Constants
    {
        // Communication timeout
        public static readonly TimeSpan Timeout = TimeSpan.FromSeconds(10);

        // Well known IDs
        public const int IdEcos = 1;
        public const int IdLocManager = 10;
        public const int IdSwitchManager = 11;
        public const int IdDeviceManager = 20;
        public const int IdFeedbackManager = 26;

        // Command names
        public const string CmdCreate = "create";
        public const string CmdDelete = "delete";
        public const string CmdGet = "get";
        public const string CmdQueryObjects = "queryObjects";
        public const string CmdRelease = "release";
        public const string CmdRequest = "request";
        public const string CmdSet = "set";

        // Option names
        public const string OptAddress = "addr";
        public const string OptControl = "control";
        public const string OptDirection = "dir";
        public const string OptDuration = "duration";
        public const string OptForce = "force";
        public const string OptFunc = "func";
        public const string OptGo = "go";
        public const string OptInfo = "info";
        public const string OptMode = "mode";
        public const string OptName = "name";
        public const string OptName1 = "name1";
        public const string OptName2 = "name2";
        public const string OptName3 = "name3";
        public const string OptPorts = "ports";
        public const string OptProtocol = "protocol";
        public const string OptSize = "size";
        public const string OptSpeed = "speed";
        public const string OptSpeedStep = "speedstep";
        public const string OptState = "state";
        public const string OptStatus = "status";
        public const string OptStop = "stop";
        public const string OptSymbol = "symbol";
        public const string OptView = "view";
    }
}

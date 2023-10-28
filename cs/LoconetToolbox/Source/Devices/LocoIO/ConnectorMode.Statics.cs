using System.Collections.Generic;
using LocoNetToolBox.Devices.LocoIO.ConnectorModes;

namespace LocoNetToolBox.Devices.LocoIO
{
    partial class ConnectorMode
    {
        public static readonly ConnectorMode None = new ConnectorModes.None();
        public static readonly ConnectorMode Mgv77 = new ConnectorModes.Mgv77();
        public static readonly ConnectorMode Mgv93 = new ConnectorModes.Mgv93();
        public static readonly ConnectorMode Mgv102 = new ConnectorModes.Mgv102();
        public static readonly ConnectorMode Mgv133 = new ConnectorModes.Mgv133();
        public static readonly ConnectorMode Mgv136 = new ConnectorModes.Mgv136();
        public static readonly ConnectorMode Mgv145J5 = new ConnectorModes.Mgv145J5();
        public static readonly ConnectorMode Mgv145J6 = new ConnectorModes.Mgv145J6();
        public static readonly ConnectorMode NotUsed = new ConnectorModes.NotUsed();

        private static readonly ConnectorMode[] Modes = new[]
        {
            //None, 
            NotUsed,
            Mgv77,
            Mgv93, 
            Mgv102, 
            Mgv133,
            Mgv136, 
            Mgv145J5,
            Mgv145J6,
            new Bs1ForSignals(), 
            new ConnectorModes.Mgv81V1(), 
        };

        /// <summary>
        /// Gets all connector modes
        /// </summary>
        public static IEnumerable<ConnectorMode> All
        {
            get { return Modes; }
        }
    }
}

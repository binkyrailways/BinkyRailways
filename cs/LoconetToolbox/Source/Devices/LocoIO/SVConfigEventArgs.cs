using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LocoNetToolBox.Devices.LocoIO
{
    public class SVConfigEventArgs : EventArgs
    {
        /// <summary>
        /// Default ctor
        /// </summary>
        public SVConfigEventArgs(SVConfig config)
        {
            this.Config = config;
        }

        /// <summary>
        /// Gets the current config
        /// </summary>
        public SVConfig Config { get; private set; }
    }
}

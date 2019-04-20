using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LocoNetToolBox.Model;

namespace LocoNetToolBox.Services
{
    /// <summary>
    /// Service used to detect loco-io units in the actual network that are not known in the configuration.
    /// </summary>
    internal class UnknownLocoIODetector
    {
        private readonly LocoNet locoNet;
        private List<LocoIO> newLocoIOs;

        /// <summary>
        /// Default ctor
        /// </summary>
        internal UnknownLocoIODetector(LocoNet locoNet)
        {
            this.locoNet = locoNet;
            locoNet.State.Idle += OnIdle;
        }

        /// <summary>
        /// Have new loco IO units been found?
        /// </summary>
        public bool HasNewLocoIOs
        {
            get
            {
                var list = newLocoIOs;
                return (list != null) && (list.Count > 0);
            }
        }

        /// <summary>
        /// Get all new loco IO units?
        /// </summary>
        public IEnumerable<LocoIO> NewLocoIOs
        {
            get
            {
                var list = newLocoIOs;
                return list ?? Enumerable.Empty<LocoIO>();
            }
        }

        /// <summary>
        /// Look for unknown loco IO units.
        /// </summary>
        void OnIdle(object sender, EventArgs e)
        {
            var configuration = locoNet.Configuration;
            newLocoIOs = locoNet.State.FoundLocoIOs.Where(x => !configuration.LocoIOs.Contains(x.Address)).ToList();
        }
    }
}

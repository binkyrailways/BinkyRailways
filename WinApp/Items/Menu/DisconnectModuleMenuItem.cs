using System;
using System.Linq;
using System.Windows.Forms;
using BinkyRailways.Core.Model;

namespace BinkyRailways.WinApp.Items.Menu
{
    /// <summary>
    /// Remove a module's connections
    /// </summary>
    internal sealed class DisconnectModuleMenuItem : ToolStripMenuItem
    {
        private readonly IRailway railway;
        private readonly IModule module;
        private readonly ItemContext context;

        /// <summary>
        /// Default ctor
        /// </summary>
        public DisconnectModuleMenuItem(IRailway railway, IModule module, ItemContext context)
        {
            this.railway = railway;
            this.module = module;
            this.context = context;
            Text = Strings.DisconnectModuleMenuItemText;
        }

        protected override void OnClick(EventArgs e)
        {
            var toRemove = railway.ModuleConnections.Where(x => (x.ModuleA == module) || (x.ModuleB == module)).ToList();
            if (!toRemove.Any())
                return;
            foreach (var conn in toRemove)
            {
                railway.ModuleConnections.Remove(conn);
            }
            context.Reload();
        }
    }
}

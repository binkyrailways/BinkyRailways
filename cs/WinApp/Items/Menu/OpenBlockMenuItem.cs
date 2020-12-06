using System;
using System.Windows.Forms;
using BinkyRailways.Core.State;

namespace BinkyRailways.WinApp.Items.Menu
{
    internal sealed class OpenBlockMenuItem : ToolStripMenuItem
    {
        private readonly IBlockState block;

        /// <summary>
        /// Default ctor
        /// </summary>
        public OpenBlockMenuItem(IBlockState block)
        {
            this.block = block;
            Text = Strings.OpenBlockMenuItemText;
        }

        protected override void OnClick(EventArgs e)
        {
            block.Closed.Requested = false;
        }
    }
}

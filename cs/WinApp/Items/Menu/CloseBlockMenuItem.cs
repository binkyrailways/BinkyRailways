using System;
using System.Windows.Forms;
using BinkyRailways.Core.State;

namespace BinkyRailways.WinApp.Items.Menu
{
    internal sealed class CloseBlockMenuItem : ToolStripMenuItem
    {
        private readonly IBlockState block;

        /// <summary>
        /// Default ctor
        /// </summary>
        public CloseBlockMenuItem(IBlockState block)
        {
            this.block = block;
            Text = Strings.CloseBlockMenuItemText;
        }

        protected override void OnClick(EventArgs e)
        {
            block.Closed.Requested = true;
        }
    }
}

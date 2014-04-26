using System;
using System.Windows.Forms;
using BinkyRailways.Core.State;

namespace BinkyRailways.WinApp.Items.Menu
{
    internal sealed class TurnTableGotoPositionMenuItem : ToolStripMenuItem
    {
        private readonly ITurnTableState turnTable;
        private readonly int position;

        /// <summary>
        /// Default ctor
        /// </summary>
        public TurnTableGotoPositionMenuItem(ITurnTableState turnTable, int position)
        {
            this.turnTable = turnTable;
            this.position = position;
            Text = string.Format(Strings.TurnTableGotoPositionX, position);
        }

        protected override void OnClick(EventArgs e)
        {
            turnTable.Position.Requested = position;
        }
    }
}

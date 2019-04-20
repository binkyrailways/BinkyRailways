using System;
using System.Windows.Forms;
using BinkyRailways.Core.State;

namespace BinkyRailways.WinApp.Items.Menu
{
    internal sealed class TurnTableGotoNextPositionMenuItem : ToolStripMenuItem
    {
        private readonly ITurnTableState turnTable;

        /// <summary>
        /// Default ctor
        /// </summary>
        public TurnTableGotoNextPositionMenuItem(ITurnTableState turnTable)
        {
            this.turnTable = turnTable;
            Text = Strings.TurnTableGotoNextPosition;
        }

        protected override void OnClick(EventArgs e)
        {
            var position = turnTable.Position.Actual + 1;
            if (position > turnTable.LastPosition)
                position = turnTable.FirstPosition;
            turnTable.Position.Requested = position;
        }
    }
}

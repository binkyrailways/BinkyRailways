using System;
using System.Windows.Forms;
using BinkyRailways.Core.State;

namespace BinkyRailways.WinApp.Items.Menu
{
    internal sealed class TurnTableGotoPreviousPositionMenuItem : ToolStripMenuItem
    {
        private readonly ITurnTableState turnTable;

        /// <summary>
        /// Default ctor
        /// </summary>
        public TurnTableGotoPreviousPositionMenuItem(ITurnTableState turnTable)
        {
            this.turnTable = turnTable;
            Text = Strings.TurnTableGotoPreviousPosition;
        }

        protected override void OnClick(EventArgs e)
        {
            var position = turnTable.Position.Actual - 1;
            if (position < turnTable.FirstPosition)
                position = turnTable.LastPosition;
            turnTable.Position.Requested = position;
        }
    }
}

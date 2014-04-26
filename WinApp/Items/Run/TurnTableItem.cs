using System.Drawing;
using System.Windows.Forms;
using BinkyRailways.Core.Model;
using BinkyRailways.Core.State;
using BinkyRailways.WinApp.Controls.VirtualCanvas;
using BinkyRailways.WinApp.Controls.VirtualCanvas.Handlers;
using BinkyRailways.WinApp.Items.Menu;

namespace BinkyRailways.WinApp.Items.Run
{
    /// <summary>
    /// Item showing a turntable state
    /// </summary>
    public sealed class TurnTableItem : Edit.TurnTableItem
    {
        private readonly ITurnTableState state;

        /// <summary>
        /// Default ctor
        /// </summary>
        public TurnTableItem(ITurnTable entity, ITurnTableState state, ItemContext context, bool interactive)
            : base(entity, false, context)
        {
            this.state = state;
            state.RequestedStateChanged += (s, _) => Invalidate();
            if (interactive)
                MouseHandler = new ClickHandler(MouseHandler, state);
        }

        /// <summary>
        /// Direction of the switch to display
        /// </summary>
        protected override string Position
        {
            get
            {
                if (state.Position.IsConsistent)
                    return state.Position.Actual.ToString();
                return string.Format("{0} -> {1}", state.Position.Actual, state.Position.Requested);
            }
        }

        /// <summary>
        /// Gets the color of the background.
        /// </summary>
        protected override Color BackgroundColor
        {
            get { return state.Position.IsConsistent ? base.BackgroundColor : Color.Orange; }
        }

        /// <summary>
        /// Fill the entries of a context menu
        /// </summary>
        public override void BuildContextMenu(ContextMenuStrip menu)
        {
            base.BuildContextMenu(menu);
            menu.Items.Add(new TurnTableGotoNextPositionMenuItem(state));
            menu.Items.Add(new TurnTableGotoPreviousPositionMenuItem(state));
            var gotoMenu = new ToolStripMenuItem("Goto ...");
            for (int position = state.FirstPosition; position <= state.LastPosition; position++)
            {
                gotoMenu.DropDownItems.Add(new TurnTableGotoPositionMenuItem(state, position));
            }
            menu.Items.Add(gotoMenu);
        }

        /// <summary>
        /// Handler for clicking on the switch
        /// </summary>
        private class ClickHandler : EntityClickHandler
        {
            private readonly ITurnTableState state;

            /// <summary>
            /// Default ctor
            /// </summary>
            public ClickHandler(MouseHandler next, ITurnTableState state)
                : base(next, state)
            {
                this.state = state;
            }

            /// <summary>
            /// Mouse is double clicked on this item
            /// </summary>
            /// <returns>True if the event was handled, false otherwise.</returns>
            public override bool OnMouseDoubleClick(VCItem sender, ItemMouseEventArgs e)
            {
                if (state.Position.Requested < state.LastPosition)
                {
                    state.Position.Requested++;
                }
                else
                {
                    state.Position.Requested = state.FirstPosition;
                }
                return true;
            }
        }
    }
}

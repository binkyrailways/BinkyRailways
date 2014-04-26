using System;
using System.Drawing;
using BinkyRailways.Core.Model;
using BinkyRailways.Core.State;
using BinkyRailways.WinApp.Controls.VirtualCanvas;
using BinkyRailways.WinApp.Controls.VirtualCanvas.Handlers;

namespace BinkyRailways.WinApp.Items.Run
{
    /// <summary>
    /// Item showing a block signal
    /// </summary>
    public class BlockSignalItem : Edit.BlockSignalItem 
    {
        private readonly IBlockSignalState state;

        /// <summary>
        /// Default ctor
        /// </summary>
        public BlockSignalItem(IBlockSignal signal, IBlockSignalState state, ItemContext context, bool interactive)
            : base(signal, false, context)
        {
            this.state = state;
            if (interactive)
                MouseHandler = new ClickHandler(null, state);
        }

        /// <summary>
        /// Gets the color of the background.
        /// </summary>
        protected override Color BackgroundColor
        {
            get
            {
                switch (state.Color.Actual)
                {
                    case BlockSignalColor.Red:
                        return Color.Red;
                    case BlockSignalColor.Green:
                        return Color.Green;
                    case BlockSignalColor.White:
                        return Color.White;
                    case BlockSignalColor.Yellow:
                        return Color.Yellow;
                    default:
                        throw new ArgumentException("Unknown color " + state.Color.Actual);
                }
            }
        }

        /// <summary>
        /// Handler for clicking on a signal
        /// </summary>
        private class ClickHandler : MouseHandler
        {
            private readonly IBlockSignalState state;

            /// <summary>
            /// Default ctor
            /// </summary>
            public ClickHandler(MouseHandler next, IBlockSignalState state)
                : base(next)
            {
                this.state = state;
            }

            /// <summary>
            /// Mouse is clicked on this item
            /// </summary>
            /// <returns>True if the event was handled, false otherwise.</returns>
            public override bool OnMouseClick(VCItem sender, ItemMouseEventArgs e)
            {
                state.Color.Requested = state.GetNextColor(state.Color.Requested);
                return true;
            }
        }
    }
}

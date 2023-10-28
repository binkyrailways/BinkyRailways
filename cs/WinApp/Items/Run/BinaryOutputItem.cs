using System.Drawing;
using BinkyRailways.Core.Model;
using BinkyRailways.Core.State;
using BinkyRailways.WinApp.Controls.VirtualCanvas;
using BinkyRailways.WinApp.Controls.VirtualCanvas.Handlers;

namespace BinkyRailways.WinApp.Items.Run
{
    /// <summary>
    /// Item showing a output state
    /// </summary>
    public sealed class BinaryOutputItem : Edit.BinaryOutputItem
    {
        private readonly IBinaryOutputState state;

        /// <summary>
        /// Default ctor
        /// </summary>
        public BinaryOutputItem(IBinaryOutput entity, IBinaryOutputState state, ItemContext context, bool interactive)
            : base(entity, false, context)
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
            get { return state.Active.Actual ? Color.Red : Color.Green; }
        }

        /// <summary>
        /// Handler for clicking on a signal
        /// </summary>
        private class ClickHandler : MouseHandler
        {
            private readonly IBinaryOutputState state;

            /// <summary>
            /// Default ctor
            /// </summary>
            public ClickHandler(MouseHandler next, IBinaryOutputState state)
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
                state.Active.Requested = !state.Active.Requested;
                return true;
            }
        }
    }
}

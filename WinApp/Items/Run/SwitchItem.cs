using System.Drawing;
using BinkyRailways.Core.Model;
using BinkyRailways.Core.State;
using BinkyRailways.WinApp.Controls.VirtualCanvas;
using BinkyRailways.WinApp.Controls.VirtualCanvas.Handlers;

namespace BinkyRailways.WinApp.Items.Run
{
    /// <summary>
    /// Item showing a standard switch state
    /// </summary>
    public sealed class SwitchItem : Edit.SwitchItem
    {
        private readonly ISwitchState state;

        /// <summary>
        /// Default ctor
        /// </summary>
        public SwitchItem(ISwitch entity, ISwitchState state, ItemContext context, bool interactive)
            : base(entity, false, context)
        {
            this.state = state;
            state.RequestedStateChanged += (s, _) => Invalidate();
            if (interactive)
                MouseHandler = new ClickHandler(null, state);
        }

        /// <summary>
        /// Direction of the switch to display
        /// </summary>
        protected override SwitchDirection Direction
        {
            get { return state.Direction.Actual; }
        }

        /// <summary>
        /// Gets the color of the background.
        /// </summary>
        protected override Color BackgroundColor
        {
            get { return state.Direction.IsConsistent ? base.BackgroundColor : Color.Orange; }
        }

        /// <summary>
        /// Handler for clicking on the switch
        /// </summary>
        private class ClickHandler : EntityClickHandler
        {
            private readonly ISwitchState state;

            /// <summary>
            /// Default ctor
            /// </summary>
            public ClickHandler(MouseHandler next, ISwitchState state)
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
                if (state.Direction.Requested == SwitchDirection.Straight)
                {
                    state.Direction.Requested = SwitchDirection.Off;
                }
                else
                {
                    state.Direction.Requested = SwitchDirection.Straight;
                }
                return true;
            }
        }
    }
}

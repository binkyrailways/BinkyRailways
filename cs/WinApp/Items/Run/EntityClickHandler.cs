using System.Diagnostics;
using BinkyRailways.Core.State;
using BinkyRailways.WinApp.Controls.VirtualCanvas;
using BinkyRailways.WinApp.Controls.VirtualCanvas.Handlers;

namespace BinkyRailways.WinApp.Items.Run
{
    /// <summary>
    /// Handler for clicking on an entity
    /// </summary>
    internal class EntityClickHandler : MouseHandler
    {
        private readonly IEntityState state;

        /// <summary>
        /// Default ctor
        /// </summary>
        public EntityClickHandler(MouseHandler next, IEntityState state)
            : base(next)
        {
            this.state = state;
        }

        public override bool OnMouseMove(VCItem sender, ItemMouseEventArgs e)
        {
            Debug.WriteLine("Move over " + state.Description);
            return base.OnMouseMove(sender, e);
        }

        public override bool OnMouseDown(VCItem sender, ItemMouseEventArgs e)
        {
            Debug.WriteLine("Down on " + state.Description);
            return base.OnMouseDown(sender, e);
        }

        /// <summary>
        /// Mouse is clicked on this item
        /// </summary>
        /// <returns>True if the event was handled, false otherwise.</returns>
        public override bool OnMouseClick(VCItem sender, ItemMouseEventArgs e)
        {
            Debug.WriteLine("Click on " + state.Description);
            var virtualMode = state.RailwayState.VirtualMode;
            if (virtualMode.Enabled)
            {
                virtualMode.EntityClick(state);
                return true;
            }
            return base.OnMouseClick(sender, e);
        }
    }
}

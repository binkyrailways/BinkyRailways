using System.Drawing;
using System.Windows.Forms;
using BinkyRailways.WinApp.Controls.VirtualCanvas;
using BinkyRailways.WinApp.Controls.VirtualCanvas.Handlers;

namespace BinkyRailways.WinApp.Items.Edit
{
    /// <summary>
    /// Mouse handler used to move items
    /// </summary>
    internal sealed class ItemMoveMouseHandler : MoveMouseHandler
    {
        /// <summary>
        /// Default ctor
        /// </summary>
        public ItemMoveMouseHandler(IVCItemContainer container, MouseHandler next)
            : base(container, next)
        {
            MoveContents = true;
        }

        /// <summary>
        /// Is the given button the right button to move?
        /// </summary>
        protected override bool IsValidMoveButton(MouseButtons button)
        {
            return (button == MouseButtons.Left);
        }

        /// <summary>
        /// Can the given item placement be moved.
        /// Override this method to block items from being moved.
        /// </summary>
        protected override bool CanMove(VCItemPlacement placement)
        {
            var item = placement.Item as IPositionedEntityItem;
            return (item != null) && !item.Entity.Locked;
        }

        /// <summary>
        /// Adjust the bounds of the given placement.
        /// </summary>
        protected override void Move(VCItemPlacement placement, int dx, int dy)
        {
            var item = placement.Item as IPositionedEntityItem;
            if (item != null)
            {
                item.Entity.X += dx;
                item.Entity.Y += dy;
            }
        }
    }
}

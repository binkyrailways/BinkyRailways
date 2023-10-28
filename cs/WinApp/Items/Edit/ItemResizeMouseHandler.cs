using System.Drawing;
using BinkyRailways.Core.Model;
using BinkyRailways.WinApp.Controls.VirtualCanvas;
using BinkyRailways.WinApp.Controls.VirtualCanvas.Handlers;

namespace BinkyRailways.WinApp.Items.Edit
{
    /// <summary>
    /// Mouse handler used to resize items
    /// </summary>
    internal sealed class ItemResizeMouseHandler : ResizeMouseHandler
    {
        /// <summary>
        /// Default ctor
        /// </summary>
        public ItemResizeMouseHandler(IVCItemContainer container, MouseHandler next)
            : base(container, next)
        {
        }

        /// <summary>
        /// Can the given item placement be resized.
        /// Override this method to block items from being resized.
        /// </summary>
        protected override bool CanResize(VCItemPlacement placement)
        {
            var item = placement.Item as IPositionedEntityItem;
            var entity = (item != null) ? item.Entity : null;
            return (entity != null) && (!entity.Locked) && (!(entity is ISensor) && (!(entity is ISignal)));
        }

        /// <summary>
        /// Adjust the bounds of the given placement.
        /// </summary>
        protected override void Resize(VCItemPlacement placement, Rectangle newBounds)
        {
            var item = placement.Item as IPositionedEntityItem;
            if (item != null)
            {
                item.Entity.X = newBounds.Left;
                item.Entity.Y = newBounds.Top;
                item.Entity.Width = newBounds.Width;
                item.Entity.Height = newBounds.Height;
                base.Resize(placement, newBounds);
            }
        }
    }
}

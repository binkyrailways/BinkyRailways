using BinkyRailways.WinApp.Controls.VirtualCanvas;
using BinkyRailways.WinApp.Controls.VirtualCanvas.Handlers;

namespace BinkyRailways.WinApp.Items.Edit
{
    /// <summary>
    /// Mouse handler used to select items
    /// </summary>
    internal sealed class ItemSelectMouseHandler : SelectMouseHandler
    {
        /// <summary>
        /// Default ctor
        /// </summary>
        public ItemSelectMouseHandler(IVCItemContainer container, MouseHandler next)
            : base(container, next)
        {
        }

        /// <summary>
        /// Can the given item placement be selected.
        /// Override this method to block items from being selected.
        /// </summary>
        protected override bool CanSelect(VCItemPlacement placement)
        {
            if (!(placement.Item is IPositionedEntityItem))
                return false;
            var moduleItem = placement.Item as ModuleEditItem;
            if (moduleItem != null)
                return !moduleItem.ContentsEditable;
            return true;
        }
    }
}

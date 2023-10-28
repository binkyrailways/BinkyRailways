using System.Linq;
using System.Windows.Forms;
using BinkyRailways.Core.Model;
using BinkyRailways.Core.State;
using BinkyRailways.Core.State.Automatic;
using BinkyRailways.WinApp.Controls.VirtualCanvas;
using BinkyRailways.WinApp.Controls.VirtualCanvas.Handlers;
using BinkyRailways.WinApp.Items.Run;
using ItemDragEventArgs = BinkyRailways.WinApp.Controls.VirtualCanvas.Handlers.ItemDragEventArgs;

namespace BinkyRailways.WinApp.Items.Handlers
{
    internal class SetLocTargetDragDropHandler : ContainerDragDropHandler
    {
        /// <summary>
        /// Default ctor
        /// </summary>
        public SetLocTargetDragDropHandler(VCItemContainer container, DragDropHandler next)
            : base(container, next)
        {
        }

        /// <summary>
        /// Test drag over.
        /// </summary>
        public override bool OnDragOver(VCItem sender, ItemDragEventArgs e)
        {
            var loc = GetLoc(e);
            if (loc != null)
            {
                var blockItem = sender as BlockItem;
                if (blockItem != null)
                {
                    var blockState = blockItem.State;
                    // Are there possible sequences?
                    if (TargetBlockRouteSelector.FindRouteSequences(loc.CurrentBlock.Actual, loc.CurrentBlockEnterSide.Actual, blockState).Any())
                    {
                        e.Effect = DragDropEffects.Move;
                        return true;
                    }
                }
            }
            return base.OnDragOver(sender, e);
        }

        /// <summary>
        /// Perform drop.
        /// </summary>
        public override bool OnDragDrop(VCItem sender, ItemDragEventArgs e)
        {
            var loc = GetLoc(e);
            if (loc != null)
            {
                var blockItem = sender as BlockItem;
                if (blockItem != null)
                {
                    var blockState = blockItem.State;
                    // Are there possible sequences?
                    if (TargetBlockRouteSelector.FindRouteSequences(loc.CurrentBlock.Actual, loc.CurrentBlockEnterSide.Actual, blockState).Any())
                    {
                        loc.RouteSelector = new TargetBlockRouteSelector(blockState);
                        return true;
                    }
                }
            }
            return base.OnDragDrop(sender, e);
        }

        /// <summary>
        /// Gets the loc state from the given drag/drop event args.
        /// </summary>
        private static ILocState GetLoc(ItemDragEventArgs e)
        {
            var container = e.Data.GetData(typeof(EntityStateContainer)) as EntityStateContainer;
            if (container != null)
            {
                var locState = container.State as ILocState;
                if ((locState != null) && (locState.CurrentBlock.Actual == null))
                    return null;
                return locState;
            }
            return null;
        }
    }
}

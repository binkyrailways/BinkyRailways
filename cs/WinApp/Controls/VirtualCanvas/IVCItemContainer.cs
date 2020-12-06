using System.Drawing;

namespace BinkyRailways.WinApp.Controls.VirtualCanvas
{
    public interface IVCItemContainer
    {
        /// <summary>
        /// Gets the item placements within this container.
        /// </summary>
        VCItemPlacementCollection Items { get; }

        /// <summary>
        /// Gets all selected items within this container.
        /// </summary>
        SelectedVCItemPlacementCollection SelectedItems { get; }

        /// <summary>
        /// Gets / Sets layout manager used to layout items.
        /// </summary>
        Layout.IVCLayoutManager LayoutManager { get; set; }

        /// <summary>
        /// Gets the size that is available to this container.
        /// Overriding this property is only done in the root container.
        /// </summary>
        Size AvailableSize { get; }

        /// <summary>
        /// Gets the current size
        /// </summary>
        Size Size { get; }

        /// <summary>
        /// Gets / sets handler to control mouse behavior.
        /// </summary>
        Handlers.MouseHandler MouseHandler { get; set; }

        /// <summary>
        /// Draw in the context of the container using the given draw handler.
        /// The graphics provided to the draw handler is adjust for the 
        /// zoom factor and position of the container.
        /// </summary>
        /// <param name="g"></param>
        void DrawContainer(ItemPaintEventArgs e, DrawContainerHandler drawHandler);

        /// <summary>
        /// Draw all child items using the given draw handler.
        /// The graphics provided to the draw handler is adjust for the position
        /// of the item and the zoom factor and position of the container.
        /// </summary>
        /// <param name="g"></param>
        void DrawItems(ItemPaintEventArgs e, DrawItemHandler drawHandler);

        /// <summary>
        /// Request a redraw of this container.
        /// </summary>
        void Invalidate();
    }

    /// <summary>
    /// Delegate used to draw in a container.
    /// </summary>
    /// <param name="e"></param>
    public delegate void DrawContainerHandler(ItemPaintEventArgs e);

    /// <summary>
    /// Delegate used to draw items in a container.
    /// </summary>
    /// <param name="e"></param>
    /// <param name="placement"></param>
    public delegate void DrawItemHandler(ItemPaintEventArgs e, VCItemPlacement placement);
}

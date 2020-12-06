namespace BinkyRailways.WinApp.Controls.VirtualCanvas
{
    public enum ContainerMessage
    {
        /// <summary>
        /// Size of sending item has changed.
        /// </summary>
        SizeChanged,

        /// <summary>
        /// Request a redraw of the sending item
        /// </summary>
        Redraw,

        /// <summary>
        /// Zoom the control to the given rectangle
        /// </summary>
        ZoomToRectangle,

        /// <summary>
        /// Block paint events
        /// </summary>
        BeginUpdate,

        /// <summary>
        /// Custom message from sending item to control
        /// </summary>
        CustomMessage,

        /// <summary>
        /// Make sure a point on the sender item it visible
        /// </summary>
        EnsureVisible,

        /// <summary>
        /// Update matrix to transform from client coordinates to control coordinates
        /// </summary>
        Local2ControlMatrix,
    }
}

using System;
using BinkyRailways.WinApp.Controls.VirtualCanvas;
using BinkyRailways.WinApp.Controls.VirtualCanvas.Handlers;

namespace BinkyRailways.WinApp.Items.Handlers
{
    /// <summary>
    /// Handler that triggers a tooltip on entry.
    /// </summary>
    internal class ToolTipMouseHandler : MouseHandler
    {
        /// <summary>
        /// Default ctor
        /// </summary>
        internal ToolTipMouseHandler(MouseHandler nextHandler)
            : base(nextHandler)
        {
        }

        /// <summary>
        /// Mouse has entered this item
        /// </summary>
        public override void OnMouseEnter(VCItem sender, EventArgs e)
        {
            if (sender is IEntityItem)
            {
                ((IEntityItem)sender).ShowToolTip();
            }
            base.OnMouseEnter(sender, e);
        }

        public override bool OnMouseUp(VCItem sender, ItemMouseEventArgs e)
        {
            if ((e.Button == System.Windows.Forms.MouseButtons.Right) && (e.Clicks == 1))
            {
                // Show context menu
                if (sender is IEntityItem)
                {
                    ((IEntityItem)sender).ShowContextMenu(e.Location);
                }
            }
            return base.OnMouseUp(sender, e);
        }
    }
}

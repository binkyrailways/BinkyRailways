using BinkyRailways.WinApp.Controls.VirtualCanvas;

namespace BinkyRailways.WinApp.Items.Messages
{
    internal class ShowToolTipMessage : ItemMessage
    {
        /// <summary>
        /// Default ctor
        /// </summary>
        internal ShowToolTipMessage(VCItemContainer sender, string toolTip)
            : base(sender)
        {
            ToolTip = toolTip;
        }

        /// <summary>
        /// Tooltip to show
        /// </summary>
        public string ToolTip { get; private set; }

        /// <summary>
        /// Accept a visit by the given visitor
        /// </summary>
        public override void Accept(ItemMessageVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}

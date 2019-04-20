using System.Drawing;
using BinkyRailways.WinApp.Controls.VirtualCanvas;

namespace BinkyRailways.WinApp.Items.Messages
{
    internal class ShowContextMenuMessage: ItemMessage
    {
        private readonly IEntityItem[] selection;
        private readonly PointF pt;

        /// <summary>
        /// Default ctor
        /// </summary>
        internal ShowContextMenuMessage(VCItemContainer sender, PointF pt, params IEntityItem[] selection)
            : base(sender)
        {
            this.selection = selection;
            this.pt = pt;
        }

        /// <summary>
        /// Location in local space
        /// </summary>
        public PointF Location
        {
            get { return pt; }
        }

        /// <summary>
        /// Accept a visit by the given visitor
        /// </summary>
        public override void Accept(ItemMessageVisitor visitor)
        {
            visitor.Visit(this);
        }

        /// <summary>
        /// Gets the selected items
        /// </summary>
        public IEntityItem[] Selection
        {
            get { return selection; }
        }
    }
}

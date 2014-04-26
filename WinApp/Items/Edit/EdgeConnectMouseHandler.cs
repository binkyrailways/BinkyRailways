using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using BinkyRailways.WinApp.Controls.VirtualCanvas;
using BinkyRailways.WinApp.Controls.VirtualCanvas.Handlers;

namespace BinkyRailways.WinApp.Items.Edit
{
    internal class EdgeConnectMouseHandler : MouseHandler
    {
        private readonly EdgeItem edgeItem;
        private readonly ItemContext context;
        private bool mouseIsOver;

        /// <summary>
        /// Default ctor
        /// </summary>
        public EdgeConnectMouseHandler(EdgeItem edgeItem, ItemContext context, MouseHandler next) : base(next)
        {
            this.edgeItem = edgeItem;
            this.context = context;
        }

        public override bool OnMouseMove(VCItem sender, ItemMouseEventArgs e)
        {
            if (context.SelectedEdge != null)
            {
                e.Cursor = Cursors.Cross;
            }
            return base.OnMouseMove(sender, e);
        }

        /// <summary>
        /// Connect edge.
        /// </summary>
        public override bool OnMouseClick(VCItem sender, ItemMouseEventArgs e)
        {
            var edge = edgeItem.Edge;
            var railway = context.Railway;

            if ((railway == null) || (railway.ModuleConnections.Any(x => (x.EdgeA == edge) || (x.EdgeB == edge))))
                return base.OnMouseClick(sender, e);

            if (context.SelectedEdge == null)
            {
                context.SelectedEdge = edge;
            }
            else if (context.SelectedEdge.Module != edge.Module)
            {
                var connection = railway.ModuleConnections.AddNew();
                connection.EdgeA = context.SelectedEdge;
                connection.EdgeB = edge;
                context.SelectedEdge = null;
                edgeItem.Invalidate();
                context.Reload();
            }
            return true;
        }

        /// <summary>
        /// Mouse handlers can draw over an already drawn item.
        /// </summary>
        public override void OnPostPaint(VCItem sender, ItemPaintEventArgs e)
        {
            if (mouseIsOver)
            {
                var sz = edgeItem.Size;
                var bounds = new Rectangle(sz.Width / -2, sz.Height / -2, sz.Width * 2, sz.Height * 2);
                e.Graphics.FillRectangle(Brushes.Red, bounds);
            }
            base.OnPostPaint(sender, e);
        }

        /// <summary>
        /// Is the mouse over this item?
        /// </summary>
        private bool MouseIsOver
        {
            get { return mouseIsOver; }
            set
            {
                if (mouseIsOver != value)
                {
                    mouseIsOver = value;
                    edgeItem.Invalidate();
                }
            }
        }
    }
}

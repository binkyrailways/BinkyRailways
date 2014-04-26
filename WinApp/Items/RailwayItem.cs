using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using BinkyRailways.Core.Model;
using BinkyRailways.WinApp.Controls.VirtualCanvas;
using BinkyRailways.WinApp.Items.Edit;

namespace BinkyRailways.WinApp.Items
{
    /// <summary>
    /// Item showing an entire railway
    /// </summary>
    public abstract class RailwayItem : VCItemContainer
    {
        private readonly IRailway railway;
        private readonly ItemContext context;
    
        /// <summary>
        /// Default ctor
        /// </summary>
        protected RailwayItem(IRailway railway, ItemContext context)
        {
            this.railway = railway;
            this.context = context;
            LayoutManager = new PositionedEntityLayoutManager();
        }

        /// <summary>
        /// Gets the drawing context
        /// </summary>
        public ItemContext Context
        {
            get { return context; }
        }

        /// <summary>
        /// Gets the railway shown in this item
        /// </summary>
        internal IRailway Railway
        {
            get { return railway; }
        }

        /// <summary>
        /// Try to get the module item showing the given module
        /// </summary>
        protected abstract ModuleItem GetModuleItem(IModule module);

        /// <summary>
        /// Draw this item and all child items
        /// </summary>
        public override void Draw(ItemPaintEventArgs e)
        {
            base.Draw(e);

            // Draw module connections
            if (railway.ModuleConnections.Count > 0)
            {
                var oldMode = e.Graphics.SmoothingMode;
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                using (var pen = new Pen(Color.DarkBlue, 2))
                {
                    foreach (var connection in railway.ModuleConnections)
                    {
                        var edgeA = GetModuleItem(connection.EdgeA);
                        var edgeB = GetModuleItem(connection.EdgeB);

                        if ((edgeA == null) || (edgeB == null))
                            continue;

                        // Find position of edges center in my coordinate space
                        var ptA = Control2Local(edgeA.Local2Control(Center(connection.EdgeA)));
                        var ptB = Control2Local(edgeB.Local2Control(Center(connection.EdgeB)));

                        e.Graphics.DrawLine(pen, ptA, ptB);
                    }
                }
                e.Graphics.SmoothingMode = oldMode;
            }
        }

        /// <summary>
        /// Gets the item showing the given edge.
        /// </summary>
        private ModuleItem GetModuleItem(IEdge edge)
        {
            if (edge == null)
                return null;
            return GetModuleItem(edge.Module);
        }

        /// <summary>
        /// Gets the center of the given edge.
        /// </summary>
        private static PointF Center(IEdge edge)
        {
            return new PointF(edge.X + edge.Width / 2.0f, edge.Y + edge.Height / 2.0f);
        }
    }
}

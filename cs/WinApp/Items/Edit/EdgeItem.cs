using System;
using System.Drawing;
using System.Linq;
using BinkyRailways.Core.Model;
using BinkyRailways.WinApp.Controls.VirtualCanvas;
using BinkyRailways.WinApp.Utils;

namespace BinkyRailways.WinApp.Items.Edit
{
    /// <summary>
    /// Item showing an edge
    /// </summary>
    public class EdgeItem : PositionedItem<IEdge>
    {
        /// <summary>
        /// Default ctor
        /// </summary>
        public EdgeItem(IEdge edge, bool editable, bool railwayEditable, ItemContext context)
            : base(edge, context)
        {
            if (railwayEditable)
            {
                MouseHandler = new EdgeConnectMouseHandler(this, context, MouseHandler);
            }
        }

        /// <summary>
        /// Gets my edge.
        /// </summary>
        public IEdge Edge { get { return Entity; } }

        /// <summary>
        /// Draw this item on the given graphics.
        /// The graphics is transformed such that this item can draw starting at (0, 0).
        /// </summary>
        protected override void DrawItem(ItemPaintEventArgs e, Size sz)
        {
            var roundSize = Math.Min(sz.Width, sz.Height) / 2;
            using (var path = GraphicsUtil.CreateRoundedRectangle(sz, roundSize))
            {
                using (var brush = new SolidBrush(Color.FromArgb(192, BackgroundColor)))
                {
                    e.Graphics.FillPath(brush, path);
                }
                using (var pen = new Pen(BorderColor))
                {
                    e.Graphics.DrawPath(pen, path);
                }
            }
            if (Context.ShowDescriptions)
            {
                using (var brush = new SolidBrush(TextColor))
                {
                    var font = SystemFonts.DefaultFont;
                    var text = Text;
                    var bounds = e.Graphics.GetTextBounds(font, text, new PointF(sz.Width/2.0F, sz.Height),
                                                          ContentAlignment.TopCenter);
                    e.Graphics.DrawString(Text, font, brush, bounds);
                }
            }
        }

        /// <summary>
        /// Gets the color of the background.
        /// </summary>
        protected virtual Color BackgroundColor
        {
            get
            {
                var selectedEntities = Context.SelectedEntities.ToList();
                if (selectedEntities.Contains(Entity))
                    return Color.Red;


                var selectedRoutes = selectedEntities.OfType<IRoute>().ToList();
                if (selectedRoutes.Count > 0)
                {
                    if (selectedRoutes.Any(x => x.To == Entity))
                        return Color.Red;
                    if (selectedRoutes.Any(x => x.From == Entity))
                        return Color.GreenYellow;
                }

                return Color.Black;
            }
        }

        /// <summary>
        /// Gets the color of the border.
        /// </summary>
        protected virtual Color BorderColor
        {
            get { return Color.Purple; }
        }

        /// <summary>
        /// Gets the color of the background.
        /// </summary>
        protected virtual Color TextColor
        {
            get { return Color.Black; }
        }

        /// <summary>
        /// Text to draw in the block.
        /// </summary>
        protected virtual string Text
        {
            get { return Entity.Description; }
        }
    }
}

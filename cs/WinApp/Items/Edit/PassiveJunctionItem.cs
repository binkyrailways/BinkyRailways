using System;
using System.Drawing;
using System.Linq;
using BinkyRailways.Core.Model;
using BinkyRailways.WinApp.Controls.VirtualCanvas;
using BinkyRailways.WinApp.Utils;

namespace BinkyRailways.WinApp.Items.Edit
{
    /// <summary>
    /// Item showing a passive junction
    /// </summary>
    public class PassiveJunctionItem : PositionedItem<IPassiveJunction>
    {
        /// <summary>
        /// Default ctor
        /// </summary>
        public PassiveJunctionItem(IPassiveJunction junction, ItemContext context)
            : base(junction, context)
        {
        }

        /// <summary>
        /// Draw this item on the given graphics.
        /// The graphics is transformed such that this item can draw starting at (0, 0).
        /// </summary>
        protected override void DrawItem(ItemPaintEventArgs e, Size sz)
        {
            // Draw background
            var roundSize = Math.Min(sz.Width, sz.Height) / 4;
            using (var path = GraphicsUtil.CreateRoundedRectangle(sz, roundSize))
            {
                using (var brush = new SolidBrush(Color.FromArgb(192, BackgroundColor)))
                {
                    e.Graphics.FillPath(brush, path);
                }
                e.Graphics.DrawPath(Pens.Silver, path);

                // Draw text);
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

                // Clip to rounded rectangle
                e.Graphics.SetClip(path);

                // Draw state
                using (var pen = new Pen(Color.Gray, 2.0f))
                {
                    e.Graphics.DrawLine(pen, 0, 0, sz.Width, sz.Height);
                    e.Graphics.DrawLine(pen, 0, sz.Height, sz.Width, 0);
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
                var junction = Entity;
                var selectedEntities = Context.SelectedEntities.ToList();
                if (selectedEntities.Contains(junction))
                    return Color.Red;

                var selectedRoutes = selectedEntities.OfType<IRoute>().ToList();
                if (selectedRoutes.Count > 0)
                {
                    if (selectedRoutes.Any(x => x.CrossingJunctions.Contains(junction)))
                        return Color.GreenYellow;
                }
                return Color.White;
            }
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

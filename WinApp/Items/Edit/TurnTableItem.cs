using System;
using System.Drawing;
using System.Linq;
using BinkyRailways.Core.Model;
using BinkyRailways.WinApp.Controls.VirtualCanvas;
using BinkyRailways.WinApp.Utils;

namespace BinkyRailways.WinApp.Items.Edit
{
    /// <summary>
    /// Item showing a turntable
    /// </summary>
    public class TurnTableItem : PositionedItem<ITurnTable>
    {
        /// <summary>
        /// Default ctor
        /// </summary>
        public TurnTableItem(ITurnTable turnTable, bool editable, ItemContext context)
            : base(turnTable, context)
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
            using (var path = GraphicsUtil.CreateCircle(Math.Min(sz.Width, sz.Height)))
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
                using (var brush = new SolidBrush(TextColor))
                {
                    var format = new StringFormat();
                    format.LineAlignment = StringAlignment.Center;
                    format.Alignment = StringAlignment.Center;
                    format.FormatFlags = StringFormatFlags.NoWrap;
                    e.Graphics.DrawString(Position.ToString(), SystemFonts.DefaultFont, brush,
                        new RectangleF(0, 0, sz.Width, sz.Height), format);
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
                var turnTable = Entity;
                var selectedEntities = Context.SelectedEntities.ToList();
                if (selectedEntities.Contains(turnTable))
                    return Color.Red;

                var selectedRoutes = selectedEntities.OfType<IRoute>().ToList();
                if (selectedRoutes.Count > 0)
                {
                    if (selectedRoutes.Any(x => x.CrossingJunctions.Contains(turnTable)))
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

        /// <summary>
        /// Position of the turntable to display
        /// </summary>
        protected virtual string Position
        {
            get { return Entity.InitialPosition.ToString(); }
        }
    }
}

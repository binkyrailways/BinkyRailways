using System;
using System.Drawing;
using System.Linq;
using BinkyRailways.Core.Model;
using BinkyRailways.WinApp.Controls.VirtualCanvas;
using BinkyRailways.WinApp.Utils;

namespace BinkyRailways.WinApp.Items.Edit
{
    /// <summary>
    /// Item showing a 4-stage clock
    /// </summary>
    public class Clock4StageOutputItem : PositionedItem<IClock4StageOutput>
    {
        /// <summary>
        /// Default ctor
        /// </summary>
        public Clock4StageOutputItem(IClock4StageOutput output, bool editable, ItemContext context)
            : base(output, context)
        {
        }

        /// <summary>
        /// Draw this item on the given graphics.
        /// The graphics is transformed such that this item can draw starting at (0, 0).
        /// </summary>
        protected override void DrawItem(ItemPaintEventArgs e, Size sz)
        {
            using (var path = GraphicsUtil.CreateCircle(Math.Min(sz.Width, sz.Height)))
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
            DrawTime(e, sz);           
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
        /// Draw the current time in the clock
        /// </summary>
        protected virtual void DrawTime(ItemPaintEventArgs e, Size sz)
        {
            var g = e.Graphics;
            var halfWidth = sz.Width / 2.0f;
            var halfHeight = sz.Height / 2.0f;
            g.DrawLine(Pens.Black, halfWidth, sz.Height / 8.0f, halfWidth, halfHeight);
            g.DrawLine(Pens.Black, halfWidth, halfHeight, halfWidth + sz.Width / 4.0f, halfHeight);
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
                return Color.White;
            }
        }

        /// <summary>
        /// Gets the color of the border.
        /// </summary>
        protected virtual Color BorderColor
        {
            get { return Color.CadetBlue; }
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

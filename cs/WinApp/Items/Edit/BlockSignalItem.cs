using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using BinkyRailways.Core.Model;
using BinkyRailways.WinApp.Controls.VirtualCanvas;
using BinkyRailways.WinApp.Utils;

namespace BinkyRailways.WinApp.Items.Edit
{
    /// <summary>
    /// Item showing a block signal
    /// </summary>
    public class BlockSignalItem : PositionedItem<IBlockSignal>
    {
        /// <summary>
        /// Default ctor
        /// </summary>
        public BlockSignalItem(IBlockSignal signal, bool editable, ItemContext context)
            : base(signal, context)
        {
        }

        public override int Priority => 3;

        /// <summary>
        /// Draw this item on the given graphics.
        /// The graphics is transformed such that this item can draw starting at (0, 0).
        /// </summary>
        protected override void DrawItem(ItemPaintEventArgs e, Size sz)
        {
            var roundSize = Math.Min(sz.Width, sz.Height) / 2;
            var offsetLeft = sz.Width / 4;
            using (var path = GraphicsUtil.CreateRoundedRectangle(new Size(sz.Width - offsetLeft, sz.Height), roundSize))
            {
                var offsetTx = new Matrix();
                offsetTx.Translate(offsetLeft, 0);
                path.Transform(offsetTx);
                using (var brush = new SolidBrush(Color.FromArgb(192, BackgroundColor)))
                {
                    e.Graphics.FillPath(brush, path);
                }
                var borderColor = BorderColor;
                using (var pen = new Pen(borderColor))
                {
                    e.Graphics.DrawPath(pen, path);
                    var m = sz.Height / 2;
                    e.Graphics.DrawLine(pen, 0, m, offsetLeft, m);
                    e.Graphics.DrawLine(pen, 0, 0, 0, sz.Height);
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
                return Color.White;
            }
        }

        /// <summary>
        /// Gets the color of the border.
        /// </summary>
        protected virtual Color BorderColor
        {
            get { return Color.Black; }
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

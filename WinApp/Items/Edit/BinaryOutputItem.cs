using System.Drawing;
using System.Linq;
using BinkyRailways.Core.Model;
using BinkyRailways.WinApp.Controls.VirtualCanvas;
using BinkyRailways.WinApp.Utils;

namespace BinkyRailways.WinApp.Items.Edit
{
    /// <summary>
    /// Item showing a output
    /// </summary>
    public class BinaryOutputItem : PositionedItem<IBinaryOutput>
    {
        /// <summary>
        /// Default ctor
        /// </summary>
        public BinaryOutputItem(IBinaryOutput output, bool editable, ItemContext context)
            : base(output, context)
        {
        }

        public override int Priority => 3;

        /// <summary>
        /// Draw this item on the given graphics.
        /// The graphics is transformed such that this item can draw starting at (0, 0).
        /// </summary>
        protected override void DrawItem(ItemPaintEventArgs e, Size sz)
        {
            using (var path = GraphicsUtil.CreateRoundedRectangle(sz, 1))
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
                return Color.Gray;
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

using System;
using System.Drawing;
using System.Linq;
using BinkyRailways.Core.Model;
using BinkyRailways.WinApp.Controls.VirtualCanvas;
using BinkyRailways.WinApp.Utils;

namespace BinkyRailways.WinApp.Items.Edit
{
    /// <summary>
    /// Item showing a standard switch
    /// </summary>
    public class SwitchItem : PositionedItem<ISwitch>
    {
        /// <summary>
        /// Default ctor
        /// </summary>
        public SwitchItem(ISwitch @switch, bool editable, ItemContext context)
            : base(@switch, context)
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
                if (Direction == SwitchDirection.Straight)
                {
                    using (var pen = new Pen(Color.Green, 3.0f))
                    {
                        e.Graphics.DrawLine(pen, 0, sz.Height/2.0f, sz.Width, sz.Height/2.0f);
                    }
                }
                else
                {
                    using (var pen = new Pen(Color.Red, 3.0f))
                    {
                        e.Graphics.DrawLine(pen, 0, 0, sz.Width, sz.Height);
                    }
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
                var @switch = Entity;
                var selectedEntities = Context.SelectedEntities.ToList();
                if (selectedEntities.Contains(@switch))
                    return Color.Red;

                var selectedRoutes = selectedEntities.OfType<IRoute>().ToList();
                if (selectedRoutes.Count > 0)
                {
                    if (selectedRoutes.Any(x => x.CrossingJunctions.Contains(@switch)))
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
        /// Direction of the switch to display
        /// </summary>
        protected virtual SwitchDirection Direction
        {
            get { return SwitchDirection.Straight; }
        }
    }
}

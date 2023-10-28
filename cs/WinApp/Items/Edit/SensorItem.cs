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
    /// Item showing a sensor
    /// </summary>
    public class SensorItem : PositionedItem<ISensor>
    {
        /// <summary>
        /// Default ctor
        /// </summary>
        public SensorItem(ISensor sensor, bool editable, ItemContext context)
            : base(sensor, context)
        {
        }

        /// <summary>
        /// Draw this item on the given graphics.
        /// The graphics is transformed such that this item can draw starting at (0, 0).
        /// </summary>
        protected override void DrawItem(ItemPaintEventArgs e, Size sz)
        {
            using (var path = CreateShape(sz))
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
                var sensor = Entity;
                var selectedEntities = Context.SelectedEntities.ToList();
                if (selectedEntities.Contains(sensor))
                    return Color.Red;

                var selectedRoutes = selectedEntities.OfType<IRoute>().ToList();
                if (selectedRoutes.Count > 0)
                {
                    if (selectedRoutes.Any(x => x.Events.Any(e => (e.Sensor == sensor) && e.Behaviors.Any(b => b.StateBehavior == RouteStateBehavior.Enter))))
                        return Color.GreenYellow;
                    if (selectedRoutes.Any(x => x.Events.Any(e => (e.Sensor == sensor) && e.Behaviors.Any(b => b.StateBehavior == RouteStateBehavior.Reached))))
                        return Color.Red;
                    if (selectedRoutes.Any(x => x.Events.Any(e => (e.Sensor == sensor))))
                        return Color.Blue;
                }

                var selectedBlocks = selectedEntities.OfType<IBlock>();
                if (selectedBlocks.Any(x => sensor.Block == x))
                    return Color.Orange;

                return Color.Gray;
            }
        }

        /// <summary>
        /// Gets the color of the border.
        /// </summary>
        protected virtual Color BorderColor
        {
            get { return Color.Yellow; }
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
        /// Create a graphics path to draw the entity shape.
        /// </summary>
        protected GraphicsPath CreateShape(Size sz)
        {
            var roundSize = Math.Min(sz.Width, sz.Height) / 2;
            switch (Entity.Shape)
            {
                case Shapes.Circle:
                    return GraphicsUtil.CreateRoundedRectangle(sz, roundSize);
                case Shapes.Diamond:
                    return GraphicsUtil.CreateDiamond(sz);
                case Shapes.Square:
                    return GraphicsUtil.CreateSquare(sz);
                case Shapes.Triangle:
                    return GraphicsUtil.CreateTriangle(sz);
                default:
                    throw new ArgumentException("Unknown shape: " + (int)Entity.Shape);
            }
        }
    }
}

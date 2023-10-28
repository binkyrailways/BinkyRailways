using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using BinkyRailways.Core.Model;
using BinkyRailways.WinApp.Controls.VirtualCanvas;
using BinkyRailways.WinApp.Utils;

namespace BinkyRailways.WinApp.Items.Edit
{
    /// <summary>
    /// Item showing a block
    /// </summary>
    public class BlockItem : PositionedItem<IBlock>
    {
        /// <summary>
        /// Default ctor
        /// </summary>
        public BlockItem(IBlock block, bool editable, ItemContext context)
            : base(block, context)
        {
        }

        public override int Priority => 5;

        /// <summary>
        /// Draw this item on the given graphics.
        /// The graphics is transformed such that this item can draw starting at (0, 0).
        /// </summary>
        protected override void DrawItem(ItemPaintEventArgs e, Size sz)
        {
            var roundSize = Math.Min(sz.Width, sz.Height) / 3;
            var reverse = Entity.ReverseSides;
            using (var path = GraphicsUtil.CreateRoundedRectangle(sz, roundSize))
            {
                var rect = new Rectangle(Point.Empty, sz);
                var colors = BackgroundColors;
                var colorFront = Color.FromArgb(192, colors.Item1);
                var colorBack = Color.FromArgb(192, colors.Item2);
                if (reverse)
                {
                    var tmp = colorBack;
                    colorBack = colorFront;
                    colorFront = tmp;
                }
                using (var brush = new LinearGradientBrush(rect, colorBack, colorFront, 0.0f))
                {
                    e.Graphics.FillPath(brush, path);
                }
                {
                    // Draw front marker
                    var curClip = e.Graphics.Clip;
                    var w = (Math.Min(sz.Width, sz.Height) * 0.5f);
                    e.Graphics.Clip = new Region(new RectangleF(reverse ? 0 : sz.Width - w, 0, w, sz.Height));
                    e.Graphics.FillPath(Brushes.Aqua, path);
                    e.Graphics.Clip = curClip;
                }
                e.Graphics.DrawPath(Entity.IsStation ? Pens.DarkRed : Pens.Blue, path);                    
            }
            // Draw "front" marker
            /*using (var path = new GraphicsPath())
            {
                var radius = (Math.Min(sz.Width, sz.Height) * 0.5f);
                var xOffset = radius * 1.3f;
                var p1 = new PointF(reverse ? xOffset : sz.Width - xOffset, (sz.Height - radius)/2.0f);
                var p2 = new PointF(reverse ? p1.X - radius : p1.X + radius, sz.Height / 2.0f);
                var p3 = new PointF(p1.X, p1.Y + radius);
                path.AddLines(new[] { p1, p2, p3 });
                path.CloseFigure();
                e.Graphics.FillPath(Brushes.Green, path);
                e.Graphics.DrawPath(Pens.Yellow, path);
            }*/
            using (var brush = new SolidBrush(TextColor))
            {
                var format = new StringFormat();
                format.LineAlignment = StringAlignment.Center;
                format.Alignment = StringAlignment.Center;
                format.FormatFlags = StringFormatFlags.NoWrap;
                e.Graphics.DrawString(Text, SystemFonts.DefaultFont, brush,
                    new RectangleF(0, 0, sz.Width, sz.Height), format);
            }
        }

        /// <summary>
        /// Gets the color of the background (front, back of block).
        /// </summary>
        protected virtual Tuple<Color, Color> BackgroundColors
        {
            get
            {
                var block = Entity;
                var selectedEntities = Context.SelectedEntities.ToList();
                if (selectedEntities.Contains(block))
                    return Tuple.Create(Color.Red, Color.Red);

                var selectedRoutes = selectedEntities.OfType<IRoute>().ToList();
                if (selectedRoutes.Count > 0)
                {
                    if (selectedRoutes.Any(x => x.To == block))
                        return Tuple.Create(Color.Red, Color.Red);
                    if (selectedRoutes.Any(x => x.From == block))
                        return Tuple.Create(Color.GreenYellow, Color.GreenYellow);
                }

                var selectedSignals = selectedEntities.OfType<IBlockSignal>();
                if (selectedSignals.Any(x => x.Block == block))
                    return Tuple.Create(Color.GreenYellow, Color.GreenYellow);

                var selectedSensors = selectedEntities.OfType<ISensor>().ToList();
                if (selectedSensors.Any(x => x.Block == block))
                    return Tuple.Create(Color.Orange, Color.Orange);
                if (selectedSensors.Any(x => GetRoutesUsingSensor(x).Any(r => (r.To == block) || (r.From == block)))) 
                    return Tuple.Create(Color.GreenYellow, Color.GreenYellow);
                
                var c = Color.White;
                return Tuple.Create(c, c);
            }
        }

        /// <summary>
        /// Gets all routes that use the given sensor
        /// </summary>
        private IEnumerable<IRoute> GetRoutesUsingSensor(ISensor sensor)
        {
            var module = sensor.Module;
            return module == null ? Enumerable.Empty<IRoute>() : module.Routes.Where(sensor.IsUsedBy);
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
        /// Create a tooltip for this item
        /// </summary>
        public override string ToolTip
        {
            get { return Text; }
        }
    }
}

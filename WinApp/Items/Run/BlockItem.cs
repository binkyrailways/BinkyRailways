using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using BinkyRailways.Core.Model;
using BinkyRailways.Core.State;
using BinkyRailways.WinApp.Controls.VirtualCanvas;
using BinkyRailways.WinApp.Controls.VirtualCanvas.Handlers;
using BinkyRailways.WinApp.Items.Handlers;
using BinkyRailways.WinApp.Items.Menu;

namespace BinkyRailways.WinApp.Items.Run
{
    /// <summary>
    /// Item showing a block state
    /// </summary>
    public sealed class BlockItem : Edit.BlockItem
    {
        private readonly IBlockState state;

        /// <summary>
        /// Default ctor
        /// </summary>
        public BlockItem(IBlock entity, IBlockState state, ItemContext context)
            : base(entity, false, context)
        {
            this.state = state;
            DragDropHandler = new SetLocTargetDragDropHandler(this, DragDropHandler);
            MouseHandler = new SelectLocHandler(this, MouseHandler);
        }

        /// <summary>
        /// Fill the entries of a context menu
        /// </summary>
        public override void BuildContextMenu(ContextMenuStrip menu)
        {
            if (state.Closed.Actual)
            {
                menu.Items.Add(new OpenBlockMenuItem(state));
            }
            else
            {
                menu.Items.Add(new CloseBlockMenuItem(state));
            }
        }

        /// <summary>
        /// Gets the blockstate represented by this item.
        /// </summary>
        internal IBlockState State { get { return state; } }

        /// <summary>
        /// Draw this block
        /// </summary>
        protected override void DrawItem(ItemPaintEventArgs e, Size sz)
        {
            base.DrawItem(e, sz);

            var blockState = state.State;
            var loc = state.LockedBy;
            if ((loc != null) && (loc.CurrentBlock.Actual == state))
            {
                var reverse = Entity.ReverseSides;
                if (loc.CurrentBlockEnterSide.Actual == BlockSide.Front)
                {
                    // If loc entered block at front, we go towards back.
                    reverse = !reverse;
                }
                // Draw "loc direction" marker
                using (var path = new GraphicsPath())
                {
                    var radius = (Math.Min(sz.Width, sz.Height) * 1.0f);
                    var xOffset = 0.0f;// radius * 0.5f;
                    var p1 = new PointF(reverse ? -xOffset : sz.Width + xOffset, sz.Height / 2.0f);
                    var brush = loc.PossibleDeadlock.Actual ? Brushes.Orange : Brushes.White;
                    e.Graphics.FillEllipse(brush, new RectangleF(new PointF(p1.X - (radius / 4.0f), p1.Y - (radius / 4.0f)), new SizeF(radius / 2.0f, radius / 2.0f)));
                }
            }
        }

        /// <summary>
        /// Gets the colors of the background (front, back of block).
        /// </summary>
        protected override Tuple<Color, Color> BackgroundColors
        {
            get
            {
                var blockState = state.State;
                var loc = state.LockedBy;
                Color c1;
                Color c2;
                switch (blockState)
                {
                    case BlockState.Free:
                        c1 = c2 = Color.White;
                        break;
                    case BlockState.OccupiedUnexpected:
                        c1 = c2 = Color.Orange;
                        break;
                    case BlockState.Occupied:
                        if (loc == null) 
                        {
                            c1 = c2 = Color.Red;
                        }
                        else if ((loc.CurrentRoute.Actual == null) || ((loc.CurrentBlock.Actual == state) && (loc.CurrentRoute.Actual.Route.To == state)))
                        {
                            if (loc.CurrentBlockEnterSide.Actual == BlockSide.Back)
                            {
                                c1 = Color.Red;
                                c2 = Color.LightGray;
                            }
                            else
                            {
                                c2 = Color.Red;
                                c1 = Color.LightGray;
                            }
                        }
                        else if (loc.CurrentRoute.Actual.Route.FromBlockSide == BlockSide.Front)
                        {
                            c1 = Color.Red;
                            c2 = Color.LightGray;
                        }
                        else
                        {
                            c2 = Color.Red;
                            c1 = Color.LightGray;
                        }
                        break;
                    case BlockState.Destination:
                        if ((loc == null) || (loc.CurrentRoute.Actual == null))
                        {
                            c1 = c2 = Color.Yellow;
                        }
                        else if (loc.CurrentRoute.Actual.Route.ToBlockSide == BlockSide.Back)
                        {
                            c1 = Color.Yellow;
                            c2 = Color.LightGray;
                        }
                        else
                        {
                            c2 = Color.Yellow;
                            c1 = Color.LightGray;
                        }
                        break;
                    case BlockState.Entering:
                        if ((loc == null) || (loc.CurrentRoute.Actual == null))
                        {
                            c1 = c2 = Color.GreenYellow;
                        }
                        else if (loc.CurrentRoute.Actual.Route.ToBlockSide == BlockSide.Back)
                        {
                            c1 = Color.GreenYellow;
                            c2 = Color.LightGray;
                        }
                        else
                        {
                            c2 = Color.GreenYellow;
                            c1 = Color.LightGray;
                        }
                        break;
                    case BlockState.Locked:
                        c1 = c2 = Color.Cyan;
                        break;
                    case BlockState.Closed:
                        c1 = c2 = Color.Gray;
                        break;
                    default:
                        throw new ArgumentException("Unknown block state: " + blockState);
                }
                return Tuple.Create(c1, c2);
            }
        }

        /// <summary>
        /// Gets the color of the background.
        /// </summary>
        protected override Color TextColor
        {
            get
            {
                var blockState = state.State;
                switch (blockState)
                {
                    case BlockState.Occupied:
                    case BlockState.Entering:
                    case BlockState.OccupiedUnexpected:
                    case BlockState.Locked:
                    case BlockState.Destination:
                        return Color.Blue;
                    case BlockState.Free:
                    case BlockState.Closed:
                        return Color.Black;
                    default:
                        throw new ArgumentException("Unknown block state: " + blockState);
                }
            }
        }

        /// <summary>
        /// Text to draw in the block.
        /// </summary>
        protected override string Text
        {
            get
            {
                if (state.IsLocked()) return state.LockedBy.Description;
                var postfix = state.Closed.Actual ? Strings.BlockClosed : Strings.BlockFree;
                return base.Text + ": " + postfix;
            }
        }

        /// <summary>
        /// Select the loc in the oc list upon double click.
        /// </summary>
        private class SelectLocHandler : MouseHandler
        {
            private readonly BlockItem item;

            /// <summary>
            /// Default ctor
            /// </summary>
            public SelectLocHandler(BlockItem item, MouseHandler next)
                : base(next)
            {
                this.item = item;
            }

            /// <summary>
            /// Mouse is double clicked on this item
            /// </summary>
            /// <param name="e"></param>
            /// <returns>True if the event was handled, false otherwise.</returns>
            public override bool OnMouseDoubleClick(Controls.VirtualCanvas.VCItem sender, ItemMouseEventArgs e)
            {
                var blockState = item.State;
                var loc = blockState.LockedBy;
                if (loc != null)
                {
                    item.Context.SelectLoc(loc);
                    return true;
                }
                return base.OnMouseDoubleClick(sender, e);
            }
        }
    }
}

using System;
using System.Drawing;
using System.Windows.Forms;
using BinkyRailways.Core.Model;
using BinkyRailways.Core.State;
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
                var postfix = state.IsLocked() ? state.LockedBy.Description :
                    state.Closed.Actual ? Strings.BlockClosed : Strings.BlockFree;
                return base.Text + ": " + postfix;
            }
        }
    }
}

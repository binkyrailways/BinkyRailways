using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace BinkyRailways.WinApp.Controls.VirtualCanvas.Layout
{
    /// <summary>
    /// Simple layout manager that places items from left to right.
    /// </summary>
    public class LeftRightLayoutManager : IVCLayoutManager
    {
        private Padding padding = new Padding(10);
        private int itemSpacing = 10;
        private Size preferredSize;
        private Size size;
        private HorizontalAlignment hAlignment = HorizontalAlignment.Center;
        private VerticalAlignment vAlignment = VerticalAlignment.Middle;

        /// <summary>
        /// Default ctor
        /// </summary>
        public LeftRightLayoutManager()
        {
        }

        /// <summary>
        /// Default ctor with padding
        /// </summary>
        /// <param name="padding"></param>
        public LeftRightLayoutManager(Padding padding)
        {
            this.Padding = padding;
        }

        /// <summary>
        /// Default ctor with padding and spacing
        /// </summary>
        /// <param name="padding"></param>
        /// <param name="itemSpacing"></param>
        public LeftRightLayoutManager(Padding padding, int itemSpacing)
        {
            this.Padding = padding;
            this.ItemSpacing = itemSpacing;
        }

        /// <summary>
        /// Padding between container and all items
        /// </summary>
        public Padding Padding
        {
            get { return padding; }
            set { padding = value; }
        }

        /// <summary>
        /// Vertical spacing between items
        /// </summary>
        public int ItemSpacing
        {
            get { return itemSpacing; }
            set { itemSpacing = value; }
        }

        /// <summary>
        /// Gets / sets the horizontal alignment of items within their container.
        /// </summary>
        public HorizontalAlignment HorizontalAlignment
        {
            get { return hAlignment; }
            set { hAlignment = value; }
        }

        /// <summary>
        /// Gets / sets the vertical alignment of items within their container.
        /// </summary>
        public VerticalAlignment VerticalAlignment
        {
            get { return vAlignment; }
            set { vAlignment = value; }
        }

        /// <summary>
        /// Set the locations of the given placements.
        /// </summary>
        /// <param name="placements">All placements that must be layed out.</param>
        /// <param name="controlSize">The size of the control (in virtual units)</param>
        public virtual void Layout(ICollection<VCItemPlacement> placements, SizeF controlSize)
        {
            if (placements.Count > 0)
            {
                // Calculate preferred size
                int maxHeight = 0;
                int width = padding.Horizontal + (Math.Max(0, placements.Count - 1) * itemSpacing);
                foreach (VCItemPlacement placement in placements)
                {
                    Size itemSz = placement.Item.PreferredSize;
                    maxHeight = Math.Max(maxHeight, itemSz.Height + padding.Vertical);
                    width += itemSz.Width;
                }
                this.preferredSize = new Size(width, maxHeight);

                // Calculate horizontal displacement to deal with horizontal alignment
                int x = padding.Left;
                if (width < controlSize.Width)
                {
                    switch (vAlignment)
                    {
                        default:
                        case VerticalAlignment.Top:
                            break;
                        case VerticalAlignment.Middle:
                            x += (int)((controlSize.Width - width - padding.Horizontal) / 2);
                            break;
                        case VerticalAlignment.Bottom:
                            x += (int)(controlSize.Width - width - padding.Right);
                            break;
                    }
                }

                // Calculate final size
                this.size = new Size(Math.Max(width, (int)controlSize.Width), maxHeight);

                // Calculate item bounds
                foreach (VCItemPlacement placement in placements)
                {
                    Size itemSize = placement.Item.PreferredSize;
                    LayoutConstraints constraints = placement.Constraints;
                    if (constraints != null)
                    {
                        if ((constraints.Fill & FillDirection.Vertical) != 0)
                        {
                            itemSize.Height = maxHeight - padding.Vertical;
                        }
                    }

                    int y;
                    switch (vAlignment)
                    {
                        default:
                        case VerticalAlignment.Top:
                            y = padding.Top;
                            break;
                        case VerticalAlignment.Middle:
                            y = Math.Max(padding.Top, (int)((Math.Max(controlSize.Height, maxHeight) - itemSize.Height) / 2));
                            break;
                        case VerticalAlignment.Bottom:
                            y = Math.Max(padding.Top, (int)(Math.Max(controlSize.Height, maxHeight) - padding.Bottom));
                            break;
                    }
                    placement.Bounds = new Rectangle(new Point(x, y), itemSize);
                    x += itemSize.Width + itemSpacing;
                }
            }
            else
            {
                this.preferredSize = Size.Empty;
                this.size = Size.Empty;
            }
        }

        /// <summary>
        /// Gets size occupied by all placements.
        /// The preferred size does not include space occupied for alignment reasons.
        /// </summary>
        public Size PreferredSize { get { return preferredSize; } }

        /// <summary>
        /// Gets actual size occupied by the entire layout.
        /// The actual size also contain space occupied for alignment.
        /// </summary>
        public Size Size { get { return size; } }

        /// <summary>
        /// Calculate size available to clients, given the size of the container itself.
        /// Typically this will return sz minus any padding.
        /// </summary>
        public Size ClientSize(Size sz)
        {
            return new Size(sz.Width - padding.Horizontal, sz.Height - padding.Vertical);
        }
    }
}

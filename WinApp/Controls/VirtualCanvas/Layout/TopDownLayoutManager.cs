using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace BinkyRailways.WinApp.Controls.VirtualCanvas.Layout
{
    /// <summary>
    /// Simple layout manager that places items from top to bottom.
    /// </summary>
    public class TopDownLayoutManager : IVCLayoutManager
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
        public TopDownLayoutManager()
        {
        }

        /// <summary>
        /// Default ctor with padding
        /// </summary>
        /// <param name="padding"></param>
        public TopDownLayoutManager(Padding padding)
        {
            this.Padding = padding;
        }

        /// <summary>
        /// Default ctor with padding and spacing
        /// </summary>
        /// <param name="padding"></param>
        /// <param name="itemSpacing"></param>
        public TopDownLayoutManager(Padding padding, int itemSpacing) 
        {
            this.Padding = padding;
            this.ItemSpacing = itemSpacing;
        }

        /// <summary>
        /// Default ctor with alignment
        /// </summary>
        public TopDownLayoutManager(HorizontalAlignment hAlignment)
        {
            this.HorizontalAlignment = hAlignment;
        }

        /// <summary>
        /// Default ctor with padding and spacing
        /// </summary>
        /// <param name="padding"></param>
        /// <param name="itemSpacing"></param>
        public TopDownLayoutManager(HorizontalAlignment hAlignment, Padding padding, int itemSpacing)
        {
            this.HorizontalAlignment = hAlignment;
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
                int maxWidth = 0;
                int height = padding.Vertical + (Math.Max(0, placements.Count - 1) * itemSpacing);
                foreach (VCItemPlacement placement in placements)
                {
                    Size itemSz = placement.Item.PreferredSize;
                    maxWidth = Math.Max(maxWidth, itemSz.Width + padding.Horizontal);
                    height += itemSz.Height;
                }
                this.preferredSize = new Size(maxWidth, height);

                // Calculate vertical displacement to deal with vertical alignment
                int y = padding.Top;
                if (height < controlSize.Height)
                {
                    switch (vAlignment)
                    {
                        default:
                        case VerticalAlignment.Top:
                            break;
                        case VerticalAlignment.Middle:
                            y += (int)((controlSize.Height - height - padding.Vertical) / 2);
                            break;
                        case VerticalAlignment.Bottom:
                            y += (int)(controlSize.Height - height - padding.Bottom);
                            break;
                    }
                }

                // Calculate final size
                this.size = new Size(maxWidth, Math.Max(height, (int)controlSize.Height));

                // Calculate item bounds
                foreach (VCItemPlacement placement in placements)
                {
                    Size itemSize = placement.Item.PreferredSize;
                    LayoutConstraints constraints = placement.Constraints;
                    if (constraints != null)
                    {
                        if ((constraints.Fill & FillDirection.Horizontal) != 0)
                        {
                            itemSize.Width = maxWidth - padding.Horizontal;
                        }
                    }

                    int x;
                    switch (hAlignment)
                    {
                        default:
                        case HorizontalAlignment.Left:
                            x = padding.Left;
                            break;
                        case HorizontalAlignment.Center:
                            x = Math.Max(padding.Left, (int)((Math.Max(controlSize.Width, maxWidth) - itemSize.Width) / 2));
                            break;
                        case HorizontalAlignment.Right:
                            x = Math.Max(padding.Left, (int)(Math.Max(controlSize.Width, maxWidth) - padding.Right));
                            break;
                    }
                    placement.Bounds = new Rectangle(new Point(x, y), itemSize);
                    y += itemSize.Height + itemSpacing;
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

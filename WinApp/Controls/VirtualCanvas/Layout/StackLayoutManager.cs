using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace BinkyRailways.WinApp.Controls.VirtualCanvas.Layout
{
    /// <summary>
    /// Simple layout manager that places items on top of each other.
    /// </summary>
    public class StackLayoutManager : IVCLayoutManager
    {
        private Padding padding = new Padding(10);
        private Size preferredSize;
        private Size size;
        private HorizontalAlignment hAlignment = HorizontalAlignment.Center;
        private VerticalAlignment vAlignment = VerticalAlignment.Middle;

        /// <summary>
        /// Default ctor
        /// </summary>
        public StackLayoutManager()
        {
        }

        /// <summary>
        /// Default ctor
        /// </summary>
        /// <param name="padding"></param>
        public StackLayoutManager(Padding padding)
        {
            this.padding = padding;
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
                // Calculate maximum width and total height
                int maxWidth = 0;
                int maxHeight = 0;
                foreach (VCItemPlacement placement in placements)
                {
                    Size itemSz = placement.Item.PreferredSize;
                    maxWidth = Math.Max(maxWidth, itemSz.Width + padding.Horizontal);
                    maxHeight = Math.Max(maxHeight, itemSz.Height + padding.Vertical);
                }

                // Calculate final size
                this.preferredSize = this.size = new Size(maxWidth, maxHeight);

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
                        if ((constraints.Fill & FillDirection.Vertical) != 0)
                        {
                            itemSize.Height = maxHeight - padding.Vertical;
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

                    Rectangle bounds = new Rectangle(new Point(x, y), itemSize);
                    placement.Bounds = bounds;
                    size.Width = Math.Max(size.Width, bounds.Right);
                    size.Height = Math.Max(size.Height, bounds.Bottom);
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

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace BinkyRailways.WinApp.Controls.VirtualCanvas.Layout
{
    /// <summary>
    /// Layout manager that places items from left to right as space allows and then starts
    /// on a new row.
    /// </summary>
    public class FlowLayoutManager : IVCLayoutManager
    {
        private Padding padding = new Padding(10);
        private int horizontalItemSpacing = 10;
        private int verticalItemSpacing = 10;
        private Size preferredSize;
        private Size size;
        private HorizontalAlignment hAlignment = HorizontalAlignment.Center;
        private VerticalAlignment vAlignment = VerticalAlignment.Middle;

        /// <summary>
        /// Default ctor
        /// </summary>
        public FlowLayoutManager()
        {
        }

        /// <summary>
        /// Default ctor with padding
        /// </summary>
        /// <param name="padding"></param>
        public FlowLayoutManager(Padding padding)
        {
            this.Padding = padding;
        }

        /// <summary>
        /// Default ctor with padding and spacing
        /// </summary>
        /// <param name="padding"></param>
        /// <param name="itemSpacing"></param>
        public FlowLayoutManager(Padding padding, int horizontalItemSpacing, int verticalItemSpacing)
        {
            this.Padding = padding;
            this.horizontalItemSpacing = horizontalItemSpacing;
            this.verticalItemSpacing = verticalItemSpacing;
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
        /// Horizontal spacing between items
        /// </summary>
        public int HorizontalItemSpacing
        {
            get { return horizontalItemSpacing; }
            set { horizontalItemSpacing = value; }
        }

        /// <summary>
        /// Vertical spacing between items
        /// </summary>
        public int VerticalItemSpacing
        {
            get { return verticalItemSpacing; }
            set { verticalItemSpacing = value; }
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
                // Calculate preferred sizes
                Size[] sizes = new Size[placements.Count];
                Size maxSz = Size.Empty;
                int totalWidth = padding.Horizontal;
                int index = 0;
                foreach (VCItemPlacement placement in placements)
                {
                    Size sz = sizes[index++] = placement.Item.PreferredSize;
                    maxSz.Width = Math.Max(maxSz.Width, sz.Width);
                    maxSz.Height = Math.Max(maxSz.Height, sz.Height);
                    totalWidth += horizontalItemSpacing + sz.Width;
                }

                // Calculate number of columns
                int cols = (int)Math.Max(1, (controlSize.Width - padding.Horizontal) / (maxSz.Width + horizontalItemSpacing));

                // Calculate item bounds
                int y = padding.Top;
                int nextY = y - verticalItemSpacing;
                index = 0;
                foreach (VCItemPlacement placement in placements)
                {
                    int col = index % cols;
                    int row = index / cols;

                    if (col == 0) { y = nextY + verticalItemSpacing; }

                    int x = padding.Left + (col * (maxSz.Width + horizontalItemSpacing));
                    placement.Bounds = new Rectangle(new Point(x, y), new Size(maxSz.Width, sizes[index].Height));
                    nextY = Math.Max(nextY, y + sizes[index].Height);

                    index++;
                }

                this.size = new Size((cols * maxSz.Width) + padding.Horizontal + (Math.Max(0, cols - 1) * horizontalItemSpacing), nextY + padding.Bottom);
                this.preferredSize = this.size;
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

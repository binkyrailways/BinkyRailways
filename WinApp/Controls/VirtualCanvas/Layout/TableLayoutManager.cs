using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using BinkyRailways.WinApp.Utils;

namespace BinkyRailways.WinApp.Controls.VirtualCanvas.Layout
{
    /// <summary>
    /// Layout manager that places items in cells in a table.
    /// </summary>
    public class TableLayoutManager : IVCLayoutManager
    {
        private Padding padding = new Padding(10);
        private Size preferredSize;
        private Size size;
        private HorizontalAlignment hAlignment = HorizontalAlignment.Center;
        private VerticalAlignment vAlignment = VerticalAlignment.Middle;
        private readonly float[] colFills;
        private readonly float[] rowsFills;

        /// <summary>
        /// Default ctor
        /// </summary>
        public TableLayoutManager(float[] colFills, float[] rowFills)
            : this(colFills, rowFills, new Padding(10))
        {
        }

        /// <summary>
        /// Default ctor
        /// </summary>
        /// <param name="padding"></param>
        public TableLayoutManager(float[] colFills, float[] rowFills, Padding padding)
        {
            this.colFills = NormalizeFills(colFills);
            this.rowsFills = NormalizeFills(rowFills);
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
                int[] maxColWidth = new int[colFills.Length];
                int[] maxRowHeight = new int[rowsFills.Length];

                TableLayoutConstraints lastConstraints = null;
                foreach (VCItemPlacement placement in placements)
                {
                    TableLayoutConstraints constraints = GetConstrains(placement, lastConstraints);
                    lastConstraints = constraints;

                    Size itemSz = placement.Item.PreferredSize + constraints.CellPadding.Size;
                    DistributeSize(itemSz.Width, constraints.ColumnIndex, constraints.ColumnSpan, colFills, maxColWidth);
                    DistributeSize(itemSz.Height, constraints.RowIndex, constraints.RowSpan, rowsFills, maxRowHeight);
                }

                // Calculate preferred size
                this.preferredSize = this.size = new Size(MathUtil.Sum(maxColWidth) + padding.Horizontal, MathUtil.Sum(maxRowHeight) + padding.Vertical);

                // Calculate full size (expand applicable columns and rows)
                if (size.Width < controlSize.Width)
                {
                    float extra = controlSize.Width - size.Width;
                    for (int i = 0; i < maxColWidth.Length; i++)
                    {
                        maxColWidth[i] += (int)(colFills[i] * extra);
                    }
                }
                if (size.Height < controlSize.Height)
                {
                    float extra = controlSize.Height - size.Height;
                    for (int i = 0; i < maxRowHeight.Length; i++)
                    {
                        maxRowHeight[i] += (int)(rowsFills[i] * extra);
                    }
                }
                this.size = new Size(MathUtil.Sum(maxColWidth) + padding.Horizontal, MathUtil.Sum(maxRowHeight) + padding.Vertical);

                // Calculate distance for dealing with table alignment (not cell alignment!)
                int dx = padding.Left;
                switch (hAlignment)
                {
                    case HorizontalAlignment.Center:
                        dx += (controlSize.Width > size.Width) ? (int)((controlSize.Width - size.Width) / 2) : 0;
                        break;
                    case HorizontalAlignment.Right:
                        dx += (controlSize.Width > size.Width) ? (int)(controlSize.Width - size.Width) : 0;
                        break;
                }

                int dy = padding.Top;
                switch (vAlignment)
                {
                    case VerticalAlignment.Middle:
                        dy += (controlSize.Height > size.Height) ? (int)((controlSize.Height - size.Height) / 2) : 0;
                        break;
                    case VerticalAlignment.Bottom:
                        dy += (controlSize.Height > size.Height) ? (int)(controlSize.Height - size.Height) : 0;
                        break;
                }


                // Calculate item bounds
                lastConstraints = null;
                foreach (VCItemPlacement placement in placements)
                {
                    TableLayoutConstraints constraints = GetConstrains(placement, lastConstraints);
                    lastConstraints = constraints;
                    Padding cellPadding = constraints.CellPadding;
                    Size maxItemSize = new Size(
                        MathUtil.Sum(maxColWidth, constraints.ColumnIndex, constraints.ColumnSpan) - cellPadding.Horizontal,
                        MathUtil.Sum(maxRowHeight, constraints.RowIndex, constraints.RowSpan) - cellPadding.Vertical);

                    Size itemSize = placement.Item.PreferredSize;
                    if ((constraints.Fill & FillDirection.Horizontal) != 0)
                    {
                        itemSize.Width = maxItemSize.Width;
                    }
                    if ((constraints.Fill & FillDirection.Vertical) != 0)
                    {
                        itemSize.Height = maxItemSize.Height;
                    }

                    int x = dx + MathUtil.Sum(maxColWidth, 0, constraints.ColumnIndex) + cellPadding.Left;
                    int y = dy + MathUtil.Sum(maxRowHeight, 0, constraints.RowIndex) + cellPadding.Top;
                    switch (constraints.ItemAlignment)
                    {
                        case ContentAlignment.TopCenter:
                        case ContentAlignment.MiddleCenter:
                        case ContentAlignment.BottomCenter:
                            x += (maxItemSize.Width - itemSize.Width) / 2;
                            break;
                        case ContentAlignment.TopRight:
                        case ContentAlignment.MiddleRight:
                        case ContentAlignment.BottomRight:
                            x += maxItemSize.Width - itemSize.Width;
                            break;
                    }
                    switch (constraints.ItemAlignment)
                    {
                        case ContentAlignment.MiddleLeft:
                        case ContentAlignment.MiddleCenter:
                        case ContentAlignment.MiddleRight:
                            y += (maxItemSize.Height - itemSize.Height) / 2;
                            break;
                        case ContentAlignment.BottomLeft:
                        case ContentAlignment.BottomCenter:
                        case ContentAlignment.BottomRight:
                            y += maxItemSize.Height - itemSize.Height;
                            break;
                    }

                    Rectangle bounds = new Rectangle(new Point(x, y), itemSize);
                    placement.Bounds = bounds;
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

        /// <summary>
        /// Normalize all given fill values, such that they all are between 0.0 and 1.0 and their
        /// sum is equal or less then 1.0.
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        private static float[] NormalizeFills(float[] values)
        {
            float total = 0;
            int count = values.Length;
            float[] result = new float[count];

            for (int i = 0; i < count; i++)
            {
                result[i] = Math.Max(0.0f, values[i]);
                total += result[i];
            }

            if (total > 1.0f)
            {
                // Limit all values to a range of 0.0f - 1.0f
                for (int i = 0; i < count; i++)
                {
                    result[i] /= total;
                }
            }

            return result;
        }

        /// <summary>
        /// Gets the constraints from the given placement or an automatic constraints in case
        /// the placement has not table constraints.
        /// </summary>
        /// <param name="placement"></param>
        /// <param name="lastConstraints"></param>
        /// <returns></returns>
        private TableLayoutConstraints GetConstrains(VCItemPlacement placement, TableLayoutConstraints lastConstraints)
        {
            TableLayoutConstraints constraints = placement.Constraints as TableLayoutConstraints;
            if (constraints != null) { return constraints; }

            // Auto generate constraints
            if (lastConstraints == null)
            {
                return new TableLayoutConstraints(0, 0);
            }
            else
            {
                int col = lastConstraints.ColumnIndex + lastConstraints.ColumnSpan;

                // Next col ok?
                if (col < colFills.Length) { return new TableLayoutConstraints(col, lastConstraints.RowIndex); }

                // Next row ok?
                int row = lastConstraints.RowIndex + lastConstraints.RowSpan;
                if (row < rowsFills.Length) { return new TableLayoutConstraints(0, row); }

                // Overflow, restart
                return new TableLayoutConstraints(0, 0);
            }
        }

        /// <summary>
        /// Distribute the required size for an item across its cells (in either column, row direction)
        /// according to fill instructions.
        /// </summary>
        /// <param name="itemSz"></param>
        /// <param name="index"></param>
        /// <param name="span"></param>
        /// <param name="fills"></param>
        /// <param name="sizes"></param>
        private static void DistributeSize(int itemSz, int index, int span, float[] fills, int[] sizes)
        {
            if (span == 1)
            {
                sizes[index] = Math.Max(sizes[index], itemSz);
            }
            else
            {
                float sumFills = MathUtil.Sum(fills, index, span);
                if (sumFills > 0)
                {
                    // Distribute according to fill
                    float size = (float)itemSz / sumFills;
                    for (int i = 0; i < span; i++)
                    {
                        sizes[index + i] = (int)Math.Max(sizes[index + i], size * fills[index + i]);
                    }
                }
                else
                {
                    // Distribute evenly
                    for (int i = 0; i < span; i++)
                    {
                        sizes[index + i] = Math.Max(sizes[index + i], itemSz / span);
                    }
                }
            }
        }
    }
}

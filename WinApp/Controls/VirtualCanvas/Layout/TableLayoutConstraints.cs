using System.Drawing;
using System.Windows.Forms;

namespace BinkyRailways.WinApp.Controls.VirtualCanvas.Layout
{
    public class TableLayoutConstraints : LayoutConstraints
    {
        private readonly int col;
        private readonly int row;
        private readonly int colSpan;
        private readonly int rowSpan;
        private readonly Padding cellPadding;
        private readonly ContentAlignment itemAlignment;

        /// <summary>
        /// Default ctor
        /// </summary>
        /// <param name="col">Column index</param>
        /// <param name="row">Row index</param>
        public TableLayoutConstraints(int col, int row)
            : this(col, row, 1, 1, ContentAlignment.MiddleCenter, FillDirection.Both, Padding.Empty)
        {
        }

        /// <summary>
        /// Default ctor
        /// </summary>
        /// <param name="col">Column index</param>
        /// <param name="row">Row index</param>
        /// <param name="fill">Fill of item within cell</param>
        public TableLayoutConstraints(int col, int row, FillDirection fill)
            : this(col, row, 1, 1, ContentAlignment.MiddleCenter, fill, Padding.Empty)
        {
        }

        /// <summary>
        /// Default ctor
        /// </summary>
        /// <param name="col">Column index</param>
        /// <param name="row">Row index</param>
        /// <param name="itemAlignment">Alignment of item within the cell</param>
        /// <param name="fill">Fill of item within cell</param>
        public TableLayoutConstraints(int col, int row, ContentAlignment itemAlignment, FillDirection fill)
            : this(col, row, 1, 1, itemAlignment, fill, Padding.Empty)
        {
        }
        /// <summary>
        /// Default ctor
        /// </summary>
        /// <param name="col">Column index</param>
        /// <param name="row">Row index</param>
        /// <param name="colSpan">Column span</param>
        /// <param name="rowSpan">Row span</param>
        public TableLayoutConstraints(int col, int row, int colSpan, int rowSpan)
            : this(col, row, colSpan, rowSpan, ContentAlignment.MiddleCenter, FillDirection.Both, Padding.Empty)
        {
        }

        /// <summary>
        /// Default ctor
        /// </summary>
        /// <param name="col">Column index</param>
        /// <param name="row">Row index</param>
        /// <param name="colSpan">Column span</param>
        /// <param name="rowSpan">Row span</param>
        /// <param name="itemAlignment">Alignment of item within the cell</param>
        /// <param name="fill">Fill of item within cell</param>
        /// <param name="cellPadding">Padding between cell and item</param>
        public TableLayoutConstraints(int col, int row, int colSpan, int rowSpan, ContentAlignment itemAlignment, FillDirection fill, Padding cellPadding)
            : base(fill)
        {
            this.col = col;
            this.row = row;
            this.colSpan = colSpan;
            this.rowSpan = rowSpan;
            this.itemAlignment = itemAlignment;
            this.cellPadding = cellPadding;
        }

        /// <summary>
        /// Gets the requested column index.
        /// </summary>
        public int ColumnIndex { get { return col; } }

        /// <summary>
        /// Gets the requested column span.
        /// </summary>
        public int ColumnSpan { get { return colSpan; } }

        /// <summary>
        /// Gets the request row index.
        /// </summary>
        public int RowIndex { get { return row; } }

        /// <summary>
        /// Gets the requested row span.
        /// </summary>
        public int RowSpan { get { return rowSpan; } }

        /// <summary>
        /// Gets the requested cell padding.
        /// </summary>
        public Padding CellPadding { get { return cellPadding; } }

        /// <summary>
        /// Gets the requested alignment of the item within the cell
        /// </summary>
        public ContentAlignment ItemAlignment { get { return itemAlignment; } }
    }
}

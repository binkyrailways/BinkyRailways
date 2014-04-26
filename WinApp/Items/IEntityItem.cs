using System.Drawing;
using System.Windows.Forms;
using BinkyRailways.Core.Model;

namespace BinkyRailways.WinApp.Items
{
    /// <summary>
    /// Item showing a block
    /// </summary>
    public interface IEntityItem
    {
        /// <summary>
        /// Gets the represented entity
        /// </summary>
        IEntity Entity { get; }

        /// <summary>
        /// Start showing the tooltip of this item
        /// </summary>
        void ShowToolTip();

        /// <summary>
        /// Show the context menu for this item
        /// </summary>
        void ShowContextMenu(PointF pt);

        /// <summary>
        /// Fill the entries of a context menu
        /// </summary>
        void BuildContextMenu(ContextMenuStrip menu);
    }
}

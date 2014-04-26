using System.Collections.Generic;
using System.Drawing;

namespace BinkyRailways.WinApp.Controls.VirtualCanvas.Layout
{
    /// <summary>
    /// Layout manager for virtual canvas item placements
    /// </summary>
    public interface IVCLayoutManager
    {
        /// <summary>
        /// Set the locations of the given placements.
        /// </summary>
        /// <param name="placements">All placements that must be layed out.</param>
        /// <param name="controlSize">The size of the control (in virtual units)</param>
        void Layout(ICollection<VCItemPlacement> placements, SizeF controlSize);

        /// <summary>
        /// Gets size occupied by all placements.
        /// The preferred size does not include space occupied for alignment reasons.
        /// </summary>
        Size PreferredSize { get; }

        /// <summary>
        /// Gets actual size occupied by the entire layout.
        /// The actual size also contain space occupied for alignment.
        /// </summary>
        Size Size { get; }

        /// <summary>
        /// Calculate size available to clients, given the size of the container itself.
        /// Typically this will return sz minus any padding.
        /// </summary>
        Size ClientSize(Size sz);
    }
}

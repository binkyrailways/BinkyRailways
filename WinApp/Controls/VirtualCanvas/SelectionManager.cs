using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using BinkyRailways.WinApp.Utils;

namespace BinkyRailways.WinApp.Controls.VirtualCanvas
{
    /// <summary>
    /// Mouse handler that implements selection of items in the given container.
    /// </summary>
    public class SelectionManager 
    {
        private readonly IVCItemContainer container;
        private readonly Predicate<VCItemPlacement> canSelect;
        private List<VCItemPlacement> startSelection;
        private bool selecting = false;
        private PointF selStart;
        private PointF selEnd;

        /// <summary>
        /// Default ctor
        /// </summary>
        public SelectionManager(IVCItemContainer container, Predicate<VCItemPlacement> canSelect)
        {
            this.container = container;
            this.canSelect = canSelect;
            container.SelectedItems.Changed += (s, e) => container.Invalidate();
        }

        /// <summary>
        /// Start the selection at the given point.
        /// </summary>
        public void Start(PointF pt, bool clearSelection)
        {
            selStart = pt;
            selEnd = pt;
            selecting = true;

            if (clearSelection)
            {
                // Clear selection
                container.SelectedItems.Clear();
            }

            // Record current selection
            startSelection = new List<VCItemPlacement>(container.SelectedItems);

            // Select appropriate items
            UpdateSelection(SelectionBounds);
            container.Invalidate();
        }

        /// <summary>
        /// End of selection
        /// </summary>
        public void Stop()
        {
            if (!selecting) return;

            selecting = false;
            container.Invalidate();
        }

        /// <summary>
        /// Extend the selection from the starting point to the given point.
        /// </summary>
        public void Extend(PointF pt)
        {
            if (!selecting) return;

            // Record position
            selEnd = pt;

            // Select appropriate items
            UpdateSelection(SelectionBounds);

            // Redraw selection box
            container.Invalidate();
        }

        /// <summary>
        /// Draw selection
        /// </summary>
        public void Paint(VCItem sender, ItemPaintEventArgs e)
        {
            if (selecting)
            {
                var rect = SelectionBounds;
                if (!rect.IsEmpty)
                {
                    using (var pen = new Pen(Color.Black))
                    {
                        pen.DashStyle = DashStyle.Dot;
                        e.Graphics.DrawRectangle(pen, SelectionBounds.Round());
                    }
                }
            }

            // Draw selection boxes (if any)
            if (container.SelectedItems.Count > 0)
            {
                container.DrawItems(e, DrawSelectionBoxes);
            }
        }

        /// <summary>
        /// Draw selection handles on the given placement
        /// </summary>
        /// <param name="e"></param>
        /// <param name="placement"></param>
        private void DrawSelectionBoxes(ItemPaintEventArgs e, VCItemPlacement placement)
        {
            if (!container.SelectedItems.Contains(placement)) return;

            var g = e.Graphics;
            using (var pen = new Pen(Color.Red, 2.0f / e.ZoomFactor))
            {
                g.DrawRectangle(pen, new Rectangle(Point.Empty, placement.Item.Size));
            }
        }

        /// <summary>
        /// Is a selection being made?
        /// </summary>
        public bool IsSelecting
        {
            get { return selecting; }
        }

        /// <summary>
        /// Calculate the selection rectangle
        /// </summary>
        public RectangleF SelectionBounds
        {
            get
            {
                var x = Math.Min(selStart.X, selEnd.X);
                var y = Math.Min(selStart.Y, selEnd.Y);
                var width = Math.Abs(selStart.X - selEnd.X);
                var height = Math.Abs(selStart.Y - selEnd.Y);

                return new RectangleF(x, y, width, height);
            }
        }

        /// <summary>
        /// Update the selection with respect to all items within the given bounds.
        /// </summary>
        private void UpdateSelection(RectangleF bounds)
        {
            Keys modifiers = Control.ModifierKeys;
            bool invert = (modifiers == Keys.Control);
            bool add = (modifiers == Keys.None) || (modifiers == Keys.Shift);

            if (invert || add)
            {
                // First build a list of items that should be in the new selection.
                var newSelection = new List<VCItemPlacement>(startSelection);

                foreach (var placement in container.Items)
                {
                    if (canSelect(placement) && placement.IntersectsWith(bounds))
                    {
                        if (invert)
                        {
                            // Invert selection
                            if (newSelection.Contains(placement))
                            {
                                newSelection.Remove(placement);
                            }
                            else
                            {
                                newSelection.Add(placement);
                            }
                        }
                        else
                        {
                            newSelection.Add(placement);
                        }
                    }
                }

                // Set new selection in container
                var selection = container.SelectedItems;
                selection.AddAll(newSelection);
                if (selection.Count != newSelection.Count)
                {
                    // Remove all placements not in the new selection
                    for (var i = 0; i < selection.Count; )
                    {
                        if (!newSelection.Contains(selection[i]))
                        {
                            selection.Remove(selection[i]);
                        }
                        else
                        {
                            i++;
                        }
                    }
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using BinkyRailways.Core.Model;
using BinkyRailways.WinApp.Controls.VirtualCanvas;
using BinkyRailways.WinApp.Controls.VirtualCanvas.Layout;

namespace BinkyRailways.WinApp.Items
{
    /// <summary>
    /// Simple layout manager that places items from left to right.
    /// </summary>
    public class PositionedEntityLayoutManager : IVCLayoutManager
    {
        private Size preferredSize;
        private Size size;

        /// <summary>
        /// Set the locations of the given placements.
        /// </summary>
        /// <param name="placements">All placements that must be layed out.</param>
        /// <param name="controlSize">The size of the control (in virtual units)</param>
        public virtual void Layout(ICollection<VCItemPlacement> placements, SizeF controlSize)
        {
            if (placements.Count == 0)
            {
                preferredSize = Size.Empty;
                size = Size.Empty;
                return;
            }

            // Calculate preferred size
            var minLeft = int.MaxValue;
            var maxRight = int.MinValue;
            var minTop = int.MaxValue;
            var maxBottom = int.MinValue;
            foreach (var placement in placements)
            {
                var itemSz = placement.Item.PreferredSize;
                placement.Item.Size = itemSz;
                var posEntityItem = placement.Item as IPositionedEntityItem;
                var tx = (posEntityItem != null) ? GetTransform(posEntityItem) : new Matrix();
                placement.Transform = tx;

                var bounds = placement.Bounds;
                minLeft = Math.Min(minLeft, bounds.Left);
                maxRight = Math.Max(maxRight, bounds.Right);
                minTop = Math.Min(minTop, bounds.Top);
                maxBottom = Math.Max(maxBottom, bounds.Bottom);
            }
            preferredSize = new Size(Math.Max(0, maxRight - minLeft), Math.Max(0, maxBottom - minTop));
            size = preferredSize;

            // Adjust for non-zero minLeft or minTop
            var dx = -minLeft;
            var dy = -minTop;
            foreach (var placement in placements)
            {
                placement.Transform.Translate(dx, dy, MatrixOrder.Append);
            }
        }

        /// <summary>
        /// Gets a transform for the given entity.
        /// </summary>
        protected virtual Matrix GetTransform(IPositionedEntityItem item)
        {
            var tx = new Matrix();
            var entity = item.Entity;
            var moduleRef = entity as IModuleRef;
            if ((moduleRef != null) && (moduleRef.ZoomFactor != 100))
            {
                var scale = moduleRef.ZoomFactor / 100.0f;
                tx.Scale(scale, scale);
            }

            if (entity.Rotation != 0)
            {
                var pt = new PointF(entity.Width / 2.0f, entity.Height / 2.0f);
                tx.RotateAt(entity.Rotation%360, pt);
            }
            tx.Translate(entity.X, entity.Y, MatrixOrder.Append);
            return tx;
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
            return sz;
        }
    }
}

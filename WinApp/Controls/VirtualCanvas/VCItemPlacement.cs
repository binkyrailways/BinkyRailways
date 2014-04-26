using System.Drawing;
using System.Drawing.Drawing2D;
using BinkyRailways.WinApp.Utils;

namespace BinkyRailways.WinApp.Controls.VirtualCanvas
{
    /// <summary>
    /// Element on a virtual canvas that places an item on a specific location.
    /// </summary>
    public sealed class VCItemPlacement
    {
        internal static readonly Matrix Identity = new Matrix();

        private readonly VCItem item;
        private readonly Layout.LayoutConstraints constraints;
        private Matrix transform;
        private Matrix inverseTransform;

        /// <summary>
        /// Default ctor
        /// </summary>
        /// <param name="item"></param>
        internal VCItemPlacement(VCItem item, Layout.LayoutConstraints constraints)
        {
            this.item = item;
            this.constraints = constraints;
            transform = Identity;
        }

        /// <summary>
        /// Gets the item contained in this placement
        /// </summary>
        public VCItem Item
        {
            get { return item; }
        }

        /// <summary>
        /// Gets constraints used to layout this placement.
        /// </summary>
        public Layout.LayoutConstraints Constraints
        {
            get { return constraints; }
        }

        /// <summary>
        /// Gets / Sets a matrix used to transform the items coordinate space into the container coordinate space.
        /// E.g. converts 0, 0 from item space to x, y in container space.
        /// </summary>
        public Matrix Transform
        {
            get { return transform; }
            set
            {
                value = value ?? Identity;
                if (!transform.Equals(value))
                {
                    transform = value;
                    inverseTransform = null;
                    var posItem = item as IPositionableItem;
                    if (posItem != null)
                    {
                        posItem.TransformChanged();
                    }
                }
            }
        }
       
        /// <summary>
        /// Gets a matrix used to transform the containers coordinate space into the items coordinate space.
        /// </summary>
        private Matrix InverseTransform
        {
            get
            {
                if (inverseTransform == null)
                {
                    var tx = transform.Clone();
                    tx.Invert();
                    inverseTransform = tx;
                }
                return inverseTransform;
            }
        }

        /// <summary>
        /// Gets the entire bounds of this placement in container space.
        /// </summary>
        public Rectangle Bounds
        {
            get { return FromLocal(new Rectangle(Point.Empty, item.Size)); }
            set
            {
                item.Size = value.Size;
                var tx = new Matrix();
                tx.Translate(value.X, value.Y);
                Transform = tx;
            }
        }

        /// <summary>
        /// Gets the top-left coordinate of this placement in container space.
        /// </summary>
        public Point Location
        {
            get { return FromLocal(Point.Empty); }
        }

        /// <summary>
        /// Does the bounds of this placement intersect with the given rectangle in container space?
        /// </summary>
        public bool IntersectsWith(RectangleF rect)
        {
            var rgn = item.GetRegion();
            rgn.Transform(Transform);
            return rgn.IsVisible(rect);
        }

        /// <summary>
        /// Does the bounds of this placement contain the given point?
        /// </summary>
        public bool Contains(PointF pt)
        {
            var rgn = item.GetRegion();
            rgn.Transform(Transform);
            return rgn.IsVisible(pt);
        }

        /// <summary>
        /// Convert the given point from virtual space to item space.
        /// </summary>
        public Point ToLocal(Point pt)
        {
            var points = new[] { pt };
            InverseTransform.TransformPoints(points);
            return points[0];
        }

        /// <summary>
        /// Convert the given point from virtual space to item space.
        /// </summary>
        public PointF ToLocal(PointF pt)
        {
            var points = new[] { pt };
            InverseTransform.TransformPoints(points);
            return points[0];
        }

        /// <summary>
        /// Convert the given rectangle from virtual space to item space.
        /// </summary>
        public Rectangle ToLocal(Rectangle rect)
        {
            return InverseTransform.Transform(rect);
        }

        /// <summary>
        /// Convert the given rectangle from virtual space to item space.
        /// </summary>
        public RectangleF ToLocal(RectangleF rect)
        {
            return InverseTransform.Transform(rect);
        }

        /// <summary>
        /// Convert the given point from item space to virtual space.
        /// </summary>
        public Point FromLocal(Point pt)
        {
            var points = new[] { pt };
            Transform.TransformPoints(points);
            return points[0];
        }

        /// <summary>
        /// Convert the given point from item space to virtual space.
        /// </summary>
        public PointF FromLocal(PointF pt)
        {
            var points = new[] {pt};
            Transform.TransformPoints(points);
            return points[0];
        }

        /// <summary>
        /// Convert the given rectangle from item space to virtual space.
        /// </summary>
        public Rectangle FromLocal(Rectangle rect)
        {
            return Transform.Transform(rect);
        }

        /// <summary>
        /// Convert the given rectangle from item space to virtual space.
        /// </summary>
        public RectangleF FromLocal(RectangleF rect)
        {
            return Transform.Transform(rect);
        }
    }
}

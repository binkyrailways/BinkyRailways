using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;

namespace BinkyRailways.WinApp.Utils
{
    internal static class MatrixUtils
    {
        /// <summary>
        /// Transform the given point using the given matrix.
        /// </summary>
        internal static Point Transform(this Matrix tx, Point pt)
        {
            if (tx.IsIdentity)
                return pt;
            var pts = new[] { pt };
            tx.TransformPoints(pts);
            return pts[0];
        }

        /// <summary>
        /// Transform the given point using the given matrix.
        /// </summary>
        internal static PointF Transform(this Matrix tx, PointF pt)
        {
            if (tx.IsIdentity)
                return pt;
            var pts = new[] { pt };
            tx.TransformPoints(pts);
            return pts[0];
        }

        /// <summary>
        /// Transform the given rectangle using the given matrix.
        /// </summary>
        public static Rectangle Transform(this Matrix tx, Rectangle rect)
        {
            if (tx.IsIdentity)
                return rect;
            var pt = rect.Location;
            var sz = rect.Size;
            var pts = new[]
                              {
                                  new Point(pt.X, pt.Y),
                                  new Point(pt.X + sz.Width, pt.Y),
                                  new Point(pt.X, pt.Y + sz.Height),
                                  new Point(pt.X + sz.Width, pt.Y + sz.Height)
                              };
            tx.TransformPoints(pts);
            var left = pts.Min(x => x.X);
            var right = pts.Max(x => x.X);
            var top = pts.Min(x => x.Y);
            var bottom = pts.Max(x => x.Y);

            return new Rectangle(left, top, right - left, bottom - top);
        }

        /// <summary>
        /// Transform the given rectangle using the given matrix.
        /// </summary>
        internal static RectangleF Transform(this Matrix tx, RectangleF rect)
        {
            if (tx.IsIdentity)
                return rect;
            var pt = rect.Location;
            var sz = rect.Size;
            var pts = new[]
                              {
                                  new PointF(pt.X, pt.Y),
                                  new PointF(pt.X + sz.Width, pt.Y),
                                  new PointF(pt.X, pt.Y + sz.Height),
                                  new PointF(pt.X + sz.Width, pt.Y + sz.Height)
                              };
            tx.TransformPoints(pts);
            var left = pts.Min(x => x.X);
            var right = pts.Max(x => x.X);
            var top = pts.Min(x => x.Y);
            var bottom = pts.Max(x => x.Y);

            return new RectangleF(left, top, right - left, bottom - top);
        }
    }
}

using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;

namespace BinkyRailways.WinApp.Utils
{
    internal static class GraphicsUtil
    {
        /// <summary>
        /// Create the segments needed for a rounded rectangle.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="cornerDiameter"></param>
        internal static void AddRoundedRectangle(GraphicsPath path, int width, int height, int cornerDiameter)
        {
            int diameter = cornerDiameter * 2;
            int right = width - diameter;
            int bottom = height - diameter;

            // top left arc 
            path.AddArc(0, 0, diameter, diameter, 180, 90);

            // top right arc 
            path.AddArc(right, 0, diameter, diameter, 270, 90);

            // bottom right arc 
            path.AddArc(right, bottom, diameter, diameter, 0, 90);

            // bottom left arc
            path.AddArc(0, bottom, diameter, diameter, 90, 90);

            path.CloseAllFigures();
        }

        /// <summary>
        /// Create a path with a rounded rectangle
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="roundSize"></param>
        /// <returns></returns>
        internal static GraphicsPath CreateRoundedRectangle(int width, int height, int roundSize)
        {
            GraphicsPath path = new GraphicsPath();
            AddRoundedRectangle(path, width, height, roundSize);
            return path;
        }

        /// <summary>
        /// Create a path with a rounded rectangle
        /// </summary>
        internal static GraphicsPath CreateRoundedRectangle(Size size, int roundSize)
        {
            GraphicsPath path = new GraphicsPath();
            AddRoundedRectangle(path, size.Width, size.Height, roundSize);
            return path;
        }

        /// <summary>
        /// Create a path with a circle
        /// </summary>
        internal static GraphicsPath CreateCircle(int radius)
        {
            GraphicsPath path = new GraphicsPath();
            path.AddArc(0, 0, radius, radius, 0, 360);
            return path;
        }

        /// <summary>
        /// Create a path with a square
        /// </summary>
        internal static GraphicsPath CreateSquare(Size sz)
        {
            var path = new GraphicsPath();
            var side = Math.Min(sz.Width, sz.Height);
            path.AddRectangle(new RectangleF((sz.Width - side) / 2.0f, (sz.Height - side) / 2.0f, side, side));
            return path;
        }

        /// <summary>
        /// Create a path with a diamond
        /// </summary>
        internal static GraphicsPath CreateDiamond(Size sz)
        {
            var path = new GraphicsPath();
            var hw = sz.Width / 2;
            var hh = sz.Height / 2;

            path.AddLines(new[] {
                new Point(hw, 0), 
                new Point(sz.Width, hh), 
                new Point(hw, sz.Height), 
                new Point(0, hh)
            });
            path.CloseFigure();

            return path;
        }

        /// <summary>
        /// Create a path with a triangle
        /// </summary>
        internal static GraphicsPath CreateTriangle(Size sz)
        {
            var path = new GraphicsPath();

            path.AddLines(new[] {
                new Point(sz.Width/2, 0), 
                new Point(sz.Width, sz.Height), 
                new Point(0, sz.Height)
            });
            path.CloseFigure();

            return path;
        }

        /// <summary>
        /// Calculate the rectangle needed to draw the given text with the given font.
        /// </summary>
        internal static RectangleF GetTextBounds(this Graphics g, Font font,
                                                 string text, PointF origin, ContentAlignment originAlignment)
        {
            var sz = g.MeasureString(text, font);
            switch (originAlignment)
            {
                default:
                    return new RectangleF(origin, sz);
                case ContentAlignment.TopCenter:
                    return new RectangleF(origin.X - sz.Width / 2, origin.Y, sz.Width, sz.Height);
                case ContentAlignment.TopRight:
                    return new RectangleF(origin.X - sz.Width, origin.Y, sz.Width, sz.Height);
                case ContentAlignment.MiddleLeft:
                    return new RectangleF(origin.X, origin.Y - sz.Height / 2, sz.Width, sz.Height);
                case ContentAlignment.MiddleCenter:
                    return new RectangleF(origin.X - sz.Width / 2, origin.Y - sz.Height / 2, sz.Width, sz.Height);
                case ContentAlignment.MiddleRight:
                    return new RectangleF(origin.X - sz.Width, origin.Y - sz.Height / 2, sz.Width, sz.Height);
                case ContentAlignment.BottomLeft:
                    return new RectangleF(origin.X, origin.Y - sz.Height, sz.Width, sz.Height);
                case ContentAlignment.BottomCenter:
                    return new RectangleF(origin.X - sz.Width / 2, origin.Y - sz.Height, sz.Width, sz.Height);
                case ContentAlignment.BottomRight:
                    return new RectangleF(origin.X - sz.Width, origin.Y - sz.Height, sz.Width, sz.Height);
            }
        }

        /// <summary>
        /// Round the given rectangle to an int based rectangle.
        /// </summary>
        internal static Point Round(this PointF pt)
        {
            return new Point((int)Math.Round(pt.X), (int)Math.Round(pt.Y));
        }

        /// <summary>
        /// Round the given rectangle to an int based rectangle.
        /// </summary>
        internal static Rectangle Round(this RectangleF rect)
        {
            return new Rectangle((int)Math.Round(rect.X), (int)Math.Round(rect.Top), (int)Math.Round(rect.Width),
                                 (int)Math.Round(rect.Height));
        }

        public static void SaveAsEmf(this Metafile me, Stream destination)
        {
            /* http://social.msdn.microsoft.com/Forums/en-US/csharpgeneral/thread/12a1c749-b320-4ce9-aff7-9de0d7fd30ea 
                How to save or serialize a Metafile: Solution found 
                by : SWAT Team member _1 
                Date : Friday, February 01, 2008 1:38 PM 
             */
            var enfMetafileHandle = me.GetHenhmetafile();
            var bufferSize = GetEnhMetaFileBits(enfMetafileHandle, 0, null); // Get required buffer size.  
            var buffer = new byte[bufferSize]; // Allocate sufficient buffer  
            if (GetEnhMetaFileBits(enfMetafileHandle, bufferSize, buffer) <= 0) // Get raw metafile data.  
                throw new InvalidOperationException("Get raw metafile bits failed");

            destination.Write(buffer, 0, bufferSize);
            destination.Position = 0;
            if (!DeleteEnhMetaFile(enfMetafileHandle)) //free handle  
                throw new InvalidOperationException("Free metafile handle failed");
        }

        [DllImport("gdi32")]
        public static extern int GetEnhMetaFileBits(IntPtr hemf, int cbBuffer, byte[] lpbBuffer);

        [DllImport("gdi32")]
        public static extern bool DeleteEnhMetaFile(IntPtr hemfbitHandle);
    }
}

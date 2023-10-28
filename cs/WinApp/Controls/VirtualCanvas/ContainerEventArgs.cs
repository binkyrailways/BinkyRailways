using System.Drawing;
using System.Drawing.Drawing2D;

namespace BinkyRailways.WinApp.Controls.VirtualCanvas
{
    /// <summary>
    /// Event arguments for zooming the virtual canvas control to a given rectangle.
    /// </summary>
    public class ContainerEventArgs : ArgumentEventArgs
    {
        private readonly VCItem sender;
        private readonly ContainerMessage message;
        private readonly RectangleF rectangle;
        private readonly PointF point;

        /// <summary>
        /// Default ctor
        /// </summary>
        public ContainerEventArgs(ContainerMessage message, VCItem sender) : base(null)
        {
            this.sender = sender;
            this.message = message;
        }

        /// <summary>
        /// Default ctor
        /// </summary>
        /// <param name="argument"></param>
        public ContainerEventArgs(ContainerMessage message, VCItem sender, object argument)
            : base(argument)
        {
            this.sender = sender;
            this.message = message;
        }

        /// <summary>
        /// Default ctor
        /// </summary>
        public ContainerEventArgs(ContainerMessage message, VCItem sender, RectangleF rectangle) : base(null)
        {
            this.message = message;
            this.sender = sender;
            this.rectangle = rectangle;
        }

        /// <summary>
        /// Default ctor
        /// </summary>
        public ContainerEventArgs(ContainerMessage message, VCItem sender, Point pt)
            : base(null)
        {
            this.message = message;
            this.sender = sender;
            this.point = pt;
        }

        /// <summary>
        /// Default ctor
        /// </summary>
        public ContainerEventArgs(ContainerMessage message, VCItem sender, Matrix tx)
            : base(null)
        {
            this.message = message;
            this.sender = sender;
            Transform = tx;
        }

        /// <summary>
        /// Gets the message being sent to the container
        /// </summary>
        public ContainerMessage Message { get { return message; } }

        /// <summary>
        /// Sender of the original message
        /// </summary>
        public VCItem Sender { get { return sender; } }

        /// <summary>
        /// Gets the contained rectangle
        /// </summary>
        public RectangleF Rectangle
        {
            get { return rectangle; }
        }

        /// <summary>
        /// Gets the contained point
        /// </summary>
        public PointF Point
        {
            get { return point; }
        }

        /// <summary>
        /// Gets a matrix
        /// </summary>
        public Matrix Transform { get; private set; }

        /// <summary>
        /// Event used to connect BeginUpdate disposables to.
        /// </summary>
        internal VCItem.Update Update { get; set; }
    }
}

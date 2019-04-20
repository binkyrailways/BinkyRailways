using BinkyRailways.WinApp.Controls.VirtualCanvas;

namespace BinkyRailways.WinApp.Items.Messages
{
    /// <summary>
    /// Custom message from item to virtual canvas.
    /// </summary>
    internal abstract class ItemMessage
    {
        private readonly VCItemContainer sender;

        /// <summary>
        /// Default ctor
        /// </summary>
        protected ItemMessage(VCItemContainer sender)
        {
            this.sender = sender;
        }

        /// <summary>
        /// Gets the source of this message
        /// </summary>
        public VCItemContainer Sender { get { return sender; } }

        /// <summary>
        /// Accept a visit by the given visitor
        /// </summary>
        public abstract void Accept(ItemMessageVisitor visitor);
    }
}

namespace BinkyRailways.WinApp.Items.Messages
{
    internal class ItemMessageVisitor
    {
        internal virtual void Visit(ShowToolTipMessage msg) { }
        internal virtual void Visit(ShowContextMenuMessage msg) { }
    }
}

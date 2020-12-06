using System.Windows.Forms;

namespace BinkyRailways.WinApp.Controls.VirtualCanvas.Handlers
{
    public class ContainerKeyboardHandler : KeyboardHandler
    {
        private readonly VCItemContainer container;
        
        /// <summary>
        /// Default ctor
        /// </summary>
        public ContainerKeyboardHandler(VCItemContainer container, KeyboardHandler next)
            : base(next)
        {
            this.container = container;
        }

        /// <summary>
        /// Catch desired keys
        /// </summary>
        public override bool IsInputKey(Keys keyData)
        {
            foreach (var placement in container.Items)
            {
                if (placement.Item.IsInputKey(keyData)) { return true; }
            }
            return base.IsInputKey(keyData);
        }

        /// <summary>
        /// Key is down on this item
        /// </summary>
        /// <returns>True if the event was handled, false otherwise.</returns>
        public override bool OnKeyDown(VCItem sender, ItemKeyEventArgs e)
        {
            var selection = container.SelectedItems;
            foreach (var placement in selection)
            {
                if (placement.Item.OnKeyDown(e)) { return true; }
            }
            foreach (var placement in container.Items)
            {
                if (!selection.Contains(placement))
                {
                    if (placement.Item.OnKeyDown(e)) { return true; }
                }
            }
            return base.OnKeyDown(sender, e);
        }

        /// <summary>
        /// Key is up on this item
        /// </summary>
        /// <returns>True if the event was handled, false otherwise.</returns>
        public override bool OnKeyUp(VCItem sender, ItemKeyEventArgs e)
        {
            var selection = container.SelectedItems;
            foreach (var placement in selection)
            {
                if (placement.Item.OnKeyUp(e)) { return true; }
            }
            foreach (var placement in container.Items)
            {
                if (!selection.Contains(placement))
                {
                    if (placement.Item.OnKeyUp(e)) { return true; }
                }
            }
            return base.OnKeyUp(sender, e);
        }
    }
}

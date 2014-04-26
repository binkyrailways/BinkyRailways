using System.Linq;
using BinkyRailways.Core.Model;
using BinkyRailways.WinApp.Controls.VirtualCanvas;

namespace BinkyRailways.WinApp.Items
{
    /// <summary>
    /// Container for positioned items
    /// </summary>
    public class PositionedItemContainer : VCItemContainer
    {
        /// <summary>
        /// Default ctor
        /// </summary>
        public PositionedItemContainer()
        {
            LayoutManager = new PositionedEntityLayoutManager();
        }

        /// <summary>
        /// Try to get the module item showing the given module
        /// </summary>
        internal IPositionedEntityItem GetItem(IPositionedEntity entity)
        {
            return Items.Select(placement => placement.Item as IPositionedEntityItem).FirstOrDefault(item => (item != null) && (item.Entity == entity));
        }
    }
}

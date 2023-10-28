using BinkyRailways.Core.Model;
using BinkyRailways.Core.State;

namespace BinkyRailways.WinApp.Items.Run
{
    /// <summary>
    /// Item showing a passive junction
    /// </summary>
    public sealed class PassiveJunctionItem : Edit.PassiveJunctionItem
    {
        /// <summary>
        /// Default ctor
        /// </summary>
        public PassiveJunctionItem(IPassiveJunction entity, IPassiveJunctionState state, ItemContext context)
            : base(entity, context)
        {
        }
    }
}

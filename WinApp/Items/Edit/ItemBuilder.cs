using BinkyRailways.Core.Model;
using BinkyRailways.WinApp.Controls.VirtualCanvas;

namespace BinkyRailways.WinApp.Items.Edit
{
    /// <summary>
    /// Visitor used to create canvas items for entities.
    /// Data arguments equals "editable"
    /// </summary>
    internal class ItemBuilder : EntityVisitor<VCItem, bool>
    {
        private readonly ItemContext context;
        private readonly bool railwayEditable;

        /// <summary>
        /// Default ctor
        /// </summary>
        public ItemBuilder(ItemContext context, bool railwayEditable)
        {
            this.context = context;
            this.railwayEditable = railwayEditable;
        }

        public override VCItem Visit(IBlock entity, bool data)
        {
            return new BlockItem(entity, data, context);
        }

        public override VCItem Visit(IBlockSignal entity, bool data)
        {
            return new BlockSignalItem(entity, data, context);
        }

        public override VCItem Visit(IEdge entity, bool data)
        {
            return new EdgeItem(entity, data, railwayEditable, context);
        }

        public override VCItem Visit(IBinaryOutput entity, bool data)
        {
            return new BinaryOutputItem(entity, data, context);
        }

        public override VCItem Visit(IClock4StageOutput entity, bool data)
        {
            return new Clock4StageOutputItem(entity, data, context);
        }

        public override VCItem Visit(ISensor entity, bool data)
        {
            return new SensorItem(entity, data, context);
        }

        public override VCItem Visit(IPassiveJunction entity, bool data)
        {
            return new PassiveJunctionItem(entity, context);
        }

        public override VCItem Visit(ISwitch entity, bool data)
        {
            return new SwitchItem(entity, data, context);
        }

        public override VCItem Visit(ITurnTable entity, bool data)
        {
            return new TurnTableItem(entity, data, context);
        }
    }
}

using BinkyRailways.Core.Model;
using BinkyRailways.Core.State;
using BinkyRailways.WinApp.Controls.VirtualCanvas;

namespace BinkyRailways.WinApp.Items.Run
{
    /// <summary>
    /// Visitor used to create canvas items for entities.
    /// </summary>
    internal class ItemBuilder : EntityVisitor<VCItem, IEntityState>
    {
        private readonly ItemContext context;
        private readonly bool interactive;

        /// <summary>
        /// Default ctor
        /// </summary>
        public ItemBuilder(ItemContext context, bool interactive)
        {
            this.context = context;
            this.interactive = interactive;
        }

        public override VCItem Visit(IBlock entity, IEntityState data)
        {
            return new BlockItem(entity, (IBlockState) data, context);
        }

        public override VCItem Visit(IBlockSignal entity, IEntityState data)
        {
            return new BlockSignalItem(entity, (IBlockSignalState)data, context, interactive);
        }

        public override VCItem Visit(IBinaryOutput entity, IEntityState data)
        {
            return new BinaryOutputItem(entity, (IBinaryOutputState)data, context, interactive);
        }

        public override VCItem Visit(IClock4StageOutput entity, IEntityState data)
        {
            return new Clock4StageOutputItem(entity, (IClock4StageOutputState) data, context, interactive);
        }

        public override VCItem Visit(ISensor entity, IEntityState data)
        {
            return new SensorItem(entity, (ISensorState) data, context, interactive);
        }

        public override VCItem Visit(IPassiveJunction entity, IEntityState data)
        {
            return new PassiveJunctionItem(entity, (IPassiveJunctionState) data, context);
        }

        public override VCItem Visit(ISwitch entity, IEntityState data)
        {
            return new SwitchItem(entity, (ISwitchState)data, context, interactive);
        }

        public override VCItem Visit(ITurnTable entity, IEntityState data)
        {
            return new TurnTableItem(entity, (ITurnTableState)data, context, interactive);
        }
    }
}

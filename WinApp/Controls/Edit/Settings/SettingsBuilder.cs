using BinkyRailways.Core.Model;

namespace BinkyRailways.WinApp.Controls.Edit.Settings
{
    /// <summary>
    /// Builder for settings instances
    /// </summary>
    internal sealed class SettingsBuilder : EntityVisitor<object, GridContext>
    {
        public override object Visit(IBinaryOutput entity, GridContext data)
        {
            return new BinaryOutputSettings(entity, data);
        }

        public override object Visit(IBinarySensor entity, GridContext data)
        {
            return new BinarySensorSettings(entity, data);
        }

        public override object Visit(IBlock entity, GridContext data)
        {
            return new BlockSettings(entity, data);
        }

        public override object Visit(IBlockGroup entity, GridContext data)
        {
            return new BlockGroupSettings(entity, data);
        }

        public override object Visit(IBlockSignal entity, GridContext data)
        {
            return new BlockSignalSettings(entity, data);
        }

        public override object Visit(IClock4StageOutput entity, GridContext data)
        {
            return new Clock4StageOutputSettings(entity, data);
        }

        public override object Visit(IDccOverRs232CommandStation entity, GridContext data)
        {
            return new DccOverRs232CommandStationSettings(entity, data);
        }

        public override object Visit(IEcosCommandStation entity, GridContext data)
        {
            return new EcosCommandStationSettings(entity, data);
        }

        public override object Visit(IMqttCommandStation entity, GridContext data)
        {
            return new MqttCommandStationSettings(entity, data);
        }

        public override object Visit(IEdge entity, GridContext data)
        {
            return new EdgeSettings(entity, data);
        }

        public override object Visit(IInitializeJunctionAction entity, GridContext data)
        {
            return new InitializeJunctionActionSettings(entity, data);
        }

        public override object Visit(ILoc entity, GridContext data)
        {
            return new LocSettings(entity, data);
        }

        public override object Visit(ILocGroup entity, GridContext data)
        {
            return new LocGroupSettings(entity, data);
        }

        public override object Visit(ILocoBufferCommandStation entity, GridContext data)
        {
            return new LocoBufferCommandStationSettings(entity, data);
        }

        public override object Visit(IModule entity, GridContext data)
        {
            if (data.EntityRef != null)
            {
                return data.EntityRef.Accept(this, data);
            }
            return new ModuleSettings(entity, data);
        }

        public override object Visit(IModuleRef entity, GridContext data)
        {
            return new ModuleRefSettings(entity, data);
        }

        public override object Visit(IModuleConnection entity, GridContext data)
        {
            return new ModuleConnectionSettings(entity, data);
        }

        public override object Visit(IPlaySoundAction entity, GridContext data)
        {
            return new PlaySoundActionSettings(entity, data);
        }

        public override object Visit(IRailway entity, GridContext data)
        {
            return new RailwaySettings(entity, data);
        }

        public override object Visit(IRoute entity, GridContext data)
        {
            return new RouteSettings(entity, data);
        }

        public override object Visit(IPassiveJunction entity, GridContext data)
        {
            return new PassiveJunctionSettings(entity, data);
        }

        public override object Visit(ISwitch entity, GridContext data)
        {
            return new SwitchSettings(entity, data);
        }

        public override object Visit(ITurnTable entity, GridContext data)
        {
            return new TurnTableSettings(entity, data);
        }

        public override object Visit(ILocFunctionAction entity, GridContext data)
        {
            return new LocFunctionActionSettings(entity, data);
        }
    }
}

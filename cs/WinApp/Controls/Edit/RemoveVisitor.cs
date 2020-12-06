using BinkyRailways.Core.Model;

namespace BinkyRailways.WinApp.Controls.Edit
{
    /// <summary>
    /// Visitor used to remove entities.
    /// </summary>
    internal sealed class RemoveVisitor : EntityVisitor<object, IModule>
    {
        public override object Visit(IBlock entity, IModule data)
        {
            data.Blocks.Remove(entity);
            return null;
        }
        public override object Visit(IEdge entity, IModule data)
        {
            data.Edges.Remove(entity);
            return null;
        }
        public override object Visit(IJunction entity, IModule data)
        {
            data.Junctions.Remove(entity);
            return null;
        }
        public override object Visit(IRoute entity, IModule data)
        {
            data.Routes.Remove(entity);
            return null;
        }
        public override object Visit(ISensor entity, IModule data)
        {
            data.Sensors.Remove(entity);
            return null;
        }
        public override object Visit(ISignal entity, IModule data)
        {
            data.Signals.Remove(entity);
            return null;
        }
        public override object Visit(IOutput entity, IModule data)
        {
            data.Outputs.Remove(entity);
            return null;
        }
    }
}

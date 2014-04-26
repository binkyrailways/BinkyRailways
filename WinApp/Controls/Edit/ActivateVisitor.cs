using BinkyRailways.Core.Model;

namespace BinkyRailways.WinApp.Controls.Edit
{
    /// <summary>
    /// Visitor used to activate (un-archive) entities.
    /// </summary>
    internal sealed class ActivateVisitor : EntityVisitor<object, IRailway>
    {
        public override object Visit(ICommandStation entity, IRailway data)
        {
            data.CommandStations.Add(entity);
            return null;
        }
        public override object Visit(ILoc entity, IRailway data)
        {
            data.Locs.Add(entity);
            return null;
        }
        public override object Visit(IModule entity, IRailway data)
        {
            data.Modules.Add(entity);
            return null;
        }
    }
}

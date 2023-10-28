using BinkyRailways.Core.Model;

namespace BinkyRailways.WinApp.Controls.Edit
{
    /// <summary>
    /// Visitor used to archive entities.
    /// </summary>
    internal sealed class ArchiveVisitor : EntityVisitor<object, IRailway>
    {
        public override object Visit(ICommandStationRef entity, IRailway data)
        {
            data.CommandStations.Remove(entity);
            return null;
        }
        public override object Visit(ILocRef entity, IRailway data)
        {
            data.Locs.Remove(entity);
            return null;
        }
        public override object Visit(IModuleRef entity, IRailway data)
        {
            data.Modules.Remove(entity);
            return null;
        }
    }
}

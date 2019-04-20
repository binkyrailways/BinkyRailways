using BinkyRailways.Core.Model;

namespace BinkyRailways.WinApp.Controls.Edit
{
    /// <summary>
    /// Visitor used to remove entities entirely from a package.
    /// </summary>
    internal sealed class RemoveFromPackageVisitor : EntityVisitor<object, IPackage>
    {
        public override object Visit(ICommandStation entity, IPackage data)
        {
            data.Remove(entity);
            return null;
        }
        public override object Visit(ICommandStationRef entity, IPackage data)
        {
            ICommandStation cs;
            var resolved = entity.TryResolve(out cs);
            data.Railway.CommandStations.Remove(entity);
            if (resolved)
                cs.Accept(this, data);
            return null;
        }
        public override object Visit(ILoc entity, IPackage data)
        {
            data.Remove(entity);
            return null;
        }
        public override object Visit(ILocRef entity, IPackage data)
        {
            ILoc loc;
            var resolved = entity.TryResolve(out loc);
            data.Railway.Locs.Remove(entity);
            if (resolved)
                loc.Accept(this, data);
            return null;
        }
        public override object Visit(IModule entity, IPackage data)
        {
            data.Remove(entity);
            return null;
        }
        public override object Visit(IModuleRef entity, IPackage data)
        {
            IModule module;
            var resolved = entity.TryResolve(out module);
            data.Railway.Modules.Remove(entity);
            if (resolved)
                module.Accept(this, data);
            return null;
        }

        public override object Visit(IModuleConnection entity, IPackage data)
        {
            data.Railway.ModuleConnections.Remove(entity);
            return null;
        }
    }
}

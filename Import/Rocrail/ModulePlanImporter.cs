using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using BinkyRailways.Core.Model;
using BinkyRailways.Core.Util;

namespace BinkyRailways.Import.Rocrail
{
    /// <summary>
    /// Import "modplan" files
    /// </summary>
    internal sealed class ModulePlanImporter : Importer
    {
        private readonly Dictionary<string, IModule> modules = new Dictionary<string, IModule>();

        /// <summary>
        /// Importer for modules.
        /// </summary>
        public ModulePlanImporter(XElement root, string path)
            : base(root, path)
        {
        }

        /// <summary>
        /// Import the plan.
        /// </summary>
        public bool Import(IPackage package)
        {
            ImportModules(package, package.Railway);
            ImportLocs(package);

            return true;
        }

        private void ImportLocs(IPackage package)
        {
            var locs = Root.GetAttributeValue("locs");
            if (string.IsNullOrEmpty(locs))
                return;

            if (!System.IO.Path.IsPathRooted(locs))
                locs = System.IO.Path.Combine(Folder, locs);
            
            if (!File.Exists(locs))
            {
                Message("Locs file '{0}' does not exist", locs);
                return;
            }

            IModule entity;
            try
            {
                var doc = XDocument.Load(locs);
                var locsRoot = doc.Root;
                if (locsRoot == null)
                    throw new ArgumentException("File has no root");
                if (locsRoot.Name.LocalName != "plan")
                    throw new ArgumentException("File is not a rocrail module");
                var importer = new LocsImporter(locsRoot, locs);
                importer.Import(package);
                AddMessages(importer.Messages);
            }
            catch (Exception ex)
            {
                Message("Failed to import '{0}' because {1}", locs, ex.Message);
            }
        }

        /// <summary>
        /// Import modules
        /// </summary>
        private void ImportModules(IPackage package, IRailway railway)
        {
            var folder = Folder;

            foreach (var mod in Root.Elements("module"))
            {
                var id = mod.GetAttributeValue("id", string.Empty);
                if (string.IsNullOrEmpty(id))
                {
                    Message("Module found without an id");
                    continue;
                }

                var filename = mod.GetAttributeValue("filename");
                if (string.IsNullOrEmpty(filename))
                {
                    Message("Module '{0}' has no filename", id);
                    continue;
                }
                if (!System.IO.Path.IsPathRooted(filename))
                    filename = System.IO.Path.Combine(folder, filename);
                if (!File.Exists(filename))
                {
                    Message("Module file '{0}' does not exist", filename);
                    continue;
                }

                IModule entity;
                try
                {
                    var doc = XDocument.Load(filename);
                    var modRoot = doc.Root;
                    if (modRoot == null)
                        throw new ArgumentException("File has no root");
                    if (modRoot.Name.LocalName != "plan")
                        throw new ArgumentException("File is not a rocrail module");
                    var moduleImporter = new ModuleImporter(modRoot, filename);
                    entity = moduleImporter.Import(package);
                    AddMessages(moduleImporter.Messages);
                }
                catch (Exception ex)
                {
                    Message("Failed to import '{0}' because {1}", filename, ex.Message);
                    continue;
                }

                var moduleRef = railway.Modules.First(x => x.IsReferenceTo(entity));
                moduleRef.X = mod.GetIntAttributeValue("x") * ModuleImporter.GridSize;
                moduleRef.Y = mod.GetIntAttributeValue("y") * ModuleImporter.GridSize;
                var cx = mod.GetIntAttributeValue("cx") * ModuleImporter.GridSize;
                var cy = mod.GetIntAttributeValue("cy") * ModuleImporter.GridSize;
                SetRotation(moduleRef, mod.GetAttributeValue("rotation"), cx, cy);

                // Record
                modules[id.ToLower()] = entity;
            }
        }

        /// <summary>
        /// Convert orientation to rotation
        /// </summary>
        private static void SetRotation(IPositionedEntity entity, string ori, int ewidth, int eheight)
        {
            switch (ori)
            {
                case "180":
                    entity.Rotation = 180;
                    break;
                case "90":
                    entity.Rotation = 270;
                    // Adjust for rotation around center
                    entity.X -= (ewidth - eheight) / 2;
                    entity.Y += (ewidth - eheight) / 2;
                    break;
                case "270":
                    entity.Rotation = 90;
                    // Adjust for rotation around center
                    entity.X -= (ewidth - eheight) / 2;
                    entity.Y += (ewidth - eheight) / 2;
                    break;
            }
        }
    }
}

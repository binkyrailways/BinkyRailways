using System.Collections.Generic;
using System.Xml.Linq;
using BinkyRailways.Core.Model;
using BinkyRailways.Core.Util;

namespace BinkyRailways.Import.Rocrail
{
    /// <summary>
    /// Import all locs
    /// </summary>
    internal class LocsImporter : Importer
    {
        private readonly Dictionary<string, ILoc> locs = new Dictionary<string, ILoc>();

        /// <summary>
        /// Importer for locs.
        /// </summary>
        public LocsImporter(XElement root, string path)
            : base(root, path)
        {
        }

        /// <summary>
        /// Import the locs.
        /// </summary>
        public void Import(IPackage package)
        {
            // Import individual objects
            ImportLocs(package, package.Railway);
        }

        /// <summary>
        /// Import all locs
        /// </summary>
        protected void ImportLocs(IPackage package, IRailway railway)
        {
            var lclist = Root.Element("lclist");
            if (lclist == null) 
                return;

            foreach (var lc in lclist.Elements("lc"))
            {
                var id = lc.GetAttributeValue("id", string.Empty);
                if (string.IsNullOrEmpty(id))
                {
                    Message("Loc found without an id");
                    continue;
                }
                if (locs.ContainsKey(id.ToLower()))
                {
                    Message("Duplicate loc found ({0})", id);
                    continue;
                }

                var addr = lc.GetIntAttributeValue("addr");

                var entity = package.AddNewLoc();
                railway.Locs.Add(entity);
                entity.Description = id;

                var addrType = (lc.GetAttributeValue("prot") == "M") ? AddressType.Motorola : AddressType.Dcc;
                entity.Address = new Address(addrType, null, addr);

                // Record
                locs[id.ToLower()] = entity;
            }
        }
    }
}

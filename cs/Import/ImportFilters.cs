using System.Collections.Generic;
using System.Linq;
using BinkyRailways.WinApp;

namespace BinkyRailways.Import
{
    public static class ImportFilters
    {
        /// <summary>
        /// Class ctor
        /// </summary>
        static ImportFilters()
        {
            var container = Program.CompositionContainer;
            Filters = container.GetExportedValues<IImportFilter>().OrderBy(x => x.Priority);
        }

        /// <summary>
        /// All available import filters.
        /// </summary>
        internal static IEnumerable<IImportFilter> Filters { get; private set; }
    }
}
